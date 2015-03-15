using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Net;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Widget;
using Android.Gms.Maps.Model;


namespace GoogleApiTest
{
	[Activity (Label = "Navigate")]			
	public class NavigateActivity: MapActivity
	{
		BuildingManager BuildingManager = new BuildingManager ();

		protected override void OnCreate (Bundle bundle)
		{
			BuildingManager.InitializeSGWBuildings ();
			BuildingManager.InitializeLoyolaBuildings ();
			base.OnCreate (bundle,Resource.Layout.Navigate);
			AutoCompleteTextView to = FindViewById<AutoCompleteTextView>(Resource.Id.To);
			to.Adapter = new ArrayAdapter<string> (this, Resource.Layout.list_locations, AllLocations (BuildingManager.GetSGWBuildings (), BuildingManager.GetLoyolaBuildings ()));
			AutoCompleteTextView from = FindViewById<AutoCompleteTextView>(Resource.Id.From);
			from.Adapter = new ArrayAdapter<string> (this, Resource.Layout.list_locations, AllLocations(BuildingManager.GetSGWBuildings (), BuildingManager.GetLoyolaBuildings ()));

		}
	}
}