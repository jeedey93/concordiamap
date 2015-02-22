using System;
using Android.Gms.Maps.Model;
using System.Collections.Generic;
using System.Net.Http;
using HtmlAgilityPack;
using System.Linq;

namespace GoogleApiTest
{
	public class Building
	{
		public string Name { get; set; }
		public string Abbreviation { get; set; }
		public double XCoordinate { get; set; }
		public double YCoordinate{ get; set; }
		public List<LatLng> Corners = new List<LatLng> ();
		public int BuildingImage;
		public string Description { get; set;}

		public Building (string Name, string Abbreviation, double XCoordinate, double YCoordinate)
		{
			this.Name = Name;
			this.Abbreviation = Abbreviation;
			this.XCoordinate=XCoordinate;
			this.YCoordinate=YCoordinate;
		}
			
		public String toString() {
			return this.Abbreviation;           
		}

		public void setCorners(List<LatLng> Points){
			Corners = Points;
		}

		public void setDescription(string url , string classname){

			var doc = new HtmlDocument (); 
			//doc.LoadHtml (responseBody);

			using (HttpClient client = new HttpClient ()) {

				HttpResponseMessage response = client.GetAsync(url).Result;
				response.EnsureSuccessStatusCode ();
				string responseBody = response.Content.ReadAsStringAsync().Result;
				doc.LoadHtml (responseBody);

			}
			var node = doc.DocumentNode.Descendants ("div").Where (
				d => d.Attributes.Contains ("class") && d.Attributes ["class"].Value.Contains (classname));

			Description = node.First().InnerHtml;

			/*foreach (var n in node) {
					Description = n.InnerText;
				}*/
		} 
			
		public Boolean isInPolygon(LatLng point){
			double x = point.Latitude;
			double y = point.Longitude;

			int   i, j= Corners.Count-1 ;
			bool  oddNodes=false;

			double [] polyX = new double [Corners.Count];
			double [] polyY = new double [Corners.Count];

			int count = 0;
			foreach (LatLng p in Corners) {
				polyX [count] = p.Latitude;
				polyY [count] = p.Longitude;
				count++;
			}
			//polyX[0] = 45.49771432066147;
			//polyX[1] = 45.497372148435424;
			//polyX[2] = 45.4968288804749256;
			//polyX[3] = 45.49715781971825;

			//polyY[0] = -73.57902020215988;
			//polyY[1] = -73.57835501432419;
			//polyY[2] = -73.57885658740997;
			//polyY[3] = -73.57953518629074;

			for (i=0; i< Corners.Count; i++) {
				if (polyY[i]<y && polyY[j]>=y ||  polyY[j]<y && polyY[i]>=y) {
					if (polyX[i]+(y-polyY[i])/(polyY[j]-polyY[i])*(polyX[j]-polyX[i])<x) {
						oddNodes=!oddNodes; 
					}
				}
				j=i; 
			}
			return oddNodes; 
		}
	}
}

