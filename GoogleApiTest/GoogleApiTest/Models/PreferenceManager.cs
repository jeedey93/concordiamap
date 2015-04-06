
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace GoogleApiTest
{
	public class PreferenceManager
	{
		Context mContext;
		public PreferenceManager(Context mContext){
			this.mContext = mContext;
		}

		// Function called from on EventListActivity MakeDefaultCalendar()
		public void SaveDefaultCalendar(){

			//store
			var prefs = Application.Context.GetSharedPreferences("ConQuestSettings", FileCreationMode.Private);
			var prefEditor = prefs.Edit();
			prefEditor.PutInt("DefaultCalendarId", BuildingManager.DefaultCalendarId);
			prefEditor.Commit();

		}


		// Function called from OnCreate
		public void RetrieveDefaultCalendar()
		{
			//retreive 
			var prefs = Application.Context.GetSharedPreferences("ConQuestSettings", FileCreationMode.Private);              
			var CalendarId = prefs.GetInt("DefaultCalendarId",0);
			BuildingManager.DefaultCalendarId = CalendarId;
			BuildingManager.isDefaultCalendarSelected = true;

			//Show a toast
			//Toast.MakeText(mContext, somePref, ToastLength.Long).Show();

		}
	}
}

