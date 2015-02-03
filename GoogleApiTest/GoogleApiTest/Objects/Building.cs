using System;

namespace GoogleApiTest
{
	public class Building
	{
		private string Name { get; set; }
		private int XCoordinate { get; set; }
		private int YCoordinate{ get; set; }

		public Building (string Name, int XCoordinate, int YCoordinate)
		{
			this.Name = Name;
			this.XCoordinate=XCoordinate;
			this.YCoordinate=YCoordinate;
		}
			
		public String toString() {
			return this.Name;           
		}

	}
}

