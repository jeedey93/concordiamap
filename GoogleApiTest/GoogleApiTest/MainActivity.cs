using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace GoogleApiTest
{
	[Activity (Label = "GoogleApiTest", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			var button = FindViewById<ImageButton> (Resource.Id.locateMe);
			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

		}
	}
}


