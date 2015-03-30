using System;
using Android.Gms.Maps.Model;
using System.Collections.Generic;
using System.Json;

namespace GoogleApiTest
{
	public class GooglePlace
	{
		LatLng location;
		string name;
		double distanceToPlace;
		int priceLvl;
		List<string> types;
		string adress;
		double raiting;

		public GooglePlace(LatLng loc, string name){
			location = loc;
			this.name = name;
		}

		public string GetLat(){
			return location.Latitude.ToString ();
		}

		public string GetLng(){
			return location.Longitude.ToString ();
		}

		public void SetRaiting(double rate){
			raiting = rate;
		}

		public double GetRaiting(){
			return raiting;
		}

		public void SetAdress(string id){
			adress = id;
		}

		public string GetAdress(){
			return adress;
		}

		public void SetType(List<string> type){
			types= type;
		}
			

		public string GetTypes(){
			string temp = "";

			foreach (string s in types) {
				temp += s + ", ";
			}
			int index = temp.LastIndexOf (',');

			return temp.Remove(index, 1);
		}

		public int GetPriceLvl(){
			return priceLvl;
		}

		public void SetPriceLvL(int lvl){
			priceLvl = lvl;
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

