

using Android.App;
using Android.OS;
using Android.Views;
using Android.Gms.Maps;
using System;
using Android.Gms.Maps.Model;
using Android.Widget;

namespace GoogleApiTest
{
	[Activity (Label = "MapActivity")]			
	public class MapActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.Main);

			MapFragment mapFrag = (MapFragment) FragmentManager.FindFragmentById(Resource.Id.map);
			GoogleMap map = mapFrag.Map;
			if (map != null) {
				// The GoogleMap object is ready to go.
				map.UiSettings.ZoomControlsEnabled = true;
				map.MyLocationEnabled = true;

				zoomSgw (map);

				// HALL BUILDING POLYGON
				PolygonOptions hallBuilding = new PolygonOptions();
				hallBuilding.Add(new LatLng(45.49770868047681,-73.57903227210045));
				hallBuilding.Add(new LatLng(45.497366508216466,-73.57833489775658));
				hallBuilding.Add(new LatLng(45.4968288804749256,-73.57885658740997));
				hallBuilding.Add(new LatLng(45.49715787001796,-73.579544390347004));
				map.AddPolygon(hallBuilding);

				//Button hallButton = FindViewById<PolygonOptions>();


				// JMSB BUILDING POLYGON
			}





			ToggleButton togglebutton = FindViewById<ToggleButton>(Resource.Id.togglebutton);

			togglebutton.Click += (o, e) => {
				// Perform action on clicks
				if (togglebutton.Checked)
					zoomLoyola (map);
				else
					zoomSgw (map);
			};
		}


		public void zoomSgw(GoogleMap map){
			LatLng location = new LatLng(45.49770868047681, -73.57903227210045);
			CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
			builder.Target(location);
			builder.Zoom(16);
			CameraPosition cameraPosition = builder.Build();
			map.MoveCamera (CameraUpdateFactory.NewCameraPosition (cameraPosition));
		}

		public void zoomLoyola(GoogleMap map){
			LatLng location = new LatLng(45.45827186715735, -73.63961935043335);
			CameraPosition.Builder builder = CameraPosition.InvokeBuilder();
			builder.Target(location);
			builder.Zoom(16);
			CameraPosition cameraPosition = builder.Build();
			map.MoveCamera (CameraUpdateFactory.NewCameraPosition (cameraPosition));
		}
	}
}

