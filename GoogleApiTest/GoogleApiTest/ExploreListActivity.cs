	
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
	[Activity (Label = "ExploreList")]			
	public class ExploreListActivity: Activity
	{
		readonly string SERVER_API_KEY = "AIzaSyCb1zAnzvZpXSd_al21N9tSQ0uWBlrUYtM";
		int radius = 5000;
		List<GooglePlace> nearbyPlacesAdapterList = new List<GooglePlace>() ;
		LocationManager locationManager;
		Location location;
		string type;

		protected override void OnCreate (Bundle bundle)
		{	
			//Init
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ExploreListView);

			//Get current type of this activity based on passed value in intent
			type = Intent.GetStringExtra ("type");

			//Get location manager to access location services
			locationManager = GetSystemService (Context.LocationService) as LocationManager; 

			//Set criteria for location provider
			Criteria locationCriteria = new Criteria();
			locationCriteria.Accuracy = Accuracy.Coarse;
			locationCriteria.PowerRequirement = Power.Low;

			//Get current location provider based on above criteria
			string locationProvider = locationManager.GetBestProvider (locationCriteria, true);

			//Reteive last best location based on above provider
			if (locationProvider != null) {
				location = locationManager.GetLastKnownLocation (locationProvider);
			} else {
				Console.WriteLine ("No Location Providers");
			}

			//Build url for REST web service - Google Places API
			string url = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?"+
				"location="+
				location.Latitude+","+location.Longitude+"&"+
				"radius="+
				radius+"&"+
				"type="+
				type+"&"+
				"key="+
				SERVER_API_KEY;


			//Make async web request and add results to nearbyPlacesAdaperList
			makeListFromWebRequest (url);


			ListView listView = (ListView)FindViewById<ListView> (Resource.Id.exploreLView);
			ExploreListAdapter adapter = new ExploreListAdapter (this , nearbyPlacesAdapterList);

			listView.Adapter = adapter;

		}

		private async Task<JsonValue> getNearbyPlacesWebRequest(string url){

			// Create an HTTP web request using the URL:
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (url));
			request.ContentType = "application/json";
			request.Method = "GET";


			// Send the request to the server and wait for the response:
			using (WebResponse response = await request.GetResponseAsync ())
			{
				// Get a stream representation of the HTTP web response:
				using (Stream stream = response.GetResponseStream ())
				{
					// Use this stream to build a JSON document object:
					JsonValue jsonDoc = await Task.Run (() => JsonObject.Load (stream));
					//Console.Out.WriteLine("Response: {0}", jsonDoc.ToString ());

					// Return the JSON document:
					return jsonDoc;
				}
			}
		}

		private async void makeListFromWebRequest(string url){

			//Issue & await async web resquest from Google Places API
			JsonValue json = await getNearbyPlacesWebRequest (url);

			JsonValue jsonWebResults = json["results"];

			LatLng gWebLocation;
			GooglePlace gWebPlace;
			LatLng currentLocation = new LatLng (0.0, 0.0);

			foreach (JsonValue jsonResult in jsonWebResults) {

				//Get needed values
				double lat = jsonResult ["geometry"] ["location"] ["lat"];
				double lng = jsonResult ["geometry"] ["location"] ["lng"];
				string name = jsonResult ["name"].ToString ();

				//Create current GooglePlace Object
				gWebLocation = new LatLng (lat, lng);
				gWebPlace = new GooglePlace (gWebLocation, name);

				//Get current location & calculate distance between current and place
				currentLocation.Latitude = location.Latitude;
				currentLocation.Longitude = location.Longitude;
				gWebPlace.setDistance (calcDistanceToPlace (currentLocation, gWebLocation));

				//Add to adapter list
				nearbyPlacesAdapterList.Add (gWebPlace);

				foreach (GooglePlace gPlace in nearbyPlacesAdapterList) {
					Console.WriteLine (gPlace.ToString ());
				}

			}
	
		}

		private double calcDistanceToPlace(LatLng start, LatLng end){
			//double old = Math.Sqrt (Math.Pow ((end.Latitude - start.Latitude), 2) + Math.Pow ((end.Longitude - start.Longitude), 2));

			double  earthRadius = 6378137; // Earth’s mean radius in meter
			double dLat = rad(end.Latitude - start.Latitude);
			double dLong = rad(end.Longitude - start.Longitude);
			double a = Math.Sin(dLat / 2.0) * Math.Sin(dLat / 2.0) +
				Math.Cos(rad(start.Latitude)) * Math.Cos(rad(end.Latitude)) *
				Math.Sin(dLong / 2.0) * Math.Sin(dLong / 2.0);
			double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
			double d = earthRadius * c;


			return d; // returns the distance in meter
		}

		private double rad(double d){
			return d * Math.PI / 180.0;
		}







	}
}


