using System;
using System.Net;
using System.Json;
using System.Threading.Tasks;
using System.IO;

namespace GoogleApiTest
{
	public class DirectionFetcher
	{
		public DirectionFetcher ()
		{
		}



		public async Task<JsonValue> getDirections(){
		
			string urlDirections = "http://maps.googleapis.com/maps/api/directions/json?origin=Chicago%2CIL&destination=Los+Angeles%2CCA&sensor=false";

			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (urlDirections));
			request.ContentType = "application/json";
			request.Method = "GET";

			using(WebResponse response = await request.GetResponseAsync()){
				using(Stream stream = response.GetResponseStream()){

					JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
			//		Console.Out.WriteLine("Reponse : {0}", jsonDoc.ToString());
					return jsonDoc;
				}
			}
		}
	}
}

