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
using Android.Net;
using System.Net.Http;


namespace GoogleApiTest
{
	[Activity (Label = "ExploreList")]			
	public class ExploreListActivity: Activity
	{
		readonly string SERVER_API_KEY = "AIzaSyCCULSA6lf_ipklViZe63kYX7kL9UFbNL0";
		private int radius = 5000;
		private List<GooglePlace> nearbyPlacesAdapterList = new List<GooglePlace>() ;
		private LocationManager locationManager;
		private Location location;
		private string type;


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
			
		private async Task<JsonValue> GooglePlaceWebRequest(string url){

			// Create an HTTP web request using the URL:
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new System.Uri (url));
			request.ContentType = "application/json";
			request.Method = "GET";


			//check if there is network connection
			if (IsConnectedToNetwork ()) {
				// Send the request to the server and wait for the response:
				using (WebResponse response = await request.GetResponseAsync ()) {
					// Get a stream representation of the HTTP web response:
					using (Stream stream = response.GetResponseStream ()) {
						// Use this stream to build a JSON document object:
						JsonValue jsonDoc = await Task.Run (() => JsonObject.Load (stream));
						//Console.Out.WriteLine("Response: {0}", jsonDoc.ToString ());

						// Return the JSON document:
						return jsonDoc;
					}
				}
			} else {

				return null;
			}
		}

		private async void CreateAndSetAdapter(string url){

			//Issue & await async web resquest from Google Places API
			JsonValue json = await GooglePlaceWebRequest (url);
			//If the HTTP request failed or returned null, don't try to open it.
			if (json != null) {
				JsonValue jsonWebResults = json ["results"];

				LatLng gWebLocation;
				GooglePlace gWebPlace;
				LatLng currentLocation = new LatLng (0.0, 0.0);

				foreach (JsonValue jsonResult in jsonWebResults){

					//Get needed values
					double lat = jsonResult ["geometry"] ["location"] ["lat"];
					double lng = jsonResult ["geometry"] ["location"] ["lng"];
					string name = jsonResult ["name"].ToString ();
					JsonValue types = null;
						
					if (jsonResult ["types"] != null) {
						types = jsonResult ["types"];
					}

					//Get id for detailed search
					string placeId = jsonResult ["place_id"];

					//Make URL
					string detailedPlaceURL = "https://maps.googleapis.com/maps/api/place/details/json?"+
						"key="+SERVER_API_KEY+"&"+
						"placeid="+placeId;

					//Issue & await async web resquest from Google Places API
					JsonValue jsonDetailed = await GooglePlaceWebRequest(detailedPlaceURL);
					string adress = "";

					//If the HTTP request failed or returned null, don't try to open it.
					if (jsonDetailed != null) {
						JsonValue jsonDetailedResults = jsonDetailed ["result"];

						adress = jsonDetailedResults["formatted_address"].ToString().Replace("\"","");


						adress = adress.Remove (adress.LastIndexOf (','));
						adress = adress.Remove (adress.LastIndexOf (','));
					}


					//Create current GooglePlace Object
					gWebLocation = new LatLng (lat, lng);
					gWebPlace = new GooglePlace (gWebLocation, name);

					//Get current location & calculate distance between current and place
					currentLocation.Latitude = location.Latitude;
					currentLocation.Longitude = location.Longitude;
					gWebPlace.SetDistance (CalcDistanceToPlace (currentLocation, gWebLocation));

					//Get types for google place
					List<string> typeList = new List<string> ();
					foreach (JsonValue type in types) {
						typeList.Add (type.ToString ());
					}
					gWebPlace.SetType (typeList);
					gWebPlace.SetAdress (adress);
						
					//Add to adapter list
					nearbyPlacesAdapterList.Add (gWebPlace);
				}

				//Create and set adapter
				ListView listView = FindViewById<ListView> (Resource.Id.exploreLView);
				listView.Adapter = new ExploreListAdapter (this, nearbyPlacesAdapterList);

				//Not connected to a network, Give message
			} else {
				Toast.MakeText (this, "Please connect to a network", ToastLength.Short).Show ();
			}
	
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

		//Check if Phone is connected to network, try this before trying to connect to the internet (httpRequests)
		public Boolean IsConnectedToNetwork()
		{
			//access android's connectivity manager
			var connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);
			//Try to check for activeNetwork info and if it is connected.
			var activeConnection = connectivityManager.ActiveNetworkInfo;

			if ((activeConnection != null) && activeConnection.IsConnected) {
				return true;
			} else {
				return false;
			}

		}





	}
}


