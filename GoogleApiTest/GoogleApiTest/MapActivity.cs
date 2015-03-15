using Android.App;
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
using System.Text.RegularExpressions;

namespace GoogleApiTest
{
	[Activity (Label = "CONCORDIA CONQUEST",MainLauncher = false)]			
	public class MapActivity : LeftDrawerActivity
	{
		BuildingManager BuildingManager = new BuildingManager ();
		DirectionFetcher DirectionFetcher = new DirectionFetcher ();
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

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle,Resource.Layout.Main );

			// Create your application here

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
			to.Adapter = new ArrayAdapter<string> (this, Resource.Layout.list_locations, AllLocations(BuildingManager.getSGWBuildings (), BuildingManager.getLoyolaBuildings ()));

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

			ZoomBuildingToTextEntry (BuildingManager.getSGWBuildings (), BuildingManager.getLoyolaBuildings ());
			//createSpinnerBuilding (map, BuildingManager.getSGWBuildings ());

			map.MapClick += HandleMapClick;

			Button clearButton = FindViewById<Button>(Resource.Id.clearMarker);
			clearButton.Click += (o, e) => {
				if(startPoint!=null){
					startPoint.Remove();
					startPoint = null;
				}
				if(endPoint!=null){
					endPoint.Remove();
					endPoint=null;
				}
				if(directionPath!=null){
					directionPath.Remove();
					directionPath=null;
				}
				if(directionPath2!=null){
					directionPath2.Remove();
					directionPath2=null;
				}
				if(busPosition!=null){
					busPosition.Remove();
					busPosition=null;
				}
				if(busPosition2!=null){
					busPosition2.Remove();
					busPosition2=null;
				}

				map.UiSettings.ZoomControlsEnabled = true;
				clearButton.Visibility = ViewStates.Invisible;
				TextView slideUp = FindViewById<TextView> (Resource.Id.SlideUpText);
				slideUp.Visibility = ViewStates.Gone;
				RelativeLayout clearLayout = FindViewById<RelativeLayout> (Resource.Id.clearLayout);
				clearLayout.SetPadding (0, 0, 0, 0);
			};
		} 
		public String[] AllLocations (List<Building> sgw,List<Building> loy){ 

			List<string> locations = new List<string> ();
			List<String> strBuildings = new List<String> ();
			foreach(Building loyBuilding in loy){
				locations.Add (loyBuilding.toString());
			}
			foreach(Building sgwBuilding in sgw){
				locations.Add (sgwBuilding.toString());
			}
			String[] locations_array = locations.ToArray ();
			return locations_array;
		}

		public void ZoomBuildingToTextEntry(List<Building> sgw,List<Building> loy){
			ImageButton toDestination = FindViewById<ImageButton>(Resource.Id.load_to_direction);
			AutoCompleteTextView entry = FindViewById<AutoCompleteTextView>(Resource.Id.AutoCompleteInput);

			List<Building> buildBuildings = new List<Building> ();
			List<String> strBuildings = new List<String> ();
			foreach(Building loyBuilding in loy){
				strBuildings.Add (loyBuilding.toString());
				buildBuildings.Add (loyBuilding);
			}
			foreach(Building sgwBuilding in sgw){
				strBuildings.Add (sgwBuilding.toString());
				buildBuildings.Add (sgwBuilding);
			}

			toDestination.Click += (o, e) => {
				// Perform action on clicks
				foreach (Building building in buildBuildings){
					string entryText = entry.Text.ToUpper();
					if(building.Abbreviation == entryText){
						zoomSpecificBuilding(map,building);
					}
				}
			};
		}

		public async void DrawDirectionsDifferentCampus(LatLng startingPoint, LatLng endingPoint){
			if (directionPath != null) {
				directionPath.Remove ();
			}
			if (directionPath2 != null) {
				directionPath2.Remove ();
			}
			JsonValue firstDirections = await DirectionFetcher.GetDirections(startingPoint,startB.Campus.ExtractionPoint);
			JsonValue firstRoutesResults = firstDirections ["routes"];
			string points1 = firstRoutesResults [0] ["overview_polyline"] ["points"];
			var polyPoints1 = DirectionFetcher.DecodePolylinePoints (points1);

			JsonValue secondDirections = await DirectionFetcher.GetDirections(endB.Campus.ExtractionPoint,endingPoint);
			JsonValue secondRoutesResults = secondDirections ["routes"];
			string points2 = secondRoutesResults [0] ["overview_polyline"] ["points"];
			var polyPoints2 = DirectionFetcher.DecodePolylinePoints (points2);

			//GET INSTRUCTIONS
			string Instructions = DirectionFetcher.GetInstructionsDifferentCampus (firstRoutesResults, secondRoutesResults);

			TextView instructionsView = FindViewById<TextView>(Resource.Id.SlideUpText);
			instructionsView.Text = DisplayStepDirections(Instructions);
			//instructionsView.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();

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

			CreateBusMarkers ();



			TextView slideUp = FindViewById<TextView> (Resource.Id.SlideUpText);
			slideUp.Visibility = ViewStates.Visible;
			RelativeLayout clearLayout = FindViewById<RelativeLayout> (Resource.Id.clearLayout);
			clearLayout.SetPadding (0, 0, 0, 200);

		}

		public void CreateBusMarkers(){
			MarkerOptions busMarker = new MarkerOptions ();
			busMarker.SetPosition (new LatLng (startB.Campus.ExtractionPoint.Latitude, startB.Campus.ExtractionPoint.Longitude));
			busMarker.InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.Bus));
			busPosition = map.AddMarker (busMarker);

			MarkerOptions busMarker2 = new MarkerOptions ();
			busMarker2.SetPosition (new LatLng (endB.Campus.ExtractionPoint.Latitude, endB.Campus.ExtractionPoint.Longitude));
			busMarker2.InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.Bus));
			busPosition2 = map.AddMarker (busMarker2);
		}

		public async void DrawDirections(LatLng startingPoint, LatLng endingPoint){
			if (directionPath != null) {
				directionPath.Remove ();
			}
			if (directionPath2 != null) {
				directionPath2.Remove ();
			}

			JsonValue directions = await DirectionFetcher.GetDirections(startingPoint,endingPoint);
			JsonValue routesResults = directions ["routes"];

			//GET INSTRUCTIONS
			string instructions = DirectionFetcher.GetInstructions (routesResults);

			TextView instructionsView = FindViewById<TextView>(Resource.Id.SlideUpText);
			//instructionsView.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
			string formattedInstructions = DisplayStepDirections (instructions);

			instructionsView.Text = formattedInstructions;


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
			TextView slideUp = FindViewById<TextView> (Resource.Id.SlideUpText);
			slideUp.Visibility = ViewStates.Visible;
			RelativeLayout clearLayout = FindViewById<RelativeLayout> (Resource.Id.clearLayout);
			clearLayout.SetPadding (0, 0, 0, 200);
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
					startingDestination.SetPosition (new LatLng (building.XCoordinate, building.YCoordinate));
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
					startingDestination.SetPosition (new LatLng (building.XCoordinate, building.YCoordinate));
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
					endDestination.SetPosition (new LatLng (building.XCoordinate, building.YCoordinate));
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
							DrawDirections (new LatLng (map.MyLocation.Latitude, map.MyLocation.Longitude), endPoint.Position);
							endB = building;
						}
					clearButton.Visibility = ViewStates.Visible;
					map.UiSettings.ZoomControlsEnabled = false;
				}
				else {
					Toast.MakeText (this, "You already have an ending destination, " + building.Name + " will be your new ending destination", ToastLength.Short).Show ();
					endPoint.Remove ();
					if (busPosition != null) {
						busPosition.Remove ();
						busPosition = null;
					}
					if (busPosition2 != null) {
						busPosition2.Remove ();
						busPosition2 = null;
					}
					MarkerOptions endDestination = new MarkerOptions ();
					endDestination.SetPosition (new LatLng (building.XCoordinate, building.YCoordinate));
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
							DrawDirections (new LatLng (map.MyLocation.Latitude, map.MyLocation.Longitude), endPoint.Position);
							endB = building;
						}
					clearButton.Visibility = ViewStates.Visible;
					map.UiSettings.ZoomControlsEnabled = false;
				}
				window.Dismiss ();
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
			Building b = BuildingManager.isInPolygon(e.Point);
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
				building.Polygon = map.AddPolygon (SGWPolygon);
			}
		}

		public void DrawLoyolaPolygons(GoogleMap map){

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
				building.Polygon = map.AddPolygon (LoyolaPolygon);
			}
		}


		public void DrawSGWMarkers(GoogleMap map){
			List<Building> b = BuildingManager.getSGWBuildings();
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
			List<Building> b = BuildingManager.getLoyolaBuildings();

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

