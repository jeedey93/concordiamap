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
		public int BuildingImage{ get; set; }
		public string Description { get; set;}
		public int BuildingOverlay{ get; set; }
		public int OverlaySize { get; set; }
		public LatLng BuildingEntrance{ get; set; }
		public Campus Campus{ get; set; }
		public Polygon Polygon{ get; set; }

		public Building (string Name, string Abbreviation, double XCoordinate, double YCoordinate)
		{
			this.Name = Name;
			this.Abbreviation = Abbreviation;
			this.XCoordinate=XCoordinate;
			this.YCoordinate=YCoordinate;
			this.BuildingOverlay = 0;
		}
			


		public String ToString() {
			return this.Abbreviation;           
		}

		public void SetCorners(List<LatLng> Points){
			Corners = Points;
		}

		public void SetDescription(string url , string classname){

			var doc = new HtmlDocument (); 

			using (HttpClient client = new HttpClient ()) {

				HttpResponseMessage response = client.GetAsync(url).Result;
				response.EnsureSuccessStatusCode ();
				string responseBody = response.Content.ReadAsStringAsync().Result;
				doc.LoadHtml (responseBody);

			}
			var node = doc.DocumentNode.Descendants ("div").Where (
				d => d.Attributes.Contains ("class") && d.Attributes ["class"].Value.Contains (classname));

			Description = node.First().InnerHtml;
		} 
			
		public Boolean IsInPolygon(LatLng point){
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

