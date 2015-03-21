
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


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.AddEventView);

			// capture our View elements
			dateDisplay = FindViewById<TextView> (Resource.Id.dateDisplay);
			pickDate = FindViewById<Button> (Resource.Id.pickDate);

			// add a click event handler to the button
			pickDate.Click += delegate { ShowDialog (DATE_DIALOG_ID); };

			// get the current date
			date = DateTime.Today;

			// display the current date (this method is below)
			UpdateDisplay ();


			Button addEvent = FindViewById<Button> (Resource.Id.AddEventBtn);
			addEvent.Click += (o, e) => {
				EditText editTitle = FindViewById<EditText> (Resource.Id.editTitle);
				TextView dateDisplay2 = FindViewById<TextView> (Resource.Id.dateDisplay); 
				EditText editLocation = FindViewById<EditText> (Resource.Id.editLocation);
				EditText editStartTime = FindViewById<EditText> (Resource.Id.editStartTime);
				EditText editEndTime = FindViewById<EditText> (Resource.Id.editEndTime);

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
			}
			return null;
		}
	}
}

