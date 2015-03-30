
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace GoogleApiTest
{
	[Activity (Label = "AddEventActivity")]			
	public class AddEventActivity : Activity
	{

		private TextView dateDisplay;
		private Button pickDate;
		private DateTime date;

		const int DATE_DIALOG_ID = 0;

		private TextView time_display;
		private Button pick_button;
		private TextView time_display2;
		private Button pick_button2;


		private int hour;
		private int minute;

		const int STARTTIME_DIALOG_ID = 1;
		const int ENDTIME_DIALOG_ID = 2;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.AddEventView);

			// capture our View elements
			dateDisplay = FindViewById<TextView> (Resource.Id.editStartDate);
			pickDate = FindViewById<Button> (Resource.Id.pickDate);

			// add a click event handler to the button
			pickDate.Click += delegate { ShowDialog (DATE_DIALOG_ID); };

			// get the current date
			date = DateTime.Today;

			// display the current date (this method is below)
			UpdateDisplay ();


			// Capture our View elements
			time_display = FindViewById<TextView> (Resource.Id.editStartTime);
			pick_button = FindViewById<Button> (Resource.Id.pickTime);

			time_display2 = FindViewById<TextView> (Resource.Id.editEndTime);
			pick_button2 = FindViewById<Button> (Resource.Id.pickTime2);

			// Add a click listener to the button
			pick_button.Click += (o, e) => ShowDialog (STARTTIME_DIALOG_ID);

			// Add a click listener to the button
			pick_button2.Click += (o, e) => ShowDialog (ENDTIME_DIALOG_ID);

			// Get the current time
			hour = DateTime.Now.Hour;
			minute = DateTime.Now.Minute;

			// Display the current date
			UpdateDisplayStartTimer ();

			// Display the current date
			UpdateDisplayEndTimer ();

			Button addEvent = FindViewById<Button> (Resource.Id.AddEventBtn);
			addEvent.Click += (o, e) => {
				EditText editTitle = FindViewById<EditText> (Resource.Id.editTitle);
				TextView dateDisplay2 = FindViewById<TextView> (Resource.Id.editStartDate); 
				EditText editLocation = FindViewById<EditText> (Resource.Id.editLocation);
				TextView editStartTime = FindViewById<TextView> (Resource.Id.editStartTime);
				TextView editEndTime = FindViewById<TextView> (Resource.Id.editEndTime);

				//var EventListActivity = new Intent (this, typeof(EventListActivity));
				//EventListActivity.PutExtra ("title", editTitle.Text);
				//EventListActivity.PutExtra ("date", dateDisplay2.Text);
				//EventListActivity.PutExtra ("location", editLocation.Text);
				//EventListActivity.PutExtra ("startTime", editStartTime.Text);
				//EventListActivity.PutExtra ("endTime", editEndTime.Text);
				//StartActivity (EventListActivity);

				var EventListActivity = new Intent();
				EventListActivity.PutExtra ("title", editTitle.Text);
				EventListActivity.PutExtra ("date", dateDisplay2.Text);
				EventListActivity.PutExtra ("location", editLocation.Text);
				EventListActivity.PutExtra ("startTime", editStartTime.Text);
				EventListActivity.PutExtra ("endTime", editEndTime.Text);
				SetResult(Result.Ok,EventListActivity);
				Finish();
			};
		}
				

		// Updates the time we display in the TextView
		private void UpdateDisplayStartTimer ()
		{
			string time = string.Format ("{0}:{1}", hour, minute.ToString ().PadLeft (2, '0'));
			time_display.Text = time;
		}

		// Updates the time we display in the TextView
		private void UpdateDisplayEndTimer ()
		{
			string time = string.Format ("{0}:{1}", hour, minute.ToString ().PadLeft (2, '0'));
			time_display2.Text = time;
		}


		private void TimePickerCallback2 (object sender, TimePickerDialog.TimeSetEventArgs e)
		{
			hour = e.HourOfDay;
			minute = e.Minute;
			UpdateDisplayEndTimer ();
		}

		private void TimePickerCallback (object sender, TimePickerDialog.TimeSetEventArgs e)
		{
			hour = e.HourOfDay;
			minute = e.Minute;
			UpdateDisplayStartTimer ();
		}



		// updates the date in the TextView
		private void UpdateDisplay ()
		{
			dateDisplay.Text = date.ToString ("d");
		}


		// the event received when the user "sets" the date in the dialog
		void OnDateSet (object sender, DatePickerDialog.DateSetEventArgs e)
		{
			this.date = e.Date;
			UpdateDisplay ();
		}


		protected override Dialog OnCreateDialog (int id)
		{
			switch (id) {
			case DATE_DIALOG_ID:
				return new DatePickerDialog (this, OnDateSet, date.Year, date.Month - 1, date.Day); 
			case STARTTIME_DIALOG_ID:
				return new TimePickerDialog (this, TimePickerCallback, hour, minute, false);
			case ENDTIME_DIALOG_ID:
				return new TimePickerDialog (this, TimePickerCallback2, hour, minute, false);
			}
			return null;
		}
	}
}

