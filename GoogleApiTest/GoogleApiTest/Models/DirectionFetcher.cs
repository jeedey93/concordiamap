using System;
using System.Net;
using System.Json;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using Android.Gms.Maps.Model;

namespace GoogleApiTest
{
	public class DirectionFetcher
	{
		public async Task<JsonValue> getDirections(LatLng startingPoint, LatLng endingPoint){
		
			string urlDirections = "http://maps.googleapis.com/maps/api/directions/json?origin="+startingPoint.Latitude+","+ startingPoint.Longitude+"&destination="+endingPoint.Latitude+","+ endingPoint.Longitude+"&sensor=false&mode=walking";

			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (urlDirections));
			request.ContentType = "application/json";
			request.Method = "GET";

			using(WebResponse response = await request.GetResponseAsync()){
				using(Stream stream = response.GetResponseStream()){

					JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
					return jsonDoc;
				}
			}
		}


		public List<LatLng> DecodePolylinePoints(string encodedPoints) 
		{
			if (encodedPoints == null || encodedPoints == "") return null;
			List<LatLng> poly = new List<LatLng>();
			char[] polylinechars = encodedPoints.ToCharArray();
			int index = 0;

			int currentLat = 0;
			int currentLng = 0;
			int next5bits;
			int sum;
			int shifter;

			try
			{
				while (index < polylinechars.Length)
				{
					// calculate next latitude
					sum = 0;
					shifter = 0;
					do
					{
						next5bits = (int)polylinechars[index++] - 63;
						sum |= (next5bits & 31) << shifter;
						shifter += 5;
					} while (next5bits >= 32 && index < polylinechars.Length);

					if (index >= polylinechars.Length)
						break;

					currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

					//calculate next longitude
					sum = 0;
					shifter = 0;
					do
					{
						next5bits = (int)polylinechars[index++] - 63;
						sum |= (next5bits & 31) << shifter;
						shifter += 5;
					} while (next5bits >= 32 && index < polylinechars.Length);

					if (index >= polylinechars.Length && next5bits >= 32)
						break;

					currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
					LatLng p = new LatLng(0,0);
					p.Latitude = Convert.ToDouble(currentLat) / 100000.0;
					p.Longitude = Convert.ToDouble(currentLng) / 100000.0;
					poly.Add(p);
				} 
			}
			catch (Exception ex)
			{
				// logo it
			}
			return poly;
		}
	}
}

