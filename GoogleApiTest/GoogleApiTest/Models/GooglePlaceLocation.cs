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

		public float getLatitude(){
			return latitude;	
		}

		public float getLongitude(){
			return longitude;
		}

		public void setLatitude(float lat){
			latitude = lat;
		}

		public void setLongitude(float lng){
			longitude = lng;
		}


		public string ToString(){
			return "Location = "+latitude.ToString()+", "+longitude.ToString();
		}


	}
}

