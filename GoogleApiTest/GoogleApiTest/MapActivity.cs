﻿using Android.App;
using Android.OS;
using Android.Views;
using Android.Gms.Maps;
using System;
using Android.Gms.Maps.Model;
using Android.Widget;
using System.Collections.Generic;
using Android.Support.V4.Widget;
using Android.Support.V4.App;
using Android.Content;
using System.Json;

namespace GoogleApiTest
{
	[Activity (Label = "MapActivity")]			
	public class MapActivity : Activity
	{
		BuildingManager BuildingManager = new BuildingManager ();
		DirectionFetcher DirectionFetcher = new DirectionFetcher ();
		ListView listview;
		DrawerLayout drawer;
		List<string> drawerSettings;
		ActionBarDrawerToggle mDrawerToggle;
		GoogleMap map;
		PopupWindow window=null;
		Marker startPoint;
		Marker endPoint;
		Polyline directionPath;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.Main);

			MapFragment mapFrag = (MapFragment) FragmentManager.FindFragmentById(Resource.Id.map);
			map = mapFrag.Map;
			if (map != null) {
				// The GoogleMap object is ready to go.
				map.UiSettings.ZoomControlsEnabled = true;
				map.MyLocationEnabled = true;
				map.UiSettings.TiltGesturesEnabled = false;
				map.UiSettings.MapToolbarEnabled = false;
				zoomSgw (map);
			}

			BuildingManager.InitializeSGWBuildings ();
			BuildingManager.InitializeLoyolaBuildings ();

			ToggleButton togglebutton = FindViewById<ToggleButton>(Resource.Id.togglebutton);

			togglebutton.Click += (o, e) => {
				// Perform action on clicks
				if (togglebutton.Checked){
					zoomLoyola (map);
					//createSpinnerBuilding (map, BuildingManager.getLoyolaBuildings());
				}else{
					zoomSgw (map);
					//createSpinnerBuilding (map, BuildingManager.getSGWBuildings());
				}
			};

			drawSGWPolygons (map);
			drawLoyolaPolygons (map);

			drawLOYMarkers (map);
			drawSGWMarkers (map);

			createSettingsDrawer ();
			//createSpinnerBuilding (map, BuildingManager.getSGWBuildings ());

			map.MapClick += HandleMapClick;
			/*
			Button exploreButton = FindViewById<Button> (Resource.Id.explore);
			exploreButton.Click += (sender, e) => {
				var exploreActivity = new Intent (this, typeof(ExploreActivity));
				StartActivity (exploreActivity);
			};*/
		} 

		public async void drawDirections(LatLng startingPoint, LatLng endingPoint){
			if (directionPath != null) {
				directionPath.Remove ();
			}
			JsonValue directions = await DirectionFetcher.getDirections(startingPoint,endingPoint);
			JsonValue routesResults = directions ["routes"];
			string points = routesResults [0] ["overview_polyline"] ["points"];
			var polyPoints = DirectionFetcher.DecodePolylinePoints (points);

			List<LatLng> direction = polyPoints;
			PolylineOptions line = new PolylineOptions();
			foreach (var point in direction) {
				line.Add (point);
			}

			directionPath = map.AddPolyline (line);
			directionPath.Width = 8;
			int Color = Int32.Parse ("ff800020", System.Globalization.NumberStyles.HexNumber);
			directionPath.Color = Color;
		}
			
		public Marker getStartDestination(){
			return startPoint;
		}

		public Marker getEndDestination(){
			return endPoint;
		}

		public void CreateBuildingDescription(Building building){
			LayoutInflater inflater = (LayoutInflater)this.GetSystemService (Context.LayoutInflaterService);
			View popUp = inflater.Inflate (Resource.Layout.BuildingDescription, null);
			TextView textView = popUp.FindViewById<TextView>(Resource.Id.buildingName);
			textView.Text = building.Name;

			TextView descriptionView = popUp.FindViewById<TextView> (Resource.Id.buildingDescription);
			if (building.Description != "" && building.Campus.CampusName == "SGW") {
				descriptionView.Text = "Located on Sir George Williams Campus" + "\r\n" + building.Description;
			} else if (building.Description != "" && building.Campus.CampusName == "Loyola") {
				descriptionView.Text = "Located on Loyola Campus" + "\r\n" + building.Description;
			} else if (building.Campus.CampusName == "SGW") {
				descriptionView.Text = "Located on Sir George Williams Campus";
			} else if (building.Campus.CampusName == "Loyola") {
				descriptionView.Text = "Located on Loyola Campus";
			}

			ImageView imageView = popUp.FindViewById<ImageView> (Resource.Id.buildingImage);
			imageView.SetImageResource(building.BuildingImage);

			window = new PopupWindow (popUp, WindowManagerLayoutParams.WrapContent, WindowManagerLayoutParams.WrapContent);
			window.ShowAtLocation (popUp, GravityFlags.Center, 0,0);

			Button fromHere = popUp.FindViewById<Button> (Resource.Id.btnFromHere);
			fromHere.Click += (sender, e) => {
				if(startPoint==null && directionPath ==null){
					MarkerOptions startingDestination = new MarkerOptions ();
					startingDestination.SetPosition (new LatLng (building.XCoordinate, building.YCoordinate));
					startingDestination.InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.StartPointPNG));
					startPoint = map.AddMarker (startingDestination);
				}
				else{
					Toast.MakeText(this, "You already have a starting destination, " + building.Name + " will be your new starting destination",ToastLength.Short).Show();
					if(startPoint!=null){
						startPoint.Remove();
					}
					MarkerOptions startingDestination = new MarkerOptions ();
					startingDestination.SetPosition (new LatLng (building.XCoordinate, building.YCoordinate));
					startingDestination.InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.StartPointPNG));
					startPoint = map.AddMarker (startingDestination);
					if(endPoint!=null){
						drawDirections (startPoint.Position, endPoint.Position);
					}
				}
				window.Dismiss();
			};
				
			Button toHere = popUp.FindViewById<Button> (Resource.Id.btnToHere);
			toHere.Click += (sender, e) => {
				if (endPoint == null) {
					MarkerOptions endDestination = new MarkerOptions ();
					endDestination.SetPosition (new LatLng (building.XCoordinate, building.YCoordinate));
					endDestination.InvokeIcon (BitmapDescriptorFactory.FromResource (Resource.Drawable.EndPointPNG));
					endPoint = map.AddMarker (endDestination);
					if(startPoint !=null && endPoint !=null){
						drawDirections (startPoint.Position, endPoint.Position);
					}
					else if(startPoint == null && endPoint !=null && map.MyLocation !=null){
						drawDirections (new LatLng(map.MyLocation.Latitude,map.MyLocation.Longitude), endPoint.Position);
					}
				} else {
					Toast.MakeText (this, "You already have an ending destination, " + building.Name + " will be your new ending destination", ToastLength.Short).Show ();
					endPoint.Remove();
					MarkerOptions endDestination = new MarkerOptions ();
					endDestination.SetPosition (new LatLng (building.XCoordinate, building.YCoordinate));
					endDestination.InvokeIcon (BitmapDescriptorFactory.FromResource (Resource.Drawable.EndPointPNG));
					endPoint = map.AddMarker (endDestination);
					if(startPoint !=null && endPoint !=null){
						drawDirections (startPoint.Position, endPoint.Position);
					}
					else if(startPoint == null && endPoint !=null && map.MyLocation !=null){
						drawDirections (new LatLng(map.MyLocation.Latitude,map.MyLocation.Longitude), endPoint.Position);
					}
				}
				window.Dismiss();
			};
		}


		public void zoomSgw(GoogleMap map){
			LatLng location = new LatLng(45.49564057468219, -73.57727140188217);
			CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
			builder.Target(location);
			builder.Zoom(16);
			CameraPosition cameraPosition = builder.Build();
			map.AnimateCamera  (CameraUpdateFactory.NewCameraPosition (cameraPosition));
		}

		void HandleMapClick (object sender, GoogleMap.MapClickEventArgs e)
		{
			Building b = BuildingManager.isInPolygon(e.Point);
			if (b != null && window ==null) {
				CreateBuildingDescription (b);
			}
			else if(window !=null){
				window.Dismiss ();
				window = null;
			}
		}
			
		public void zoomLoyola(GoogleMap map){
			LatLng location = new LatLng(45.458593581866786, -73.64008069038391);
			CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
			builder.Target(location);
			builder.Zoom(16);
			CameraPosition cameraPosition = builder.Build();
			map.AnimateCamera  (CameraUpdateFactory.NewCameraPosition (cameraPosition));
		}
			
		/*public void createSpinnerBuilding(GoogleMap map, List<Building> buildings){
			Spinner spinner = FindViewById<Spinner> (Resource.Id.spinner);
			ArrayAdapter _adapterFrom;

			List<string> strBuildings = new List<string> ();
			strBuildings.Add("Choose a Building");
			foreach(Building building in buildings){
				strBuildings.Add (building.toString());
			}

			_adapterFrom = new ArrayAdapter (this, Android.Resource.Layout.SimpleSpinnerItem, strBuildings);

			_adapterFrom.SetDropDownViewResource (Android.Resource.Layout.SimpleSpinnerDropDownItem);
			spinner.Adapter = _adapterFrom; 

			spinner.ItemSelected += (object sender, AdapterView.ItemSelectedEventArgs e) => {
				foreach(Building building in buildings){
					if(e.Parent.GetItemAtPosition(e.Position).ToString()=="Choose a Building"){

					}
					else if(e.Parent.GetItemAtPosition(e.Position).ToString()==building.toString())
						zoomSpecificBuilding(map, building);
				}
			};
		}*/

		public void zoomSpecificBuilding(GoogleMap map, Building buildingToZoom){
			LatLng location = new LatLng(buildingToZoom.XCoordinate, buildingToZoom.YCoordinate);
			CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
			builder.Target(location);
			builder.Zoom(18);
			CameraPosition cameraPosition = builder.Build();
			map.AnimateCamera  (CameraUpdateFactory.NewCameraPosition (cameraPosition));
		}

		public void createSettingsDrawer(){
			drawerSettings = new List<string> ();
			listview = FindViewById<ListView> (Resource.Id.left_drawer);
			drawer = FindViewById<DrawerLayout> (Resource.Id.drawer_layout);
			mDrawerToggle = new ActionBarDrawerToggle (this, drawer, Resource.Drawable.ic_navigation_drawer, Resource.String.open_drawer, Resource.String.close_drawer);

			drawerSettings.Add ("Settings");
			drawerSettings.Add ("Settings Working");
			ArrayAdapter mLeftAdapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, drawerSettings);
			listview.Adapter = mLeftAdapter;

			drawer.SetDrawerListener (mDrawerToggle);
			ActionBar.SetDisplayHomeAsUpEnabled (true);
			ActionBar.SetHomeButtonEnabled (true);
		}

		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);
			mDrawerToggle.SyncState ();
		}

		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			if (mDrawerToggle.OnOptionsItemSelected (item)) {
				return true;
			}

			return base.OnOptionsItemSelected (item);
		}

		public void drawSGWPolygons(GoogleMap map){
			List<Building> b = BuildingManager.getSGWBuildings();
			PolygonOptions SGWPolygon;
			foreach (var building in b) {
				SGWPolygon = new PolygonOptions();
				foreach (var p in building.Corners) {
					SGWPolygon.Add (p);
				}
				int Color = Int32.Parse("50800020", System.Globalization.NumberStyles.HexNumber);
				SGWPolygon.InvokeFillColor(Color);
				SGWPolygon.InvokeStrokeWidth (4);
				map.AddPolygon (SGWPolygon);
			}
		}

		public void drawLoyolaPolygons(GoogleMap map){

			List<Building> b = BuildingManager.getLoyolaBuildings();
			PolygonOptions LoyolaPolygon = new PolygonOptions();
			foreach (var building in b) {
				LoyolaPolygon = new PolygonOptions();
				foreach (var p in building.Corners) {
					LoyolaPolygon.Add (p);
				}
				int Color = Int32.Parse ("50800020", System.Globalization.NumberStyles.HexNumber);
				LoyolaPolygon.InvokeFillColor(Color);
				LoyolaPolygon.InvokeStrokeWidth (4);
				map.AddPolygon (LoyolaPolygon);
			}
		}


		public void drawSGWMarkers(GoogleMap map){
			List<Building> b = BuildingManager.getSGWBuildings();
			GroundOverlayOptions SGWOverlay;
			foreach (var building in b) { 

				if (building.BuildingOverlay != 0) {
					BitmapDescriptor image = BitmapDescriptorFactory.FromResource (building.BuildingOverlay);
					GroundOverlayOptions byo = new GroundOverlayOptions ().Position (new LatLng (building.XCoordinate, building.YCoordinate), building.OverlaySize).InvokeImage (image);

					map.AddGroundOverlay (byo);
				}
			}
		}


		public void drawLOYMarkers(GoogleMap map){
			List<Building> b = BuildingManager.getLoyolaBuildings();

			foreach (var building in b) { 

				if (building.BuildingOverlay != 0) {
					BitmapDescriptor image = BitmapDescriptorFactory.FromResource (building.BuildingOverlay);
					GroundOverlayOptions byo = new GroundOverlayOptions ().Position (new LatLng (building.XCoordinate, building.YCoordinate), building.OverlaySize).InvokeImage (image);

					map.AddGroundOverlay (byo);
				}
			}
		}
	}
}

