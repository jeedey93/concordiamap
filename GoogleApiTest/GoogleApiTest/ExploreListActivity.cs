	
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


namespace GoogleApiTest
{
	[Activity (Label = "ExploreList")]			
	public class ExploreListActivity: LeftDrawerActivity
	{
		readonly string SERVER_API_KEY = "AIzaSyCb1zAnzvZpXSd_al21N9tSQ0uWBlrUYtM";
		int radius = 5000;
		List<GooglePlace> nearbyPlacesAdapterList = null ;
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



			ListView list = (ListView)FindViewById (Resource.Id.exploreLView);
			//list.Adapter = new ExploreListAdapter (this, nearbyPlacesList);

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

			GooglePlaceLocation gWebLocation;
			GooglePlace gWebPlace;

			foreach (JsonValue jsonResult in jsonWebResults) {

				//Instantiate Object for 
				gWebLocation = new GooglePlaceLocation (jsonResult ["geometry"] ["location"] ["lat"].ToString (), jsonResult ["geometry"] ["location"] ["lng"].ToString ());
				gWebPlace = new GooglePlace (gWebLocation, jsonResult ["name"].ToString ());

				nearbyPlacesAdapterList.Add (gWebPlace);


				//Console.WriteLine (jsonResult ["geometry"].ToString ());
				//Console.WriteLine (jsonResult ["geometry"] ["location"] ["lat"].ToString ());
				//Console.WriteLine (jsonResult ["geometry"] ["location"] ["lng"].ToString ());
			}
			//Console.WriteLine (json.ToString());
			//Console.WriteLine (jsonWebLocation.ToString ());
		}






	}
}


