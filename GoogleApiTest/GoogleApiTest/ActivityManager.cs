using System;
using Android.App;

namespace GoogleApiTest
{
	public class ActivityManager
	{
		Activity Activity;
		String Name;

		public ActivityManager (Activity activity, string ActivityName)
		{
			this.Activity = activity;
			this.Name = ActivityName;
		}

		string getActivityName(){
		
			return this.Name;
		}

	}
}

