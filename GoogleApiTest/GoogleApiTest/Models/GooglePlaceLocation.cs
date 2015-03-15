using System;

namespace GoogleApiTest
{
	public class GooglePlaceLocation
	{
		float latitude;
		float longitude;

		public GooglePlaceLocation(string lat, string lng){
			latitude = float.Parse (lat);
			longitude = float.Parse (lng);
		}

		public float GetLatitude(){
			return latitude;	
		}

		public float GetLongitude(){
			return longitude;
		}

		public void SetLatitude(float lat){
			latitude = lat;
		}

		public void SetLongitude(float lng){
			longitude = lng;
		}


		public string ToString(){
			return "Location = "+latitude.ToString()+", "+longitude.ToString();
		}


	}
}

