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
using System.Collections;
using Android.Content;

namespace GoogleApiTest
{
	[Activity (Label = "MapActivity")]			
	public class MapActivity : Activity
	{
		BuildingManager BuildingManager = new BuildingManager ();
		ListView listview;
		DrawerLayout drawer;
		List<string> drawerSettings;
		ActionBarDrawerToggle mDrawerToggle;
		GoogleMap map;

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
					createSpinnerBuilding (map, BuildingManager.getLoyolaBuildings());
				}else{
					zoomSgw (map);
					createSpinnerBuilding (map, BuildingManager.getSGWBuildings());
				}
			};

			drawSGWPolygons (map);
			drawLoyolaPolygons (map);
			createSettingsDrawer ();
			createSpinnerBuilding (map, BuildingManager.getSGWBuildings ());

			map.MapClick += HandleMapClick;
			//MarkerOptions markerOpt1 = new MarkerOptions();
			//markerOpt1.SetPosition(new LatLng(45.49770868047681,-73.57903227210045));
			//markerOpt1.SetTitle("Hall Building");
			//markerOpt1.InvokeIcon (BitmapDescriptorFactory.FromResource(Resource.Drawable.h));
			//map.AddMarker(markerOpt1);

			Button exploreButton = FindViewById<Button> (Resource.Id.explore);
			exploreButton.Click += (sender, e) => {
				var exploreActivity = new Intent (this, typeof(ExploreActivity));
				StartActivity (exploreActivity);
			};
		}

		public void CreateBuildingDescription(String Description){
			LayoutInflater inflater = (LayoutInflater)this.GetSystemService (Context.LayoutInflaterService);
			View popUp = inflater.Inflate (Resource.Layout.BuildingDescription, null);
			TextView textView = popUp.FindViewById<TextView>(Resource.Id.description);
			textView.Text = Description;
			PopupWindow window = new PopupWindow (popUp, WindowManagerLayoutParams.WrapContent, WindowManagerLayoutParams.WrapContent);

			Button btnDismiss = popUp.FindViewById<Button> (Resource.Id.btnPopUpOk);
			btnDismiss.Click += (sender, e) => {
				window.Dismiss();
			};

			window.ShowAtLocation (popUp, GravityFlags.Center, 0,0);
		}


		public void zoomSgw(GoogleMap map){
			LatLng location = new LatLng(45.49564057468219, -73.57727140188217);
			CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
			builder.Target(location);
			builder.Zoom(16);
			CameraPosition cameraPosition = builder.Build();
			map.MoveCamera (CameraUpdateFactory.NewCameraPosition (cameraPosition));
		}

		void HandleMapClick (object sender, GoogleMap.MapClickEventArgs e)
		{
			Building b = BuildingManager.isInPolygon(e.Point);
			if (b != null) {
				CreateBuildingDescription (b.Name);
			}
		}
			
		public void zoomLoyola(GoogleMap map){
			LatLng location = new LatLng(45.458593581866786, -73.64008069038391);
			CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
			builder.Target(location);
			builder.Zoom(16);
			CameraPosition cameraPosition = builder.Build();
			map.MoveCamera (CameraUpdateFactory.NewCameraPosition (cameraPosition));
		}
			
		public void createSpinnerBuilding(GoogleMap map, List<Building> buildings){
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
		}

		public void zoomSpecificBuilding(GoogleMap map, Building buildingToZoom){
			LatLng location = new LatLng(buildingToZoom.XCoordinate, buildingToZoom.YCoordinate);
			CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
			builder.Target(location);
			builder.Zoom(18);
			CameraPosition cameraPosition = builder.Build();
			map.MoveCamera (CameraUpdateFactory.NewCameraPosition (cameraPosition));
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
				int Color = Int32.Parse("ff800020", System.Globalization.NumberStyles.HexNumber);
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
				int Color = Int32.Parse("ff800020", System.Globalization.NumberStyles.HexNumber);
				LoyolaPolygon.InvokeFillColor(Color);
				LoyolaPolygon.InvokeStrokeWidth (4);
				map.AddPolygon (LoyolaPolygon);
			}
		}
	}
}

