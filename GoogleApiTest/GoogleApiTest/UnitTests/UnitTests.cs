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
		public void IsInPolygonTestHall ()
		{

			LatLng testPointInside = new LatLng (45.497260, -73.578983);
			Building HallBuilding = SGW.Find (x => x.Abbreviation == "H");
			Assert.Equals (HallBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestB ()
		{

			LatLng testPointInside = new LatLng (45.497818, -73.579545);
			Building BBuilding = SGW.Find (x => x.Abbreviation == "B");
			Assert.Equals (BBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestCB ()
		{

			LatLng testPointInside = new LatLng (45.495189, -73.574308);
			Building CBBuilding = SGW.Find (x => x.Abbreviation == "CB");
			Assert.Equals (CBBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestCI ()
		{

			LatLng testPointInside = new LatLng (45.497438, -73.579957);
			Building CIBuilding = SGW.Find (x => x.Abbreviation == "CI");
			Assert.Equals (CIBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestCL ()
		{

			LatLng testPointInside = new LatLng (45.494195, -73.579295);
			Building CLBuilding = SGW.Find (x => x.Abbreviation == "CL");
			Assert.Equals (CLBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestD ()
		{

			LatLng testPointInside = new LatLng (45.497736, -73.579390);
			Building DBuilding = SGW.Find (x => x.Abbreviation == "D");
			Assert.Equals (DBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestEN ()
		{
	
			LatLng testPointInside = new LatLng (45.496823, -73.579661);
			Building ENBuilding = SGW.Find (x => x.Abbreviation == "EN");
			Assert.Equals (ENBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestEV ()
		{

			LatLng testPointInside = new LatLng (45.495572, -73.578285);
			Building EVBuilding = SGW.Find (x => x.Abbreviation == "EV");
			Assert.Equals (EVBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestFA ()
		{

			LatLng testPointInside = new LatLng (45.496801, -73.579528);
			Building FABuilding = SGW.Find (x => x.Abbreviation == "FA");
			Assert.Equals (FABuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestFB ()
		{

			LatLng testPointInside = new LatLng (45.494673, -73.577642);
			Building FBBuilding = SGW.Find (x => x.Abbreviation == "FB");
			Assert.Equals (FBBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestFG ()
		{

			LatLng testPointInside = new LatLng (45.494195, -73.578328);
			Building FGBuilding = SGW.Find (x => x.Abbreviation == "FB");
			Assert.Equals (FGBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestGM ()
		{

			LatLng testPointInside = new LatLng (45.495857, -73.578858);
			Building GMBuilding = SGW.Find (x => x.Abbreviation == "GM");
			Assert.Equals (GMBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestGN ()
		{
		
			LatLng testPointInside = new LatLng (45.493522, -73.576724);
			Building GNBuilding = SGW.Find (x => x.Abbreviation == "GN");
			Assert.Equals (GNBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestK ()
		{
		
			LatLng testPointInside = new LatLng (45.497749, -73.579496);
			Building KBuilding = SGW.Find (x => x.Abbreviation == "K");
			Assert.Equals (KBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestLB ()
		{

			LatLng testPointInside = new LatLng (45.494673, -73.577642);
			Building LBBuilding = SGW.Find (x => x.Abbreviation == "LB");
			Assert.Equals (LBBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestM ()
		{

			LatLng testPointInside = new LatLng (45.497357, -73.579789);
			Building MBuilding = SGW.Find (x => x.Abbreviation == "FB");
			Assert.Equals (MBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestMB ()
		{

			LatLng testPointInside = new LatLng (45.495270187715924, -73.57906848192215);
			Building MBBuilding = SGW.Find (x => x.Abbreviation == "MB");
			Assert.Equals (MBBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestMT ()
		{
		
			LatLng testPointInside = new LatLng (45.494408, -73.576165);
			Building MTBuilding = SGW.Find (x => x.Abbreviation == "MT");
			Assert.Equals (MTBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestMU ()
		{
	
			LatLng testPointInside = new LatLng (45.497853, -73.579622);
			Building MUBuilding = SGW.Find (x => x.Abbreviation == "MU");
			Assert.Equals (MUBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestMI ()
		{
		
			LatLng testPointInside = new LatLng (45.497700, -73.579307);
			Building MIBuilding = SGW.Find (x => x.Abbreviation == "MI");
			Assert.Equals (MIBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestOS ()
		{
		
			LatLng testPointInside = new LatLng (45.497189, -73.573149);
			Building OSBuilding = SGW.Find (x => x.Abbreviation == "OS");
			Assert.Equals (OSBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestP ()
		{
		
			LatLng testPointInside = new LatLng (45.496648, -73.579202);
			Building PBuilding = SGW.Find (x => x.Abbreviation == "P");
			Assert.Equals (PBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestPR ()
		{
		
			LatLng testPointInside = new LatLng (45.496921, -73.579917);
			Building PRBuilding = SGW.Find (x => x.Abbreviation == "PR");
			Assert.Equals (PRBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestQ ()
		{
		
			LatLng testPointInside = new LatLng (45.496604, -73.579129);
			Building QBuilding = SGW.Find (x => x.Abbreviation == "Q");
			Assert.Equals (QBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestR ()
		{
		
			LatLng testPointInside = new LatLng (45.496757, -73.579445);
			Building RBuilding = SGW.Find (x => x.Abbreviation == "R");
			Assert.Equals (RBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestRR ()
		{
		
			LatLng testPointInside = new LatLng (45.496702, -73.579380);
			Building RRBuilding = SGW.Find (x => x.Abbreviation == "RR");
			Assert.Equals (RRBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestS ()
		{

			LatLng testPointInside = new LatLng (45.497400, -73.579867);
			Building SBuilding = SGW.Find (x => x.Abbreviation == "S");
			Assert.Equals (SBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestSB ()
		{

			LatLng testPointInside = new LatLng (45.496553, -73.586140);
			Building SBBuilding = SGW.Find (x => x.Abbreviation == "SB");
			Assert.Equals (SBBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestT ()
		{
		
			LatLng testPointInside = new LatLng (45.496676, -73.579289);
			Building TBuilding = SGW.Find (x => x.Abbreviation == "T");
			Assert.Equals (TBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestTD ()
		{

			LatLng testPointInside = new LatLng (45.494667, -73.578743);
			Building TDBuilding = SGW.Find (x => x.Abbreviation == "TD");
			Assert.Equals (TDBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestV ()
		{

			LatLng testPointInside = new LatLng (45.497011, -73.579956);
			Building VBuilding = SGW.Find (x => x.Abbreviation == "V");
			Assert.Equals (VBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestVA ()
		{

			LatLng testPointInside = new LatLng (45.495730, -73.573868);
			Building VABuilding = SGW.Find (x => x.Abbreviation == "VA");
			Assert.Equals (VABuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestX ()
		{

			LatLng testPointInside = new LatLng (45.496884, -73.579699);
			Building XBuilding = SGW.Find (x => x.Abbreviation == "X");
			Assert.Equals (XBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestZ ()
		{

			LatLng testPointInside = new LatLng (45.496920, -73.579785);
			Building ZBuilding = SGW.Find (x => x.Abbreviation == "Z");
			Assert.Equals (ZBuilding, buildingManager.isInPolygon(testPointInside));

		}
		////////////////////////////
		/// LOYOLA BUILDING TESTS
		///////////////////////////
		[Test]
		public void IsInPolygonTestAD ()
		{
		
			LatLng testPointInside = new LatLng (45.458011, -73.639854);
			Building ADBuilding = Loyola.Find (x => x.Abbreviation == "AD");
			Assert.Equals (ADBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestBB ()
		{

			LatLng testPointInside = new LatLng (45.459856, -73.639320);
			Building BBBuilding = Loyola.Find (x => x.Abbreviation == "BB");
			Assert.Equals (BBBuilding, buildingManager.isInPolygon(testPointInside));

		}
		[Test]
		public void IsInPolygonTestBH ()
		{

			LatLng testPointInside = new LatLng (45.459738, -73.639154);
			Building BHBuilding = Loyola.Find (x => x.Abbreviation == "BH");
			Assert.Equals (BHBuilding, buildingManager.isInPolygon(testPointInside));

		}
		[Test]
		public void IsInPolygonTestCC ()
		{

			LatLng testPointInside = new LatLng (45.458225, -73.640425);
			Building CCBuilding = Loyola.Find (x => x.Abbreviation == "CC");
			Assert.Equals (CCBuilding, buildingManager.isInPolygon(testPointInside));

		}
		[Test]
		public void IsInPolygonTestCJ ()
		{

			LatLng testPointInside = new LatLng (45.457452, -73.640427);
			Building CJBuilding = Loyola.Find (x => x.Abbreviation == "CJ");
			Assert.Equals (CJBuilding, buildingManager.isInPolygon(testPointInside));

		}
		[Test]
		public void IsInPolygonTestDO ()
		{

			LatLng testPointInside = new LatLng (45.457595, -73.636173);
			Building DOBuilding = Loyola.Find (x => x.Abbreviation == "DO");
			Assert.Equals (DOBuilding, buildingManager.isInPolygon(testPointInside));

		}
		[Test]
		public void IsInPolygonTestFC ()
		{

			LatLng testPointInside = new LatLng (45.458487, -73.639347);
			Building FCBuilding = Loyola.Find (x => x.Abbreviation == "FC");
			Assert.Equals (FCBuilding, buildingManager.isInPolygon(testPointInside));

		}
		[Test]
		public void IsInPolygonTestGE ()
		{

			LatLng testPointInside = new LatLng (45.456910, -73.640416);
			Building GEBuilding = Loyola.Find (x => x.Abbreviation == "GE");
			Assert.Equals (GEBuilding, buildingManager.isInPolygon(testPointInside));

		}	[Test]
		public void IsInPolygonTestHA ()
		{

			LatLng testPointInside = new LatLng (45.459450, -73.641269);
			Building HABuilding = Loyola.Find (x => x.Abbreviation == "HA");
			Assert.Equals (HABuilding, buildingManager.isInPolygon(testPointInside));

		}	[Test]
		public void IsInPolygonTestHB ()
		{

			LatLng testPointInside = new LatLng (45.459234, -73.641903);
			Building HBBuilding = Loyola.Find (x => x.Abbreviation == "HB");
			Assert.Equals (HBBuilding, buildingManager.isInPolygon(testPointInside));

		}
		[Test]
		public void IsInPolygonTestHC ()
		{

			LatLng testPointInside = new LatLng (45.459669, -73.642079);
			Building HCBuilding = Loyola.Find (x => x.Abbreviation == "HC");
			Assert.Equals (HCBuilding, buildingManager.isInPolygon(testPointInside));

		}
		[Test]
		public void IsInPolygonTestJR ()
		{

			LatLng testPointInside = new LatLng (45.458408, -73.643297);
			Building JRBuilding = Loyola.Find (x => x.Abbreviation == "JR");
			Assert.Equals (JRBuilding, buildingManager.isInPolygon(testPointInside));

		}
		[Test]
		public void IsInPolygonTestPS ()
		{

			LatLng testPointInside = new LatLng (45.459649, -73.639795);
			Building PSBuilding = Loyola.Find (x => x.Abbreviation == "PS");
			Assert.Equals (PSBuilding, buildingManager.isInPolygon(testPointInside));

		}
		[Test]
		public void IsInPolygonTestPT ()
		{

			LatLng testPointInside = new LatLng (45.459324, -73.639006);
			Building PTBuilding = Loyola.Find (x => x.Abbreviation == "PT");
			Assert.Equals (PTBuilding, buildingManager.isInPolygon(testPointInside));

		}
		[Test]
		public void IsInPolygonTestPY ()
		{

			LatLng testPointInside = new LatLng (45.458906, -73.640530);
			Building PYBuilding = Loyola.Find (x => x.Abbreviation == "PY");
			Assert.Equals (PYBuilding, buildingManager.isInPolygon(testPointInside));

		}
		[Test]
		public void IsInPolygonTestRA ()
		{

			LatLng testPointInside = new LatLng (45.456703, -73.637680);
			Building RABuilding = Loyola.Find (x => x.Abbreviation == "RA");
			Assert.Equals (RABuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestRF ()
		{

			LatLng testPointInside = new LatLng (45.458479, -73.641053);
			Building RFBuilding = Loyola.Find (x => x.Abbreviation == "RF");
			Assert.Equals (RFBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestSC ()
		{

			LatLng testPointInside = new LatLng (45.459127, -73.639204);
			Building SCBuilding = Loyola.Find (x => x.Abbreviation == "SC");
			Assert.Equals (SCBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestSH ()
		{

			LatLng testPointInside = new LatLng (45.459399, -73.642394);
			Building SHBuilding = Loyola.Find (x => x.Abbreviation == "SH");
			Assert.Equals (SHBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]  
		public void IsInPolygonTestSI ()
		{

			LatLng testPointInside = new LatLng (45.457836, -73.642331);
			Building SIBuilding = Loyola.Find (x => x.Abbreviation == "SI");
			Assert.Equals (SIBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestSP ()
		{

			LatLng testPointInside = new LatLng (45.457625, -73.641703);
			Building SPBuilding = Loyola.Find (x => x.Abbreviation == "SP");
			Assert.Equals (SPBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestVE ()
		{

			LatLng testPointInside = new LatLng (45.458845, -73.638635);
			Building VEBuilding = Loyola.Find (x => x.Abbreviation == "VE");
			Assert.Equals (VEBuilding, buildingManager.isInPolygon(testPointInside));

		}

		[Test]
		public void IsInPolygonTestVL ()
		{

			LatLng testPointInside = new LatLng (45.459093, -73.638367);
			Building VLBuilding = Loyola.Find (x => x.Abbreviation == "VL");
			Assert.Equals (VLBuilding, buildingManager.isInPolygon(testPointInside));

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


