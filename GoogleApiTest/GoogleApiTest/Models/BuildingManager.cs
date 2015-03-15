using System;
using System.Collections.Generic;
using Android.Gms.Maps.Model;

namespace GoogleApiTest
{
	public class BuildingManager
	{

		List<Building> SGWBuildings = new List<Building> ();
		List<Building> LoyolaBuildings = new List<Building> ();

		public List<Building> GetSGWBuildings ()
		{
			return SGWBuildings;
		}

		public List<Building> GetLoyolaBuildings ()
		{
			return LoyolaBuildings;
		}

		public Building IsInPolygon (LatLng point)
		{

			foreach (var building in SGWBuildings) {
				if (building.IsInPolygon (point)) {
					return building;
				}
			}
			foreach (var building in LoyolaBuildings) {
				if (building.IsInPolygon (point)) {
					return building;
				}
			}
			return null;
		}

		public List<Building> InitializeSGWBuildings ()
		{
			Campus SGWCampus = new Campus ("SGW", new LatLng(45.497083, -73.578440));

			Building BBuilding = new Building ("B Building", "B", 45.497818, -73.579545);
			BBuilding.BuildingImage = Resource.Drawable.B;
			BBuilding.Corners.Add (new LatLng (45.497923, -73.579494));
			BBuilding.Corners.Add (new LatLng (45.497887, -73.579419));
			BBuilding.Corners.Add (new LatLng (45.497715, -73.579588));
			BBuilding.Corners.Add (new LatLng (45.49775, -73.579662));
			BBuilding.BuildingOverlay = Resource.Drawable.B_Logo;
			BBuilding.OverlaySize = 17;
			SGWBuildings.Add (BBuilding);


			Building CBBuilding = new Building ("CB Building", "CB", 45.495189, -73.574308);
			CBBuilding.BuildingImage = Resource.Drawable.CB;
			CBBuilding.Corners.Add (new LatLng (45.495462, -73.574258));
			CBBuilding.Corners.Add (new LatLng (45.495377, -73.57408));
			CBBuilding.Corners.Add (new LatLng (45.495383, -73.574074));
			CBBuilding.Corners.Add (new LatLng (45.495365, -73.574032));
			CBBuilding.Corners.Add (new LatLng (45.495359, -73.574038));
			CBBuilding.Corners.Add (new LatLng (45.495304, -73.573932));
			CBBuilding.Corners.Add (new LatLng (45.495226, -73.574008));
			CBBuilding.Corners.Add (new LatLng (45.495255, -73.574073));
			CBBuilding.Corners.Add (new LatLng (45.495197, -73.574129));
			CBBuilding.Corners.Add (new LatLng (45.495167, -73.574066));
			CBBuilding.Corners.Add (new LatLng (45.495092, -73.574139));
			CBBuilding.Corners.Add (new LatLng (45.49512, -73.574204));
			CBBuilding.Corners.Add (new LatLng (45.495062, -73.574265));
			CBBuilding.Corners.Add (new LatLng (45.495031, -73.574194));
			CBBuilding.Corners.Add (new LatLng (45.494957, -73.574265));
			CBBuilding.Corners.Add (new LatLng (45.495078, -73.574535));
			CBBuilding.Corners.Add (new LatLng (45.49505, -73.574568));
			CBBuilding.Corners.Add (new LatLng (45.495081, -73.574638));
			CBBuilding.BuildingOverlay = Resource.Drawable.CB_Logo;
			CBBuilding.OverlaySize = 17;
			SGWBuildings.Add (CBBuilding);


			Building CIBuilding = new Building ("CI Building", "CI", 45.497438, -73.579957);
			CIBuilding.BuildingImage = Resource.Drawable.CI;
			CIBuilding.Corners.Add (new LatLng (45.497522, -73.579937));
			CIBuilding.Corners.Add (new LatLng (45.497478, -73.579848));
			CIBuilding.Corners.Add (new LatLng (45.497368, -73.579958));
			CIBuilding.Corners.Add (new LatLng (45.497411, -73.580047));
			CIBuilding.BuildingOverlay = Resource.Drawable.CI_Logo;
			CIBuilding.OverlaySize = 17;
			SGWBuildings.Add (CIBuilding);

			Building CLBuilding = new Building ("CL Building", "CL", 45.494195, -73.579295);
			CLBuilding.BuildingImage = Resource.Drawable.CL;
			CLBuilding.Corners.Add (new LatLng (45.494478, -73.579272));
			CLBuilding.Corners.Add (new LatLng (45.494268, -73.578926));
			CLBuilding.Corners.Add (new LatLng (45.494017, -73.579237));
			CLBuilding.Corners.Add (new LatLng (45.493994, -73.579286));
			CLBuilding.Corners.Add (new LatLng (45.493985, -73.579319));
			CLBuilding.Corners.Add (new LatLng (45.49399, -73.579345));
			CLBuilding.Corners.Add (new LatLng (45.49417, -73.579649));
			CLBuilding.BuildingOverlay = Resource.Drawable.CL_Logo;
			CLBuilding.OverlaySize = 30;
			SGWBuildings.Add (CLBuilding);

			Building DBuilding = new Building ("D Building", "D", 45.497736, -73.579390);
			DBuilding.BuildingImage = Resource.Drawable.D;
			DBuilding.Corners.Add (new LatLng (45.497846, -73.579335));
			DBuilding.Corners.Add (new LatLng (45.497815, -73.57927));
			DBuilding.Corners.Add (new LatLng (45.497649, -73.579432));
			DBuilding.Corners.Add (new LatLng (45.49768, -73.579496));
			DBuilding.BuildingOverlay = Resource.Drawable.D_Logo;
			DBuilding.OverlaySize = 17;
			SGWBuildings.Add (DBuilding);

			Building ENBuilding = new Building ("EN Building", "EN", 45.496823, -73.579661);
			ENBuilding.BuildingImage = Resource.Drawable.EN;
			ENBuilding.Corners.Add (new LatLng (45.496937, -73.579579));
			ENBuilding.Corners.Add (new LatLng (45.496918, -73.579537));
			ENBuilding.Corners.Add (new LatLng (45.496909, -73.579547));
			ENBuilding.Corners.Add (new LatLng (45.496892, -73.579516));
			ENBuilding.Corners.Add (new LatLng (45.496795, -73.579614));
			ENBuilding.Corners.Add (new LatLng (45.496804, -73.579632));
			ENBuilding.Corners.Add (new LatLng (45.496674, -73.579766));
			ENBuilding.Corners.Add (new LatLng (45.4967, -73.579819));
			ENBuilding.BuildingOverlay = Resource.Drawable.EN_Logo;
			ENBuilding.OverlaySize = 13;
			SGWBuildings.Add (ENBuilding);

			Building eVBuilding = new Building ("Computer Science, Engineering and Visual Arts Integrated Complex", "EV", 45.495572, -73.578285);
			//eVBuilding.setDescription("http://www.concordia.ca/maps/buildings/ev.html", "rte");
			eVBuilding.Description = "The Computer Science, Engineering and Visual Arts Integrated Complex (EV Building)"+
				" opened in September 2005, a striking addition to Montreal’s downtown landscape.The two towers of the high-tech"+
				" complex are completely integrated with links at every floor and a common corridor";
			eVBuilding.BuildingImage = Resource.Drawable.EV;
			eVBuilding.Corners.Add (new LatLng (45.495826707314045, -73.57856959104538));
			eVBuilding.Corners.Add (new LatLng (45.49552588659263, -73.57787489891052));
			eVBuilding.Corners.Add (new LatLng (45.49553716739868, -73.57781052589417));
			eVBuilding.Corners.Add (new LatLng (45.49542623937432, -73.57765763998032));
			eVBuilding.Corners.Add (new LatLng (45.49518746136016, -73.57789367437363));
			eVBuilding.Corners.Add (new LatLng (45.49526266714314, -73.57806265354156));
			eVBuilding.Corners.Add (new LatLng (45.49527582814485, -73.57813775539398));
			eVBuilding.Corners.Add (new LatLng (45.495369835210354, -73.5784462094307));
			eVBuilding.Corners.Add (new LatLng (45.49556160913736, -73.57885658740997));
			eVBuilding.BuildingOverlay = Resource.Drawable.EV_Logo;
			eVBuilding.OverlaySize = 25;
			SGWBuildings.Add (eVBuilding);

			Building FABuilding = new Building ("FA Building", "FA", 45.496801, -73.579528);
			FABuilding.BuildingImage = Resource.Drawable.FA;
			FABuilding.Corners.Add (new LatLng (45.496865, -73.57952));
			FABuilding.Corners.Add (new LatLng (45.496828, -73.579447));
			FABuilding.Corners.Add (new LatLng (45.496745, -73.579529));
			FABuilding.Corners.Add (new LatLng (45.496783, -73.579605));
			FABuilding.BuildingOverlay = Resource.Drawable.FA_Logo;
			FABuilding.OverlaySize = 14;
			SGWBuildings.Add (FABuilding);

			Building FBBuilding = new Building ("Faubourg Tower", "FB", 45.494673, -73.577642);
			FBBuilding.BuildingImage = Resource.Drawable.FB;
			FBBuilding.Corners.Add (new LatLng (45.494696, -73.578038));
			FBBuilding.Corners.Add (new LatLng (45.494912, -73.577786));
			FBBuilding.Corners.Add (new LatLng (45.494872, -73.577713));
			FBBuilding.Corners.Add (new LatLng (45.494876, -73.577704));
			FBBuilding.Corners.Add (new LatLng (45.494837, -73.577634));
			FBBuilding.Corners.Add (new LatLng (45.494842, -73.577625));
			FBBuilding.Corners.Add (new LatLng (45.494799, -73.57755));
			FBBuilding.Corners.Add (new LatLng (45.494806, -73.57754));
			FBBuilding.Corners.Add (new LatLng (45.494763, -73.577465));
			FBBuilding.Corners.Add (new LatLng (45.494775, -73.577452));
			FBBuilding.Corners.Add (new LatLng (45.494692, -73.577309));
			FBBuilding.Corners.Add (new LatLng (45.4947, -73.5773));
			FBBuilding.Corners.Add (new LatLng (45.494654, -73.57722));
			FBBuilding.Corners.Add (new LatLng (45.494397, -73.577521));
			FBBuilding.BuildingOverlay = Resource.Drawable.FB_Logo;
			FBBuilding.OverlaySize = 25;
			SGWBuildings.Add (FBBuilding);

			Building FGBuilding = new Building ("FG Building", "FG", 45.494195, -73.578328);
			FGBuilding.BuildingImage = Resource.Drawable.FG;
			FGBuilding.Corners.Add (new LatLng (45.494694, -73.578039));
			FGBuilding.Corners.Add (new LatLng (45.494362, -73.578431));
			FGBuilding.Corners.Add (new LatLng (45.494369, -73.578443));
			FGBuilding.Corners.Add (new LatLng (45.494301, -73.578523));
			FGBuilding.Corners.Add (new LatLng (45.494293, -73.578511));
			FGBuilding.Corners.Add (new LatLng (45.493823, -73.579068));
			FGBuilding.Corners.Add (new LatLng (45.493626, -73.578728));
			FGBuilding.Corners.Add (new LatLng (45.493848, -73.578465));
			FGBuilding.Corners.Add (new LatLng (45.493836, -73.578441));
			FGBuilding.Corners.Add (new LatLng (45.493883, -73.578386));
			FGBuilding.Corners.Add (new LatLng (45.493891, -73.5784));
			FGBuilding.Corners.Add (new LatLng (45.493923, -73.578362));
			FGBuilding.Corners.Add (new LatLng (45.493912, -73.578343));
			FGBuilding.Corners.Add (new LatLng (45.494105, -73.578115));
			FGBuilding.Corners.Add (new LatLng (45.494111, -73.578126));
			FGBuilding.Corners.Add (new LatLng (45.494204, -73.578017));
			FGBuilding.Corners.Add (new LatLng (45.494186, -73.577986));
			FGBuilding.Corners.Add (new LatLng (45.494371, -73.577768));
			FGBuilding.Corners.Add (new LatLng (45.494393, -73.577802));
			FGBuilding.Corners.Add (new LatLng (45.494428, -73.577762));
			FGBuilding.Corners.Add (new LatLng (45.494389, -73.57769));
			FGBuilding.Corners.Add (new LatLng (45.494452, -73.577618));
			FGBuilding.BuildingOverlay = Resource.Drawable.FG_Logo;
			FGBuilding.OverlaySize = 25;
			SGWBuildings.Add (FGBuilding);

			Building gMBuilding = new Building ("Guy-Metro Building", "GM", 45.495857, -73.578858);
			//gMBuilding.setDescription ("http://www.concordia.ca/maps/buildings/gm.html", "rte");
			gMBuilding.Description = "Concordia has gradually reclaimed rented spaces in the Guy Métro Building to accommodate "+
				"Concordia administrative offices and there are currently very few external tenants with long-term leases. "+
				"The Office of the President and members of the Cabinet took over the eighth floor in 2005; other administrative "+
				"units have also recently joined long-time residents of the building.\n\nRenovations are ";
			gMBuilding.BuildingImage = Resource.Drawable.GM;
			gMBuilding.Corners.Add (new LatLng (45.49611, -73.57888));
			gMBuilding.Corners.Add (new LatLng (45.49595, -73.57852));
			gMBuilding.Corners.Add (new LatLng (45.49562, -73.57884));
			gMBuilding.Corners.Add (new LatLng (45.49579, -73.57919));
			gMBuilding.BuildingOverlay = Resource.Drawable.GM_Logo;
			gMBuilding.OverlaySize = 20;
			SGWBuildings.Add (gMBuilding);

			Building GNBuilding = new Building ("Grey Nuns Building", "GN", 45.493522, -73.576724);
			//GNBuilding.setDescription ("http://www.concordia.ca/maps/buildings/gn.html", "rte");
			GNBuilding.Description = "Nearly 150 years ago the Sisters of Charity began construction of the Motherhouse of the "+
				"Grey Nuns of Montreal. Acquired by Concordia University, this landmark has a new purpose, all the while preserving "+
				"its outstanding heritage.";
			GNBuilding.BuildingImage = Resource.Drawable.GN;
			GNBuilding.Corners.Add (new LatLng (45.49439, -73.577132));
			GNBuilding.Corners.Add (new LatLng (45.494019, -73.576357));
			GNBuilding.Corners.Add (new LatLng (45.494122, -73.576256));
			GNBuilding.Corners.Add (new LatLng (45.494035, -73.576074));
			GNBuilding.Corners.Add (new LatLng (45.493933, -73.576172));
			GNBuilding.Corners.Add (new LatLng (45.493716, -73.57572));
			GNBuilding.Corners.Add (new LatLng (45.493602, -73.575832));
			GNBuilding.Corners.Add (new LatLng (45.49382, -73.576294));
			GNBuilding.Corners.Add (new LatLng (45.493492, -73.576616));
			GNBuilding.Corners.Add (new LatLng (45.493472, -73.576574));
			GNBuilding.Corners.Add (new LatLng (45.493344, -73.576704));
			GNBuilding.Corners.Add (new LatLng (45.493367, -73.576755));
			GNBuilding.Corners.Add (new LatLng (45.493033, -73.577081));
			GNBuilding.Corners.Add (new LatLng (45.492934, -73.576878));
			GNBuilding.Corners.Add (new LatLng (45.492946, -73.576846));
			GNBuilding.Corners.Add (new LatLng (45.492925, -73.576798));
			GNBuilding.Corners.Add (new LatLng (45.492899, -73.576787));
			GNBuilding.Corners.Add (new LatLng (45.492761, -73.576495));
			GNBuilding.Corners.Add (new LatLng (45.492625, -73.576629));
			GNBuilding.Corners.Add (new LatLng (45.492762, -73.576928));
			GNBuilding.Corners.Add (new LatLng (45.492686, -73.577007));
			GNBuilding.Corners.Add (new LatLng (45.492718, -73.577072));
			GNBuilding.Corners.Add (new LatLng (45.49277, -73.577021));
			GNBuilding.Corners.Add (new LatLng (45.492821, -73.577017));
			GNBuilding.Corners.Add (new LatLng (45.492904, -73.577192));
			GNBuilding.Corners.Add (new LatLng (45.492855, -73.57724));
			GNBuilding.Corners.Add (new LatLng (45.492935, -73.577408));
			GNBuilding.Corners.Add (new LatLng (45.492999, -73.577349));
			GNBuilding.Corners.Add (new LatLng (45.493105, -73.577569));
			GNBuilding.Corners.Add (new LatLng (45.493203, -73.577474));
			GNBuilding.Corners.Add (new LatLng (45.493104, -73.577263));
			GNBuilding.Corners.Add (new LatLng (45.493188, -73.57718));
			GNBuilding.Corners.Add (new LatLng (45.493198, -73.5772));
			GNBuilding.Corners.Add (new LatLng (45.493363, -73.577036));
			GNBuilding.Corners.Add (new LatLng (45.493354, -73.577015));
			GNBuilding.Corners.Add (new LatLng (45.493439, -73.57693));
			GNBuilding.Corners.Add (new LatLng (45.493479, -73.577013));
			GNBuilding.Corners.Add (new LatLng (45.493453, -73.577039));
			GNBuilding.Corners.Add (new LatLng (45.493531, -73.577204));
			GNBuilding.Corners.Add (new LatLng (45.493579, -73.577158));
			GNBuilding.Corners.Add (new LatLng (45.493603, -73.577209));
			GNBuilding.Corners.Add (new LatLng (45.493583, -73.57723));
			GNBuilding.Corners.Add (new LatLng (45.493616, -73.577298));
			GNBuilding.Corners.Add (new LatLng (45.493727, -73.577192));
			GNBuilding.Corners.Add (new LatLng (45.493674, -73.577076));
			GNBuilding.Corners.Add (new LatLng (45.493728, -73.577022));
			GNBuilding.Corners.Add (new LatLng (45.493669, -73.5769));
			GNBuilding.Corners.Add (new LatLng (45.493616, -73.576952));
			GNBuilding.Corners.Add (new LatLng (45.493554, -73.576824));
			GNBuilding.Corners.Add (new LatLng (45.4939, -73.576485));
			GNBuilding.Corners.Add (new LatLng (45.494194, -73.5771));
			GNBuilding.Corners.Add (new LatLng (45.494043, -73.577248));
			GNBuilding.Corners.Add (new LatLng (45.494118, -73.577399));
			GNBuilding.BuildingOverlay = Resource.Drawable.GN_Logo;
			GNBuilding.OverlaySize = 17;
			SGWBuildings.Add (GNBuilding);

			Building HallBuilding = new Building ("Henry F.Hall Building", "H", 45.497260, -73.578983);
			//HallBuilding.setDescription ("http://www.concordia.ca/maps/buildings/h.html", "rte");
			HallBuilding.Description = "The Henry F. Hall Building is a high-density hub of Concordia’s downtown campus. It is a "+
				"utilitarian, cube-shaped, 1960s-style high-rise university building made of pre-fabricated stressed concrete.";
			HallBuilding.BuildingImage = Resource.Drawable.Hall;
			HallBuilding.Corners.Add (new LatLng (45.49770868047681, -73.57903227210045));
			HallBuilding.Corners.Add (new LatLng (45.497366508216466, -73.57833489775658));
			HallBuilding.Corners.Add (new LatLng (45.4968288804749256, -73.57885658740997));
			HallBuilding.Corners.Add (new LatLng (45.49715787001796, -73.579544390347004));
			HallBuilding.BuildingOverlay = Resource.Drawable.Hall_Logo;
			HallBuilding.OverlaySize = 50;
			SGWBuildings.Add (HallBuilding);


			Building KBuilding = new Building ("K Building", "K", 45.497749, -73.579496);
			KBuilding.BuildingImage = Resource.Drawable.K;
			KBuilding.Corners.Add (new LatLng (45.497885, -73.579421));
			KBuilding.Corners.Add (new LatLng (45.497847, -73.579337));
			KBuilding.Corners.Add (new LatLng (45.497592, -73.579583));
			KBuilding.Corners.Add (new LatLng (45.497631, -73.579665));
			KBuilding.BuildingOverlay = Resource.Drawable.K_Logo;
			KBuilding.OverlaySize = 17;
			SGWBuildings.Add (KBuilding);

			Building libraryBuilding = new Building ("McConnell Library Building", "LB", 45.496775, -73.577904);
			//libraryBuilding.setDescription ("http://www.concordia.ca/maps/buildings/lb.html", "rte");
			libraryBuilding.Description = "The J. W. McConnell Building opened in 1992 to house the R. Howard Webster Library, "+
				"teaching and research facilities, the Leonard and Bina Ellen Art Gallery, the de Sève Cinema and academic and "+
				"administrative offices.";
			libraryBuilding.BuildingImage = Resource.Drawable.LB;
			libraryBuilding.Corners.Add (new LatLng (45.496721, -73.578588));
			libraryBuilding.Corners.Add (new LatLng (45.4967, -73.578549));
			libraryBuilding.Corners.Add (new LatLng (45.496673, -73.578574));
			libraryBuilding.Corners.Add (new LatLng (45.496555, -73.578326));
			libraryBuilding.Corners.Add (new LatLng (45.49658, -73.578301));
			libraryBuilding.Corners.Add (new LatLng (45.496493, -73.578117));
			libraryBuilding.Corners.Add (new LatLng (45.496465, -73.57814));
			libraryBuilding.Corners.Add (new LatLng (45.496279, -73.577752));
			libraryBuilding.Corners.Add (new LatLng (45.496271, -73.577759));
			libraryBuilding.Corners.Add (new LatLng (45.496246, -73.577708));
			libraryBuilding.Corners.Add (new LatLng (45.496487, -73.577473));
			libraryBuilding.Corners.Add (new LatLng (45.4966, -73.577711));
			libraryBuilding.Corners.Add (new LatLng (45.496661, -73.57765));
			libraryBuilding.Corners.Add (new LatLng (45.496622, -73.577568));
			libraryBuilding.Corners.Add (new LatLng (45.496694, -73.577495));
			libraryBuilding.Corners.Add (new LatLng (45.496692, -73.577487));
			libraryBuilding.Corners.Add (new LatLng (45.496883, -73.577302));
			libraryBuilding.Corners.Add (new LatLng (45.497, -73.577545));
			libraryBuilding.Corners.Add (new LatLng (45.496977, -73.577568));
			libraryBuilding.Corners.Add (new LatLng (45.496999, -73.577617));
			libraryBuilding.Corners.Add (new LatLng (45.496991, -73.577627));
			libraryBuilding.Corners.Add (new LatLng (45.496999, -73.577644));
			libraryBuilding.Corners.Add (new LatLng (45.497039, -73.577602));
			libraryBuilding.Corners.Add (new LatLng (45.497112, -73.577757));
			libraryBuilding.Corners.Add (new LatLng (45.497074, -73.577797));
			libraryBuilding.Corners.Add (new LatLng (45.497082, -73.577817));
			libraryBuilding.Corners.Add (new LatLng (45.497089, -73.577811));
			libraryBuilding.Corners.Add (new LatLng (45.497114, -73.577859));
			libraryBuilding.Corners.Add (new LatLng (45.497139, -73.577833));
			libraryBuilding.Corners.Add (new LatLng (45.497254, -73.578069));
			libraryBuilding.Corners.Add (new LatLng (45.49702, -73.578295));
			libraryBuilding.Corners.Add (new LatLng (45.497002, -73.578258));
			libraryBuilding.Corners.Add (new LatLng (45.496964, -73.578294));
			libraryBuilding.Corners.Add (new LatLng (45.496941, -73.578248));
			libraryBuilding.Corners.Add (new LatLng (45.496889, -73.578296));
			libraryBuilding.Corners.Add (new LatLng (45.496911, -73.578343));
			libraryBuilding.Corners.Add (new LatLng (45.496873, -73.578378));
			libraryBuilding.Corners.Add (new LatLng (45.49689, -73.578414));
			libraryBuilding.BuildingOverlay = Resource.Drawable.LB_Logo;
			libraryBuilding.OverlaySize = 40;
			SGWBuildings.Add (libraryBuilding);

			Building MBuilding = new Building ("M Building", "M", 45.497357, -73.579789);
			MBuilding.BuildingImage = Resource.Drawable.M;
			MBuilding.Corners.Add (new LatLng (45.497433, -73.579765));
			MBuilding.Corners.Add (new LatLng (45.497396, -73.579688));
			MBuilding.Corners.Add (new LatLng (45.497287, -73.579797));
			MBuilding.Corners.Add (new LatLng (45.497325, -73.579871));
			MBuilding.BuildingOverlay = Resource.Drawable.M_Logo;
			MBuilding.OverlaySize = 13;
			SGWBuildings.Add (MBuilding);

			Building JMSBBuilding = new Building ("John Molson School of Business Building", "MB", 45.495270187715924, -73.57906848192215);
			JMSBBuilding.SetDescription ("http://www.concordia.ca/maps/buildings/mb.html", "rte");
			JMSBBuilding.Description = "In 2009 the John Molson School of Business Building officially opened on the corner of Guy "+
				"and de Maisonneuve. It includes digitally equipped teaching amphitheatres and classrooms, faculty and graduate "+
				"student offices, the Office of the Dean, student and faculty social space, as well as space for privatized programs. "+
				"Special features also include case study rooms designed for group work, and laboratories for consumer behaviour research.";
			JMSBBuilding.BuildingImage = Resource.Drawable.MB;
			JMSBBuilding.Corners.Add (new LatLng (45.495624, -73.57928));
			JMSBBuilding.Corners.Add (new LatLng (45.495394, -73.579538));
			JMSBBuilding.Corners.Add (new LatLng (45.495381, -73.579507));
			JMSBBuilding.Corners.Add (new LatLng (45.495312, -73.57959));
			JMSBBuilding.Corners.Add (new LatLng (45.495179, -73.579335));
			JMSBBuilding.Corners.Add (new LatLng (45.495235, -73.579267));
			JMSBBuilding.Corners.Add (new LatLng (45.495001, -73.578826));
			JMSBBuilding.Corners.Add (new LatLng (45.495064, -73.578755));
			JMSBBuilding.Corners.Add (new LatLng (45.495086, -73.578792));
			JMSBBuilding.Corners.Add (new LatLng (45.49513, -73.578747));
			JMSBBuilding.Corners.Add (new LatLng (45.495105, -73.578686));
			JMSBBuilding.Corners.Add (new LatLng (45.495259, -73.578516));
			JMSBBuilding.BuildingOverlay = Resource.Drawable.MB_Logo;
			JMSBBuilding.OverlaySize = 25;
			SGWBuildings.Add (JMSBBuilding);

			Building MTBuilding = new Building ("Montefiore Building", "MT", 45.494408, -73.576165);
			MTBuilding.BuildingImage = Resource.Drawable.MT;
			MTBuilding.Corners.Add (new LatLng (45.494553, -73.5762));
			MTBuilding.Corners.Add (new LatLng (45.494463, -73.575983));
			MTBuilding.Corners.Add (new LatLng (45.494305, -73.576125));
			MTBuilding.Corners.Add (new LatLng (45.494397, -73.576333));
			MTBuilding.BuildingOverlay = Resource.Drawable.MT_Logo;
			MTBuilding.OverlaySize = 15;
			SGWBuildings.Add (MTBuilding);

			Building MUBuilding = new Building ("MU Building", "MU", 45.497853, -73.579622);
			MUBuilding.BuildingImage = Resource.Drawable.MU;
			MUBuilding.Corners.Add (new LatLng (45.497963, -73.579571));
			MUBuilding.Corners.Add (new LatLng (45.497929, -73.579496));
			MUBuilding.Corners.Add (new LatLng (45.497753, -73.579666));
			MUBuilding.Corners.Add (new LatLng (45.497789, -73.579739));
			MUBuilding.BuildingOverlay = Resource.Drawable.MU_Logo;
			MUBuilding.OverlaySize = 11;
			SGWBuildings.Add (MUBuilding);

			Building MIBuilding = new Building ("MI Building", "MI", 45.497700, -73.579307);
			MIBuilding.BuildingImage = Resource.Drawable.MI;
			MIBuilding.Corners.Add (new LatLng (45.497813, -73.579266));
			MIBuilding.Corners.Add (new LatLng (45.497769, -73.579173));
			MIBuilding.Corners.Add (new LatLng (45.497601, -73.579336));
			MIBuilding.Corners.Add (new LatLng (45.497645, -73.579429));
			MIBuilding.BuildingOverlay = Resource.Drawable.MI_Logo;
			MIBuilding.OverlaySize = 15;
			SGWBuildings.Add (MIBuilding);

			Building OSBuilding = new Building ("OS Building", "OS", 45.497189, -73.573149);
			OSBuilding.BuildingImage = Resource.Drawable.OS;
			OSBuilding.Corners.Add (new LatLng (45.497291, -73.57319));
			OSBuilding.Corners.Add (new LatLng (45.497203, -73.573013));
			OSBuilding.Corners.Add (new LatLng (45.4971, -73.573119));
			OSBuilding.Corners.Add (new LatLng (45.497186, -73.573295));
			OSBuilding.BuildingOverlay = Resource.Drawable.OS_Logo;
			OSBuilding.OverlaySize = 15;
			SGWBuildings.Add (OSBuilding);

			Building PBuilding = new Building ("P Building", "P", 45.496648, -73.579202);
			PBuilding.BuildingImage = Resource.Drawable.P;
			PBuilding.Corners.Add (new LatLng (45.496719, -73.579193));
			PBuilding.Corners.Add (new LatLng (45.496682, -73.579117));
			PBuilding.Corners.Add (new LatLng (45.496583, -73.579217));
			PBuilding.Corners.Add (new LatLng (45.496619, -73.57929));
			PBuilding.BuildingOverlay = Resource.Drawable.P_Logo;
			PBuilding.OverlaySize = 13;
			SGWBuildings.Add (PBuilding);

			Building PRBuilding = new Building ("PR Building", "PR", 45.496921, -73.579917);
			PRBuilding.BuildingImage = Resource.Drawable.PR;
			PRBuilding.Corners.Add (new LatLng (45.49703, -73.579868));
			PRBuilding.Corners.Add (new LatLng (45.496989, -73.579783));
			PRBuilding.Corners.Add (new LatLng (45.496796, -73.579975));
			PRBuilding.Corners.Add (new LatLng (45.496839, -73.580062));
			PRBuilding.BuildingOverlay = Resource.Drawable.PR_Logo;
			PRBuilding.OverlaySize = 14;
			SGWBuildings.Add (PRBuilding);

			Building QBuilding = new Building ("Q Building", "Q", 45.496604, -73.579129);
			//QBuilding.setDescription ("http://www.concordia.ca/maps/buildings/quartier-concordia.html", "rte");
			QBuilding.Description = "Quartier Concordia will transform the Sir George Williams campus from a collection of scattered "+
				"buildings into a welcoming and cohesive urban campus in the area bordered generally by Sherbrooke, Guy, René-Lévesque, "+
				"and Bishop.\n\nThe goals of the Quartier Concordia project include improving the use of outdoor spaces, stimulating "+
				"street life, and providing respite for the Concordia community and the public.";
			QBuilding.BuildingImage = Resource.Drawable.Q;
			QBuilding.Corners.Add (new LatLng (45.496683, -73.579112));
			QBuilding.Corners.Add (new LatLng (45.496653, -73.579054));
			QBuilding.Corners.Add (new LatLng (45.496549, -73.579156));
			QBuilding.Corners.Add (new LatLng (45.496578, -73.579214));
			QBuilding.BuildingOverlay = Resource.Drawable.Q_Logo;
			QBuilding.OverlaySize = 14;
			SGWBuildings.Add (QBuilding);

			Building RBuilding = new Building ("R Building", "R", 45.496757, -73.579445);
			RBuilding.BuildingImage = Resource.Drawable.R;
			RBuilding.Corners.Add (new LatLng (45.496826, -73.579442));
			RBuilding.Corners.Add (new LatLng (45.496787, -73.57936));
			RBuilding.Corners.Add (new LatLng (45.4967, -73.579447));
			RBuilding.Corners.Add (new LatLng (45.496739, -73.579529));
			RBuilding.BuildingOverlay = Resource.Drawable.R_Logo;
			RBuilding.OverlaySize = 15;
			SGWBuildings.Add (RBuilding);

			Building RRBuilding = new Building ("RR Building", "RR", 45.496702, -73.579380);
			RRBuilding.BuildingImage = Resource.Drawable.RR;
			RRBuilding.Corners.Add (new LatLng (45.496783, -73.579358));
			RRBuilding.Corners.Add (new LatLng (45.496743, -73.579274));
			RRBuilding.Corners.Add (new LatLng (45.496612, -73.579405));
			RRBuilding.Corners.Add (new LatLng (45.496653, -73.579488));
			RRBuilding.BuildingOverlay = Resource.Drawable.RR_Logo;
			RRBuilding.OverlaySize = 11;
			SGWBuildings.Add (RRBuilding);

			Building SBuilding = new Building ("S Building", "S", 45.497400, -73.579867);
			SBuilding.BuildingImage = Resource.Drawable.S;
			SBuilding.Corners.Add (new LatLng (45.497488, -73.579834));
			SBuilding.Corners.Add (new LatLng (45.497458, -73.579771));
			SBuilding.Corners.Add (new LatLng (45.497435, -73.579792));
			SBuilding.Corners.Add (new LatLng (45.497425, -73.579775));
			SBuilding.Corners.Add (new LatLng (45.497328, -73.579874));
			SBuilding.Corners.Add (new LatLng (45.497365, -73.579953));
			SBuilding.BuildingOverlay = Resource.Drawable.S_Logo;
			SBuilding.OverlaySize = 15;
			SGWBuildings.Add (SBuilding);

			Building SBBuilding = new Building ("Samuel Bronfman Building", "SB", 45.496553, -73.586140);
			SBBuilding.BuildingImage = Resource.Drawable.SB;
			SBBuilding.Corners.Add (new LatLng (45.496697, -73.586202));
			SBBuilding.Corners.Add (new LatLng (45.496665, -73.586129));
			SBBuilding.Corners.Add (new LatLng (45.496672, -73.586121));
			SBBuilding.Corners.Add (new LatLng (45.496595, -73.585944));
			SBBuilding.Corners.Add (new LatLng (45.496587, -73.58595));
			SBBuilding.Corners.Add (new LatLng (45.496563, -73.585901));
			SBBuilding.Corners.Add (new LatLng (45.496523, -73.58591));
			SBBuilding.Corners.Add (new LatLng (45.496522, -73.585894));
			SBBuilding.Corners.Add (new LatLng (45.496507, -73.585852));
			SBBuilding.Corners.Add (new LatLng (45.496481, -73.585854));
			SBBuilding.Corners.Add (new LatLng (45.496448, -73.586316));
			SBBuilding.Corners.Add (new LatLng (45.496512, -73.586324));
			SBBuilding.Corners.Add (new LatLng (45.496517, -73.586248));
			SBBuilding.Corners.Add (new LatLng (45.496534, -73.586248));
			SBBuilding.Corners.Add (new LatLng (45.496532, -73.586282));
			SBBuilding.Corners.Add (new LatLng (45.496551, -73.586323));
			SBBuilding.BuildingOverlay = Resource.Drawable.SB_Logo;
			SBBuilding.OverlaySize = 17;
			SGWBuildings.Add (SBBuilding);

			Building TBuilding = new Building ("T Building", "T", 45.496676, -73.579289);
			TBuilding.BuildingImage = Resource.Drawable.T;
			TBuilding.Corners.Add (new LatLng (45.496731, -73.579283));
			TBuilding.Corners.Add (new LatLng (45.496698, -73.579218));
			TBuilding.Corners.Add (new LatLng (45.496625, -73.579292));
			TBuilding.Corners.Add (new LatLng (45.496656, -73.579359));
			TBuilding.BuildingOverlay = Resource.Drawable.T_Logo;
			TBuilding.OverlaySize = 13;
			SGWBuildings.Add (TBuilding);

			Building TDBuilding = new Building ("Toronto Dominion Building", "TD", 45.494667, -73.578743);
			//TDBuilding.setDescription ("http://www.concordia.ca/maps/buildings/td.html", "rte");
			TDBuilding.Description = "The classic bank building at Guy and Ste-Catherine opened in 1903 as a branch of the Bank of "+
				"Toronto. (The Bank became the Toronto-Dominion Bank in 1954 and TD Canada Trust in 2000). The building was one of "+
				"the first commercial projects of the Montreal architectural firm Ross and MacFarlane.";
			TDBuilding.BuildingImage = Resource.Drawable.TD;
			TDBuilding.Corners.Add (new LatLng (45.494838, -73.578827));
			TDBuilding.Corners.Add (new LatLng (45.494654, -73.578513));
			TDBuilding.Corners.Add (new LatLng (45.494533, -73.578653));
			TDBuilding.Corners.Add (new LatLng (45.494724, -73.578974));
			TDBuilding.BuildingOverlay = Resource.Drawable.TD_Logo;
			TDBuilding.OverlaySize = 17;
			SGWBuildings.Add (TDBuilding);

			Building VBuilding = new Building ("V Building", "V", 45.497011, -73.579956);
			VBuilding.BuildingImage = Resource.Drawable.V;
			VBuilding.Corners.Add (new LatLng (45.49709, -73.579941));
			VBuilding.Corners.Add (new LatLng (45.497049, -73.57986));
			VBuilding.Corners.Add (new LatLng (45.497034, -73.579874));
			VBuilding.Corners.Add (new LatLng (45.497032, -73.579871));
			VBuilding.Corners.Add (new LatLng (45.496943, -73.57996));
			VBuilding.Corners.Add (new LatLng (45.496984, -73.580046));
			VBuilding.BuildingOverlay = Resource.Drawable.V_Logo;
			VBuilding.OverlaySize = 13;
			SGWBuildings.Add (VBuilding);

			Building VABuilding = new Building ("Visual Arts Building", "VA", 45.495730, -73.573868);
			VABuilding.BuildingImage = Resource.Drawable.VA;
			VABuilding.Corners.Add (new LatLng (45.495434, -73.57375));
			VABuilding.Corners.Add (new LatLng (45.495702, -73.574293));
			VABuilding.Corners.Add (new LatLng (45.496209, -73.573787));
			VABuilding.Corners.Add (new LatLng (45.49609, -73.573543));
			VABuilding.Corners.Add (new LatLng (45.495846, -73.573785));
			VABuilding.Corners.Add (new LatLng (45.495698, -73.573482));
			VABuilding.BuildingOverlay = Resource.Drawable.VA_Logo;
			VABuilding.OverlaySize = 35;
			SGWBuildings.Add (VABuilding);

			Building XBuilding = new Building ("X Building", "X", 45.496884, -73.579699);
			XBuilding.BuildingImage = Resource.Drawable.X;
			XBuilding.Corners.Add (new LatLng (45.496906, -73.579615));
			XBuilding.Corners.Add (new LatLng (45.496823, -73.579701));
			XBuilding.Corners.Add (new LatLng (45.496859, -73.579776));
			XBuilding.Corners.Add (new LatLng (45.496944, -73.579691));
			XBuilding.BuildingOverlay = Resource.Drawable.X_Logo;
			XBuilding.OverlaySize = 13;
			SGWBuildings.Add (XBuilding);

			Building ZBuilding = new Building ("Z Building", "Z", 45.496920, -73.579785);
			ZBuilding.BuildingImage = Resource.Drawable.Z;
			ZBuilding.Corners.Add (new LatLng (45.49699, -73.579776));
			ZBuilding.Corners.Add (new LatLng (45.496948, -73.579693));
			ZBuilding.Corners.Add (new LatLng (45.496859, -73.579781));
			ZBuilding.Corners.Add (new LatLng (45.496901, -73.579866));
			ZBuilding.BuildingOverlay = Resource.Drawable.Z_Logo;
			ZBuilding.OverlaySize = 14;
			SGWBuildings.Add (ZBuilding);


			//Assign buildings to SGW Campus
			foreach (Building sgwBuilding in SGWBuildings) {
				sgwBuilding.Campus = SGWCampus;
				SGWCampus.Buildings.Add (sgwBuilding);
			}

			return SGWBuildings;
		}

		public List<Building> InitializeLoyolaBuildings ()
		{
			Campus LoyolaCampus = new Campus ("LOYOLA", new LatLng(45.457683, -73.638978));

			Building AdministrationBuilding = new Building ("Administration Building", "AD", 45.458011, -73.639854);
			AdministrationBuilding.BuildingImage = Resource.Drawable.AD;
			AdministrationBuilding.Corners.Add (new LatLng (45.45783350439407, -73.6401692032814));
			AdministrationBuilding.Corners.Add (new LatLng (45.45789464961923, -73.64012360572815));
			AdministrationBuilding.Corners.Add (new LatLng (45.45786783979789, -73.64004917442799));
			AdministrationBuilding.Corners.Add (new LatLng (45.45800000739008, -73.63994590938091));
			AdministrationBuilding.Corners.Add (new LatLng (45.45800894397767, -73.63996870815754));
			AdministrationBuilding.Corners.Add (new LatLng (45.45803057992068, -73.63995127379894));
			AdministrationBuilding.Corners.Add (new LatLng (45.458053156548004, -73.64001162350178));
			AdministrationBuilding.Corners.Add (new LatLng (45.45808514008792, -73.63998681306839));
			AdministrationBuilding.Corners.Add (new LatLng (45.45806350416585, -73.63993182778358));
			AdministrationBuilding.Corners.Add (new LatLng (45.458083258703724, -73.63991439342499));
			AdministrationBuilding.Corners.Add (new LatLng (45.45807432212791, -73.63989293575287));
			AdministrationBuilding.Corners.Add (new LatLng (45.458190027157706, -73.63980039954185));
			AdministrationBuilding.Corners.Add (new LatLng (45.458216366480876, -73.63986678421497));
			AdministrationBuilding.Corners.Add (new LatLng (45.45828127404625, -73.63981649279594));
			AdministrationBuilding.Corners.Add (new LatLng (45.458166980239795, -73.63952547311783));
			AdministrationBuilding.Corners.Add (new LatLng (45.458113360843356, -73.63956704735756));
			AdministrationBuilding.Corners.Add (new LatLng (45.4581481664223, -73.63965958356857));
			AdministrationBuilding.Corners.Add (new LatLng (45.45800612189753, -73.6397722363472));
			AdministrationBuilding.Corners.Add (new LatLng (45.45800000739008, -73.63975681364536));
			AdministrationBuilding.Corners.Add (new LatLng (45.45793321811177, -73.6398084461689));
			AdministrationBuilding.Corners.Add (new LatLng (45.4579388622792, -73.63982386887074));
			AdministrationBuilding.Corners.Add (new LatLng (45.45781610151019, -73.63992109894753));
			AdministrationBuilding.Corners.Add (new LatLng (45.45777894398335, -73.63982856273651));
			AdministrationBuilding.Corners.Add (new LatLng (45.45771826898183, -73.63987617194653));
			AdministrationBuilding.BuildingOverlay = Resource.Drawable.AD_Logo;
			AdministrationBuilding.OverlaySize = 25;
			LoyolaBuildings.Add (AdministrationBuilding);

			Building BBBuilding = new Building ("BB Building", "BB", 45.459856, -73.639320);
			BBBuilding.BuildingImage = Resource.Drawable.BB;
			BBBuilding.Corners.Add (new LatLng (45.45983338922264, -73.63943159580231));
			BBBuilding.Corners.Add (new LatLng (45.45995191262564, -73.63935649394989));
			BBBuilding.Corners.Add (new LatLng (45.459923692790376, -73.63925188779831));
			BBBuilding.Corners.Add (new LatLng (45.45979764402045, -73.63933771848679));
			BBBuilding.BuildingOverlay = Resource.Drawable.BB_Logo;
			BBBuilding.OverlaySize = 14;
			LoyolaBuildings.Add (BBBuilding);

			Building BHBuilding = new Building ("BH Building", "BH", 45.459738, -73.639154);
			BHBuilding.BuildingImage = Resource.Drawable.BH;
			BHBuilding.Corners.Add (new LatLng (45.459720509559546, -73.63926127552986));
			BHBuilding.Corners.Add (new LatLng (45.459816457287566, -73.6391781270504));
			BHBuilding.Corners.Add (new LatLng (45.459755314146435, -73.6390346288681));
			BHBuilding.Corners.Add (new LatLng (45.45965936631435, -73.63911241292953));
			BHBuilding.BuildingOverlay = Resource.Drawable.BH_Logo;
			BHBuilding.OverlaySize = 15;
			LoyolaBuildings.Add (BHBuilding);

			Building CentralBuilding = new Building ("Central Building", "CC", 45.458225, -73.640425);
			CentralBuilding.BuildingImage = Resource.Drawable.CC;
			CentralBuilding.Corners.Add (new LatLng (45.45829444368806, -73.64083841443062));
			CentralBuilding.Corners.Add (new LatLng (45.45844871640609, -73.64071771502495));
			CentralBuilding.Corners.Add (new LatLng (45.458241765102315, -73.64017724990845));
			CentralBuilding.Corners.Add (new LatLng (45.45823612096522, -73.64018127322197));
			CentralBuilding.Corners.Add (new LatLng (45.4581429926214, -73.63994121551514));
			CentralBuilding.Corners.Add (new LatLng (45.45799906669659, -73.64005655050278));
			CentralBuilding.Corners.Add (new LatLng (45.45809125458613, -73.6402952671051));
			CentralBuilding.Corners.Add (new LatLng (45.45808749181809, -73.64030063152313));
			CentralBuilding.BuildingOverlay = Resource.Drawable.CC_Logo;
			CentralBuilding.OverlaySize = 14;
			LoyolaBuildings.Add (CentralBuilding);


			Building CJBuilding = new Building ("Communication & Journalism Building", "CJ", 45.457452, -73.640427);
			//CJBuilding.setDescription ("http://www.concordia.ca/maps/buildings/cj.html", "rte");
			CJBuilding.Description = "The completely renovated and expanded Communication Studies and Journalism Building opened "+
				"in September 2005. This building began life in 1962 as the Drummond Science Complex. It now houses state-of-the-art "+
				"pecialized facilities for Communications and Journalism, open lounge space, the bookstore for the Loyola Campus, and a"+
				"small café.";
			CJBuilding.BuildingImage = Resource.Drawable.CJ;
			CJBuilding.Corners.Add (new LatLng (45.4574214779174, -73.64024430513382));
			CJBuilding.Corners.Add (new LatLng (45.45737162062813, -73.6401142179966));
			CJBuilding.Corners.Add (new LatLng (45.457306712015665, -73.64007599651814));
			CJBuilding.Corners.Add (new LatLng (45.457299186374605, -73.6400954425335));
			CJBuilding.Corners.Add (new LatLng (45.45723145555984, -73.64004850387573));
			CJBuilding.Corners.Add (new LatLng (45.45722298920231, -73.6399707198143));
			CJBuilding.Corners.Add (new LatLng (45.457232396266164, -73.63990634679794));
			CJBuilding.Corners.Add (new LatLng (45.45725121038923, -73.6398620903492));
			CJBuilding.Corners.Add (new LatLng (45.4572737873286, -73.63982185721397));
			CJBuilding.Corners.Add (new LatLng (45.45730483060549, -73.63980174064636));
			CJBuilding.Corners.Add (new LatLng (45.457331170342194, -73.6397910118103));
			CJBuilding.Corners.Add (new LatLng (45.45736127288332, -73.63978430628777));
			CJBuilding.Corners.Add (new LatLng (45.457372561332114, -73.63978162407875));
			CJBuilding.Corners.Add (new LatLng (45.45739701963006, -73.63978564739227));
			CJBuilding.Corners.Add (new LatLng (45.45742053721423, -73.63979771733284));
			CJBuilding.Corners.Add (new LatLng (45.45744405478861, -73.63981112837791));
			CJBuilding.Corners.Add (new LatLng (45.45746286884104, -73.63982856273651));
			CJBuilding.Corners.Add (new LatLng (45.4574722758649, -73.63984867930412));
			CJBuilding.Corners.Add (new LatLng (45.45745816532853, -73.63997206091881));
			CJBuilding.Corners.Add (new LatLng (45.457441232680196, -73.63996267318726));
			CJBuilding.Corners.Add (new LatLng (45.45743746986878, -73.64006593823433));
			CJBuilding.Corners.Add (new LatLng (45.45748732709983, -73.64019066095352));
			CJBuilding.Corners.Add (new LatLng (45.45762278803329, -73.64008873701096));
			CJBuilding.Corners.Add (new LatLng (45.45771685793452, -73.64033281803131));
			CJBuilding.Corners.Add (new LatLng (45.45775730794381, -73.64029929041862));
			CJBuilding.Corners.Add (new LatLng (45.45783350439407, -73.64048838615417));
			CJBuilding.Corners.Add (new LatLng (45.45765194971945, -73.64063456654549));
			CJBuilding.Corners.Add (new LatLng (45.457621847333485, -73.64056080579758));
			CJBuilding.Corners.Add (new LatLng (45.45754753199931, -73.64062517881393));
			CJBuilding.Corners.Add (new LatLng (45.45753342148178, -73.64059031009674));
			CJBuilding.Corners.Add (new LatLng (45.457331170342194, -73.64075392484665));
			CJBuilding.Corners.Add (new LatLng (45.45730483060549, -73.6406908929348));
			CJBuilding.Corners.Add (new LatLng (45.457279431562036, -73.64071100950241));
			CJBuilding.Corners.Add (new LatLng (45.45717407244481, -73.64044144749641));
			CJBuilding.BuildingOverlay = Resource.Drawable.CJ_Logo;
			CJBuilding.OverlaySize = 25;
			LoyolaBuildings.Add (CJBuilding);

			Building DOBuilding = new Building ("Stinger Dome (seasonal)", "DO", 45.457595, -73.636173);
			DOBuilding.BuildingImage = Resource.Drawable.do1;
			DOBuilding.Corners.Add (new LatLng (45.457372561332114, -73.63708734512329));
			DOBuilding.Corners.Add (new LatLng (45.45833583397091, -73.63596081733704));
			DOBuilding.Corners.Add (new LatLng (45.45792945533317, -73.63523930311203));
			DOBuilding.Corners.Add (new LatLng (45.456956768643806, -73.6363497376442));
			DOBuilding.BuildingOverlay = Resource.Drawable.DO_Logo;
			DOBuilding.OverlaySize = 40;
			LoyolaBuildings.Add (DOBuilding);

			               
			Building FCBuilding = new Building ("F. C. Smith Building", "FC", 45.458487, -73.639347);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
			FCBuilding.BuildingImage = Resource.Drawable.FC;
			FCBuilding.Corners.Add (new LatLng (45.458573827507685, -73.63969512283802));
			FCBuilding.Corners.Add (new LatLng (45.458657548315394, -73.63962605595589));
			FCBuilding.Corners.Add (new LatLng (45.45865990002167, -73.6395824700594));
			FCBuilding.Corners.Add (new LatLng (45.45863591261296, -73.63951809704304));
			FCBuilding.Corners.Add (new LatLng (45.458646260123835, -73.63950870931149));
			FCBuilding.Corners.Add (new LatLng (45.45861333621894, -73.63942958414555));
			FCBuilding.Corners.Add (new LatLng (45.458620861684615, -73.63942489027977));
			FCBuilding.Corners.Add (new LatLng (45.45851221267656, -73.63915599882603));
			FCBuilding.Corners.Add (new LatLng (45.45852208986775, -73.63914459943771));
			FCBuilding.Corners.Add (new LatLng (45.45852632294917, -73.63913454115391));
			FCBuilding.Corners.Add (new LatLng (45.45852867466092, -73.6391231417656));
			FCBuilding.Corners.Add (new LatLng (45.45852773397625, -73.63911174237728));
			FCBuilding.Corners.Add (new LatLng (45.4585244415797, -73.6390969902277));
			FCBuilding.Corners.Add (new LatLng (45.458521619525364, -73.63909028470516));
			FCBuilding.Corners.Add (new LatLng (45.45851456438889, -73.6390782147646));
			FCBuilding.Corners.Add (new LatLng (45.45850703890905, -73.63907352089882));
			FCBuilding.Corners.Add (new LatLng (45.458500454113334, -73.63907150924206));
			FCBuilding.Corners.Add (new LatLng (45.45849386931688, -73.63907217979431));
			FCBuilding.Corners.Add (new LatLng (45.45848681417692, -73.63907352089882));
			FCBuilding.Corners.Add (new LatLng (45.458333952595076, -73.63918349146843));
			FCBuilding.Corners.Add (new LatLng (45.458349003599984, -73.6392230540514));
			FCBuilding.Corners.Add (new LatLng (45.45833771534668, -73.63923244178295));
			FCBuilding.Corners.Add (new LatLng (45.458349003599984, -73.63926127552986));
			FCBuilding.Corners.Add (new LatLng (45.458359821507294, -73.63925524055958));
			FCBuilding.Corners.Add (new LatLng (45.45842472890751, -73.63942623138428));
			FCBuilding.Corners.Add (new LatLng (45.45839133452977, -73.63945171236992));
			FCBuilding.Corners.Add (new LatLng (45.45842096616171, -73.63952480256557));
			FCBuilding.Corners.Add (new LatLng (45.458452008807036, -73.63949932157993));
			FCBuilding.Corners.Add (new LatLng (45.45849998377077, -73.6396186798811));
			FCBuilding.Corners.Add (new LatLng (45.45851221267656, -73.63960929214954));
			FCBuilding.Corners.Add (new LatLng (45.4585409035605, -73.6396823823452));
			FCBuilding.BuildingOverlay = Resource.Drawable.FC_Logo;
			FCBuilding.OverlaySize = 20;
			LoyolaBuildings.Add (FCBuilding);

			Building GEBuilding = new Building ("Centre for Structural and Functional Genomics", "GE", 45.456910, -73.640416);
			GEBuilding.BuildingImage = Resource.Drawable.GE;
			GEBuilding.Corners.Add (new LatLng (45.4568796302966, -73.64075660705566));
			GEBuilding.Corners.Add (new LatLng (45.45710163793753, -73.64055275917053));
			GEBuilding.Corners.Add (new LatLng (45.45697746427997, -73.64006459712982));
			GEBuilding.Corners.Add (new LatLng (45.45668772468234, -73.64031136035919));
			GEBuilding.BuildingOverlay = Resource.Drawable.GE_Logo;
			GEBuilding.OverlaySize = 25;
			LoyolaBuildings.Add (GEBuilding);

			Building HingstonABuilding = new Building ("Hingston Wing A", "HA", 45.459450, -73.641269);
			HingstonABuilding.BuildingImage = Resource.Drawable.HA;
			HingstonABuilding.Corners.Add (new LatLng (45.45942608070822, -73.6415733397007));
			HingstonABuilding.Corners.Add (new LatLng (45.45967065430309, -73.64138424396515));
			HingstonABuilding.Corners.Add (new LatLng (45.459647137657356, -73.64132389426231));
			HingstonABuilding.Corners.Add (new LatLng (45.45965654431683, -73.64131584763527));
			HingstonABuilding.Corners.Add (new LatLng (45.45952485094153, -73.64096447825432));
			HingstonABuilding.Corners.Add (new LatLng (45.45950791891383, -73.64097520709038));
			HingstonABuilding.Corners.Add (new LatLng (45.45948534286898, -73.64091619849205));
			HingstonABuilding.Corners.Add (new LatLng (45.45946370748417, -73.64093363285065));
			HingstonABuilding.Corners.Add (new LatLng (45.459459944807705, -73.6409242451191));
			HingstonABuilding.Corners.Add (new LatLng (45.45926522595792, -73.64107847213745));
			HingstonABuilding.Corners.Add (new LatLng (45.45926804797503, -73.64108920097351));
			HingstonABuilding.Corners.Add (new LatLng (45.4592492345249, -73.64110663533211));
			HingstonABuilding.Corners.Add (new LatLng (45.45927086999199, -73.64116430282593));
			HingstonABuilding.Corners.Add (new LatLng (45.4592548785606, -73.64117905497551));
			HingstonABuilding.Corners.Add (new LatLng (45.45939033524779, -73.64152908325195));
			HingstonABuilding.Corners.Add (new LatLng (45.45940444530066, -73.64151701331139));
			HingstonABuilding.BuildingOverlay = Resource.Drawable.HA_Logo;
			HingstonABuilding.OverlaySize = 25;
			LoyolaBuildings.Add (HingstonABuilding);

			Building HingstonBBuilding = new Building ("Hingston Wing B", "HB", 45.459234, -73.641903);
			HingstonBBuilding.BuildingImage = Resource.Drawable.HB;
			HingstonBBuilding.Corners.Add (new LatLng (45.45942608070822, -73.6415733397007));
			HingstonBBuilding.Corners.Add (new LatLng (45.45940444530066, -73.64151701331139));
			HingstonBBuilding.Corners.Add (new LatLng (45.459372462509094, -73.64153981208801));
			HingstonBBuilding.Corners.Add (new LatLng (45.45936211513147, -73.64151701331139));
			HingstonBBuilding.Corners.Add (new LatLng (45.45895386253506, -73.64184156060219));
			HingstonBBuilding.Corners.Add (new LatLng (45.458974557438246, -73.64189520478249));
			HingstonBBuilding.Corners.Add (new LatLng (45.45895386253506, -73.64191263914108));
			HingstonBBuilding.Corners.Add (new LatLng (45.45895950660033, -73.64192739129066));
			HingstonBBuilding.Corners.Add (new LatLng (45.458949159146904, -73.64193543791771));
			HingstonBBuilding.Corners.Add (new LatLng (45.45908367589315, -73.64228814840317));
			HingstonBBuilding.Corners.Add (new LatLng (45.45909684534755, -73.64228010177612));
			HingstonBBuilding.Corners.Add (new LatLng (45.459101548723396, -73.64229083061218));
			HingstonBBuilding.Corners.Add (new LatLng (45.459123184247154, -73.6422747373581));
			HingstonBBuilding.Corners.Add (new LatLng (45.45914481976262, -73.6423310637474));
			HingstonBBuilding.Corners.Add (new LatLng (45.45934518305489, -73.64217415452003));
			HingstonBBuilding.Corners.Add (new LatLng (45.45932260694488, -73.64211849868298));
			HingstonBBuilding.Corners.Add (new LatLng (45.459346594061465, -73.64209905266762));
			HingstonBBuilding.Corners.Add (new LatLng (45.459341420370514, -73.64208698272705));
			HingstonBBuilding.Corners.Add (new LatLng (45.45950462657451, -73.64195689558983));
			HingstonBBuilding.Corners.Add (new LatLng (45.45953002461565, -73.64201724529266));
			HingstonBBuilding.Corners.Add (new LatLng (45.45955260064262, -73.64199846982956));
			HingstonBBuilding.Corners.Add (new LatLng (45.45938798357197, -73.64157736301422));
			HingstonBBuilding.BuildingOverlay = Resource.Drawable.HB_Logo;
			HingstonBBuilding.OverlaySize = 35;
			LoyolaBuildings.Add (HingstonBBuilding);

			Building HingstonCBuilding = new Building ("Hingston Wing C", "HC", 45.459669, -73.642079);
			HingstonCBuilding.BuildingImage = Resource.Drawable.HC;
			HingstonCBuilding.Corners.Add (new LatLng (45.45969134894318, -73.64189520478249));
			HingstonCBuilding.Corners.Add (new LatLng (45.45951873659878, -73.64203333854675));
			HingstonCBuilding.Corners.Add (new LatLng (45.45961562533672, -73.64228010177612));
			HingstonCBuilding.Corners.Add (new LatLng (45.459788237384515, -73.64214397966862));
			HingstonCBuilding.Corners.Add (new LatLng (45.4597750680916, -73.64211045205593));
			HingstonCBuilding.Corners.Add (new LatLng (45.45988653665243, -73.64201925694942));
			HingstonCBuilding.Corners.Add (new LatLng (45.459817397950786, -73.64184357225895));
			HingstonCBuilding.Corners.Add (new LatLng (45.459706399585784, -73.64192940294743));
			HingstonCBuilding.BuildingOverlay = Resource.Drawable.HC_Logo;
			HingstonCBuilding.OverlaySize = 20;
			LoyolaBuildings.Add (HingstonCBuilding);

			Building JesuitBuilding = new Building ("Jesuit Residence", "JR", 45.458408, -73.643297);
			//JesuitBuilding.setDescription ("http://www.concordia.ca/maps/buildings/jr.html", "rte");
			JesuitBuilding.Description = "In 1969 Jesuit priests, the owners and founders of Concordia University's founding "+
				"institution Loyola College, moved from their cramped residence facilities in the Administration Building into "+
				"the new seven-storey Jesuit Residence on the northwest corner of the campus. ";
			JesuitBuilding.BuildingImage = Resource.Drawable.JR;
			JesuitBuilding.Corners.Add (new LatLng (45.45850939062161, -73.64330872893333));
			JesuitBuilding.Corners.Add (new LatLng (45.45852726363388, -73.64329196512699));
			JesuitBuilding.Corners.Add (new LatLng (45.458487754862304, -73.6431933939457));
			JesuitBuilding.Corners.Add (new LatLng (45.45847411492285, -73.6432034522295));
			JesuitBuilding.Corners.Add (new LatLng (45.45844307228968, -73.64312835037708));
			JesuitBuilding.Corners.Add (new LatLng (45.458388982812295, -73.64317260682583));
			JesuitBuilding.Corners.Add (new LatLng (45.45839415659065, -73.64318534731865));
			JesuitBuilding.Corners.Add (new LatLng (45.45838286834637, -73.6431947350502));
			JesuitBuilding.Corners.Add (new LatLng (45.458371580099836, -73.64316992461681));
			JesuitBuilding.Corners.Add (new LatLng (45.4583019691963, -73.64322759211063));
			JesuitBuilding.Corners.Add (new LatLng (45.45833066018721, -73.64330068230629));
			JesuitBuilding.Corners.Add (new LatLng (45.4583137278009, -73.64331610500813));
			JesuitBuilding.Corners.Add (new LatLng (45.45835370703822, -73.64341400563717));
			JesuitBuilding.Corners.Add (new LatLng (45.458368758037864, -73.64340126514435));
			JesuitBuilding.Corners.Add (new LatLng (45.45839838968166, -73.64347368478775));
			JesuitBuilding.Corners.Add (new LatLng (45.45846282669458, -73.64342004060745));
			JesuitBuilding.Corners.Add (new LatLng (45.458470352180356, -73.64343747496605));
			JesuitBuilding.Corners.Add (new LatLng (45.45853808150697, -73.64338114857674));
			JesuitBuilding.BuildingOverlay = Resource.Drawable.JR_Logo;
			JesuitBuilding.OverlaySize = 25;
			LoyolaBuildings.Add (JesuitBuilding);

			Building PCBUilding = new Building ("PERFORM Centre", "PC", 45.457053, -73.637310);
			PCBUilding.BuildingImage = Resource.Drawable.PC;
			PCBUilding.Corners.Add (new LatLng (45.45711104502166, -73.63783299922943));
			PCBUilding.Corners.Add (new LatLng (45.45737820555565, -73.6376291513443));
			PCBUilding.Corners.Add (new LatLng (45.457048958237415, -73.63678425550461));
			PCBUilding.Corners.Add (new LatLng (45.45680437327082, -73.6369800567627));
			PCBUilding.BuildingOverlay = Resource.Drawable.PC_logo;
			PCBUilding.OverlaySize = 30;
			LoyolaBuildings.Add (PCBUilding);


			Building PhysicalServicesBuilding = new Building ("Physical Services Building", "PS", 45.459649, -73.639795);
			//PhysicalServicesBuilding.setDescription ("http://www.concordia.ca/maps/buildings/ps.html", "rte");
			PhysicalServicesBuilding.Description = "The building in which Concordia Physical Services has been housed for many years "+
				"was built in 1923 as the first rink and arena built for Concordia's founding institution, Loyola College. ";
			PhysicalServicesBuilding.BuildingImage = Resource.Drawable.PS;
			PhysicalServicesBuilding.Corners.Add (new LatLng (45.4597026369255, -73.64032879471779));
			PhysicalServicesBuilding.Corners.Add (new LatLng (45.45984843982731, -73.64021345973015));
			PhysicalServicesBuilding.Corners.Add (new LatLng (45.45982398259268, -73.64015311002731));
			PhysicalServicesBuilding.Corners.Add (new LatLng (45.45994156535435, -73.64006325602531));
			PhysicalServicesBuilding.Corners.Add (new LatLng (45.459661247645954, -73.63933637738228));
			PhysicalServicesBuilding.Corners.Add (new LatLng (45.459619858336055, -73.63936588168144));
			PhysicalServicesBuilding.Corners.Add (new LatLng (45.459566240321195, -73.63922908902168));
			PhysicalServicesBuilding.Corners.Add (new LatLng (45.459277454697734, -73.63945573568344));
			PhysicalServicesBuilding.Corners.Add (new LatLng (45.45933107298719, -73.63959386944771));
			PhysicalServicesBuilding.Corners.Add (new LatLng (45.459400682620256, -73.63954290747643));
			PhysicalServicesBuilding.Corners.Add (new LatLng (45.45944113142161, -73.6396461725235));
			PhysicalServicesBuilding.Corners.Add (new LatLng (45.459411029990804, -73.63967165350914));
			PhysicalServicesBuilding.Corners.Add (new LatLng (45.459605748337076, -73.64017322659492));
			PhysicalServicesBuilding.Corners.Add (new LatLng (45.45963302766522, -73.6401517689228));
			PhysicalServicesBuilding.BuildingOverlay = Resource.Drawable.PS_Logo;
			PhysicalServicesBuilding.OverlaySize = 40;
			LoyolaBuildings.Add (PhysicalServicesBuilding);

			Building PTBuilding = new Building ("Oscar Peterson Concert Hall", "PT", 45.459324, -73.639006);
			PTBuilding.BuildingImage = Resource.Drawable.PT;
			PTBuilding.Corners.Add (new LatLng (45.45949004621238, -73.63913387060165));
			PTBuilding.Corners.Add (new LatLng (45.459459944807705, -73.63905474543571));
			PTBuilding.Corners.Add (new LatLng (45.45944865677681, -73.63906279206276));
			PTBuilding.Corners.Add (new LatLng (45.45931037821484, -73.6387087404728));
			PTBuilding.Corners.Add (new LatLng (45.45932166627344, -73.63869935274124));
			PTBuilding.Corners.Add (new LatLng (45.45931508157287, -73.63868191838264));
			PTBuilding.Corners.Add (new LatLng (45.45916363324759, -73.6387999355793));
			PTBuilding.Corners.Add (new LatLng (45.45914952313447, -73.63877043128014));
			PTBuilding.Corners.Add (new LatLng (45.45906015900261, -73.63884016871452));
			PTBuilding.Corners.Add (new LatLng (45.45910625209883, -73.63895818591118));
			PTBuilding.Corners.Add (new LatLng (45.45915046380878, -73.63892331719398));
			PTBuilding.Corners.Add (new LatLng (45.459173039987725, -73.63897830247879));
			PTBuilding.Corners.Add (new LatLng (45.45919561615761, -73.6389608681202));
			PTBuilding.Corners.Add (new LatLng (45.459305674856424, -73.63923981785774));
			PTBuilding.Corners.Add (new LatLng (45.459349886409996, -73.63920629024506));
			PTBuilding.Corners.Add (new LatLng (45.45936211513147, -73.6392317712307));
			PTBuilding.BuildingOverlay = Resource.Drawable.PT_Logo;
			PTBuilding.OverlaySize = 20;
			LoyolaBuildings.Add (PTBuilding);

			Building PsychologyBuilding = new Building ("Psychology Building", "PY", 45.458906, -73.640530);
			PsychologyBuilding.BuildingImage = Resource.Drawable.PY;
			PsychologyBuilding.Corners.Add (new LatLng (45.45879488779811, -73.64084377884865));
			PsychologyBuilding.Corners.Add (new LatLng (45.45910060804827, -73.64060908555984));
			PsychologyBuilding.Corners.Add (new LatLng (45.45912036222279, -73.64066138863564));
			PsychologyBuilding.Corners.Add (new LatLng (45.45921066693252, -73.64058896899223));
			PsychologyBuilding.Corners.Add (new LatLng (45.45916175189937, -73.64046290516853));
			PsychologyBuilding.Corners.Add (new LatLng (45.45916927729184, -73.6404575407505));
			PsychologyBuilding.Corners.Add (new LatLng (45.45914481976262, -73.64039584994316));
			PsychologyBuilding.Corners.Add (new LatLng (45.45913541301778, -73.64040523767471));
			PsychologyBuilding.Corners.Add (new LatLng (45.45904416751145, -73.64016517996788));
			PsychologyBuilding.Corners.Add (new LatLng (45.45895386253506, -73.64023357629776));
			PsychologyBuilding.Corners.Add (new LatLng (45.458956684567745, -73.64024430513382));
			PsychologyBuilding.Corners.Add (new LatLng (45.45891999813161, -73.64027515053749));
			PsychologyBuilding.Corners.Add (new LatLng (45.4589143540624, -73.64025637507439));
			PsychologyBuilding.Corners.Add (new LatLng (45.4586961162861, -73.64042267203331));
			PsychologyBuilding.Corners.Add (new LatLng (45.458697056967964, -73.64043340086937));
			PsychologyBuilding.Corners.Add (new LatLng (45.45865190421989, -73.64046961069107));
			PsychologyBuilding.BuildingOverlay = Resource.Drawable.PY_Logo;
			PsychologyBuilding.OverlaySize = 18;
			LoyolaBuildings.Add (PsychologyBuilding);

			Building athleticBuilding = new Building ("Recreation and Athletics Complex", "RA", 45.456703, -73.637680);
			//athleticBuilding.setDescription ("http://www.concordia.ca/maps/buildings/ra.html", "rte");
			athleticBuilding.Description = "Sports facilities and playing fields have always been an important part of the Loyola "+
				"Campus. The first phase began in 2003 to develop both the outdoor and indoor Loyola Campus recreation and athletics "+
				"facilities. Artificial surfaces were installed to create two new outdoor playing fields for football, soccer, rugby, "+
				"and recreational activities.";
			athleticBuilding.BuildingImage = Resource.Drawable.RA;
			athleticBuilding.Corners.Add (new LatLng (45.45695817971015, -73.63793425261974));
			athleticBuilding.Corners.Add (new LatLng (45.4570235590787, -73.63787725567818));
			athleticBuilding.Corners.Add (new LatLng (45.45672206078385, -73.63710813224316));
			athleticBuilding.Corners.Add (new LatLng (45.456386694952066, -73.63737635314465));
			athleticBuilding.Corners.Add (new LatLng (45.45668913575542, -73.63814748823643));
			athleticBuilding.Corners.Add (new LatLng (45.45679026256728, -73.63806769251823));
			athleticBuilding.Corners.Add (new LatLng (45.456839650014146, -73.63819509744644));
			athleticBuilding.Corners.Add (new LatLng (45.45677097793344, -73.63825142383575));
			athleticBuilding.Corners.Add (new LatLng (45.45679825863304, -73.6383231729269));
			athleticBuilding.Corners.Add (new LatLng (45.45676298186381, -73.6383506655693));
			athleticBuilding.Corners.Add (new LatLng (45.45679825863304, -73.63844521343708));
			athleticBuilding.Corners.Add (new LatLng (45.45682836145869, -73.6384217441082));
			athleticBuilding.Corners.Add (new LatLng (45.456884804213445, -73.63856390118599));
			athleticBuilding.Corners.Add (new LatLng (45.456884804213445, -73.63856390118599));
			athleticBuilding.Corners.Add (new LatLng (45.45701368162502, -73.63845996558666)); 
			athleticBuilding.Corners.Add (new LatLng (45.45702732191775, -73.6384928226471));
			athleticBuilding.Corners.Add (new LatLng (45.45705131001079, -73.63847203552723));
			athleticBuilding.Corners.Add (new LatLng (45.457038140078616, -73.63843850791454));
			athleticBuilding.Corners.Add (new LatLng (45.45715996183382, -73.63834127783775));
			athleticBuilding.Corners.Add (new LatLng (45.457038140078616, -73.63803550601006));
			athleticBuilding.Corners.Add (new LatLng (45.45700427452465, -73.6380623281002));
			athleticBuilding.BuildingOverlay = Resource.Drawable.RA_Logo;
			athleticBuilding.OverlaySize = 35;
			LoyolaBuildings.Add (athleticBuilding);

			Building RefectoryBuilding = new Building ("Loyola Jesuit Hall and Conference Centre", "RF", 45.458479, -73.641053);
			//RefectoryBuilding.setDescription ("http://www.concordia.ca/maps/buildings/rf.html", "rte");
			RefectoryBuilding.Description = "The Refectory building has been renovated to create the Loyola Jesuit Hall and Conference "+
				"Centre, an important element in the revitalization of the Loyola Campus. The new space was officially unveiled in 2012.";
			RefectoryBuilding.BuildingImage = Resource.Drawable.RF;
			RefectoryBuilding.Corners.Add (new LatLng (45.45842237719143, -73.64139631390572));
			RefectoryBuilding.Corners.Add (new LatLng (45.45858041229482, -73.64127293229103));
			RefectoryBuilding.Corners.Add (new LatLng (45.458552191773144, -73.6411964893341));
			RefectoryBuilding.Corners.Add (new LatLng (45.45857853092714, -73.64117369055748));
			RefectoryBuilding.Corners.Add (new LatLng (45.458571005455816, -73.64115089178085));
			RefectoryBuilding.Corners.Add (new LatLng (45.45860675143564, -73.64112541079521));
			RefectoryBuilding.Corners.Add (new LatLng (45.458576649559404, -73.6410503089428));
			RefectoryBuilding.Corners.Add (new LatLng (45.458657548315394, -73.64098593592644));
			RefectoryBuilding.Corners.Add (new LatLng (45.45860957348573, -73.6408706009388));
			RefectoryBuilding.Corners.Add (new LatLng (45.45852208986775, -73.64093899726868));
			RefectoryBuilding.Corners.Add (new LatLng (45.45845624189373, -73.64077404141426));
			RefectoryBuilding.Corners.Add (new LatLng (45.45843366542777, -73.64079013466835));
			RefectoryBuilding.Corners.Add (new LatLng (45.45841579238582, -73.6407445371151));
			RefectoryBuilding.Corners.Add (new LatLng (45.45835558841338, -73.64079281687737));
			RefectoryBuilding.Corners.Add (new LatLng (45.45837440216168, -73.64084243774414));
			RefectoryBuilding.Corners.Add (new LatLng (45.45832924915522, -73.64087998867035));
			RefectoryBuilding.Corners.Add (new LatLng (45.45836499528832, -73.64097386598587));
			RefectoryBuilding.Corners.Add (new LatLng (45.45838569040763, -73.64095643162727));
			RefectoryBuilding.Corners.Add (new LatLng (45.45841108895281, -73.64102348685265));
			RefectoryBuilding.Corners.Add (new LatLng (45.45830008781933, -73.64111199975014));
			RefectoryBuilding.Corners.Add (new LatLng (45.45835088497533, -73.64124342799187));
			RefectoryBuilding.Corners.Add (new LatLng (45.458416733072404, -73.64119783043861));
			RefectoryBuilding.Corners.Add (new LatLng (45.45845247915003, -73.6412863433361));
			RefectoryBuilding.Corners.Add (new LatLng (45.45839791933823, -73.64133059978485));
			RefectoryBuilding.BuildingOverlay = Resource.Drawable.RF_Logo;
			RefectoryBuilding.OverlaySize = 14;
			LoyolaBuildings.Add (RefectoryBuilding);


			Building SCBuilding = new Building ("Student Centre", "SC", 45.459127, -73.639204);
			SCBuilding.BuildingImage = Resource.Drawable.SC;
			SCBuilding.Corners.Add (new LatLng (45.45899337098003, -73.63912582397461));
			SCBuilding.Corners.Add (new LatLng (45.4591043707487, -73.63940745592117));
			SCBuilding.Corners.Add (new LatLng (45.459216310972096, -73.63932028412819));
			SCBuilding.Corners.Add (new LatLng (45.4592351244332, -73.63936856389046));
			SCBuilding.Corners.Add (new LatLng (45.45929438679466, -73.6393229663372));
			SCBuilding.Corners.Add (new LatLng (45.45925581923316, -73.63921567797661));
			SCBuilding.Corners.Add (new LatLng (45.45927933604209, -73.63919824361801));
			SCBuilding.Corners.Add (new LatLng (45.45919091278962, -73.63897562026978));
			SCBuilding.Corners.Add (new LatLng (45.45914952313447, -73.63900512456894));
			SCBuilding.Corners.Add (new LatLng (45.459131650319414, -73.63896355032921));
			SCBuilding.Corners.Add (new LatLng (45.45907709116479, -73.63900914788246));
			SCBuilding.Corners.Add (new LatLng (45.45909120129605, -73.63904938101768));
			SCBuilding.Corners.Add (new LatLng (45.45899525233385, -73.63912582397461));
			SCBuilding.BuildingOverlay = Resource.Drawable.SC_Logo;
			SCBuilding.OverlaySize = 23;
			LoyolaBuildings.Add (SCBuilding);

			Building SHBuilding = new Building ("Solar House", "SH", 45.459399, -73.642394);
			SHBuilding.BuildingImage = Resource.Drawable.SH;
			SHBuilding.Corners.Add (new LatLng (45.459395038599155, -73.64253491163254));
			SHBuilding.Corners.Add (new LatLng (45.45941008932082, -73.64237666130066));
			SHBuilding.Corners.Add (new LatLng (45.45935364909381, -73.64237666130066));
			SHBuilding.Corners.Add (new LatLng (45.45935176775193, -73.64252418279648));
			SHBuilding.BuildingOverlay = Resource.Drawable.SH_Logo;
			SHBuilding.OverlaySize = 11;
			LoyolaBuildings.Add (SHBuilding);

			Building stIgnatiusBuilding = new Building ("Saint-Ignatius of Loyola Church", "SI", 45.457836, -73.642331);
			stIgnatiusBuilding.BuildingImage = Resource.Drawable.SI;
			stIgnatiusBuilding.Corners.Add (new LatLng (45.45815334022274, -73.6425469815731));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.458103483580565, -73.64241756498814));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.45808608077999, -73.64243097603321));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.458028698534676, -73.64227943122387));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.458040927542676, -73.64224925637245));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.45798307490449, -73.64210106432438));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.45795626512519, -73.64209838211536));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.457927573943785, -73.64202596247196));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.457868780493605, -73.64206954836845));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.45784573344443, -73.64200919866562));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.45780998698213, -73.64203803241253));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.457818453251576, -73.64206083118916));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.45775448585107, -73.64211246371269));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.45776624456984, -73.64214263856411));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.457709802697366, -73.6421862244606));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.457719680029115, -73.6422123759985));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.457667471255924, -73.64225462079048));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.457677348595126, -73.64228010177612));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.45762890258163, -73.64231765270233));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.45765947531344, -73.64240013062954));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.457570108820065, -73.64246919751167));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.45761855488404, -73.64259526133537));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.45771027304652, -73.64252418279648));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.457718739330936, -73.64254362881184));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.457764363175, -73.64250876009464));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.45777282945127, -73.64253088831902));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.45782127534108, -73.64249266684055));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.45783209334964, -73.64251881837845));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.45788853509969, -73.64247657358646));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.45792287047003, -73.6425644159317));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.4579026455284, -73.64257983863354));
			stIgnatiusBuilding.Corners.Add (new LatLng (45.45795156165383, -73.64270523190498));
			stIgnatiusBuilding.BuildingOverlay = Resource.Drawable.SI_Logo;
			stIgnatiusBuilding.OverlaySize = 30;
			LoyolaBuildings.Add (stIgnatiusBuilding);


			Building RichardScienceBuilding = new Building ("Richard J. Renaud Science Complex", "SP", 45.457625, -73.641703);
			//RichardScienceBuilding.setDescription ("http://www.concordia.ca/maps/buildings/sp.html", "rte");
			RichardScienceBuilding.Description = "The Richard J. Renaud Science Complex has changed the face of Concordia's west-end "+
				"campus. This state-of-the-art teaching and research facility is a lynchpin of the Loyola Campus revitalization. "+
				"The $85 million purpose-built complex includes teaching facilities, offices, and laboratories for various science "+
				"departments.";
			RichardScienceBuilding.BuildingImage = Resource.Drawable.SP;
			RichardScienceBuilding.Corners.Add (new LatLng (45.4582079002712, -73.64158071577549));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45819661198965, -73.64154987037182));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45832360502687, -73.64144995808601));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45828127404625, -73.64133931696415));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45820178578563, -73.64140100777149));
			RichardScienceBuilding.Corners.Add (new LatLng (45.458171683693195, -73.6413212120533));
			RichardScienceBuilding.Corners.Add (new LatLng (45.458249760962254, -73.64126086235046));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45820037475041, -73.64113345742226));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45810113185108, -73.64121325314045));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45809031389416, -73.64118777215481));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45802822818814, -73.64123672246933));
			RichardScienceBuilding.Corners.Add (new LatLng (45.457974608659775, -73.64110127091408));
			RichardScienceBuilding.Corners.Add (new LatLng (45.4578904164904, -73.6411689966917));
			RichardScienceBuilding.Corners.Add (new LatLng (45.457934629153684, -73.64128567278385));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45786689910221, -73.6413386464119));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45783726717908, -73.64126086235046));
			RichardScienceBuilding.Corners.Add (new LatLng (45.4575211923637, -73.64150896668434));
			RichardScienceBuilding.Corners.Add (new LatLng (45.457419596511066, -73.6412487924099));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45741207088506, -73.64125482738018));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45724227368157, -73.64082098007202));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45719006446636, -73.64086121320724));
			RichardScienceBuilding.Corners.Add (new LatLng (45.457135033079055, -73.64072106778622));
			RichardScienceBuilding.Corners.Add (new LatLng (45.456972290371645, -73.64085115492344));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45701932588448, -73.64097118377686));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45699204529182, -73.64099331200123));
			RichardScienceBuilding.Corners.Add (new LatLng (45.457015092689936, -73.64105097949505));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45703766972384, -73.64103354513645));
			RichardScienceBuilding.Corners.Add (new LatLng (45.457161372895065, -73.64135205745697));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45715290652697, -73.64135876297951));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45717642421297, -73.6414197832346));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45716748749345, -73.64142782986164));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45718206845615, -73.64146672189236));
			RichardScienceBuilding.Corners.Add (new LatLng (45.457210760016906, -73.64144593477249));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45743935127452, -73.642034009099));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45755223550439, -73.64194549620152));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45759409668217, -73.64205077290535));
			RichardScienceBuilding.Corners.Add (new LatLng (45.457677348595126, -73.64198438823223));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45767076370252, -73.64196226000786));
			RichardScienceBuilding.Corners.Add (new LatLng (45.457996714962725, -73.64170409739017));
			RichardScienceBuilding.Corners.Add (new LatLng (45.45800894397767, -73.64173494279385));
			RichardScienceBuilding.BuildingOverlay = Resource.Drawable.SP_Logo;
			RichardScienceBuilding.OverlaySize = 30;
			LoyolaBuildings.Add (RichardScienceBuilding);


			Building TerreBonneBuilding = new Building ("Terrebonne Building", "TA", 45.459997, -73.640900);
			TerreBonneBuilding.BuildingImage = Resource.Drawable.TA;
			TerreBonneBuilding.Corners.Add (new LatLng (45.46008031269777, -73.64090748131275));
			TerreBonneBuilding.Corners.Add (new LatLng (45.4600426863333, -73.64080622792244));
			TerreBonneBuilding.Corners.Add (new LatLng (45.45993262907309, -73.64089138805866));
			TerreBonneBuilding.Corners.Add (new LatLng (45.45997119617164, -73.64099331200123));
			TerreBonneBuilding.BuildingOverlay = Resource.Drawable.TA_Logo;
			TerreBonneBuilding.OverlaySize = 20;
			LoyolaBuildings.Add (TerreBonneBuilding);


			Building VanierExtensionBuilding = new Building ("Vanier Extension", "VE", 45.458845, -73.638635);
			//VanierExtensionBuilding.setDescription ("http://www.concordia.ca/maps/buildings/ve.html", "rte");
			VanierExtensionBuilding.Description = "The original Vanier Library Building (1964) has been called the Vanier Extension "+
				"since the new Vanier Library opened in 1989. During the spring and summer of 2005 the second and third floors "+
				"(3,257 sq. metres) of the older building were renovated and refitted to accommodate the specific needs of the "+
				"Department of Applied Human Sciences. The new facilities include digitally equipped multi-functional classrooms "+
				"that can be set up for traditional teaching, workshops, or seminars.";
			VanierExtensionBuilding.BuildingImage = Resource.Drawable.VE;
			VanierExtensionBuilding.Corners.Add (new LatLng (45.45882028615895, -73.63900043070316));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.45883533703404, -73.6389883607626));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.45884709552742, -73.63901786506176));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.45888801506525, -73.63898567855358));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.45889977354766, -73.63901250064373));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.45901594722187, -73.63891996443272));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.45900512944044, -73.6388911306858));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.45902723533941, -73.63887369632721));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.45901312519214, -73.63883949816227));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.459076150489246, -73.6387898772955));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.45886779046982, -73.63824874162674));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.45881746411941, -73.63828763365746));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.45881323105988, -73.63827891647816));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.458784069974286, -73.63830238580704));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.458788773376206, -73.63831512629986));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.45869470526324, -73.63838955760002));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.458689061171484, -73.63837748765945));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.45865895933916, -73.63840229809284));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.45867777298624, -73.6384492367506));
			VanierExtensionBuilding.Corners.Add (new LatLng (45.45862039134304, -73.63849483430386));
			VanierExtensionBuilding.BuildingOverlay = Resource.Drawable.VE_Logo;
			VanierExtensionBuilding.OverlaySize = 20;
			LoyolaBuildings.Add (VanierExtensionBuilding);


			Building VanierLibraryBuilding = new Building ("Vanier Library", "VL", 45.459093, -73.638367);
			VanierLibraryBuilding.BuildingImage = Resource.Drawable.VL;
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45931320022971, -73.63867253065109));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45928121738638, -73.63858938217163));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45927651402553, -73.63859206438065));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45925628956943, -73.63854512572289));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45926240394065, -73.63854110240936));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45914858246013, -73.63824404776096));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.459134472343216, -73.63825611770153));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.4591245952593, -73.63822996616364));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45921819231848, -73.63815888762474));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45913729436689, -73.63795034587383));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.459128828295476, -73.63795705139637));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45911942154797, -73.63793157041073));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.459126946946114, -73.63792553544044));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45910013771069, -73.63785780966282));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45907756150256, -73.63787591457367));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45908931994541, -73.63790407776833));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45887343454368, -73.63807305693626));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.4588898964226, -73.6381159722805));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45885650232037, -73.63814279437065));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45887390488314, -73.63818772137165));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45885226926374, -73.63820649683475));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45908226487998, -73.63880395889282));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45914858246013, -73.6387536674738));
			VanierLibraryBuilding.Corners.Add (new LatLng (45.45916363324759, -73.63879054784775));
			VanierLibraryBuilding.BuildingOverlay = Resource.Drawable.VL_Logo;
			VanierLibraryBuilding.OverlaySize = 25;
			LoyolaBuildings.Add (VanierLibraryBuilding);

			//Assign buildings to LOYOLA Campus
			foreach (Building loyolaBuilding in LoyolaBuildings) {
				loyolaBuilding.Campus = LoyolaCampus;
				LoyolaCampus.Buildings.Add (loyolaBuilding);
			}

			return LoyolaBuildings;
		}
	}
}

