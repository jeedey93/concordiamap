
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
	[Activity (Label = "CalendarActivity")]			
	public class CalendarActivity : LeftDrawerActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here

			GoogleCalendar calendar = new GoogleCalendar
				("Justin Do", "Google account name", "Google account password");
			CalendarEventObject[] events = calendar.GetEvents ();
			Console.WriteLine (events);
		}
	}
}

