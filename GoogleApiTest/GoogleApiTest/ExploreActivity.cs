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
	[Activity (Label = "ExploreActivity")]			
	public class ExploreActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ExploreView);

			var gridView = FindViewById<GridView> (Resource.Id.gridView1);
			gridView.Adapter = new ImageAdapter (this);

			gridView.ItemClick += delegate(object sender, AdapterView.ItemClickEventArgs e) {
				Toast.MakeText (this, e.Position.ToString (), ToastLength.Short).Show ();
			};
		}
	}
}

