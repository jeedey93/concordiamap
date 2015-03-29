﻿using Android.App;
using Android.OS;
using Android.Views;
using Android.Gms.Maps;
using System;
using Android.Gms.Maps.Model;
using Android.Widget;
using System.Collections.Generic;
using Android.Content;
using System.Json;
using System.Text.RegularExpressions;
using Android.Views.InputMethods;
using Android.Content.PM;


namespace GoogleApiTest
{
	[Activity (Label = "CONCORDIA CONQUEST",MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]			
	public class MapActivity : LeftDrawerActivity
	{
		BuildingManager BuildingManager = new BuildingManager ();
		BusManager BusManager = new BusManager();
		DirectionFetcher DirectionFetcher;
		GoogleMap map;
		PopupWindow window=null;
		Marker startPoint;
		Marker endPoint;
		Building startB;
		Building endB;
		Polyline directionPath;
		Polyline directionPath2;
		Marker busPosition;
		Marker busPosition2;
		Polygon ClickedPolygon;
		enum TravelMode{Walking, Driving, Transit};
		TravelMode TravelModeChosen = TravelMode.Walking;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle,Resource.Layout.Main );

			// Create your application here
			DirectionFetcher = new DirectionFetcher (this);
			MapFragment mapFrag = (MapFragment) FragmentManager.FindFragmentById(Resource.Id.map);
			map = mapFrag.Map;
			if (map != null) {
				// The GoogleMap object is ready to go.
				map.UiSettings.ZoomControlsEnabled = true;
				map.MyLocationEnabled = true;
				map.UiSettings.TiltGesturesEnabled = false;
				map.UiSettings.MapToolbarEnabled = false;
				ZoomSgw (map);
			}

			BuildingManager.InitializeSGWBuildings ();
			BuildingManager.InitializeLoyolaBuildings ();

			AutoCompleteTextView to = FindViewById<AutoCompleteTextView>(Resource.Id.AutoCompleteInput);
			to.Adapter = new ArrayAdapter<string> (this, Resource.Layout.list_locations, AllLocations(BuildingManager.GetSGWBuildings (), BuildingManager.GetLoyolaBuildings ()));


			ImageButton myLocation = FindViewById<ImageButton> (Resource.Id.imageButton2);
			myLocation.Click += (o, e) => {
				ZoomMyLocation();
			};

			ToggleButton togglebutton = FindViewById<ToggleButton>(Resource.Id.togglebutton);

			togglebutton.Click += (o, e) => {
				// Perform action on clicks
				if (togglebutton.Checked){
					zoomLoyola (map);
					//createSpinnerBuilding (map, BuildingManager.getLoyolaBuildings());
				}else{
					ZoomSgw (map);
					//createSpinnerBuilding (map, BuildingManager.getSGWBuildings());
				}
			};

			DrawSGWPolygons (map);
			DrawLoyolaPolygons (map);

			DrawLOYMarkers (map);
			DrawSGWMarkers (map);

			ZoomBuildingToTextEntry (BuildingManager.GetSGWBuildings (), BuildingManager.GetLoyolaBuildings ());
			//createSpinnerBuilding (map, BuildingManager.getSGWBuildings ());

			map.MapClick += HandleMapClick;

			Button DrivingMode = FindViewById<Button> (Resource.Id.Driving);
			Button WalkingMode = FindViewById<Button> (Resource.Id.Walking);
			Button TransitMode = FindViewById<Button> (Resource.Id.Transit);
			WalkingMode.SetBackgroundColor(Android.Graphics.Color.Gold);


			DrivingModeClick (DrivingMode, WalkingMode, TransitMode);
			WalkingModeClick (DrivingMode, WalkingMode, TransitMode);
			TransitModeClick (DrivingMode, WalkingMode, TransitMode);

			ClearMarkerFromMap ();

		} 

		void DrivingModeClick (Button DrivingMode, Button WalkingMode, Button TransitMode)
		{
			DrivingMode.Click += (o, e) =>  {
				DrivingMode.SetBackgroundColor (Android.Graphics.Color.Gold);
				WalkingMode.SetBackgroundResource (Resource.Drawable.exploreMButtonStyle);
				TransitMode.SetBackgroundResource (Resource.Drawable.exploreMButtonStyle);
				TravelModeChosen = TravelMode.Driving;
				ClearBusMarkers ();
				if (startB != null && endB != null)
					DrawDirections (new LatLng (startB.XCoordinate, startB.YCoordinate), new LatLng (endB.XCoordinate, endB.YCoordinate), "driving");
				else
					DrawDirections (new LatLng (map.MyLocation.Latitude, map.MyLocation.Longitude), new LatLng (endB.XCoordinate, endB.YCoordinate), "driving");
			};
		}
			
		void WalkingModeClick (Button DrivingMode, Button WalkingMode, Button TransitMode)
		{
			WalkingMode.Click += (o, e) =>  {
				DrivingMode.SetBackgroundResource (Resource.Drawable.exploreMButtonStyle);
				WalkingMode.SetBackgroundColor (Android.Graphics.Color.Gold);
				TransitMode.SetBackgroundResource (Resource.Drawable.exploreMButtonStyle);
				TravelModeChosen = TravelMode.Walking;
				if (startB != null && endB != null && startB.Campus == endB.Campus) {
					DrawDirections (new LatLng (startB.XCoordinate, startB.YCoordinate), new LatLng (endB.XCoordinate, endB.YCoordinate));
				}
				else
					if (startB != null && startB.Campus != endB.Campus) {
						DrawDirectionsDifferentCampus (new LatLng (startB.XCoordinate, startB.YCoordinate), new LatLng (endB.XCoordinate, endB.YCoordinate));
					}
					else
						if (startB == null) {
							DrawDirectionsDifferentCampus (new LatLng (map.MyLocation.Latitude, map.MyLocation.Longitude), new LatLng (endB.XCoordinate, endB.YCoordinate));
						}
			};
		}

		void TransitModeClick (Button DrivingMode, Button WalkingMode, Button TransitMode)
		{
			TransitMode.Click += (o, e) =>  {
				DrivingMode.SetBackgroundResource (Resource.Drawable.exploreMButtonStyle);
				WalkingMode.SetBackgroundResource (Resource.Drawable.exploreMButtonStyle);
				TransitMode.SetBackgroundColor (Android.Graphics.Color.Gold);
				TravelModeChosen = TravelMode.Transit;
				ClearBusMarkers ();
				if (startB != null && endB != null)
					DrawDirections (new LatLng (startB.XCoordinate, startB.YCoordinate), new LatLng (endB.XCoordinate, endB.YCoordinate), "transit");
				else
					DrawDirections (new LatLng (map.MyLocation.Latitude, map.MyLocation.Longitude), new LatLng (endB.XCoordinate, endB.YCoordinate), "transit");
			};
		}

		void ClearMarkerFromMap ()
		{
			Button clearButton = FindViewById<Button> (Resource.Id.clearMarker);
			Button Reload = FindViewById<Button> (Resource.Id.Reload);
			clearButton.Click += (o, e) =>  {
				ClearStartEndPath ();
				ClearBusMarkers ();
				map.UiSettings.ZoomControlsEnabled = true;
				clearButton.Visibility = ViewStates.Invisible;
				Reload.Visibility = ViewStates.Invisible;
				LinearLayout slideUp = FindViewById<LinearLayout> (Resource.Id.SlideUpText);
				slideUp.Visibility = ViewStates.Gone;
				RelativeLayout clearLayout = FindViewById<RelativeLayout> (Resource.Id.clearLayout);
				clearLayout.SetPadding (0, 0, 0, 0);
				RelativeLayout mode = FindViewById<RelativeLayout> (Resource.Id.mode);
				mode.Visibility = ViewStates.Gone;
			};
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			if(data!=null){
				if (requestCode==0) {
					double startPositionX = data.GetDoubleExtra ("startPositionX", 0);
					double startPositionY = data.GetDoubleExtra ("startPositionY", 0);
					double endPositionX = data.GetDoubleExtra ("endPositionX", 0);
					double endPositionY = data.GetDoubleExtra ("endPositionY", 0);
					Boolean campus = data.GetBooleanExtra ("sameCampus", false);
					string startBuilding = data.GetStringExtra("startBuilding");
					string endBuilding = data.GetStringExtra("endBuilding");
					startB = FindBuildingName (startBuilding);
					endB = FindBuildingName (endBuilding);


					if (campus) {
						//set start and End buildings before drawing directions accross campuses.
						startB = FindBuildingFromCoordinates (startPositionX, startPositionY);
						endB = FindBuildingFromCoordinates (endPositionX, endPositionY);
						if (startB == null || endB == null) {
							Toast.MakeText (this, "Building doesn't exist", ToastLength.Short).Show ();
							return;
						}

						SetStartMarker (startB);
						SetEndMarker (endB);

						DrawDirections (startPoint.Position, endPoint.Position);

					} else {
						//set start and End buildings before drawing directions accross campuses.
						startB = FindBuildingFromCoordinates (startPositionX, startPositionY);
						endB = FindBuildingFromCoordinates (endPositionX, endPositionY);
						if (startB == null || endB == null) {
							Toast.MakeText (this, "Building doesn't exist", ToastLength.Short).Show ();
							return;
						}

						SetStartMarker (startB);
						SetEndMarker (endB);
						DrawDirectionsDifferentCampus(startPoint.Position,	endPoint.Position);
					}
				}
				else if (requestCode==1) {
					string buildingAbrev = data.GetStringExtra ("nextBuilding");
					if (buildingAbrev != null) {
						Building Destination = FindBuildingName (buildingAbrev);

						DrawDirectionsDifferentCampus (new LatLng (map.MyLocation.Latitude, map.MyLocation.Longitude), new LatLng (Destination.XCoordinate, Destination.YCoordinate));
						endB = Destination;
						SetEndMarker (endB);
					}
				}
			}
		}

		//retrieve building from Both Loyola and SGW using their Coordinates
		Building FindBuildingFromCoordinates(double XCoordinate, double YCoordinate){

			foreach(Building b in BuildingManager.GetSGWBuildings()){

				if (b.XCoordinate == XCoordinate && b.YCoordinate == YCoordinate) {
					return b;
				}

			}
			foreach(Building b in BuildingManager.GetLoyolaBuildings()){

				if (b.XCoordinate == XCoordinate && b.YCoordinate == YCoordinate) {
					return b;
				} 


			}
			return null;

		}

		Building FindBuildingName (string buildingAbrev)
		{
			foreach (Building item in BuildingManager.GetSGWBuildings()) {
				if (item.Abbreviation == buildingAbrev) {
					return item;
				}
			}
			foreach (Building item in BuildingManager.GetLoyolaBuildings()) {
				if (item.Abbreviation == buildingAbrev) {
					return item;
				}
			}
			return null;
		}

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

		void ClearStartEndPath ()
		{
			if (startPoint != null) {
				startPoint.Remove ();
				startPoint = null;
			}
			if (endPoint != null) {
				endPoint.Remove ();
				endPoint = null;
			}
			if (directionPath != null) {
				directionPath.Remove ();
				directionPath = null;
			}
			if (directionPath2 != null) {
				directionPath2.Remove ();
				directionPath2 = null;
			}
		}

		public void ZoomMyLocation(){
			if (map.MyLocation != null) {
				LatLng location = new LatLng (map.MyLocation.Latitude, map.MyLocation.Longitude);
				CameraPosition.Builder builder = CameraPosition.InvokeBuilder ();
				builder.Target (location);
				builder.Zoom (18);
				CameraPosition cameraPosition = builder.Build ();
				map.AnimateCamera (CameraUpdateFactory.NewCameraPosition (cameraPosition));
			}
		}

		public void ClearBusMarkers ()
		{
			if (busPosition != null) {
				busPosition.Remove ();
				busPosition = null;
			}
			if (busPosition2 != null) {
				busPosition2.Remove ();
				busPosition2 = null;
			}
		}

		public void ZoomBuildingToTextEntry(List<Building> sgw,List<Building> loy){
			ImageButton toDestination = FindViewById<ImageButton>(Resource.Id.load_to_direction);
			AutoCompleteTextView entry = FindViewById<AutoCompleteTextView>(Resource.Id.AutoCompleteInput);

			List<Building> buildBuildings = new List<Building> ();
			List<String> strBuildings = new List<String> ();
			foreach(Building loyBuilding in loy){
				strBuildings.Add (loyBuilding.ToString());
				buildBuildings.Add (loyBuilding);
			}
			foreach(Building sgwBuilding in sgw){
				strBuildings.Add (sgwBuilding.ToString());
				buildBuildings.Add (sgwBuilding);
			}

			toDestination.Click += (o, e) => {
				// Perform action on clicks
				InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
				imm.HideSoftInputFromWindow(entry.WindowToken, 0);
				foreach (Building building in buildBuildings){
					string entryText = entry.Text.ToUpper();
					if(building.Abbreviation == entryText){

						zoomSpecificBuilding(map,building);
					}
				}

			};

		}

		public Campus GetClosestCampus(double xCoordinate=0, double yCoordinate=0){
			double locationLat;
			double locationLong;

			if (xCoordinate != 0 && yCoordinate != 0) {
				locationLat = xCoordinate;
				locationLong = yCoordinate;
			} else {
				locationLat =  map.MyLocation.Latitude;
				locationLong = map.MyLocation.Longitude;
			}
			double distanceSGW = Math.Sqrt (Math.Pow (45.49564057468219 - locationLat,2) + Math.Pow (-73.57727140188217 -
				locationLong,2));
			double distanceLoyola = Math.Sqrt (Math.Pow (45.458593581866786 - locationLat,2) + Math.Pow (-73.64008069038391 -
				locationLong,2));

			if (distanceSGW < distanceLoyola) {
				return new Campus ("SGW", new LatLng (45.497083, -73.578440));
			} else {
				return new Campus ("LOYOLA", new LatLng(45.457683, -73.638978));
			}
		}

		public async void DrawDirectionsDifferentCampus(LatLng startingPoint, LatLng endingPoint){
			if (directionPath != null) {
				directionPath.Remove ();
			}
			if (directionPath2 != null) {
				directionPath2.Remove ();
			}

			Campus ClosestCampus;
			if (startB == null) {
				ClosestCampus = GetClosestCampus ();
			} else {
				ClosestCampus = startB.Campus;
			}
			JsonValue firstDirections = await DirectionFetcher.GetDirections(startingPoint,ClosestCampus.ExtractionPoint);
			if (firstDirections == null) {
				Toast.MakeText (this, "Please connect to a network", ToastLength.Short).Show ();
				return;
			}
			JsonValue firstRoutesResults = firstDirections ["routes"];
			string points1 = firstRoutesResults [0] ["overview_polyline"] ["points"];
			var polyPoints1 = DirectionFetcher.DecodePolylinePoints (points1);

			Campus OtherCampus;
			if (endB == null) {
				if (ClosestCampus.CampusName == "SGW") {
					OtherCampus = new Campus ("LOYOLA", new LatLng (45.457683, -73.638978));
				} else {
					OtherCampus = new Campus ("SGW", new LatLng (45.497083, -73.578440));
				}
			} else {
				OtherCampus = endB.Campus;
			}

			JsonValue secondDirections = await DirectionFetcher.GetDirections(OtherCampus.ExtractionPoint,endingPoint);
			if (secondDirections == null) {
				Toast.MakeText (this, "Please connect to a network", ToastLength.Short).Show ();
				return;
			}
			JsonValue secondRoutesResults = secondDirections ["routes"];
			string points2 = secondRoutesResults [0] ["overview_polyline"] ["points"];
			var polyPoints2 = DirectionFetcher.DecodePolylinePoints (points2);

			GetInstructionDifferentCampus (ClosestCampus, OtherCampus, firstRoutesResults, polyPoints1, secondRoutesResults, polyPoints2);
		}

		void GetInstructionDifferentCampus (Campus ClosestCampus, Campus OtherCampus, JsonValue firstRoutesResults, List<LatLng> polyPoints1, JsonValue secondRoutesResults, List<LatLng> polyPoints2)
		{
			string Instructions = DisplayStepDirections (DirectionFetcher.GetInstructionsDifferentCampus (firstRoutesResults, secondRoutesResults));

			ListView instructionsView = FindViewById<ListView> (Resource.Id.SlideUpList);
			List<String> instructionslist = new List<String> ();
			instructionslist.Add (Instructions);

			ArrayAdapter instructionsAdapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, instructionslist);
			instructionsView.Adapter = instructionsAdapter;
			List<LatLng> direction1 = polyPoints1;
			List<LatLng> direction2 = polyPoints2;
			PolylineOptions line1 = new PolylineOptions ();
			PolylineOptions line2 = new PolylineOptions ();
			foreach (var point in direction1) {
				line1.Add (point);
			}
			foreach (var point in direction2) {
				line2.Add (point);
			}
			directionPath = map.AddPolyline (line1);
			directionPath.Width = 9;
			int Color = Int32.Parse ("ff800020", System.Globalization.NumberStyles.HexNumber);
			directionPath.Color = Color;
			directionPath2 = map.AddPolyline (line2);
			directionPath2.Width = 9;
			directionPath2.Color = Color;
			CreateBusMarkers (ClosestCampus, OtherCampus);
			LinearLayout slideUp = FindViewById<LinearLayout> (Resource.Id.SlideUpText);
			slideUp.Visibility = ViewStates.Visible;
			RelativeLayout clearLayout = FindViewById<RelativeLayout> (Resource.Id.clearLayout);
			clearLayout.SetPadding (0, 0, 0, 200);
			RelativeLayout mode = FindViewById<RelativeLayout> (Resource.Id.mode);
			mode.Visibility = ViewStates.Visible;
		}

		public void CreateBusMarkers(Campus StartingCampus =null, Campus EndingCampus = null){
			MarkerOptions busMarker = new MarkerOptions ();
			if (StartingCampus != null) {
				busMarker.SetPosition (new LatLng (StartingCampus.ExtractionPoint.Latitude, StartingCampus.ExtractionPoint.Longitude));
			} else {
				busMarker.SetPosition (new LatLng (startB.Campus.ExtractionPoint.Latitude, startB.Campus.ExtractionPoint.Longitude));
			}
			busMarker.InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.Bus));
			DefineBusTime (busMarker);
			busPosition = map.AddMarker (busMarker);


			MarkerOptions busMarker2 = new MarkerOptions ();

			if (EndingCampus != null) {
				busMarker2.SetPosition (new LatLng (EndingCampus.ExtractionPoint.Latitude, EndingCampus.ExtractionPoint.Longitude));
			} else {
				busMarker2.SetPosition (new LatLng (endB.Campus.ExtractionPoint.Latitude, endB.Campus.ExtractionPoint.Longitude));
			}
			busMarker2.InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.Bus));
			DefineBusTime (busMarker2);
			busPosition2 = map.AddMarker (busMarker2);
		}

		public void DefineBusTime(MarkerOptions marker){
			double xCoordinate = marker.Position.Latitude;
			double yCoordinate = marker.Position.Longitude;

			Campus BusCampus = GetClosestCampus (xCoordinate,yCoordinate);
			string NextBusTime = GetNextBusTime (BusCampus);
			marker.SetTitle (NextBusTime);
		}

		public string GetNextBusTime(Campus campus){
			DateTime now = DateTime.Now;
			List<String> BusListTime = new List<string>();
			string NextBusTime = "No next bus available";

			if (now.DayOfWeek == DayOfWeek.Saturday || now.DayOfWeek == DayOfWeek.Sunday) {
				BusListTime = BusManager.InitializeSGWBusMonThur ();
			} else if (now.DayOfWeek == DayOfWeek.Friday && campus.CampusName == "SGW") {
				BusListTime = BusManager.InitializeSGWBusFriday ();
			} else if (now.DayOfWeek == DayOfWeek.Friday && campus.CampusName == "LOYOLA") {
				BusListTime = BusManager.InitializeLOYBusFriday ();
			} else if (campus.CampusName == "SGW") {
				BusListTime = BusManager.InitializeSGWBusMonThur ();
			} else if (campus.CampusName == "LOYOLA") {
				BusListTime = BusManager.InitializeLOYBusMonThur ();
			}

			if (BusListTime != null) {
				foreach (string time  in BusListTime) {
					if (time[0] != 'F') {
						int l = time.IndexOf (":");
						string hourString;
						string minuteString;
						int hour = 0;
						int minutes = 0;

						if (l > 0) {
							hourString = time.Substring (0, l);
							minuteString = time.Substring (l + 1);
							hour = Convert.ToInt32 (hourString);
							minutes = Convert.ToInt32 (minuteString);
						}
						if (hour != 0 && hour == now.Hour && minutes > now.Minute) {
							return time;
						}
					} else {
						DateTime Interval = new DateTime ();
						TimeSpan ts = new TimeSpan(11, 45, 0);
						Interval = Interval.Date + ts;
						while (Interval.Hour != 19) {
							if (Interval.Hour == now.Hour && Interval.Minute > now.Minute) {
								return Interval.ToString("t");
							} else {
								Interval= Interval.AddMinutes (20);
							}
						}
					}
				}
			}
			return NextBusTime;
		}

		public async void DrawDirections(LatLng startingPoint, LatLng endingPoint, string transitMode=""){
			if (directionPath != null) {
				directionPath.Remove ();
			}
			if (directionPath2 != null) {
				directionPath2.Remove ();
			}

			JsonValue directions;
			if(transitMode!= "")
				directions = await DirectionFetcher.GetDirections(startingPoint,endingPoint, transitMode);
			else 
				directions = await DirectionFetcher.GetDirections(startingPoint,endingPoint);
			if (directions == null) {
				Toast.MakeText (this, "Please connect to a network", ToastLength.Short).Show ();
				return;
			}

			JsonValue routesResults = directions ["routes"];

			GetInstructionSameCampus (routesResults);
		}

		void GetInstructionSameCampus (JsonValue routesResults)
		{
			string instructions = DirectionFetcher.GetInstructions (routesResults);
		
			string formattedInstructions = DisplayStepDirections (instructions);

			ListView instructionsView = FindViewById<ListView> (Resource.Id.SlideUpList);
			List<String> instructionslist = new List<String> ();
			instructionslist.Add (formattedInstructions);
			ArrayAdapter instructionsAdapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, instructionslist);
			instructionsView.Adapter = instructionsAdapter;
			string points = routesResults [0] ["overview_polyline"] ["points"];
			var polyPoints = DirectionFetcher.DecodePolylinePoints (points);
			//LatLng center = new LatLng (0.0, 0.0);
			LatLngBounds.Builder boundsbuilder = new LatLngBounds.Builder ();
			List<LatLng> direction = polyPoints;
			PolylineOptions line = new PolylineOptions ();
			foreach (var point in direction) {
				line.Add (point);
				boundsbuilder.Include (point);
			}
			directionPath = map.AddPolyline (line);
			directionPath.Width = 9;
			int Color = Int32.Parse ("ff800020", System.Globalization.NumberStyles.HexNumber);
			directionPath.Color = Color;
			LatLngBounds bounds = boundsbuilder.Build ();
			//Set Camera so that it can fit the bounds with padding
			map.AnimateCamera (CameraUpdateFactory.NewLatLngBounds (bounds, 400));
			LinearLayout slideUp = FindViewById<LinearLayout> (Resource.Id.SlideUpText);
			slideUp.Visibility = ViewStates.Visible;
			RelativeLayout clearLayout = FindViewById<RelativeLayout> (Resource.Id.clearLayout);
			clearLayout.SetPadding (0, 0, 0, 200);
			RelativeLayout mode = FindViewById<RelativeLayout> (Resource.Id.mode);
			mode.Visibility = ViewStates.Visible;
		}

		public String DisplayStepDirections(string unParsedInstructions){
			String parsedInstructions = Regex.Replace(unParsedInstructions,@"<(.|\n)*?>",string.Empty);
			parsedInstructions = Regex.Replace (parsedInstructions, @"(Destination)", "\nDestination");

			return parsedInstructions;

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

			SetStartPoint(building, popUp);

			SetEndPoint (building, popUp);
		}


		//extracted logic for placing start markers on buildings of the map
		void SetStartMarker (Building building){
			MarkerOptions startingDestination = new MarkerOptions ();
			if (building.BuildingEntrance == null) 
				startingDestination.SetPosition (new LatLng (building.XCoordinate, building.YCoordinate));
			else
				startingDestination.SetPosition (building.BuildingEntrance);
			startingDestination.InvokeIcon (BitmapDescriptorFactory.FromResource (Resource.Drawable.StartPointPNG));
			startPoint = map.AddMarker (startingDestination);
			startB = building;
			//Allow clearMarker buttons so that the user can clear the screen
			Button clearButton = FindViewById<Button> (Resource.Id.clearMarker);
			clearButton.Visibility = ViewStates.Visible;
		}

		//extracted logic for placing end markers on buildings of the map
		void SetEndMarker(Building building){
			MarkerOptions endDestination = new MarkerOptions ();
			if (building.BuildingEntrance == null) 
				endDestination.SetPosition (new LatLng (building.XCoordinate, building.YCoordinate));
			else
				endDestination.SetPosition (building.BuildingEntrance);
			endDestination.InvokeIcon (BitmapDescriptorFactory.FromResource (Resource.Drawable.EndPointPNG));
			endPoint = map.AddMarker (endDestination);
			endB = building;

			//Allow clearMarker buttons so that the user can clear the screen
			Button clearButton = FindViewById<Button> (Resource.Id.clearMarker);
			clearButton.Visibility = ViewStates.Visible;
		}

		void SetStartPoint (Building building, View popUp)
		{
			Button fromHere = popUp.FindViewById<Button> (Resource.Id.btnFromHere);
			fromHere.Click += (sender, e) =>  {
				Button clearButton = FindViewById<Button> (Resource.Id.clearMarker);
				ResetColorOfPolygon ();
				if (startPoint == null && directionPath == null) {
					SetStartMarker(building);
				}
				else {
					Toast.MakeText (this, "You already have a starting destination, " + building.Name + " will be your new starting destination", ToastLength.Short).Show ();
					if (startPoint != null) {
						startPoint.Remove ();
					}
					if (busPosition != null) {
						busPosition.Remove ();
						busPosition = null;
					}
					if (busPosition2 != null) {
						busPosition2.Remove ();
						busPosition2 = null;
					}
					SetStartMarker(building);
					if (endPoint != null) {
						//BUILDINGS NOT ON SAME CAMPUS
						if (startB.Campus != endB.Campus) {
							DrawDirectionsDifferentCampus (startPoint.Position, endPoint.Position);
						}
						else {
							DrawDirections (startPoint.Position, endPoint.Position);
						}
					}
					clearButton.Visibility = ViewStates.Visible;
					map.UiSettings.ZoomControlsEnabled = false;
				}
				window.Dismiss ();
			};
		}

		void SetEndPoint (Building building, View popUp)
		{
			Button toHere = popUp.FindViewById<Button> (Resource.Id.btnToHere);
			toHere.Click += (sender, e) =>  {
				Button clearButton = FindViewById<Button> (Resource.Id.clearMarker);
				ResetColorOfPolygon ();
				if (endPoint == null) {
					SetEndMarker(building);
					if (startPoint != null && endPoint != null) {

						//BUILDINGS NOT ON SAME CAMPUS
						if (startB.Campus != endB.Campus) {
							DrawDirectionsDifferentCampus (startPoint.Position, endPoint.Position);
						}
						else {
							DrawDirections (startPoint.Position, endPoint.Position);
						}
					}
					else
						if (startPoint == null && endPoint != null && map.MyLocation != null) {
							//DrawDirections (new LatLng (map.MyLocation.Latitude, map.MyLocation.Longitude), endPoint.Position);
							DrawDirectionsDifferentCampus(new LatLng (map.MyLocation.Latitude, map.MyLocation.Longitude), endPoint.Position);
							endB = building;
							StartCurrentLocationPath();
						}
					clearButton.Visibility = ViewStates.Visible;
					map.UiSettings.ZoomControlsEnabled = false;
				}
				else {
					Toast.MakeText (this, "You already have an ending destination, " + building.Name + " will be your new ending destination", ToastLength.Short).Show ();
					endPoint.Remove ();
					ClearBusMarkers();
					SetEndMarker(building);
					if (startPoint != null && endPoint != null) {

						//BUILDINGS NOT ON SAME CAMPUS
						if (startB.Campus != endB.Campus) {
							DrawDirectionsDifferentCampus (startPoint.Position, endPoint.Position);
						}
						else {
							DrawDirections (startPoint.Position, endPoint.Position);
						}
					}
					else
						if (startPoint == null && endPoint != null && map.MyLocation != null) {
							//DrawDirections (new LatLng (map.MyLocation.Latitude, map.MyLocation.Longitude), endPoint.Position);
							DrawDirectionsDifferentCampus (new LatLng (map.MyLocation.Latitude, map.MyLocation.Longitude), endPoint.Position);
							StartCurrentLocationPath();
							endB = building;
						}
					clearButton.Visibility = ViewStates.Visible;
					map.UiSettings.ZoomControlsEnabled = false;
				}
				window.Dismiss ();
			};
		}
			
		public void StartCurrentLocationPath(){
			Button Reload = FindViewById<Button> (Resource.Id.Reload);
			Reload.Visibility = ViewStates.Visible;

			Reload.Click += (o, e) => {
				if(TravelModeChosen == TravelMode.Walking){
					DrawDirections (new LatLng (map.MyLocation.Latitude, map.MyLocation.Longitude), endPoint.Position);
				}else if(TravelModeChosen == TravelMode.Driving){
					DrawDirections (new LatLng (map.MyLocation.Latitude, map.MyLocation.Longitude), endPoint.Position, "driving");
				}else if(TravelModeChosen == TravelMode.Transit){
					DrawDirections (new LatLng (map.MyLocation.Latitude, map.MyLocation.Longitude), endPoint.Position, "transit");
				}
			};
		}


		public void ZoomSgw(GoogleMap map){
			LatLng location = new LatLng(45.49564057468219, -73.57727140188217);
			CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
			builder.Target(location);
			builder.Zoom(16);
			CameraPosition cameraPosition = builder.Build();
			map.AnimateCamera  (CameraUpdateFactory.NewCameraPosition (cameraPosition));
		}

		void HandleMapClick (object sender, GoogleMap.MapClickEventArgs e)
		{
			Building b = BuildingManager.IsInPolygon(e.Point);
			if (b != null && window ==null) {
				ClickedPolygon = b.Polygon;
				int Color = Int32.Parse("f0800020", System.Globalization.NumberStyles.HexNumber);
				ClickedPolygon.FillColor = Color;
				CreateBuildingDescription (b);
			}
			else if(window !=null){
				window.Dismiss ();
				window = null;
				ResetColorOfPolygon ();
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

		public void zoomSpecificBuilding(GoogleMap map, Building buildingToZoom){
			LatLng location = new LatLng(buildingToZoom.XCoordinate, buildingToZoom.YCoordinate);
			CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
			builder.Target(location);
			builder.Zoom(18);
			CameraPosition cameraPosition = builder.Build();
			map.AnimateCamera  (CameraUpdateFactory.NewCameraPosition (cameraPosition));
		}
	

		public void DrawSGWPolygons(GoogleMap map){
			List<Building> b = BuildingManager.GetSGWBuildings();
			PolygonOptions SGWPolygon;
			foreach (var building in b) {
				SGWPolygon = new PolygonOptions();
				foreach (var p in building.Corners) {
					SGWPolygon.Add (p);
				}
				int Color = Int32.Parse("50800020", System.Globalization.NumberStyles.HexNumber);
				SGWPolygon.InvokeFillColor(Color);
				SGWPolygon.InvokeStrokeWidth (4);
				building.Polygon = map.AddPolygon (SGWPolygon);
			}
		}

		public void DrawLoyolaPolygons(GoogleMap map){

			List<Building> b = BuildingManager.GetLoyolaBuildings();
			PolygonOptions LoyolaPolygon = new PolygonOptions();
			foreach (var building in b) {
				LoyolaPolygon = new PolygonOptions();
				foreach (var p in building.Corners) {
					LoyolaPolygon.Add (p);
				}
				int Color = Int32.Parse ("50800020", System.Globalization.NumberStyles.HexNumber);
				LoyolaPolygon.InvokeFillColor(Color);
				LoyolaPolygon.InvokeStrokeWidth (4);
				building.Polygon = map.AddPolygon (LoyolaPolygon);
			}
		}


		public void DrawSGWMarkers(GoogleMap map){
			List<Building> b = BuildingManager.GetSGWBuildings();
			GroundOverlayOptions SGWOverlay;
			foreach (var building in b) { 

				if (building.BuildingOverlay != 0) {
					BitmapDescriptor image = BitmapDescriptorFactory.FromResource (building.BuildingOverlay);
					SGWOverlay = new GroundOverlayOptions ().Position (new LatLng (building.XCoordinate, building.YCoordinate), building.OverlaySize).InvokeImage (image);

					map.AddGroundOverlay (SGWOverlay);
				}
			}
		}


		public void DrawLOYMarkers(GoogleMap map){
			List<Building> b = BuildingManager.GetLoyolaBuildings();

			foreach (var building in b) { 

				if (building.BuildingOverlay != 0) {
					BitmapDescriptor image = BitmapDescriptorFactory.FromResource (building.BuildingOverlay);
					GroundOverlayOptions byo = new GroundOverlayOptions ().Position (new LatLng (building.XCoordinate, building.YCoordinate), building.OverlaySize).InvokeImage (image);

					map.AddGroundOverlay (byo);
				}
			}
		}

		public void ResetColorOfPolygon ()
		{
			int Color = Int32.Parse ("50800020", System.Globalization.NumberStyles.HexNumber);
			ClickedPolygon.FillColor = Color;
		}




	}
}

