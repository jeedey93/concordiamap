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


		public string GetInstructions(JsonValue results){
			string allInstructions = "";

			//START ADDRESS
			string startAdress = results [0] ["legs"][0]["start_address"];
			string endAdress = results [0] ["legs"][0]["end_address"];

			//INSTRUCTIONS
			string instructions="\r\n";
			int numberSteps = results [0] ["legs"] [0] ["steps"].Count;
			for (int i = 0; i < numberSteps; i++) {
				int instructionNumber = i + 1;
				instructions += instructionNumber + "." +results [0] ["legs"] [0] ["steps"] [i] ["html_instructions"] + "\r\n";
			}

			string duration = results [0] ["legs"][0]["duration"]["text"];

			//DISTANCE IN KM = firstRoutesResults [0] ["overview_polyline"] ["points"];
			//DURATION IN MIN = firstRoutesResults [0] ["legs"][0]["duration"]["text"];
			//START ADDRESS = firstRoutesResults [0] ["legs"][0]["start_address"];
			//END ADDRESS = firstRoutesResults [0] ["legs"][0]["end_address"];


			//INSTRUCTIONS = 
			//	firstRoutesResults [0] ["legs"][0]["steps"].Count
			//	firstRoutesResults [0] ["legs"][0]["steps"][0]["html_instructions"];

			allInstructions = "START:\r\n" + startAdress + "\r\nEND:\r\n"+  endAdress + "\r\nDURATION:\r\n" + duration + "\r\nINSTRUCTIONS :" + instructions;
			return allInstructions;
		}


		public string GetInstructionsDifferentCampus(JsonValue results, JsonValue results2){
			string allInstructions = "";

			//START ADDRESS
			string startAdress = results [0] ["legs"][0]["start_address"];
			string endAdress = results2 [0] ["legs"][0]["end_address"];

			int instructionNumber = 0;
			//INSTRUCTIONS
			string instructions="\r\n";
			int numberSteps = results [0] ["legs"] [0] ["steps"].Count;
			for (int i = 0; i < numberSteps; i++) {
				instructionNumber++;
				instructions += instructionNumber + "." +results [0] ["legs"] [0] ["steps"] [i] ["html_instructions"] + "\r\n";
			}

			instructionNumber++;
			instructions += instructionNumber + "." +"Take the bus to the other campus\r\n";

			//INSTRUCTIONS
			int numberSteps2 = results2 [0] ["legs"] [0] ["steps"].Count;
			for (int i = 0; i < numberSteps2; i++) {
				instructionNumber++;
				instructions += instructionNumber + "." +results2 [0] ["legs"] [0] ["steps"] [i] ["html_instructions"] + "\r\n";
			}

			string duration = results [0] ["legs"][0]["duration"]["text"] +" + bus travel time (~20mins) + "+ results2 [0] ["legs"][0]["duration"]["text"];

			//DISTANCE IN KM = firstRoutesResults [0] ["overview_polyline"] ["points"];
			//DURATION IN MIN = firstRoutesResults [0] ["legs"][0]["duration"]["text"];
			//START ADDRESS = firstRoutesResults [0] ["legs"][0]["start_address"];
			//END ADDRESS = firstRoutesResults [0] ["legs"][0]["end_address"];


			//INSTRUCTIONS = 
			//	firstRoutesResults [0] ["legs"][0]["steps"].Count
			//	firstRoutesResults [0] ["legs"][0]["steps"][0]["html_instructions"];

			allInstructions = "START:\r\n" + startAdress + "\r\nEND:\r\n"+  endAdress + "\r\nDURATION:\r\n" + duration + "\r\nINSTRUCTIONS :" + instructions;
			return allInstructions;
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

