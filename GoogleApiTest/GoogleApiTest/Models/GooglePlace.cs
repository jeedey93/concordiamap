using System;
using Android.Gms.Maps.Model;

namespace GoogleApiTest
{
	public class GooglePlace
	{
		LatLng location;
		string name;
		double distanceToPlace;

		public GooglePlace(LatLng loc, string name){
			location = loc;
			this.name = name;
		}

		public void SetDistance(double dist){
			distanceToPlace = dist;
		}

		public double GetDistance(){
			return distanceToPlace;
		}

		public LatLng GetLocation(){
			return location;
		}

		public string GetName(){
			return name;
		}

		public void SetLocation(LatLng loc){
			location = loc;
		}

		public void SetName(string name){
			this.name = name;
		}

		public override string ToString(){
			return "---------------------------------------------\nPlace - " + name + "\n" + location.ToString ()+"\nDistance = "+distanceToPlace.ToString();
		}

		public GooglePlace Clone(){
			return new GooglePlace (location, name);
		}

	}
}

