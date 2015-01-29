using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace GoogleApiTest
{
	[Activity (Label = "GoogleApiTest", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.Splash")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			System.Threading.Thread.Sleep (3000);
			var activity1 = new Intent (this, typeof(MapActivity));
			StartActivity (activity1);
			var button = FindViewById<ImageButton> (Resource.Id.locateMe);
		}
	}
}


