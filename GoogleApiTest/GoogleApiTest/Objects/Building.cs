using System;

namespace GoogleApiTest
{
	public class Building
	{
		private string Name { get; set; }
		private string Abbreviation { get; set; }
		private double XCoordinate { get; set; }
		private double YCoordinate{ get; set; }

		public Building (string Name, string Abbreviation, double XCoordinate, double YCoordinate)
		{
			this.Name = Name;
			this.Abbreviation = Abbreviation;
			this.XCoordinate=XCoordinate;
			this.YCoordinate=YCoordinate;
		}
			
		public String toString() {
			return this.Name;           
		}

	}
}

