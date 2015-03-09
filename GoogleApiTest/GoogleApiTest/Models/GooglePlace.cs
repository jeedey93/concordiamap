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

		public void setDistance(double dist){
			distanceToPlace = dist;
		}

		public double getDistance(){
			return distanceToPlace;
		}

		public LatLng getLocation(){
			return location;
		}

		public string getName(){
			return name;
		}

		public void setLocation(LatLng loc){
			location = loc;
		}

		public void setName(string name){
			this.name = name;
		}

		public override string ToString(){
			return "---------------------------------------------\nPlace - " + name + "\n" + location.ToString ()+"\nDistance = "+distanceToPlace.ToString();
		}

		public GooglePlace clone(){
			return new GooglePlace (location, name);
		}

	}
}

