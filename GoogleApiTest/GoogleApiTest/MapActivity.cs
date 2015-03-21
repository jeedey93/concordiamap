using Android.App;
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


			DrivingMode.Click += (o, e) => {
				DrivingMode.SetBackgroundColor(Android.Graphics.Color.Gold);
				WalkingMode.SetBackgroundResource(Resource.Drawable.exploreMButtonStyle);
				TransitMode.SetBackgroundResource(Resource.Drawable.exploreMButtonStyle);
				TravelModeChosen = TravelMode.Driving;

				ClearBusMarkers ();
					
				if(startB!=null && endB !=null)
					DrawDirections(new LatLng(startB.XCoordinate,startB.YCoordinate),new LatLng(endB.XCoordinate,endB.YCoordinate), "driving");
				else
					DrawDirections(new LatLng(map.MyLocation.Latitude,map.MyLocation.Longitude),new LatLng(endB.XCoordinate,endB.YCoordinate), "driving");
			};


			WalkingMode.Click += (o, e) => {
				DrivingMode.SetBackgroundResource(Resource.Drawable.exploreMButtonStyle);
				WalkingMode.SetBackgroundColor(Android.Graphics.Color.Gold);
				TransitMode.SetBackgroundResource(Resource.Drawable.exploreMButtonStyle);
				TravelModeChosen= TravelMode.Walking;

				if(startB!=null && endB !=null && startB.Campus == endB.Campus){
					DrawDirections(new LatLng(startB.XCoordinate,startB.YCoordinate),new LatLng(endB.XCoordinate,endB.YCoordinate));
				}
				else if(startB!= null && startB.Campus != endB.Campus){
					DrawDirectionsDifferentCampus(new LatLng(startB.XCoordinate,startB.YCoordinate),new LatLng(endB.XCoordinate,endB.YCoordinate));
				}
				else if(startB==null){
					DrawDirectionsDifferentCampus(new LatLng(map.MyLocation.Latitude, map.MyLocation.Longitude),new LatLng(endB.XCoordinate,endB.YCoordinate));
				}
			};


			TransitMode.Click += (o, e) => {
				DrivingMode.SetBackgroundResource(Resource.Drawable.exploreMButtonStyle);
				WalkingMode.SetBackgroundResource(Resource.Drawable.exploreMButtonStyle);
				TransitMode.SetBackgroundColor(Android.Graphics.Color.Gold);
				TravelModeChosen = TravelMode.Transit;

				ClearBusMarkers ();
				if(startB!=null && endB !=null)
					DrawDirections(new LatLng(startB.XCoordinate,startB.YCoordinate),new LatLng(endB.XCoordinate,endB.YCoordinate), "transit");
				else
					DrawDirections(new LatLng(map.MyLocation.Latitude,map.MyLocation.Longitude),new LatLng(endB.XCoordinate,endB.YCoordinate), "transit");
			};

			Button clearButton = FindViewById<Button>(Resource.Id.clearMarker);
			Button Reload = FindViewById<Button> (Resource.Id.Reload);

			clearButton.Click += (o, e) => {
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

		public Campus GetClosestCampus(){
			double distanceSGW = Math.Sqrt (Math.Pow (45.49564057468219 - map.MyLocation.Latitude,2) + Math.Pow (-73.57727140188217 -
			map.MyLocation.Longitude,2));
			double distanceLoyola = Math.Sqrt (Math.Pow (45.458593581866786 - map.MyLocation.Latitude,2) + Math.Pow (-73.64008069038391 -
				map.MyLocation.Longitude,2));
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

			JsonValue secondDirections = await DirectionFetcher.GetDirections(endB.Campus.ExtractionPoint,endingPoint);
			if (secondDirections == null) {
				Toast.MakeText (this, "Please connect to a network", ToastLength.Short).Show ();
				return;
			}
			JsonValue secondRoutesResults = secondDirections ["routes"];
			string points2 = secondRoutesResults [0] ["overview_polyline"] ["points"];
			var polyPoints2 = DirectionFetcher.DecodePolylinePoints (points2);

			//GET INSTRUCTIONS
			string Instructions = DisplayStepDirections(DirectionFetcher.GetInstructionsDifferentCampus (firstRoutesResults, secondRoutesResults));

			//TextView instructionsView = FindViewById<TextView>(Resource.Id.SlideUpText);
			//instructionsView.Text = DisplayStepDirections(Instructions);
			//instructionsView.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
			ListView instructionsView = FindViewById<ListView> (Resource.Id.SlideUpList);
			List<String> instructionslist = new List<String> ();
			instructionslist.Add (Instructions);
			//instructionsView.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();

			ArrayAdapter instructionsAdapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, instructionslist);
			instructionsView.Adapter = instructionsAdapter;

			List<LatLng> direction1 = polyPoints1;
			List<LatLng> direction2 = polyPoints2;
			PolylineOptions line1 = new PolylineOptions();
			PolylineOptions line2 = new PolylineOptions();
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


			directionPath2 = map.AddPolyline(line2);
			directionPath2.Width = 9;
			directionPath2.Color = Color;

			CreateBusMarkers (ClosestCampus);

			LinearLayout slideUp = FindViewById<LinearLayout> (Resource.Id.SlideUpText);
			slideUp.Visibility = ViewStates.Visible;
			RelativeLayout clearLayout = FindViewById<RelativeLayout> (Resource.Id.clearLayout);
			clearLayout.SetPadding (0, 0, 0, 200);

			RelativeLayout mode = FindViewById<RelativeLayout> (Resource.Id.mode);
			mode.Visibility = ViewStates.Visible;
		}

		public void CreateBusMarkers(Campus StartingCampus =null){
			MarkerOptions busMarker = new MarkerOptions ();
			if (StartingCampus != null) {
				busMarker.SetPosition (new LatLng (StartingCampus.ExtractionPoint.Latitude, StartingCampus.ExtractionPoint.Longitude));
			} else {
				busMarker.SetPosition (new LatLng (startB.Campus.ExtractionPoint.Latitude, startB.Campus.ExtractionPoint.Longitude));
			}
			busMarker.InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.Bus));
			busPosition = map.AddMarker (busMarker);

			MarkerOptions busMarker2 = new MarkerOptions ();
			busMarker2.SetPosition (new LatLng (endB.Campus.ExtractionPoint.Latitude, endB.Campus.ExtractionPoint.Longitude));
			busMarker2.InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.Bus));
			busPosition2 = map.AddMarker (busMarker2);
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

			//GET INSTRUCTIONS
			string instructions = DirectionFetcher.GetInstructions (routesResults);

			//TextView instructionsView = FindViewById<TextView>(Resource.Id.SlideUpText);
			//instructionsView.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
			string formattedInstructions = DisplayStepDirections (instructions);

			//instructionsView.Text = formattedInstructions;
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
			PolylineOptions line = new PolylineOptions();
			foreach (var point in direction) {
				line.Add (point);
				boundsbuilder.Include(point);

			}

			directionPath = map.AddPolyline (line);
			directionPath.Width = 9;
			int Color = Int32.Parse ("ff800020", System.Globalization.NumberStyles.HexNumber);
			directionPath.Color = Color;

			LatLngBounds bounds = boundsbuilder.Build();
			map.AnimateCamera  (CameraUpdateFactory.NewLatLngBounds(bounds,200));
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

		void SetStartPoint (Building building, View popUp)
		{
			Button fromHere = popUp.FindViewById<Button> (Resource.Id.btnFromHere);
			fromHere.Click += (sender, e) =>  {
				Button clearButton = FindViewById<Button> (Resource.Id.clearMarker);
				ResetColorOfPolygon ();
				if (startPoint == null && directionPath == null) {
					MarkerOptions startingDestination = new MarkerOptions ();
					if (building.BuildingEntrance == null) 
						startingDestination.SetPosition (new LatLng (building.XCoordinate, building.YCoordinate));
					else
						startingDestination.SetPosition (building.BuildingEntrance);
					startingDestination.InvokeIcon (BitmapDescriptorFactory.FromResource (Resource.Drawable.StartPointPNG));
					startPoint = map.AddMarker (startingDestination);
					clearButton.Visibility = ViewStates.Visible;
					startB = building;
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
					MarkerOptions startingDestination = new MarkerOptions ();
					if (building.BuildingEntrance == null) 
						startingDestination.SetPosition (new LatLng (building.XCoordinate, building.YCoordinate));
					else
						startingDestination.SetPosition (building.BuildingEntrance);
					startingDestination.InvokeIcon (BitmapDescriptorFactory.FromResource (Resource.Drawable.StartPointPNG));
					startPoint = map.AddMarker (startingDestination);
					startB = building;
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
					MarkerOptions endDestination = new MarkerOptions ();
					if (building.BuildingEntrance == null) 
					endDestination.SetPosition (new LatLng (building.XCoordinate, building.YCoordinate));
					else
						endDestination.SetPosition (building.BuildingEntrance);
					endDestination.InvokeIcon (BitmapDescriptorFactory.FromResource (Resource.Drawable.EndPointPNG));
					endPoint = map.AddMarker (endDestination);
					if (startPoint != null && endPoint != null) {
						endB = building;
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
					MarkerOptions endDestination = new MarkerOptions ();
					if (building.BuildingEntrance == null) 
					endDestination.SetPosition (new LatLng (building.XCoordinate, building.YCoordinate));
					else
						endDestination.SetPosition (building.BuildingEntrance);
					endDestination.InvokeIcon (BitmapDescriptorFactory.FromResource (Resource.Drawable.EndPointPNG));
					endPoint = map.AddMarker (endDestination);
					if (startPoint != null && endPoint != null) {
						endB = building;
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

