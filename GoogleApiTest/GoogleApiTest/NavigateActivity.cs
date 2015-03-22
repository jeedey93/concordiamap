using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Android.Views.InputMethods;


namespace GoogleApiTest
{
	[Activity (Label = "Navigate", NoHistory = true)]			
	public class NavigateActivity: LeftDrawerActivity
	{
		BuildingManager BuildingManager = new BuildingManager ();

		public String[] AllLocations (List<Building> sgw,List<Building> loy){ 

			List<string> locations = new List<string> ();
			List<String> strBuildings = new List<String> ();
			foreach(Building loyBuilding in loy){
				locations.Add (loyBuilding.ToString());
			}
			foreach(Building sgwBuilding in sgw){
				locations.Add (sgwBuilding.ToString());
			}
			String[] locations_array = locations.ToArray ();
			return locations_array;
		}
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle,Resource.Layout.Navigate);
			var sgwB = BuildingManager.InitializeSGWBuildings ();
			var loyB = BuildingManager.InitializeLoyolaBuildings ();

		
			Button search = FindViewById<Button>(Resource.Id.Search);
			AutoCompleteTextView to = FindViewById<AutoCompleteTextView>(Resource.Id.To);
			to.Adapter = new ArrayAdapter<string> (this, Resource.Layout.list_locations, AllLocations (BuildingManager.GetSGWBuildings (), BuildingManager.GetLoyolaBuildings ()));
			AutoCompleteTextView from = FindViewById<AutoCompleteTextView>(Resource.Id.From);
			from.Adapter = new ArrayAdapter<string> (this, Resource.Layout.list_locations, AllLocations(BuildingManager.GetSGWBuildings (), BuildingManager.GetLoyolaBuildings ()));


			List<Building> buildBuildings = new List<Building> ();
			List<String> strBuildings = new List<String> ();
			foreach(Building loyBuilding in sgwB){
				strBuildings.Add (loyBuilding.ToString());
				buildBuildings.Add (loyBuilding);
			}
			foreach(Building sgwBuilding in loyB){
				strBuildings.Add (sgwBuilding.ToString());
				buildBuildings.Add (sgwBuilding);
			}

			search.Click += (o, e) => {
				// Perform action on clicks
				InputMethodManager imm = (InputMethodManager)GetSystemService (Context.InputMethodService);
				imm.HideSoftInputFromWindow (from.WindowToken, 0);
				imm.HideSoftInputFromWindow (to.WindowToken, 0);
				Double startPositionX,startPositionY,endPositionX,endPositionY;
				foreach (Building fromBuilding in buildBuildings) {
					string fromText = from.Text.ToUpper ();
					string toText = to.Text.ToUpper ();
					if (fromBuilding.Abbreviation == fromText) {
						startPositionX = fromBuilding.XCoordinate;
						startPositionY = fromBuilding.YCoordinate;
						String campusStart = fromBuilding.Campus.CampusName;
						foreach(Building toBuilding in buildBuildings){
							if (toBuilding.Abbreviation == toText) {
								endPositionX = toBuilding.XCoordinate;
								endPositionY = toBuilding.YCoordinate;
								String campusEnd = toBuilding.Campus.CampusName;
								var mapActivity = new Intent (this, typeof(MapActivity));
								mapActivity.PutExtra("startPositionX",startPositionX);
								mapActivity.PutExtra("startPositionY",startPositionY);
								mapActivity.PutExtra("endPositionX",endPositionX);
								mapActivity.PutExtra("endPositionY",endPositionY);
								if(campusStart == campusEnd)
									mapActivity.PutExtra("sameCampus",true);
								else
									mapActivity.PutExtra("sameCampus",false);
								SetResult(Result.Ok,mapActivity);								
								Finish();
							}
						}
					}
				}
			};
		}
	}
}