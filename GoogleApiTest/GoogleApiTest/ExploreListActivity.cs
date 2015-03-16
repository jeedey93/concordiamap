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

			//Set activity label to current type
			this.Title = char.ToUpper(type[0])+type.Substring(1);

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
				//"radius="+
				//radius+"&"+
				"types="+
				type+"&"+
				"key="+
				SERVER_API_KEY+"&"+
				"rankby=distance";


			//Make async web request and add results to nearbyPlacesAdaperList
			CreateAndSetAdapter (url);
		}

		private async Task<JsonValue> GetNearbyPlacesWebRequest(string url){

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

		private async void CreateAndSetAdapter(string url){

			//Issue & await async web resquest from Google Places API
			JsonValue json = await GetNearbyPlacesWebRequest (url);

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
				gWebPlace.SetDistance (CalcDistanceToPlace (currentLocation, gWebLocation));

				//Add to adapter list
				nearbyPlacesAdapterList.Add (gWebPlace);

				foreach (GooglePlace gPlace in nearbyPlacesAdapterList) {
					Console.WriteLine (gPlace.ToString ());
				}

			}

			//Create and set adapter
			ListView listView = FindViewById<ListView> (Resource.Id.exploreLView);
			listView.Adapter = new ExploreListAdapter(this , nearbyPlacesAdapterList);
	
		}

		private double CalcDistanceToPlace(LatLng start, LatLng end){
			double  earthRadius = 6378137; // Earth’s mean radius in meter
			double dLat = Rad(end.Latitude - start.Latitude);
			double dLong = Rad(end.Longitude - start.Longitude);
			double a = Math.Sin(dLat / 2.0) * Math.Sin(dLat / 2.0) +
				Math.Cos(Rad(start.Latitude)) * Math.Cos(Rad(end.Latitude)) *
				Math.Sin(dLong / 2.0) * Math.Sin(dLong / 2.0);
			double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
			double d = earthRadius * c;


			return d; // returns the distance in meter
		}

		private double Rad(double d){
			return d * Math.PI / 180.0;
		}







	}
}


