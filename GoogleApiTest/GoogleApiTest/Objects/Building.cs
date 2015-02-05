﻿using System;

namespace GoogleApiTest
{
	public class Building
	{
		public string Name { get; set; }
		public string Abbreviation { get; set; }
		public double XCoordinate { get; set; }
		public double YCoordinate{ get; set; }
		public double CenterCoordinate{ get; set; }

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

	}
}

