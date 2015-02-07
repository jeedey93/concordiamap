using System;
using Android.Gms.Maps.Model;
using System.Collections.Generic;

namespace GoogleApiTest
{
	public class Building
	{
		public string Name { get; set; }
		public string Abbreviation { get; set; }
		public double XCoordinate { get; set; }
		public double YCoordinate{ get; set; }
		public List<LatLng> Corners { get; set; }

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
			// we need 3 points Bottom Left, Bottom Right and Top left
			//Latitude X Longitude Y
			LatLng BotLeft=Points[0];
			LatLng BotRight=Points[0];
			LatLng TopLeft=Points[0];
			foreach(LatLng p in Points){
				if (p.Longitude < BotLeft.Longitude) {
					BotLeft = p;
				}

				if (p.Longitude > BotRight.Longitude) {
					BotRight = p;
				}

				if (p.Latitude > TopLeft.Latitude && TopLeft!=BotLeft && TopLeft!=BotRight) {
					TopLeft = p;
				}
			}

			if (Corners.Count < 3) {
				Corners [0] = (BotLeft);
				Corners [1] = (TopLeft);
				Corners [2] = (BotRight);
			} else {
				Corners.Add (BotLeft);

				Corners.Add (TopLeft);

				Corners.Add (BotRight);
			}
		}

		public Boolean isInPolygon(LatLng point){
			double Ax = Corners[0].Longitude, Ay = Corners[0].Latitude;
			double Bx = Corners[1].Longitude, By = Corners[1].Latitude;
			double Dx = Corners[2].Longitude, Dy = Corners[2].Latitude;

			double AMx = Ax - point.Latitude;
			double AMy = Ay - point.Longitude;
			double ABx = Ax - Bx;
			double ABy = Ay - By;
			double ADx = Ax - Dx;
			double ADy = Ay - Dy;

			double AMAB = AMx * ABx + AMy * ABy;
			double ABAB = ABx * ABx + ABy * ABy;
			double AMAD = AMx * ADx + AMy * ADy;
			double ADAD = ADx * ADx + ADy * ADy;

			Console.WriteLine ();

			if (0 < AMAB && AMAB < ABAB) {
				if (0 < AMAD && AMAD < ADAD) {
					Console.WriteLine ("Point is in rectangle");
					return true;
				}
				else Console.WriteLine ("Point is not in rectangle");
				return false;
			}
			else Console.WriteLine ("Point is not in rectangle");
			return false;

		}
	}
}

