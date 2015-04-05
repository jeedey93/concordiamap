using System;
using System.Collections.Generic;
using Android.Gms.Maps.Model;

namespace GoogleApiTest
{
	public class Campus
	{
		public enum CampusEnum
		{
			LOYOLA,
			SGW
		};

		public string CampusName { get; set; }
		public List<Building> Buildings = new List<Building>();
		public LatLng ExtractionPoint { get; set; } 

		public Campus (string name, LatLng point)
		{
			CampusName = name;
			ExtractionPoint = point;
		}
	}
}

