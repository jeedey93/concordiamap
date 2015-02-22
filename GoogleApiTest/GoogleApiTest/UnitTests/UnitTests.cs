using System;
using NUnit.Framework;
using Android.Gms.Maps.Model;
using System.Collections.Generic;
//using GoogleApiTest;

namespace GoogleApiTest
{
	[TestFixture]
	public class TestsSample
	{
		BuildingManager buildingManager = new BuildingManager ();
		List<Building> SGW;
		List<Building> Loyola;
		[SetUp]
		public void Setup ()
		{
			//BuildingManager buildingManager = new BuildingManager ();
			buildingManager.InitializeLoyolaBuildings ();
			buildingManager.InitializeSGWBuildings ();
			SGW = buildingManager.getSGWBuildings ();
			Loyola = buildingManager.getLoyolaBuildings ();
		}


		[TearDown]
		public void Tear ()
		{
		}

		[Test]
		public void IsInPolygonTest ()
		{
			Console.WriteLine ("is in Polygon");
			//test Hall building
			LatLng hallTestPointInside = new LatLng (45.4972,-73.5786);
			Building HallBuilding = SGW.Find (x => x.Abbreviation == "H");
			Assert.Equals (HallBuilding, buildingManager.isInPolygon(hallTestPointInside));

		}

		[Test]
		public void Fail ()
		{
			Assert.False (true);
		}

		[Test]
		[Ignore ("another time")]
		public void Ignore ()
		{
			Assert.True (false);
		}

		[Test]
		public void Inconclusive ()
		{
			Assert.Inconclusive ("Inconclusive");
		}
	}
}


