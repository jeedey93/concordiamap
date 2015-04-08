using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Provider;
using Java.Util;
using System.Collections.Generic;

namespace GoogleApiTest
{
    [Activity (Label = "EventListActivity")]            
	public class EventListActivity : ListActivity
    {   
        private int _calId;
		private bool isNextClass;
		private string title;
		private string date;
		private string location;
		private string startingTime;
		private string endTime;

		//Parsing Properties
		private int startingYear;
		private int startingMonth;
		private int startingDay;
		private int startingHour;
		private int startingMinute;

		private int endingHour;
		private int endingMinute;

		List<Event> EventList = new List<Event>();
        
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.EventList);

			//Get current type of this activity based on passed value in intent
            _calId = Intent.GetIntExtra ("calId", -1); 
			isNextClass = Intent.GetBooleanExtra ("isNextClass", false);
		
            ListEvents ();

			if(isNextClass){
				Toast.MakeText (this, "Let's fetch the next class", ToastLength.Short).Show ();
				var nextEvent = PickNextClass ();
				var mapActivity = new Intent (this, typeof(MapActivity));
				mapActivity.PutExtra("nextBuilding",nextEvent.Abbreviation);
				SetResult(Result.Ok, mapActivity);
				Finish();
			}

            
            InitAddEvent ();

			MakeDefaultCalendar ();
			AutomaticReminder (); 
        }

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			title = data.GetStringExtra ("title");
			date = data.GetStringExtra ("date");
			location = data.GetStringExtra ("location");
			startingTime = data.GetStringExtra ("startTime");
			endTime = data.GetStringExtra ("endTime");
			CreateEvent();
		}

		void CreateEvent(){
			// Create Event code
			ContentValues eventValues = new ContentValues ();
			eventValues.Put (CalendarContract.Events.InterfaceConsts.CalendarId, _calId);
			eventValues.Put (CalendarContract.Events.InterfaceConsts.Title, title);
			//eventValues.Put (CalendarContract.Events.InterfaceConsts.Description, "This is an event created from Mono for Android");
			eventValues.Put (CalendarContract.Events.InterfaceConsts.EventLocation, location);
			ParseDayAndTime (date,startingTime, endTime);
			eventValues.Put (CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS (startingYear, startingMonth, startingDay, startingHour, startingMinute));
			eventValues.Put (CalendarContract.Events.InterfaceConsts.Dtend, GetDateTimeMS (startingYear, startingMonth, startingDay, endingHour, endingMinute));

			// GitHub issue #9 : Event start and end times need timezone support.
			// https://github.com/xamarin/monodroid-samples/issues/9
			 eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
			 eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");

			 var uri = ContentResolver.Insert (CalendarContract.Events.ContentUri, eventValues);
			 Console.WriteLine ("Uri for new event: {0}", uri);

		}

		void ParseDayAndTime(string date, string startingTime, string endTime){
			var dateParsed = DateTime.Parse(date);
			startingYear = dateParsed.Year;
			startingMonth = dateParsed.Month;
			startingDay = dateParsed.Day;

			var startingTimeParsed = DateTime.Parse (startingTime);
			startingHour = startingTimeParsed.Hour;
			startingMinute = startingTimeParsed.Minute;

			var endTimeParsed = DateTime.Parse (endTime);
			endingHour = endTimeParsed.Hour;
			endingMinute = endTimeParsed.Minute;
		}

		void MakeDefaultCalendar(){
			var defaultCalendar = FindViewById<Button> (Resource.Id.defaultCalendar);
			var preferenceManager = new PreferenceManager (this);				
			if (BuildingManager.isDefaultCalendarSelected) {
				defaultCalendar.Text = "Change Default Calendar";
			}
				
			defaultCalendar.Click += (sender, e) => { 
				if (!BuildingManager.isDefaultCalendarSelected) {
					defaultCalendar.Text = "Change Default Calendar";
					BuildingManager.isDefaultCalendarSelected = true;
					BuildingManager.DefaultCalendarId = _calId;
					preferenceManager.SaveDefaultCalendar();
				} else {
					defaultCalendar.Text = "Make this my default calendar";
					BuildingManager.isDefaultCalendarSelected = false;
					BuildingManager.DefaultCalendarId = 0; 
				}
			};
		}
			
		void AutomaticReminder (){
			Switch automatic = FindViewById<Switch> (Resource.Id.autoSwitch);

			automatic.CheckedChange += delegate(object sender, CompoundButton.CheckedChangeEventArgs e) {
				if(automatic.Checked){
					var numberofEvents = ListAdapter.Count;
					var eventsUri = CalendarContract.Events.ContentUri;

					string[] eventsProjection = { 
						CalendarContract.Events.InterfaceConsts.Id,
						CalendarContract.Events.InterfaceConsts.Title,
						CalendarContract.Events.InterfaceConsts.Dtstart,
						CalendarContract.Events.InterfaceConsts.EventLocation
					};

					var cursor = ManagedQuery (eventsUri, eventsProjection, 
						String.Format ("calendar_id={0}", _calId), null, "dtstart ASC");


					for (int i = 0; i < numberofEvents; i++) {
						cursor.MoveToPosition (i);
						var eventId = cursor.GetInt (cursor.GetColumnIndex (eventsProjection [0]));
						var eventTitle = cursor.GetString (cursor.GetColumnIndex (eventsProjection [1]));
						var eventDtStart = cursor.GetDouble (cursor.GetColumnIndex (eventsProjection [2]));
						var eventLocation = cursor.GetString (cursor.GetColumnIndex (eventsProjection [3]));

						EventList.Add(new Event(_calId, eventId,eventTitle,eventDtStart,eventLocation));
					}

					var classesOfTheDay = Event.GetClassesOfDay(EventList);

					foreach(var a in classesOfTheDay){
						Intent alarm = new Intent(AlarmClock.ActionSetAlarm);
						alarm.PutExtra(AlarmClock.ExtraHour, a.mDtStart.Hour);
						alarm.PutExtra(AlarmClock.ExtraMinutes, a.mDtStart.Minute);
						alarm.PutExtra(AlarmClock.ExtraMessage, a.mTitle + " in " + a.mEventLocation + " at " + a.mDtStart);
						StartActivity(alarm);
					}
				}
			};

		}
			
		Building PickNextClass(){
			var numberofEvents = ListAdapter.Count;
			var eventsUri = CalendarContract.Events.ContentUri;

			string[] eventsProjection = { 
				CalendarContract.Events.InterfaceConsts.Id,
				CalendarContract.Events.InterfaceConsts.Title,
				CalendarContract.Events.InterfaceConsts.Dtstart,
				CalendarContract.Events.InterfaceConsts.EventLocation
			};

			var cursor = ManagedQuery (eventsUri, eventsProjection, 
				String.Format ("calendar_id={0}", _calId), null, "dtstart ASC");


			for (int i = 0; i < numberofEvents; i++) {
				cursor.MoveToPosition (i);
				var eventId = cursor.GetInt (cursor.GetColumnIndex (eventsProjection [0]));
				var eventTitle = cursor.GetString (cursor.GetColumnIndex (eventsProjection [1]));
				var eventDtStart = cursor.GetDouble (cursor.GetColumnIndex (eventsProjection [2]));
				var eventLocation = cursor.GetString (cursor.GetColumnIndex (eventsProjection [3]));

				EventList.Add(new Event(_calId, eventId,eventTitle,eventDtStart,eventLocation));
			}
			//Event tempEvent=new Event();
			var NextClass = Event.GetNextEventBuilding (EventList);
			return NextClass;

		}

        void ListEvents ()
        {       
            var eventsUri = CalendarContract.Events.ContentUri;
             
            string[] eventsProjection = { 
                CalendarContract.Events.InterfaceConsts.Id,
                CalendarContract.Events.InterfaceConsts.Title,
                CalendarContract.Events.InterfaceConsts.Dtstart,
				CalendarContract.Events.InterfaceConsts.EventLocation
             };
         
            var cursor = ManagedQuery (eventsUri, eventsProjection, 
             String.Format ("calendar_id={0}", _calId), null, "dtstart ASC");
         
            string[] sourceColumns = {
                CalendarContract.Events.InterfaceConsts.Title, 
                CalendarContract.Events.InterfaceConsts.Dtstart,
				CalendarContract.Events.InterfaceConsts.EventLocation
            };
         
			int[] targetResources = { Resource.Id.eventTitle, Resource.Id.eventStartDate, Resource.Id.eventLocation };
         
            var adapter = new SimpleCursorAdapter (this, Resource.Layout.EventListItem, 
             cursor, sourceColumns, targetResources); 
         
            adapter.ViewBinder = new ViewBinder ();
         
            ListAdapter = adapter;
            
            ListView.ItemClick += (sender, e) => { 
                int i = e.Position;
                
                cursor.MoveToPosition(i);
                int eventId = cursor.GetInt (cursor.GetColumnIndex (eventsProjection [0]));
                var uri = ContentUris.WithAppendedId(CalendarContract.Events.ContentUri, eventId);
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);              
            };
        }
        
        void InitAddEvent ()
        {
            var addSampleEvent = FindViewById<Button> (Resource.Id.addSampleEvent);
         
            addSampleEvent.Click += (sender, e) => {       

				var AddEventActivity = new Intent (this, typeof(AddEventActivity));
				StartActivityForResult(AddEventActivity, 0);
            };
        }
        
        class ViewBinder : Java.Lang.Object, SimpleCursorAdapter.IViewBinder
        {     
            public bool SetViewValue (View view, Android.Database.ICursor cursor, int columnIndex)
            {
                if (columnIndex == 2) {
                    long ms = cursor.GetLong (columnIndex);
                    
                    DateTime date = 
                        new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds (ms).ToLocalTime ();
                  
                    TextView textView = (TextView)view;
                    textView.Text = date.ToLongDateString ();
                    
                    return true;
                }
                return false;
            }     
        }
        
        long GetDateTimeMS (int yr, int month, int day, int hr, int min)
        {
            Calendar c = Calendar.GetInstance (Java.Util.TimeZone.Default);
            
			c.Set (Calendar.DayOfMonth, day);
            c.Set (Calendar.HourOfDay, hr);
            c.Set (Calendar.Minute, min);
			c.Set (Calendar.Month, month);
			c.Set (Calendar.Year, yr);
                  
            return c.TimeInMillis;
        }
        
    }
}

