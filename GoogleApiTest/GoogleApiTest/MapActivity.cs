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
		ListView listview;
		DrawerLayout drawer;
		List<string> drawerSettings;
		ActionBarDrawerToggle mDrawerToggle;
		GoogleMap map;
		PopupWindow window=null;
		Marker startPoint;
		Marker endPoint;
		Building startB;
		Building endB;
		Polyline directionPath;
		Polyline directionPath2;

		static string[] locations={"EV","HALL","FG","JMSB"};
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle, Resource.Layout.Main);

			// Create your application here

			AutoCompleteTextView act = FindViewById<AutoCompleteTextView>(Resource.Id.AutoCompleteInput);
			act.Adapter = new ArrayAdapter<string> (this, Resource.Layout.list_locations, locations);

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
				map.UiSettings.ZoomControlsEnabled = true;
				clearButton.Visibility = ViewStates.Invisible;
				TextView slideUp = FindViewById<TextView> (Resource.Id.SlideUpText);
				slideUp.Visibility = ViewStates.Gone;
				RelativeLayout clearLayout = FindViewById<RelativeLayout> (Resource.Id.clearLayout);
				clearLayout.SetPadding (0, 0, 0, 0);
			};
			/*
			Button exploreButton = FindViewById<Button> (Resource.Id.explore);
			exploreButton.Click += (sender, e) => {
				var exploreActivity = new Intent (this, typeof(ExploreActivity));
				StartActivity (exploreActivity);
			};*/
		} 


		public async void drawDirectionsDifferentCampus(LatLng startingPoint, LatLng endingPoint){
			if (directionPath != null) {
				directionPath.Remove ();
			}
			if (directionPath2 != null) {
				directionPath2.Remove ();
			}
			JsonValue firstDirections = await DirectionFetcher.getDirections(startingPoint,startB.Campus.ExtractionPoint);
			JsonValue firstRoutesResults = firstDirections ["routes"];
			string points1 = firstRoutesResults [0] ["overview_polyline"] ["points"];
			var polyPoints1 = DirectionFetcher.DecodePolylinePoints (points1);

			JsonValue secondDirections = await DirectionFetcher.getDirections(endB.Campus.ExtractionPoint,endingPoint);
			JsonValue secondRoutesResults = secondDirections ["routes"];
			string points2 = secondRoutesResults [0] ["overview_polyline"] ["points"];
			var polyPoints2 = DirectionFetcher.DecodePolylinePoints (points2);

			//GET INSTRUCTIONS
			string firstInstructions = DirectionFetcher.GetInstructions (firstRoutesResults);
			string secondInstructions = DirectionFetcher.GetInstructions (secondRoutesResults);

			TextView instructionsView = FindViewById<TextView>(Resource.Id.SlideUpText);
			instructionsView.Text = DisplayStepDirections(firstInstructions + secondInstructions);
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



			TextView slideUp = FindViewById<TextView> (Resource.Id.SlideUpText);
			slideUp.Visibility = ViewStates.Visible;
			RelativeLayout clearLayout = FindViewById<RelativeLayout> (Resource.Id.clearLayout);
			clearLayout.SetPadding (0, 0, 0, 200);

		}

		public async void drawDirections(LatLng startingPoint, LatLng endingPoint){
			if (directionPath != null) {
				directionPath.Remove ();
			}
			if (directionPath2 != null) {
				directionPath2.Remove ();
			}

			JsonValue directions = await DirectionFetcher.getDirections(startingPoint,endingPoint);
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

			CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
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

			//Set Start Point Button
			Button fromHere = popUp.FindViewById<Button> (Resource.Id.btnFromHere);
			fromHere.Click += (sender, e) => {

				Button clearButton = FindViewById<Button>(Resource.Id.clearMarker);

				if(startPoint==null && directionPath ==null){
					MarkerOptions startingDestination = new MarkerOptions ();
					startingDestination.SetPosition (new LatLng (building.XCoordinate, building.YCoordinate));
					startingDestination.InvokeIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.StartPointPNG));
					startPoint = map.AddMarker (startingDestination);
					clearButton.Visibility=ViewStates.Visible;
					startB=building;
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
					startB=building;
					if(endPoint!=null){
						//BUILDINGS NOT ON SAME CAMPUS
						if (startB.Campus != endB.Campus) {
							drawDirectionsDifferentCampus (startPoint.Position, endPoint.Position);
						}else{
							drawDirections (startPoint.Position, endPoint.Position);
						}
					}
					clearButton.Visibility=ViewStates.Visible;
					map.UiSettings.ZoomControlsEnabled = false;
				}
				window.Dismiss();
			};

			//Set End Point Button
			Button toHere = popUp.FindViewById<Button> (Resource.Id.btnToHere);
			toHere.Click += (sender, e) => {
				Button clearButton = FindViewById<Button>(Resource.Id.clearMarker);
				if (endPoint == null) {
					MarkerOptions endDestination = new MarkerOptions ();
					endDestination.SetPosition (new LatLng (building.XCoordinate, building.YCoordinate));
					endDestination.InvokeIcon (BitmapDescriptorFactory.FromResource (Resource.Drawable.EndPointPNG));
					endPoint = map.AddMarker (endDestination);
					if(startPoint !=null && endPoint !=null){
						endB=building;
						//BUILDINGS NOT ON SAME CAMPUS
						if (startB.Campus != endB.Campus) {
							drawDirectionsDifferentCampus (startPoint.Position, endPoint.Position);
						}else{
							drawDirections (startPoint.Position, endPoint.Position);
						}
					}
					else if(startPoint == null && endPoint !=null && map.MyLocation !=null){
						drawDirections (new LatLng(map.MyLocation.Latitude,map.MyLocation.Longitude), endPoint.Position);
						endB=building;
					}
					clearButton.Visibility=ViewStates.Visible;
					map.UiSettings.ZoomControlsEnabled = false;
				} else {
					Toast.MakeText (this, "You already have an ending destination, " + building.Name + " will be your new ending destination", ToastLength.Short).Show ();
					endPoint.Remove();
					MarkerOptions endDestination = new MarkerOptions ();
					endDestination.SetPosition (new LatLng (building.XCoordinate, building.YCoordinate));
					endDestination.InvokeIcon (BitmapDescriptorFactory.FromResource (Resource.Drawable.EndPointPNG));
					endPoint = map.AddMarker (endDestination);
					if(startPoint !=null && endPoint !=null){
						endB=building;
						//BUILDINGS NOT ON SAME CAMPUS
						if (startB.Campus != endB.Campus) {
							drawDirectionsDifferentCampus (startPoint.Position, endPoint.Position);
						}else{
							drawDirections (startPoint.Position, endPoint.Position);
						}
					}
					else if(startPoint == null && endPoint !=null && map.MyLocation !=null){
						drawDirections (new LatLng(map.MyLocation.Latitude,map.MyLocation.Longitude), endPoint.Position);
						endB=building;
					}
					clearButton.Visibility=ViewStates.Visible;
					map.UiSettings.ZoomControlsEnabled = false;
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

