
using System;
using System.Collections;
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

	[Activity (Label = "ExploreList")]			
	public class ExploreListActivity: Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ExploreListView);

			string[] items = {"Pizza Hut", "Pizza Pizza", "Pizza for days" };


			ListView list = (ListView)FindViewById (Resource.Id.listView1);
			list.Adapter = new ExploreListAdapter (this, items);

		}
	}
}


