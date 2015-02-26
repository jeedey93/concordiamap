using System;
using Android.Gms.Maps.Model;
using System.Collections.Generic;
using System.Net.Http;
using HtmlAgilityPack;
using System.Linq;

namespace GoogleApiTest
{
	public class BuildingOverlay: Building{

		public int ImageSize;
		public int ImageReference;
		

		public BuildingOverlay(string Name, string Abbreviation, double XCoordinate, double YCoordinate, int ImageSize)
		{
			string imageRef = Abbreviation + "_Logo";
			this.Name = Name;
			this.Abbreviation = Abbreviation;
			this.XCoordinate=XCoordinate;
			this.YCoordinate=YCoordinate;
			this.ImageSize = ImageSize;


		}

	}
}

