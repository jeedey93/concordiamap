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

			ToggleButton togglebutton = FindViewById<ToggleButton>(Resource.Id.togglebutton);

			togglebutton.Click += (o, e) => {
				// Perform action on clicks
				if (togglebutton.Checked){
					zoomLoyola (map);
					createSpinnerBuilding (map, LoyolaBuildings());
				}else{
					zoomSgw (map);
					createSpinnerBuilding (map, SGWBuildings());
				}
			};
				
			drawSGWPolygons (map);
			drawLoyolaPolygons (map);
			createSettingsDrawer ();
			createSpinnerBuilding (map, SGWBuildings());

			map.MapClick += HandleMapClick;
		}

		public void CreateBuildingDescription(){
			LayoutInflater inflater = (LayoutInflater)this.GetSystemService (Context.LayoutInflaterService);
			View popUp = inflater.Inflate (Resource.Layout.BuildingDescription, null);

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
			if (isInPolygon (e.Point)) {
				//var window = new PopupWindow (this, 400, 500, true);

				//MarkerOptions marker = new MarkerOptions ();
				//marker.SetPosition (e.Point);
				//marker.SetTitle ("You clicked on the Hall Building");
				//marker.InvokeIcon (BitmapDescriptorFactory.FromResource(Resource.Drawable.h));
				//map.AddMarker (marker);
				CreateBuildingDescription ();
			}
		}

		public Boolean isInPolygon(LatLng point){
			double Ax = 45.49770868047681, Ay = -73.57903227210045;
			double Bx = 45.497366508216466, By = -73.57833489775658;
			double Cx = 45.4968288804749256, Cy = -73.57885658740997;
			double Dx = 45.49715787001796, Dy = -73.579544390347004;

			double AMx = Ax - point.Latitude;
			double AMy = Ay - point.Longitude;
			double ABx = Ax - Bx;
			double ABy = Ay - By;
			double ADx = Ax - Dx;
			double ADy = Ay - Dy;

			double AMAB = AMx * ABx + AMy * ABy;
			double ABAB = ABx * ABx + ABy * ABy;
			double AMAD = AMx * ADx + AMy * ADy;
			double ADAD = ADx * ADx + ADy * ADy;

			if (0 < AMAB && AMAB < ABAB) {
				if (0 < AMAD && AMAD < ADAD) {
					Console.WriteLine ("Point is in rectangle");
					return true;
				}
			}
			return false;
		}

		public void zoomLoyola(GoogleMap map){
			LatLng location = new LatLng(45.458593581866786, -73.64008069038391);
			CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
			builder.Target(location);
			builder.Zoom(16);
			CameraPosition cameraPosition = builder.Build();
			map.MoveCamera (CameraUpdateFactory.NewCameraPosition (cameraPosition));
		}
			
		public List<Building> SGWBuildings(){
			var SGWBuildings = new List<Building> ();
			SGWBuildings.Add (new Building("B Building", "B", 45.497818, -73.579545));
			SGWBuildings.Add (new Building("CB Building", "CB", 45.495189, -73.574308));
			SGWBuildings.Add (new Building("CI Building", "CI", 45.497438, -73.579957));
			SGWBuildings.Add (new Building("CL Building", "CL", 45.494195, -73.579295));
			SGWBuildings.Add (new Building("D Building", "D", 45.497736, -73.579390));
			SGWBuildings.Add (new Building("EN Building", "EN", 45.496823, -73.579661));
			SGWBuildings.Add (new Building("Computer Science, Engineering and Visual Arts Integrated Complex", "EV", 45.495572, -73.578285));
			SGWBuildings.Add (new Building("FA Building", "FA", 45.496801, -73.579528));
			SGWBuildings.Add (new Building("Faubourg Tower", "FB", 45.494673, -73.577642));
			SGWBuildings.Add (new Building("FG Building", "FG", 45.494195, -73.578328));
			SGWBuildings.Add (new Building("Guy-Metro Building", "GM", 45.495857, -73.578858));
			SGWBuildings.Add (new Building("Grey Nuns Building", "GN", 45.493522, -73.576724));
			SGWBuildings.Add (new Building("Henry F.Hall Building", "H", 45.497260, -73.578983));
			SGWBuildings.Add (new Building("K Building", "K", 45.497749, -73.579496));
			SGWBuildings.Add (new Building("McConnell Library Building", "LB", 45.496775, -73.577904));
			SGWBuildings.Add (new Building("M Building", "M", 45.497357, -73.579789));
			SGWBuildings.Add (new Building("John Molson School of Business Building", "MB", 45.495270187715924,-73.57906848192215));
			SGWBuildings.Add (new Building("Montefiore Building", "MT", 45.494408, -73.576165));
			SGWBuildings.Add (new Building("MU Building", "MU", 45.497853, -73.579622));
			SGWBuildings.Add (new Building("MI Building", "MI", 45.497700, -73.579307));
			SGWBuildings.Add (new Building("OS Building", "OS", 45.497189, -73.573149));
			SGWBuildings.Add (new Building("P Building", "P", 45.496648, -73.579202));
			SGWBuildings.Add (new Building("PR Building", "PR", 45.496921, -73.579917));
			SGWBuildings.Add (new Building("Q Building", "Q", 45.496604, -73.579129));
			SGWBuildings.Add (new Building("R Building", "R", 45.496757, -73.579445));
			SGWBuildings.Add (new Building("RR Building", "RR", 45.496702, -73.579380));
			SGWBuildings.Add (new Building("S Building", "S", 45.497400, -73.579867));
			SGWBuildings.Add (new Building("Samuel Bronfman Building", "SB", 45.496553, -73.586140));
			SGWBuildings.Add (new Building("T Building", "T", 45.496676, -73.579289));
			SGWBuildings.Add (new Building("Toronto Dominion Building", "TD", 45.494667, -73.578743));
			SGWBuildings.Add (new Building("V Building", "V", 45.497011, -73.579956));
			SGWBuildings.Add (new Building("Visual Arts Building", "VA", 45.495730, -73.573868));
			SGWBuildings.Add (new Building("X Building", "X", 45.496884, -73.579699));
			SGWBuildings.Add (new Building("Z Building", "Z", 45.496920, -73.579785));
			return SGWBuildings;
		}

		public List<Building> LoyolaBuildings(){
			var LoyolaBuildings = new List<Building> ();
			LoyolaBuildings.Add (new Building ("Administration Building", "AD", 45.458011, -73.639854));
			LoyolaBuildings.Add (new Building ("BB Building", "BB", 45.459856, -73.639320));
			LoyolaBuildings.Add (new Building ("BH Building", "BH", 45.459738, -73.639154));
			LoyolaBuildings.Add (new Building ("Central Building", "CC", 45.458225, -73.640425));
			LoyolaBuildings.Add (new Building ("Communication & Journalism Building", "CJ", 45.457452, -73.640427));
			LoyolaBuildings.Add (new Building ("Stinger Dome (seasonal)", "DO", 45.457595, -73.636173));
			LoyolaBuildings.Add (new Building ("F. C. Smith Building", "FC", 45.458487, -73.639347));
			LoyolaBuildings.Add (new Building ("Centre for Structural and Functional Genomics", "GE", 45.456910, -73.640416));
			LoyolaBuildings.Add (new Building ("Hingston Wing A", "HA", 45.459450, -73.641269));
			LoyolaBuildings.Add (new Building ("Hingston Wing B", "HB", 45.459234, -73.641903));
			LoyolaBuildings.Add (new Building ("Hingston Wing C", "HC", 45.459669, -73.642079));
			LoyolaBuildings.Add (new Building ("Jesuit Residence", "JR", 45.458408, -73.643297));
			LoyolaBuildings.Add (new Building ("PERFORM Centre", "PC", 45.457053, -73.637310));
			LoyolaBuildings.Add (new Building ("Physical Services Building", "PS", 45.459649, -73.639795));
			LoyolaBuildings.Add (new Building ("Oscar Peterson Concert Hall", "PT", 45.459324, -73.639006));
			LoyolaBuildings.Add (new Building ("Psychology Building", "PY", 45.458906, -73.640530));
			LoyolaBuildings.Add (new Building ("Recreation and Athletics Complex", "RA", 45.456703, -73.637680));
			LoyolaBuildings.Add (new Building ("Loyola Jesuit Hall and Conference Centre", "RF", 45.458479, -73.641053));
			LoyolaBuildings.Add (new Building ("Student Centre", "SC", 45.459127, -73.639204));
			LoyolaBuildings.Add (new Building ("Solar House", "SH", 45.459399, -73.642394));
			LoyolaBuildings.Add (new Building ("Saint-Ignatius of Loyola Church", "SI",45.457836, -73.642331));
			LoyolaBuildings.Add (new Building ("Richard J. Renaud Science Complex", "SP", 45.457625, -73.641703));
			LoyolaBuildings.Add (new Building ("Terrebonne Building", "TA", 45.459997, -73.640900));
			LoyolaBuildings.Add (new Building ("Vanier Extension", "VE", 45.458845, -73.638635));
			LoyolaBuildings.Add (new Building ("Vanier Library", "VL", 45.459093, -73.638367));
			return LoyolaBuildings;
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
	
			PolygonOptions hallBuilding = new PolygonOptions();
			hallBuilding.Add(new LatLng(45.49770868047681,-73.57903227210045));
			hallBuilding.Add(new LatLng(45.497366508216466,-73.57833489775658));
			hallBuilding.Add(new LatLng(45.4968288804749256,-73.57885658740997));
			hallBuilding.Add(new LatLng(45.49715787001796,-73.579544390347004));
			hallBuilding.InvokeFillColor(-65536);
			hallBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(hallBuilding);

			PolygonOptions JMSBBuilding = new PolygonOptions();
			JMSBBuilding.Add(new LatLng(45.495624, -73.57928));
			JMSBBuilding.Add(new LatLng(45.495394, -73.579538));
			JMSBBuilding.Add(new LatLng(45.495381, -73.579507));
			JMSBBuilding.Add(new LatLng(45.495312, -73.57959));
			JMSBBuilding.Add(new LatLng(45.495179, -73.579335));
			JMSBBuilding.Add(new LatLng(45.495235, -73.579267));
			JMSBBuilding.Add(new LatLng(45.495001, -73.578826));
			JMSBBuilding.Add(new LatLng(45.495064, -73.578755));
			JMSBBuilding.Add(new LatLng(45.495086, -73.578792));
			JMSBBuilding.Add(new LatLng(45.49513, -73.578747));
			JMSBBuilding.Add(new LatLng(45.495105, -73.578686));
			JMSBBuilding.Add(new LatLng(45.495259, -73.578516));
			JMSBBuilding.InvokeFillColor(-65536);
			JMSBBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(JMSBBuilding);

			PolygonOptions OSBuilding = new PolygonOptions ();
			OSBuilding.Add (new LatLng (45.497291, -73.57319));
			OSBuilding.Add (new LatLng (45.497203, -73.573013));
			OSBuilding.Add (new LatLng (45.4971, -73.573119));
			OSBuilding.Add (new LatLng (45.497186, -73.573295));
			OSBuilding.InvokeFillColor(-65536);
			OSBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(OSBuilding);


			PolygonOptions FGBuilding = new PolygonOptions();
			FGBuilding.Add(new LatLng(45.494694, -73.578039));
			FGBuilding.Add(new LatLng(45.494362, -73.578431));
			FGBuilding.Add(new LatLng(45.494369, -73.578443));
			FGBuilding.Add(new LatLng(45.494301, -73.578523));
			FGBuilding.Add(new LatLng(45.494293, -73.578511));
			FGBuilding.Add(new LatLng(45.493823, -73.579068));
			FGBuilding.Add(new LatLng(45.493626, -73.578728));
			FGBuilding.Add(new LatLng(45.493848, -73.578465));
			FGBuilding.Add(new LatLng(45.493836, -73.578441));
			FGBuilding.Add(new LatLng(45.493883, -73.578386));
			FGBuilding.Add(new LatLng(45.493891, -73.5784));
			FGBuilding.Add(new LatLng(45.493923, -73.578362));
			FGBuilding.Add(new LatLng(45.493912, -73.578343));
			FGBuilding.Add(new LatLng(45.494105, -73.578115));
			FGBuilding.Add(new LatLng(45.494111, -73.578126));
			FGBuilding.Add(new LatLng(45.494204, -73.578017));
			FGBuilding.Add(new LatLng(45.494186, -73.577986));
			FGBuilding.Add(new LatLng(45.494371, -73.577768));
			FGBuilding.Add(new LatLng(45.494393, -73.577802));
			FGBuilding.Add(new LatLng(45.494428, -73.577762));
			FGBuilding.Add(new LatLng(45.494389, -73.57769));
			FGBuilding.Add(new LatLng(45.494452, -73.577618));
			FGBuilding.InvokeFillColor(-65536);
			FGBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(FGBuilding);

			PolygonOptions FBBuilding = new PolygonOptions();
			FBBuilding.Add(new LatLng(45.494696, -73.578038));
			FBBuilding.Add(new LatLng(45.494912, -73.577786));
			FBBuilding.Add(new LatLng(45.494872, -73.577713));
			FBBuilding.Add(new LatLng(45.494876, -73.577704));
			FBBuilding.Add(new LatLng(45.494837, -73.577634));
			FBBuilding.Add(new LatLng(45.494842, -73.577625));
			FBBuilding.Add(new LatLng(45.494799, -73.57755));
			FBBuilding.Add(new LatLng(45.494806, -73.57754));
			FBBuilding.Add(new LatLng(45.494763, -73.577465));
			FBBuilding.Add(new LatLng(45.494775, -73.577452));
			FBBuilding.Add(new LatLng(45.494692, -73.577309));
			FBBuilding.Add(new LatLng(45.4947, -73.5773));
			FBBuilding.Add(new LatLng(45.494654, -73.57722));
			FBBuilding.Add(new LatLng(45.494397, -73.577521));
			FBBuilding.InvokeFillColor(-65536);
			FBBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(FBBuilding);

			PolygonOptions eVBuilding = new PolygonOptions();
			eVBuilding.Add(new LatLng(45.495826707314045, -73.57856959104538));
			eVBuilding.Add(new LatLng(45.49552588659263, -73.57787489891052));
			eVBuilding.Add(new LatLng(45.49553716739868, -73.57781052589417));
			eVBuilding.Add(new LatLng(45.49542623937432, -73.57765763998032));
			eVBuilding.Add(new LatLng(45.49518746136016, -73.57789367437363));
			eVBuilding.Add(new LatLng(45.49526266714314, -73.57806265354156));
			eVBuilding.Add(new LatLng(45.49527582814485, -73.57813775539398));
			eVBuilding.Add(new LatLng(45.495369835210354, -73.5784462094307));
			eVBuilding.Add(new LatLng(45.49556160913736, -73.57885658740997));
			eVBuilding.InvokeFillColor(-65536);
			eVBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(eVBuilding);

			PolygonOptions gMBuilding = new PolygonOptions ();
			gMBuilding.Add(new LatLng(45.49611,-73.57888));
			gMBuilding.Add(new LatLng(45.49595,-73.57852));
			gMBuilding.Add(new LatLng(45.49562,-73.57884));
			gMBuilding.Add(new LatLng(45.49579,-73.57919));
			gMBuilding.InvokeFillColor(-65536);
			gMBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(gMBuilding);

			PolygonOptions libraryBuilding = new PolygonOptions ();
			libraryBuilding.Add(new LatLng(45.496721, -73.578588));
			libraryBuilding.Add(new LatLng(45.4967, -73.578549));
			libraryBuilding.Add(new LatLng(45.496673, -73.578574));
			libraryBuilding.Add(new LatLng(45.496555, -73.578326));
			libraryBuilding.Add(new LatLng(45.49658, -73.578301));
			libraryBuilding.Add(new LatLng(45.496493, -73.578117));
			libraryBuilding.Add(new LatLng(45.496465, -73.57814));
			libraryBuilding.Add(new LatLng(45.496279, -73.577752));
			libraryBuilding.Add(new LatLng(45.496271, -73.577759));
			libraryBuilding.Add(new LatLng(45.496246, -73.577708));
			libraryBuilding.Add(new LatLng(45.496487, -73.577473));
			libraryBuilding.Add(new LatLng(45.4966, -73.577711));
			libraryBuilding.Add(new LatLng(45.496661, -73.57765));
			libraryBuilding.Add(new LatLng(45.496622, -73.577568));
			libraryBuilding.Add(new LatLng(45.496694, -73.577495));
			libraryBuilding.Add(new LatLng(45.496692, -73.577487));
			libraryBuilding.Add(new LatLng(45.496883, -73.577302));
			libraryBuilding.Add(new LatLng(45.497, -73.577545));
			libraryBuilding.Add(new LatLng(45.496977, -73.577568));
			libraryBuilding.Add(new LatLng(45.496999, -73.577617));
			libraryBuilding.Add(new LatLng(45.496991, -73.577627));
			libraryBuilding.Add(new LatLng(45.496999, -73.577644));
			libraryBuilding.Add(new LatLng(45.497039, -73.577602));
			libraryBuilding.Add(new LatLng(45.497112, -73.577757));
			libraryBuilding.Add(new LatLng(45.497074, -73.577797));
			libraryBuilding.Add(new LatLng(45.497082, -73.577817));
			libraryBuilding.Add(new LatLng(45.497089, -73.577811));
			libraryBuilding.Add(new LatLng(45.497114, -73.577859));
			libraryBuilding.Add(new LatLng(45.497139, -73.577833));
			libraryBuilding.Add(new LatLng(45.497254, -73.578069));
			libraryBuilding.Add(new LatLng(45.49702, -73.578295));
			libraryBuilding.Add(new LatLng(45.497002, -73.578258));
			libraryBuilding.Add(new LatLng(45.496964, -73.578294));
			libraryBuilding.Add(new LatLng(45.496941, -73.578248));
			libraryBuilding.Add(new LatLng(45.496889, -73.578296));
			libraryBuilding.Add(new LatLng(45.496911, -73.578343));
			libraryBuilding.Add(new LatLng(45.496873, -73.578378));
			libraryBuilding.Add(new LatLng(45.49689, -73.578414));
			libraryBuilding.InvokeFillColor(-65536);
			libraryBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(libraryBuilding);

			PolygonOptions PBuilding = new PolygonOptions();
			PBuilding.Add(new LatLng(45.496719, -73.579193));
			PBuilding.Add(new LatLng(45.496682, -73.579117));
			PBuilding.Add(new LatLng(45.496583, -73.579217));
			PBuilding.Add(new LatLng(45.496619, -73.57929));
			PBuilding.InvokeFillColor(-65536);
			PBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(PBuilding);

			PolygonOptions TBuilding = new PolygonOptions();
			TBuilding.Add(new LatLng(45.496731, -73.579283));
			TBuilding.Add(new LatLng(45.496698, -73.579218));
			TBuilding.Add(new LatLng(45.496625, -73.579292));
			TBuilding.Add(new LatLng(45.496656, -73.579359));
			TBuilding.InvokeFillColor(-65536);
			TBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(TBuilding);

			PolygonOptions RRBuilding = new PolygonOptions();
			RRBuilding.Add(new LatLng(45.496783, -73.579358));
			RRBuilding.Add(new LatLng(45.496743, -73.579274));
			RRBuilding.Add(new LatLng(45.496612, -73.579405));
			RRBuilding.Add(new LatLng(45.496653, -73.579488));
			RRBuilding.InvokeFillColor(-65536);
			RRBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(RRBuilding);

			PolygonOptions RBuilding = new PolygonOptions();
			RBuilding.Add(new LatLng(45.496826, -73.579442));
			RBuilding.Add(new LatLng(45.496787, -73.57936));
			RBuilding.Add(new LatLng(45.4967, -73.579447));
			RBuilding.Add(new LatLng(45.496739, -73.579529));
			RBuilding.InvokeFillColor(-65536);
			RBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(RBuilding);

			PolygonOptions FABuilding = new PolygonOptions();
			FABuilding.Add(new LatLng(45.496865, -73.57952));
			FABuilding.Add(new LatLng(45.496828, -73.579447));
			FABuilding.Add(new LatLng(45.496745, -73.579529));
			FABuilding.Add(new LatLng(45.496783, -73.579605));
			FABuilding.InvokeFillColor(-65536);
			FABuilding.InvokeStrokeWidth (4);
			map.AddPolygon(FABuilding);

			PolygonOptions ENBuilding = new PolygonOptions();
			ENBuilding.Add(new LatLng(45.496937, -73.579579));
			ENBuilding.Add(new LatLng(45.496918, -73.579537));
			ENBuilding.Add(new LatLng(45.496909, -73.579547));
			ENBuilding.Add(new LatLng(45.496892, -73.579516));
			ENBuilding.Add(new LatLng(45.496795, -73.579614));
			ENBuilding.Add(new LatLng(45.496804, -73.579632));
			ENBuilding.Add(new LatLng(45.496674, -73.579766));
			ENBuilding.Add(new LatLng(45.4967, -73.579819));
			ENBuilding.InvokeFillColor(-65536);
			ENBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(ENBuilding);

			PolygonOptions XBuilding = new PolygonOptions();
			XBuilding.Add(new LatLng(45.496906, -73.579615));
			XBuilding.Add(new LatLng(45.496823, -73.579701));
			XBuilding.Add(new LatLng(45.496859, -73.579776));
			XBuilding.Add(new LatLng(45.496944, -73.579691));
			XBuilding.InvokeFillColor(-65536);
			XBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(XBuilding);

			PolygonOptions ZBuilding = new PolygonOptions();
			ZBuilding.Add(new LatLng(45.49699, -73.579776));
			ZBuilding.Add(new LatLng(45.496948, -73.579693));
			ZBuilding.Add(new LatLng(45.496859, -73.579781));
			ZBuilding.Add(new LatLng(45.496901, -73.579866));
			ZBuilding.InvokeFillColor(-65536);
			ZBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(ZBuilding);

			PolygonOptions PRBuilding = new PolygonOptions();
			PRBuilding.Add(new LatLng(45.49703, -73.579868));
			PRBuilding.Add(new LatLng(45.496989, -73.579783));
			PRBuilding.Add(new LatLng(45.496796, -73.579975));
			PRBuilding.Add(new LatLng(45.496839, -73.580062));
			PRBuilding.InvokeFillColor(-65536);
			PRBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(PRBuilding);

			PolygonOptions VBuilding = new PolygonOptions();
			VBuilding.Add(new LatLng(45.49709, -73.579941));
			VBuilding.Add(new LatLng(45.497049, -73.57986));
			VBuilding.Add(new LatLng(45.497034, -73.579874));
			VBuilding.Add(new LatLng(45.497032, -73.579871));
			VBuilding.Add(new LatLng(45.496943, -73.57996));
			VBuilding.Add(new LatLng(45.496984, -73.580046));
			VBuilding.InvokeFillColor(-65536);
			VBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(VBuilding);

			PolygonOptions QBuilding = new PolygonOptions();
			QBuilding.Add(new LatLng(45.496683, -73.579112));
			QBuilding.Add(new LatLng(45.496653, -73.579054));
			QBuilding.Add(new LatLng(45.496549, -73.579156));
			QBuilding.Add(new LatLng(45.496578, -73.579214));
			QBuilding.InvokeFillColor(-65536);
			QBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(QBuilding);

			PolygonOptions GNBuilding = new PolygonOptions();
			GNBuilding.Add(new LatLng(45.49439, -73.577132));
			GNBuilding.Add(new LatLng(45.494019, -73.576357));
			GNBuilding.Add(new LatLng(45.494122, -73.576256));
			GNBuilding.Add(new LatLng(45.494035, -73.576074));
			GNBuilding.Add(new LatLng(45.493933, -73.576172));
			GNBuilding.Add(new LatLng(45.493716, -73.57572));
			GNBuilding.Add(new LatLng(45.493602, -73.575832));
			GNBuilding.Add(new LatLng(45.49382, -73.576294));
			GNBuilding.Add(new LatLng(45.493492, -73.576616));
			GNBuilding.Add(new LatLng(45.493472, -73.576574));
			GNBuilding.Add(new LatLng(45.493344, -73.576704));
			GNBuilding.Add(new LatLng(45.493367, -73.576755));
			GNBuilding.Add(new LatLng(45.493033, -73.577081));
			GNBuilding.Add(new LatLng(45.492934, -73.576878));
			GNBuilding.Add(new LatLng(45.492946, -73.576846));
			GNBuilding.Add(new LatLng(45.492925, -73.576798));
			GNBuilding.Add(new LatLng(45.492899, -73.576787));
			GNBuilding.Add(new LatLng(45.492761, -73.576495));
			GNBuilding.Add(new LatLng(45.492625, -73.576629));
			GNBuilding.Add(new LatLng(45.492762, -73.576928));
			GNBuilding.Add(new LatLng(45.492686, -73.577007));
			GNBuilding.Add(new LatLng(45.492718, -73.577072));
			GNBuilding.Add(new LatLng(45.49277, -73.577021));
			GNBuilding.Add(new LatLng(45.492821, -73.577017));
			GNBuilding.Add(new LatLng(45.492904, -73.577192));
			GNBuilding.Add(new LatLng(45.492855, -73.57724));
			GNBuilding.Add(new LatLng(45.492935, -73.577408));
			GNBuilding.Add(new LatLng(45.492999, -73.577349));
			GNBuilding.Add(new LatLng(45.493105, -73.577569));
			GNBuilding.Add(new LatLng(45.493203, -73.577474));
			GNBuilding.Add(new LatLng(45.493104, -73.577263));
			GNBuilding.Add(new LatLng(45.493188, -73.57718));
			GNBuilding.Add(new LatLng(45.493198, -73.5772));
			GNBuilding.Add(new LatLng(45.493363, -73.577036));
			GNBuilding.Add(new LatLng(45.493354, -73.577015));
			GNBuilding.Add(new LatLng(45.493439, -73.57693));
			GNBuilding.Add(new LatLng(45.493479, -73.577013));
			GNBuilding.Add(new LatLng(45.493453, -73.577039));
			GNBuilding.Add(new LatLng(45.493531, -73.577204));
			GNBuilding.Add(new LatLng(45.493579, -73.577158));
			GNBuilding.Add(new LatLng(45.493603, -73.577209));
			GNBuilding.Add(new LatLng(45.493583, -73.57723));
			GNBuilding.Add(new LatLng(45.493616, -73.577298));
			GNBuilding.Add(new LatLng(45.493727, -73.577192));
			GNBuilding.Add(new LatLng(45.493674, -73.577076));
			GNBuilding.Add(new LatLng(45.493728, -73.577022));
			GNBuilding.Add(new LatLng(45.493669, -73.5769));
			GNBuilding.Add(new LatLng(45.493616, -73.576952));
			GNBuilding.Add(new LatLng(45.493554, -73.576824));
			GNBuilding.Add(new LatLng(45.4939, -73.576485));
			GNBuilding.Add(new LatLng(45.494194, -73.5771));
			GNBuilding.Add(new LatLng(45.494043, -73.577248));
			GNBuilding.Add(new LatLng(45.494118, -73.577399));
			GNBuilding.InvokeFillColor(-65536);
			GNBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(GNBuilding);

			PolygonOptions MTBuilding = new PolygonOptions ();
			MTBuilding.Add (new LatLng (45.494553, -73.5762));
			MTBuilding.Add (new LatLng (45.494463, -73.575983));
			MTBuilding.Add (new LatLng (45.494305, -73.576125));
			MTBuilding.Add (new LatLng (45.494397, -73.576333));
			MTBuilding.InvokeFillColor(-65536);
			MTBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(MTBuilding);

			PolygonOptions DBuilding= new PolygonOptions ();
			DBuilding.Add (new LatLng (45.497846, -73.579335));
			DBuilding.Add (new LatLng (45.497815, -73.57927));
			DBuilding.Add (new LatLng (45.497649, -73.579432));
			DBuilding.Add (new LatLng (45.49768, -73.579496));
			DBuilding.InvokeFillColor(-65536);
			DBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(DBuilding);

			PolygonOptions TDBuilding = new PolygonOptions ();
			TDBuilding.Add (new LatLng (45.494838, -73.578827));
			TDBuilding.Add (new LatLng (45.494654, -73.578513));
			TDBuilding.Add (new LatLng (45.494533, -73.578653));
			TDBuilding.Add (new LatLng (45.494724, -73.578974));
			TDBuilding.InvokeFillColor(-65536);
			TDBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(TDBuilding);

			PolygonOptions BBuilding= new PolygonOptions ();
			BBuilding.Add (new LatLng (45.497885, -73.579421));
			BBuilding.Add (new LatLng (45.497847, -73.579337));
			BBuilding.Add (new LatLng (45.497592, -73.579583));
			BBuilding.Add (new LatLng (45.497631, -73.579665));
			BBuilding.InvokeFillColor(-65536);
			BBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(BBuilding);

			PolygonOptions CLBuilding = new PolygonOptions ();
			CLBuilding.Add (new LatLng (45.494478, -73.579272));
			CLBuilding.Add (new LatLng (45.494268, -73.578926));
			CLBuilding.Add (new LatLng (45.494017, -73.579237));
			CLBuilding.Add (new LatLng (45.493994, -73.579286));
			CLBuilding.Add (new LatLng (45.493985, -73.579319));
			CLBuilding.Add (new LatLng (45.49399, -73.579345));
			CLBuilding.Add (new LatLng (45.49417, -73.579649));
			CLBuilding.InvokeFillColor(-65536);
			CLBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(CLBuilding);

			PolygonOptions KBuilding= new PolygonOptions ();
			KBuilding.Add (new LatLng (45.497923, -73.579494));
			KBuilding.Add (new LatLng (45.497887, -73.579419));
			KBuilding.Add (new LatLng (45.497715, -73.579588));
			KBuilding.Add (new LatLng (45.49775, -73.579662));
			KBuilding.InvokeFillColor(-65536);
			KBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(KBuilding);

			PolygonOptions MUbuilding= new PolygonOptions ();
			MUbuilding.Add (new LatLng (45.497963, -73.579571));
			MUbuilding.Add (new LatLng (45.497929, -73.579496));
			MUbuilding.Add (new LatLng (45.497753, -73.579666));
			MUbuilding.Add (new LatLng (45.497789, -73.579739));
			MUbuilding.InvokeFillColor(-65536);
			MUbuilding.InvokeStrokeWidth (4);
			map.AddPolygon(MUbuilding);

			PolygonOptions CBBuilding = new PolygonOptions ();
			CBBuilding.Add (new LatLng (45.495462, -73.574258));
			CBBuilding.Add (new LatLng (45.495377, -73.57408));
			CBBuilding.Add (new LatLng (45.495383, -73.574074));
			CBBuilding.Add (new LatLng (45.495365, -73.574032));
			CBBuilding.Add (new LatLng (45.495359, -73.574038));
			CBBuilding.Add (new LatLng (45.495304, -73.573932));
			CBBuilding.Add (new LatLng (45.495226, -73.574008));
			CBBuilding.Add (new LatLng (45.495255, -73.574073));
			CBBuilding.Add (new LatLng (45.495197, -73.574129));
			CBBuilding.Add (new LatLng (45.495167, -73.574066));
			CBBuilding.Add (new LatLng (45.495092, -73.574139));
			CBBuilding.Add (new LatLng (45.49512, -73.574204));
			CBBuilding.Add (new LatLng (45.495062, -73.574265));
			CBBuilding.Add (new LatLng (45.495031, -73.574194));
			CBBuilding.Add (new LatLng (45.494957, -73.574265));
			CBBuilding.Add (new LatLng (45.495078, -73.574535));
			CBBuilding.Add (new LatLng (45.49505, -73.574568));
			CBBuilding.Add (new LatLng (45.495081, -73.574638));
			CBBuilding.InvokeFillColor(-65536);
			CBBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(CBBuilding);

			PolygonOptions Mbuilding= new PolygonOptions ();
			Mbuilding.Add (new LatLng (45.497433, -73.579765));
			Mbuilding.Add (new LatLng (45.497396, -73.579688));
			Mbuilding.Add (new LatLng (45.497287, -73.579797));
			Mbuilding.Add (new LatLng (45.497325, -73.579871));
			Mbuilding.InvokeFillColor(-65536);
			Mbuilding.InvokeStrokeWidth (4);
			map.AddPolygon(Mbuilding);

			PolygonOptions VABuilding = new PolygonOptions ();
			VABuilding.Add (new LatLng (45.495434, -73.57375));
			VABuilding.Add (new LatLng (45.495702, -73.574293));
			VABuilding.Add (new LatLng (45.496209, -73.573787));
			VABuilding.Add (new LatLng (45.49609, -73.573543));
			VABuilding.Add (new LatLng (45.495846, -73.573785));
			VABuilding.Add (new LatLng (45.495698, -73.573482));
			VABuilding.InvokeFillColor(-65536);
			VABuilding.InvokeStrokeWidth (4);
			map.AddPolygon(VABuilding);

			PolygonOptions Sbuilding= new PolygonOptions ();
			Sbuilding.Add (new LatLng (45.497488, -73.579834));
			Sbuilding.Add (new LatLng (45.497458, -73.579771));
			Sbuilding.Add (new LatLng (45.497435, -73.579792));
			Sbuilding.Add (new LatLng (45.497425, -73.579775));
			Sbuilding.Add (new LatLng (45.497328, -73.579874));
			Sbuilding.Add (new LatLng (45.497365, -73.579953));
			Sbuilding.InvokeFillColor(-65536);
			Sbuilding.InvokeStrokeWidth (4);
			map.AddPolygon(Sbuilding);

			PolygonOptions MIBuilding = new PolygonOptions ();
			MIBuilding.Add (new LatLng (45.497813, -73.579266));
			MIBuilding.Add (new LatLng (45.497769, -73.579173));
			MIBuilding.Add (new LatLng (45.497601, -73.579336));
			MIBuilding.Add (new LatLng (45.497645, -73.579429));
			MIBuilding.InvokeFillColor(-65536);
			MIBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(MIBuilding);

			PolygonOptions CIBuilding = new PolygonOptions ();
			CIBuilding.Add (new LatLng (45.497522, -73.579937));
			CIBuilding.Add (new LatLng (45.497478, -73.579848));
			CIBuilding.Add (new LatLng (45.497368, -73.579958));
			CIBuilding.Add (new LatLng (45.497411, -73.580047));
			CIBuilding.InvokeFillColor(-65536);
			CIBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(CIBuilding);


			PolygonOptions SBBuilding = new PolygonOptions ();
			SBBuilding.Add (new LatLng (45.496697, -73.586202));
			SBBuilding.Add (new LatLng (45.496665, -73.586129));
			SBBuilding.Add (new LatLng (45.496672, -73.586121));
			SBBuilding.Add (new LatLng (45.496595, -73.585944));
			SBBuilding.Add (new LatLng (45.496587, -73.58595));
			SBBuilding.Add (new LatLng (45.496563, -73.585901));
			SBBuilding.Add (new LatLng (45.496523, -73.58591));
			SBBuilding.Add (new LatLng (45.496522, -73.585894));
			SBBuilding.Add (new LatLng (45.496507, -73.585852));
			SBBuilding.Add (new LatLng (45.496481, -73.585854));
			SBBuilding.Add (new LatLng (45.496448, -73.586316));
			SBBuilding.Add (new LatLng (45.496512, -73.586324));
			SBBuilding.Add (new LatLng (45.496517, -73.586248));
			SBBuilding.Add (new LatLng (45.496534, -73.586248));
			SBBuilding.Add (new LatLng (45.496532, -73.586282));
			SBBuilding.Add (new LatLng (45.496551, -73.586323));
			SBBuilding.InvokeFillColor(-65536);
			SBBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(SBBuilding);
		}

		public void drawLoyolaPolygons(GoogleMap map){

			PolygonOptions athleticBuilding = new PolygonOptions ();
			athleticBuilding.Add (new LatLng (45.45695817971015, -73.63793425261974));
			athleticBuilding.Add (new LatLng (45.4570235590787, -73.63787725567818));
			athleticBuilding.Add (new LatLng (45.45672206078385, -73.63710813224316));
			athleticBuilding.Add (new LatLng (45.456386694952066, -73.63737635314465));
			athleticBuilding.Add (new LatLng (45.45668913575542, -73.63814748823643));
			athleticBuilding.Add (new LatLng (45.45679026256728, -73.63806769251823));
			athleticBuilding.Add (new LatLng (45.456839650014146, -73.63819509744644));
			athleticBuilding.Add (new LatLng (45.45677097793344, -73.63825142383575));
			athleticBuilding.Add (new LatLng (45.45679825863304, -73.6383231729269));
			athleticBuilding.Add (new LatLng (45.45676298186381, -73.6383506655693));
			athleticBuilding.Add (new LatLng (45.45679825863304, -73.63844521343708));
			athleticBuilding.Add (new LatLng (45.45682836145869, -73.6384217441082));
			athleticBuilding.Add (new LatLng (45.456884804213445, -73.63856390118599));
			athleticBuilding.Add (new LatLng (45.456884804213445, -73.63856390118599));
			athleticBuilding.Add (new LatLng (45.45701368162502, -73.63845996558666)); 
			athleticBuilding.Add (new LatLng (45.45702732191775, -73.6384928226471));
			athleticBuilding.Add (new LatLng (45.45705131001079, -73.63847203552723));
			athleticBuilding.Add (new LatLng (45.457038140078616, -73.63843850791454));
			athleticBuilding.Add (new LatLng (45.45715996183382, -73.63834127783775));
			athleticBuilding.Add (new LatLng (45.457038140078616, -73.63803550601006));
			athleticBuilding.Add (new LatLng (45.45700427452465, -73.6380623281002));
			athleticBuilding.InvokeFillColor(-65536);
			athleticBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(athleticBuilding);

			PolygonOptions jesuitBuilding = new PolygonOptions ();
			jesuitBuilding.Add (new LatLng (45.45850939062161, -73.64330872893333));
			jesuitBuilding.Add (new LatLng (45.45852726363388, -73.64329196512699));
			jesuitBuilding.Add (new LatLng (45.458487754862304, -73.6431933939457));
			jesuitBuilding.Add (new LatLng (45.45847411492285, -73.6432034522295));
			jesuitBuilding.Add (new LatLng (45.45844307228968, -73.64312835037708));
			jesuitBuilding.Add (new LatLng (45.458388982812295, -73.64317260682583));
			jesuitBuilding.Add (new LatLng (45.45839415659065, -73.64318534731865));
			jesuitBuilding.Add (new LatLng (45.45838286834637, -73.6431947350502));
			jesuitBuilding.Add (new LatLng (45.458371580099836, -73.64316992461681));
			jesuitBuilding.Add (new LatLng (45.4583019691963, -73.64322759211063));
			jesuitBuilding.Add (new LatLng (45.45833066018721, -73.64330068230629));
			jesuitBuilding.Add (new LatLng (45.4583137278009, -73.64331610500813));
			jesuitBuilding.Add (new LatLng (45.45835370703822, -73.64341400563717));
			jesuitBuilding.Add (new LatLng (45.458368758037864, -73.64340126514435));
			jesuitBuilding.Add (new LatLng (45.45839838968166, -73.64347368478775));
			jesuitBuilding.Add (new LatLng (45.45846282669458, -73.64342004060745));
			jesuitBuilding.Add (new LatLng (45.458470352180356, -73.64343747496605));
			jesuitBuilding.Add (new LatLng (45.45853808150697, -73.64338114857674));
			jesuitBuilding.InvokeFillColor(-65536);
			jesuitBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(jesuitBuilding);

			PolygonOptions stIgnatiusBuilding = new PolygonOptions ();
			stIgnatiusBuilding.Add (new LatLng (45.45815334022274, -73.6425469815731));
			stIgnatiusBuilding.Add (new LatLng (45.458103483580565, -73.64241756498814));
			stIgnatiusBuilding.Add (new LatLng (45.45808608077999, -73.64243097603321));
			stIgnatiusBuilding.Add (new LatLng (45.458028698534676, -73.64227943122387));
			stIgnatiusBuilding.Add (new LatLng (45.458040927542676, -73.64224925637245));
			stIgnatiusBuilding.Add (new LatLng (45.45798307490449, -73.64210106432438));
			stIgnatiusBuilding.Add (new LatLng (45.45795626512519, -73.64209838211536));
			stIgnatiusBuilding.Add (new LatLng (45.457927573943785, -73.64202596247196));
			stIgnatiusBuilding.Add (new LatLng (45.457868780493605, -73.64206954836845));
			stIgnatiusBuilding.Add (new LatLng (45.45784573344443, -73.64200919866562));
			stIgnatiusBuilding.Add (new LatLng (45.45780998698213, -73.64203803241253));
			stIgnatiusBuilding.Add (new LatLng (45.457818453251576, -73.64206083118916));
			stIgnatiusBuilding.Add (new LatLng (45.45775448585107, -73.64211246371269));
			stIgnatiusBuilding.Add (new LatLng (45.45776624456984, -73.64214263856411));
			stIgnatiusBuilding.Add (new LatLng (45.457709802697366, -73.6421862244606));
			stIgnatiusBuilding.Add (new LatLng (45.457719680029115, -73.6422123759985));
			stIgnatiusBuilding.Add (new LatLng (45.457667471255924, -73.64225462079048));
			stIgnatiusBuilding.Add (new LatLng (45.457677348595126, -73.64228010177612));
			stIgnatiusBuilding.Add (new LatLng (45.45762890258163, -73.64231765270233));
			stIgnatiusBuilding.Add (new LatLng (45.45765947531344, -73.64240013062954));
			stIgnatiusBuilding.Add (new LatLng (45.457570108820065, -73.64246919751167));
			stIgnatiusBuilding.Add (new LatLng (45.45761855488404, -73.64259526133537));
			stIgnatiusBuilding.Add (new LatLng (45.45771027304652, -73.64252418279648));
			stIgnatiusBuilding.Add (new LatLng (45.457718739330936, -73.64254362881184));
			stIgnatiusBuilding.Add (new LatLng (45.457764363175, -73.64250876009464));
			stIgnatiusBuilding.Add (new LatLng (45.45777282945127, -73.64253088831902));
			stIgnatiusBuilding.Add (new LatLng (45.45782127534108, -73.64249266684055));
			stIgnatiusBuilding.Add (new LatLng (45.45783209334964, -73.64251881837845));
			stIgnatiusBuilding.Add (new LatLng (45.45788853509969, -73.64247657358646));
			stIgnatiusBuilding.Add (new LatLng (45.45792287047003, -73.6425644159317));
			stIgnatiusBuilding.Add (new LatLng (45.4579026455284, -73.64257983863354));
			stIgnatiusBuilding.Add (new LatLng (45.45795156165383, -73.64270523190498));
			stIgnatiusBuilding.InvokeFillColor(-65536);
			stIgnatiusBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(stIgnatiusBuilding);

			PolygonOptions terrebonneBuilding= new PolygonOptions ();
			terrebonneBuilding.Add (new LatLng (45.46008031269777, -73.64090748131275));
			terrebonneBuilding.Add (new LatLng (45.4600426863333, -73.64080622792244));
			terrebonneBuilding.Add (new LatLng (45.45993262907309, -73.64089138805866));
			terrebonneBuilding.Add (new LatLng (45.45997119617164, -73.64099331200123));
			terrebonneBuilding.InvokeFillColor(-65536);
			terrebonneBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(terrebonneBuilding);

			PolygonOptions hingstonABuilding= new PolygonOptions ();
			hingstonABuilding.Add (new LatLng (45.45942608070822, -73.6415733397007));
			hingstonABuilding.Add (new LatLng (45.45967065430309, -73.64138424396515));
			hingstonABuilding.Add (new LatLng (45.459647137657356, -73.64132389426231));
			hingstonABuilding.Add (new LatLng (45.45965654431683, -73.64131584763527));
			hingstonABuilding.Add (new LatLng (45.45952485094153, -73.64096447825432));
			hingstonABuilding.Add (new LatLng (45.45950791891383, -73.64097520709038));
			hingstonABuilding.Add (new LatLng (45.45948534286898, -73.64091619849205));
			hingstonABuilding.Add (new LatLng (45.45946370748417, -73.64093363285065));
			hingstonABuilding.Add (new LatLng (45.459459944807705, -73.6409242451191));
			hingstonABuilding.Add (new LatLng (45.45926522595792, -73.64107847213745));
			hingstonABuilding.Add (new LatLng (45.45926804797503, -73.64108920097351));
			hingstonABuilding.Add (new LatLng (45.4592492345249, -73.64110663533211));
			hingstonABuilding.Add (new LatLng (45.45927086999199, -73.64116430282593));
			hingstonABuilding.Add (new LatLng (45.4592548785606, -73.64117905497551));
			hingstonABuilding.Add (new LatLng (45.45939033524779, -73.64152908325195));
			hingstonABuilding.Add (new LatLng (45.45940444530066, -73.64151701331139));
			hingstonABuilding.InvokeFillColor(-65536);
			hingstonABuilding.InvokeStrokeWidth (4);
			map.AddPolygon(hingstonABuilding);

			PolygonOptions hingstonBBuilding= new PolygonOptions ();
			hingstonBBuilding.Add (new LatLng (45.45942608070822, -73.6415733397007));
			hingstonBBuilding.Add (new LatLng (45.45940444530066, -73.64151701331139));
			hingstonBBuilding.Add (new LatLng (45.459372462509094, -73.64153981208801));
			hingstonBBuilding.Add (new LatLng (45.45936211513147, -73.64151701331139));
			hingstonBBuilding.Add (new LatLng (45.45895386253506, -73.64184156060219));
			hingstonBBuilding.Add (new LatLng (45.458974557438246, -73.64189520478249));
			hingstonBBuilding.Add (new LatLng (45.45895386253506, -73.64191263914108));
			hingstonBBuilding.Add (new LatLng (45.45895950660033, -73.64192739129066));
			hingstonBBuilding.Add (new LatLng (45.458949159146904, -73.64193543791771));
			hingstonBBuilding.Add (new LatLng (45.45908367589315, -73.64228814840317));
			hingstonBBuilding.Add (new LatLng (45.45909684534755, -73.64228010177612));
			hingstonBBuilding.Add (new LatLng (45.459101548723396, -73.64229083061218));
			hingstonBBuilding.Add (new LatLng (45.459123184247154, -73.6422747373581));
			hingstonBBuilding.Add (new LatLng (45.45914481976262, -73.6423310637474));
			hingstonBBuilding.Add (new LatLng (45.45934518305489, -73.64217415452003));
			hingstonBBuilding.Add (new LatLng (45.45932260694488, -73.64211849868298));
			hingstonBBuilding.Add (new LatLng (45.459346594061465, -73.64209905266762));
			hingstonBBuilding.Add (new LatLng (45.459341420370514, -73.64208698272705));
			hingstonBBuilding.Add (new LatLng (45.45950462657451, -73.64195689558983));
			hingstonBBuilding.Add (new LatLng (45.45953002461565, -73.64201724529266));
			hingstonBBuilding.Add (new LatLng (45.45955260064262, -73.64199846982956));
			hingstonBBuilding.Add (new LatLng (45.45938798357197, -73.64157736301422));
			hingstonBBuilding.InvokeFillColor(-65536);
			hingstonBBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(hingstonBBuilding);

			PolygonOptions hingstonCBuilding= new PolygonOptions ();
			hingstonCBuilding.Add (new LatLng (45.45969134894318, -73.64189520478249));
			hingstonCBuilding.Add (new LatLng (45.45951873659878, -73.64203333854675));
			hingstonCBuilding.Add (new LatLng (45.45961562533672, -73.64228010177612));
			hingstonCBuilding.Add (new LatLng (45.459788237384515, -73.64214397966862));
			hingstonCBuilding.Add (new LatLng (45.4597750680916, -73.64211045205593));
			hingstonCBuilding.Add (new LatLng (45.45988653665243, -73.64201925694942));
			hingstonCBuilding.Add (new LatLng (45.459817397950786, -73.64184357225895));
			hingstonCBuilding.Add (new LatLng (45.459706399585784, -73.64192940294743));
			hingstonCBuilding.InvokeFillColor(-65536);
			hingstonCBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(hingstonCBuilding);

			PolygonOptions cJBuilding = new PolygonOptions ();
			cJBuilding.Add (new LatLng (45.4574214779174, -73.64024430513382));
			cJBuilding.Add (new LatLng (45.45737162062813, -73.6401142179966));
			cJBuilding.Add (new LatLng (45.457306712015665, -73.64007599651814));
			cJBuilding.Add (new LatLng (45.457299186374605, -73.6400954425335));
			cJBuilding.Add (new LatLng (45.45723145555984, -73.64004850387573));
			cJBuilding.Add (new LatLng (45.45722298920231, -73.6399707198143));
			cJBuilding.Add (new LatLng (45.457232396266164, -73.63990634679794));
			cJBuilding.Add (new LatLng (45.45725121038923, -73.6398620903492));
			cJBuilding.Add (new LatLng (45.4572737873286, -73.63982185721397));
			cJBuilding.Add (new LatLng (45.45730483060549, -73.63980174064636));
			cJBuilding.Add (new LatLng (45.457331170342194, -73.6397910118103));
			cJBuilding.Add (new LatLng (45.45736127288332, -73.63978430628777));
			cJBuilding.Add (new LatLng (45.457372561332114, -73.63978162407875));
			cJBuilding.Add (new LatLng (45.45739701963006, -73.63978564739227));
			cJBuilding.Add (new LatLng (45.45742053721423, -73.63979771733284));
			cJBuilding.Add (new LatLng (45.45744405478861, -73.63981112837791));
			cJBuilding.Add (new LatLng (45.45746286884104, -73.63982856273651));
			cJBuilding.Add (new LatLng (45.4574722758649, -73.63984867930412));
			cJBuilding.Add (new LatLng (45.45745816532853, -73.63997206091881));
			cJBuilding.Add (new LatLng (45.457441232680196, -73.63996267318726));
			cJBuilding.Add (new LatLng (45.45743746986878, -73.64006593823433));
			cJBuilding.Add (new LatLng (45.45748732709983, -73.64019066095352));
			cJBuilding.Add (new LatLng (45.45762278803329, -73.64008873701096));
			cJBuilding.Add (new LatLng (45.45771685793452, -73.64033281803131));
			cJBuilding.Add (new LatLng (45.45775730794381, -73.64029929041862));
			cJBuilding.Add (new LatLng (45.45783350439407, -73.64048838615417));
			cJBuilding.Add (new LatLng (45.45765194971945, -73.64063456654549));
			cJBuilding.Add (new LatLng (45.457621847333485, -73.64056080579758));
			cJBuilding.Add (new LatLng (45.45754753199931, -73.64062517881393));
			cJBuilding.Add (new LatLng (45.45753342148178, -73.64059031009674));
			cJBuilding.Add (new LatLng (45.457331170342194, -73.64075392484665));
			cJBuilding.Add (new LatLng (45.45730483060549, -73.6406908929348));
			cJBuilding.Add (new LatLng (45.457279431562036, -73.64071100950241));
			cJBuilding.Add (new LatLng (45.45717407244481, -73.64044144749641));
			cJBuilding.InvokeFillColor(-65536);
			cJBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(cJBuilding);

			PolygonOptions RichardScienceBuilding = new PolygonOptions ();
			RichardScienceBuilding.Add (new LatLng (45.4582079002712, -73.64158071577549));
			RichardScienceBuilding.Add (new LatLng (45.45819661198965, -73.64154987037182));
			RichardScienceBuilding.Add (new LatLng (45.45832360502687, -73.64144995808601));
			RichardScienceBuilding.Add (new LatLng (45.45828127404625, -73.64133931696415));
			RichardScienceBuilding.Add (new LatLng (45.45820178578563, -73.64140100777149));
			RichardScienceBuilding.Add (new LatLng (45.458171683693195, -73.6413212120533));
			RichardScienceBuilding.Add (new LatLng (45.458249760962254, -73.64126086235046));
			RichardScienceBuilding.Add (new LatLng (45.45820037475041, -73.64113345742226));
			RichardScienceBuilding.Add (new LatLng (45.45810113185108, -73.64121325314045));
			RichardScienceBuilding.Add (new LatLng (45.45809031389416, -73.64118777215481));
			RichardScienceBuilding.Add (new LatLng (45.45802822818814, -73.64123672246933));
			RichardScienceBuilding.Add (new LatLng (45.457974608659775, -73.64110127091408));
			RichardScienceBuilding.Add (new LatLng (45.4578904164904, -73.6411689966917));
			RichardScienceBuilding.Add (new LatLng (45.457934629153684, -73.64128567278385));
			RichardScienceBuilding.Add (new LatLng (45.45786689910221, -73.6413386464119));
			RichardScienceBuilding.Add (new LatLng (45.45783726717908, -73.64126086235046));
			RichardScienceBuilding.Add (new LatLng (45.4575211923637, -73.64150896668434));
			RichardScienceBuilding.Add (new LatLng (45.457419596511066, -73.6412487924099));
			RichardScienceBuilding.Add (new LatLng (45.45741207088506, -73.64125482738018));
			RichardScienceBuilding.Add (new LatLng (45.45724227368157, -73.64082098007202));
			RichardScienceBuilding.Add (new LatLng (45.45719006446636, -73.64086121320724));
			RichardScienceBuilding.Add (new LatLng (45.457135033079055, -73.64072106778622));
			RichardScienceBuilding.Add (new LatLng (45.456972290371645, -73.64085115492344));
			RichardScienceBuilding.Add (new LatLng (45.45701932588448, -73.64097118377686));
			RichardScienceBuilding.Add (new LatLng (45.45699204529182, -73.64099331200123));
			RichardScienceBuilding.Add (new LatLng (45.457015092689936, -73.64105097949505));
			RichardScienceBuilding.Add (new LatLng (45.45703766972384, -73.64103354513645));
			RichardScienceBuilding.Add (new LatLng (45.457161372895065, -73.64135205745697));
			RichardScienceBuilding.Add (new LatLng (45.45715290652697, -73.64135876297951));
			RichardScienceBuilding.Add (new LatLng (45.45717642421297, -73.6414197832346));
			RichardScienceBuilding.Add (new LatLng (45.45716748749345, -73.64142782986164));
			RichardScienceBuilding.Add (new LatLng (45.45718206845615, -73.64146672189236));
			RichardScienceBuilding.Add (new LatLng (45.457210760016906, -73.64144593477249));
			RichardScienceBuilding.Add (new LatLng (45.45743935127452, -73.642034009099));
			RichardScienceBuilding.Add (new LatLng (45.45755223550439, -73.64194549620152));
			RichardScienceBuilding.Add (new LatLng (45.45759409668217, -73.64205077290535));
			RichardScienceBuilding.Add (new LatLng (45.457677348595126, -73.64198438823223));
			RichardScienceBuilding.Add (new LatLng (45.45767076370252, -73.64196226000786));
			RichardScienceBuilding.Add (new LatLng (45.457996714962725, -73.64170409739017));
			RichardScienceBuilding.Add (new LatLng (45.45800894397767, -73.64173494279385));
			RichardScienceBuilding.InvokeFillColor(-65536);
			RichardScienceBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(RichardScienceBuilding);

			PolygonOptions RefectoryBuilding = new PolygonOptions ();
			RefectoryBuilding.Add (new LatLng (45.45842237719143, -73.64139631390572));
			RefectoryBuilding.Add (new LatLng (45.45858041229482, -73.64127293229103));
			RefectoryBuilding.Add (new LatLng (45.458552191773144, -73.6411964893341));
			RefectoryBuilding.Add (new LatLng (45.45857853092714, -73.64117369055748));
			RefectoryBuilding.Add (new LatLng (45.458571005455816, -73.64115089178085));
			RefectoryBuilding.Add (new LatLng (45.45860675143564, -73.64112541079521));
			RefectoryBuilding.Add (new LatLng (45.458576649559404, -73.6410503089428));
			RefectoryBuilding.Add (new LatLng (45.458657548315394, -73.64098593592644));
			RefectoryBuilding.Add (new LatLng (45.45860957348573, -73.6408706009388));
			RefectoryBuilding.Add (new LatLng (45.45852208986775, -73.64093899726868));
			RefectoryBuilding.Add (new LatLng (45.45845624189373, -73.64077404141426));
			RefectoryBuilding.Add (new LatLng (45.45843366542777, -73.64079013466835));
			RefectoryBuilding.Add (new LatLng (45.45841579238582, -73.6407445371151));
			RefectoryBuilding.Add (new LatLng (45.45835558841338, -73.64079281687737));
			RefectoryBuilding.Add (new LatLng (45.45837440216168, -73.64084243774414));
			RefectoryBuilding.Add (new LatLng (45.45832924915522, -73.64087998867035));
			RefectoryBuilding.Add (new LatLng (45.45836499528832, -73.64097386598587));
			RefectoryBuilding.Add (new LatLng (45.45838569040763, -73.64095643162727));
			RefectoryBuilding.Add (new LatLng (45.45841108895281, -73.64102348685265));
			RefectoryBuilding.Add (new LatLng (45.45830008781933, -73.64111199975014));
			RefectoryBuilding.Add (new LatLng (45.45835088497533, -73.64124342799187));
			RefectoryBuilding.Add (new LatLng (45.458416733072404, -73.64119783043861));
			RefectoryBuilding.Add (new LatLng (45.45845247915003, -73.6412863433361));
			RefectoryBuilding.Add (new LatLng (45.45839791933823, -73.64133059978485));
			RefectoryBuilding.InvokeFillColor(-65536);
			RefectoryBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(RefectoryBuilding);

			PolygonOptions CentralBuilding = new PolygonOptions ();
			CentralBuilding.Add (new LatLng (45.45829444368806, -73.64083841443062));
			CentralBuilding.Add (new LatLng (45.45844871640609, -73.64071771502495));
			CentralBuilding.Add (new LatLng (45.458241765102315, -73.64017724990845));
			CentralBuilding.Add (new LatLng (45.45823612096522, -73.64018127322197));
			CentralBuilding.Add (new LatLng (45.4581429926214, -73.63994121551514));
			CentralBuilding.Add (new LatLng (45.45799906669659, -73.64005655050278));
			CentralBuilding.Add (new LatLng (45.45809125458613, -73.6402952671051));
			CentralBuilding.Add (new LatLng (45.45808749181809, -73.64030063152313));
			CentralBuilding.InvokeFillColor(-65536);
			CentralBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(CentralBuilding);

			PolygonOptions CampusRetailStoreBuilding = new PolygonOptions ();
			CampusRetailStoreBuilding.Add (new LatLng (45.45783350439407, -73.6401692032814));
			CampusRetailStoreBuilding.Add (new LatLng (45.45789464961923, -73.64012360572815));
			CampusRetailStoreBuilding.Add (new LatLng (45.45786783979789, -73.64004917442799));
			CampusRetailStoreBuilding.Add (new LatLng (45.45800000739008, -73.63994590938091));
			CampusRetailStoreBuilding.Add (new LatLng (45.45800894397767, -73.63996870815754));
			CampusRetailStoreBuilding.Add (new LatLng (45.45803057992068, -73.63995127379894));
			CampusRetailStoreBuilding.Add (new LatLng (45.458053156548004, -73.64001162350178));
			CampusRetailStoreBuilding.Add (new LatLng (45.45808514008792, -73.63998681306839));
			CampusRetailStoreBuilding.Add (new LatLng (45.45806350416585, -73.63993182778358));
			CampusRetailStoreBuilding.Add (new LatLng (45.458083258703724, -73.63991439342499));
			CampusRetailStoreBuilding.Add (new LatLng (45.45807432212791, -73.63989293575287));
			CampusRetailStoreBuilding.Add (new LatLng (45.458190027157706, -73.63980039954185));
			CampusRetailStoreBuilding.Add (new LatLng (45.458216366480876, -73.63986678421497));
			CampusRetailStoreBuilding.Add (new LatLng (45.45828127404625, -73.63981649279594));
			CampusRetailStoreBuilding.Add (new LatLng (45.458166980239795, -73.63952547311783));
			CampusRetailStoreBuilding.Add (new LatLng (45.458113360843356, -73.63956704735756));
			CampusRetailStoreBuilding.Add (new LatLng (45.4581481664223, -73.63965958356857));
			CampusRetailStoreBuilding.Add (new LatLng (45.45800612189753, -73.6397722363472));
			CampusRetailStoreBuilding.Add (new LatLng (45.45800000739008, -73.63975681364536));
			CampusRetailStoreBuilding.Add (new LatLng (45.45793321811177, -73.6398084461689));
			CampusRetailStoreBuilding.Add (new LatLng (45.4579388622792, -73.63982386887074));
			CampusRetailStoreBuilding.Add (new LatLng (45.45781610151019, -73.63992109894753));
			CampusRetailStoreBuilding.Add (new LatLng (45.45777894398335, -73.63982856273651));
			CampusRetailStoreBuilding.Add (new LatLng (45.45771826898183, -73.63987617194653));
			CampusRetailStoreBuilding.InvokeFillColor(-65536);
			CampusRetailStoreBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(CampusRetailStoreBuilding);

			PolygonOptions ChapelBuilding = new PolygonOptions ();
			ChapelBuilding.Add (new LatLng (45.458573827507685, -73.63969512283802));
			ChapelBuilding.Add (new LatLng (45.458657548315394, -73.63962605595589));
			ChapelBuilding.Add (new LatLng (45.45865990002167, -73.6395824700594));
			ChapelBuilding.Add (new LatLng (45.45863591261296, -73.63951809704304));
			ChapelBuilding.Add (new LatLng (45.458646260123835, -73.63950870931149));
			ChapelBuilding.Add (new LatLng (45.45861333621894, -73.63942958414555));
			ChapelBuilding.Add (new LatLng (45.458620861684615, -73.63942489027977));
			ChapelBuilding.Add (new LatLng (45.45851221267656, -73.63915599882603));
			ChapelBuilding.Add (new LatLng (45.45852208986775, -73.63914459943771));
			ChapelBuilding.Add (new LatLng (45.45852632294917, -73.63913454115391));
			ChapelBuilding.Add (new LatLng (45.45852867466092, -73.6391231417656));
			ChapelBuilding.Add (new LatLng (45.45852773397625, -73.63911174237728));
			ChapelBuilding.Add (new LatLng (45.4585244415797, -73.6390969902277));
			ChapelBuilding.Add (new LatLng (45.458521619525364, -73.63909028470516));
			ChapelBuilding.Add (new LatLng (45.45851456438889, -73.6390782147646));
			ChapelBuilding.Add (new LatLng (45.45850703890905, -73.63907352089882));
			ChapelBuilding.Add (new LatLng (45.458500454113334, -73.63907150924206));
			ChapelBuilding.Add (new LatLng (45.45849386931688, -73.63907217979431));
			ChapelBuilding.Add (new LatLng (45.45848681417692, -73.63907352089882));
			ChapelBuilding.Add (new LatLng (45.458333952595076, -73.63918349146843));
			ChapelBuilding.Add (new LatLng (45.458349003599984, -73.6392230540514));
			ChapelBuilding.Add (new LatLng (45.45833771534668, -73.63923244178295));
			ChapelBuilding.Add (new LatLng (45.458349003599984, -73.63926127552986));
			ChapelBuilding.Add (new LatLng (45.458359821507294, -73.63925524055958));
			ChapelBuilding.Add (new LatLng (45.45842472890751, -73.63942623138428));
			ChapelBuilding.Add (new LatLng (45.45839133452977, -73.63945171236992));
			ChapelBuilding.Add (new LatLng (45.45842096616171, -73.63952480256557));
			ChapelBuilding.Add (new LatLng (45.458452008807036, -73.63949932157993));
			ChapelBuilding.Add (new LatLng (45.45849998377077, -73.6396186798811));
			ChapelBuilding.Add (new LatLng (45.45851221267656, -73.63960929214954));
			ChapelBuilding.Add (new LatLng (45.4585409035605, -73.6396823823452));
			ChapelBuilding.InvokeFillColor(-65536);
			ChapelBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(ChapelBuilding);


			PolygonOptions PsychologyBuilding = new PolygonOptions ();
			PsychologyBuilding.Add (new LatLng (45.45879488779811, -73.64084377884865));
			PsychologyBuilding.Add (new LatLng (45.45910060804827, -73.64060908555984));
			PsychologyBuilding.Add (new LatLng (45.45912036222279, -73.64066138863564));
			PsychologyBuilding.Add (new LatLng (45.45921066693252, -73.64058896899223));
			PsychologyBuilding.Add (new LatLng (45.45916175189937, -73.64046290516853));
			PsychologyBuilding.Add (new LatLng (45.45916927729184, -73.6404575407505));
			PsychologyBuilding.Add (new LatLng (45.45914481976262, -73.64039584994316));
			PsychologyBuilding.Add (new LatLng (45.45913541301778, -73.64040523767471));
			PsychologyBuilding.Add (new LatLng (45.45904416751145, -73.64016517996788));
			PsychologyBuilding.Add (new LatLng (45.45895386253506, -73.64023357629776));
			PsychologyBuilding.Add (new LatLng (45.458956684567745, -73.64024430513382));
			PsychologyBuilding.Add (new LatLng (45.45891999813161, -73.64027515053749));
			PsychologyBuilding.Add (new LatLng (45.4589143540624, -73.64025637507439));
			PsychologyBuilding.Add (new LatLng (45.4586961162861, -73.64042267203331));
			PsychologyBuilding.Add (new LatLng (45.458697056967964, -73.64043340086937));
			PsychologyBuilding.Add (new LatLng (45.45865190421989, -73.64046961069107));
			PsychologyBuilding.InvokeFillColor(-65536);
			PsychologyBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(PsychologyBuilding);

			PolygonOptions CampusBuilding = new PolygonOptions ();
			CampusBuilding.Add (new LatLng (45.45899337098003, -73.63912582397461));
			CampusBuilding.Add (new LatLng (45.4591043707487, -73.63940745592117));
			CampusBuilding.Add (new LatLng (45.459216310972096, -73.63932028412819));
			CampusBuilding.Add (new LatLng (45.4592351244332, -73.63936856389046));
			CampusBuilding.Add (new LatLng (45.45929438679466, -73.6393229663372));
			CampusBuilding.Add (new LatLng (45.45925581923316, -73.63921567797661));
			CampusBuilding.Add (new LatLng (45.45927933604209, -73.63919824361801));
			CampusBuilding.Add (new LatLng (45.45919091278962, -73.63897562026978));
			CampusBuilding.Add (new LatLng (45.45914952313447, -73.63900512456894));
			CampusBuilding.Add (new LatLng (45.459131650319414, -73.63896355032921));
			CampusBuilding.Add (new LatLng (45.45907709116479, -73.63900914788246));
			CampusBuilding.Add (new LatLng (45.45909120129605, -73.63904938101768));
			CampusBuilding.Add (new LatLng (45.45899525233385, -73.63912582397461));
			CampusBuilding.InvokeFillColor(-65536);
			CampusBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(CampusBuilding);

			PolygonOptions OPConcertBuilding = new PolygonOptions ();
			OPConcertBuilding.Add (new LatLng (45.45949004621238, -73.63913387060165));
			OPConcertBuilding.Add (new LatLng (45.459459944807705, -73.63905474543571));
			OPConcertBuilding.Add (new LatLng (45.45944865677681, -73.63906279206276));
			OPConcertBuilding.Add (new LatLng (45.45931037821484, -73.6387087404728));
			OPConcertBuilding.Add (new LatLng (45.45932166627344, -73.63869935274124));
			OPConcertBuilding.Add (new LatLng (45.45931508157287, -73.63868191838264));
			OPConcertBuilding.Add (new LatLng (45.45916363324759, -73.6387999355793));
			OPConcertBuilding.Add (new LatLng (45.45914952313447, -73.63877043128014));
			OPConcertBuilding.Add (new LatLng (45.45906015900261, -73.63884016871452));
			OPConcertBuilding.Add (new LatLng (45.45910625209883, -73.63895818591118));
			OPConcertBuilding.Add (new LatLng (45.45915046380878, -73.63892331719398));
			OPConcertBuilding.Add (new LatLng (45.459173039987725, -73.63897830247879));
			OPConcertBuilding.Add (new LatLng (45.45919561615761, -73.6389608681202));
			OPConcertBuilding.Add (new LatLng (45.459305674856424, -73.63923981785774));
			OPConcertBuilding.Add (new LatLng (45.459349886409996, -73.63920629024506));
			OPConcertBuilding.Add (new LatLng (45.45936211513147, -73.6392317712307));
			OPConcertBuilding.InvokeFillColor(-65536);
			OPConcertBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(OPConcertBuilding);

			PolygonOptions BHBuilding = new PolygonOptions ();
			BHBuilding.Add (new LatLng (45.459720509559546, -73.63926127552986));
			BHBuilding.Add (new LatLng (45.459816457287566, -73.6391781270504));
			BHBuilding.Add (new LatLng (45.459755314146435, -73.6390346288681));
			BHBuilding.Add (new LatLng (45.45965936631435, -73.63911241292953));
			BHBuilding.InvokeFillColor(-65536);
			BHBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(BHBuilding);

			PolygonOptions VanierLibraryBuilding = new PolygonOptions ();
			VanierLibraryBuilding.Add (new LatLng (45.45931320022971, -73.63867253065109));
			VanierLibraryBuilding.Add (new LatLng (45.45928121738638, -73.63858938217163));
			VanierLibraryBuilding.Add (new LatLng (45.45927651402553, -73.63859206438065));
			VanierLibraryBuilding.Add (new LatLng (45.45925628956943, -73.63854512572289));
			VanierLibraryBuilding.Add (new LatLng (45.45926240394065, -73.63854110240936));
			VanierLibraryBuilding.Add (new LatLng (45.45914858246013, -73.63824404776096));
			VanierLibraryBuilding.Add (new LatLng (45.459134472343216, -73.63825611770153));
			VanierLibraryBuilding.Add (new LatLng (45.4591245952593, -73.63822996616364));
			VanierLibraryBuilding.Add (new LatLng (45.45921819231848, -73.63815888762474));
			VanierLibraryBuilding.Add (new LatLng (45.45913729436689, -73.63795034587383));
			VanierLibraryBuilding.Add (new LatLng (45.459128828295476, -73.63795705139637));
			VanierLibraryBuilding.Add (new LatLng (45.45911942154797, -73.63793157041073));
			VanierLibraryBuilding.Add (new LatLng (45.459126946946114, -73.63792553544044));
			VanierLibraryBuilding.Add (new LatLng (45.45910013771069, -73.63785780966282));
			VanierLibraryBuilding.Add (new LatLng (45.45907756150256, -73.63787591457367));
			VanierLibraryBuilding.Add (new LatLng (45.45908931994541, -73.63790407776833));
			VanierLibraryBuilding.Add (new LatLng (45.45887343454368, -73.63807305693626));
			VanierLibraryBuilding.Add (new LatLng (45.4588898964226, -73.6381159722805));
			VanierLibraryBuilding.Add (new LatLng (45.45885650232037, -73.63814279437065));
			VanierLibraryBuilding.Add (new LatLng (45.45887390488314, -73.63818772137165));
			VanierLibraryBuilding.Add (new LatLng (45.45885226926374, -73.63820649683475));
			VanierLibraryBuilding.Add (new LatLng (45.45908226487998, -73.63880395889282));
			VanierLibraryBuilding.Add (new LatLng (45.45914858246013, -73.6387536674738));
			VanierLibraryBuilding.Add (new LatLng (45.45916363324759, -73.63879054784775));
			VanierLibraryBuilding.InvokeFillColor(-65536);
			VanierLibraryBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(VanierLibraryBuilding);

			PolygonOptions VanierExtensionBuilding = new PolygonOptions ();
			VanierExtensionBuilding.Add (new LatLng (45.45882028615895, -73.63900043070316));
			VanierExtensionBuilding.Add (new LatLng (45.45883533703404, -73.6389883607626));
			VanierExtensionBuilding.Add (new LatLng (45.45884709552742, -73.63901786506176));
			VanierExtensionBuilding.Add (new LatLng (45.45888801506525, -73.63898567855358));
			VanierExtensionBuilding.Add (new LatLng (45.45889977354766, -73.63901250064373));
			VanierExtensionBuilding.Add (new LatLng (45.45901594722187, -73.63891996443272));
			VanierExtensionBuilding.Add (new LatLng (45.45900512944044, -73.6388911306858));
			VanierExtensionBuilding.Add (new LatLng (45.45902723533941, -73.63887369632721));
			VanierExtensionBuilding.Add (new LatLng (45.45901312519214, -73.63883949816227));
			VanierExtensionBuilding.Add (new LatLng (45.459076150489246, -73.6387898772955));
			VanierExtensionBuilding.Add (new LatLng (45.45886779046982, -73.63824874162674));
			VanierExtensionBuilding.Add (new LatLng (45.45881746411941, -73.63828763365746));
			VanierExtensionBuilding.Add (new LatLng (45.45881323105988, -73.63827891647816));
			VanierExtensionBuilding.Add (new LatLng (45.458784069974286, -73.63830238580704));
			VanierExtensionBuilding.Add (new LatLng (45.458788773376206, -73.63831512629986));
			VanierExtensionBuilding.Add (new LatLng (45.45869470526324, -73.63838955760002));
			VanierExtensionBuilding.Add (new LatLng (45.458689061171484, -73.63837748765945));
			VanierExtensionBuilding.Add (new LatLng (45.45865895933916, -73.63840229809284));
			VanierExtensionBuilding.Add (new LatLng (45.45867777298624, -73.6384492367506));
			VanierExtensionBuilding.Add (new LatLng (45.45862039134304, -73.63849483430386));
			VanierExtensionBuilding.InvokeFillColor(-65536);
			VanierExtensionBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(VanierExtensionBuilding);

			PolygonOptions PhysicalServicesBuilding = new PolygonOptions ();
			PhysicalServicesBuilding.Add (new LatLng (45.4597026369255, -73.64032879471779));
			PhysicalServicesBuilding.Add (new LatLng (45.45984843982731, -73.64021345973015));
			PhysicalServicesBuilding.Add (new LatLng (45.45982398259268, -73.64015311002731));
			PhysicalServicesBuilding.Add (new LatLng (45.45994156535435, -73.64006325602531));
			PhysicalServicesBuilding.Add (new LatLng (45.459661247645954, -73.63933637738228));
			PhysicalServicesBuilding.Add (new LatLng (45.459619858336055, -73.63936588168144));
			PhysicalServicesBuilding.Add (new LatLng (45.459566240321195, -73.63922908902168));
			PhysicalServicesBuilding.Add (new LatLng (45.459277454697734, -73.63945573568344));
			PhysicalServicesBuilding.Add (new LatLng (45.45933107298719, -73.63959386944771));
			PhysicalServicesBuilding.Add (new LatLng (45.459400682620256, -73.63954290747643));
			PhysicalServicesBuilding.Add (new LatLng (45.45944113142161, -73.6396461725235));
			PhysicalServicesBuilding.Add (new LatLng (45.459411029990804, -73.63967165350914));
			PhysicalServicesBuilding.Add (new LatLng (45.459605748337076, -73.64017322659492));
			PhysicalServicesBuilding.Add (new LatLng (45.45963302766522, -73.6401517689228));
			PhysicalServicesBuilding.InvokeFillColor(-65536);
			PhysicalServicesBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(PhysicalServicesBuilding);

			PolygonOptions StructuralFunctionalBuilding = new PolygonOptions ();
			StructuralFunctionalBuilding.Add (new LatLng (45.4568796302966, -73.64075660705566));
			StructuralFunctionalBuilding.Add (new LatLng (45.45710163793753, -73.64055275917053));
			StructuralFunctionalBuilding.Add (new LatLng (45.45697746427997, -73.64006459712982));
			StructuralFunctionalBuilding.Add (new LatLng (45.45668772468234, -73.64031136035919));
			StructuralFunctionalBuilding.InvokeFillColor(-65536);
			StructuralFunctionalBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(StructuralFunctionalBuilding);

			PolygonOptions PerformCentreBuilding = new PolygonOptions ();
			PerformCentreBuilding.Add (new LatLng (45.45711104502166, -73.63783299922943));
			PerformCentreBuilding.Add (new LatLng (45.45737820555565, -73.6376291513443));
			PerformCentreBuilding.Add (new LatLng (45.457048958237415, -73.63678425550461));
			PerformCentreBuilding.Add (new LatLng (45.45680437327082, -73.6369800567627));
			PerformCentreBuilding.InvokeFillColor(-65536);
			PerformCentreBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(PerformCentreBuilding);

			PolygonOptions StingerDomeBuilding = new PolygonOptions ();
			StingerDomeBuilding.Add (new LatLng (45.457372561332114, -73.63708734512329));
			StingerDomeBuilding.Add (new LatLng (45.45833583397091, -73.63596081733704));
			StingerDomeBuilding.Add (new LatLng (45.45792945533317, -73.63523930311203));
			StingerDomeBuilding.Add (new LatLng (45.456956768643806, -73.6363497376442));
			StingerDomeBuilding.InvokeFillColor(-65536);
			StingerDomeBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(StingerDomeBuilding);

			PolygonOptions BBBuilding = new PolygonOptions ();
			BBBuilding.Add (new LatLng (45.45983338922264, -73.63943159580231));
			BBBuilding.Add (new LatLng (45.45995191262564, -73.63935649394989));
			BBBuilding.Add (new LatLng (45.459923692790376, -73.63925188779831));
			BBBuilding.Add (new LatLng (45.45979764402045, -73.63933771848679));
			BBBuilding.InvokeFillColor(-65536);
			BBBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(BBBuilding);

			PolygonOptions SolarhouseBuilding = new PolygonOptions ();
			SolarhouseBuilding.Add (new LatLng (45.459395038599155, -73.64253491163254));
			SolarhouseBuilding.Add (new LatLng (45.45941008932082, -73.64237666130066));
			SolarhouseBuilding.Add (new LatLng (45.45935364909381, -73.64237666130066));
			SolarhouseBuilding.Add (new LatLng (45.45935176775193, -73.64252418279648));
			SolarhouseBuilding.InvokeFillColor(-65536);
			SolarhouseBuilding.InvokeStrokeWidth (4);
			map.AddPolygon(SolarhouseBuilding);
		}
	}
}

