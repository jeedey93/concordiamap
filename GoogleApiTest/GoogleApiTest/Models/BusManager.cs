using System;
using System.Collections.Generic;

namespace GoogleApiTest
{
	public class BusManager
	{
		List<String> LOYBusMonThur = new List<String>();
		List<String> SGWBusMonThur = new List<String>();
		List<String> SGWBusFriday = new List<String>();
		List<String> LOYBusFriday = new List<String>();

		 


		public List<String> InitializeLOYBusMonThur(){
				
			LOYBusMonThur.Add ("7:45");
			LOYBusMonThur.Add ("8:00");
			LOYBusMonThur.Add ("8:15");
			LOYBusMonThur.Add ("8:30");
			LOYBusMonThur.Add ("8:45");
			LOYBusMonThur.Add ("9:00");
			LOYBusMonThur.Add ("9:15");
			LOYBusMonThur.Add ("9:30");
			LOYBusMonThur.Add ("9:45");
			LOYBusMonThur.Add ("10:00");
			LOYBusMonThur.Add ("10:15");
			LOYBusMonThur.Add ("10:30");
			LOYBusMonThur.Add ("10:55");
			LOYBusMonThur.Add ("11:15");
			LOYBusMonThur.Add ("From 11:30 to 19:00\n\nA bus leaves each campus every 20 – 25 minutes.");
			LOYBusMonThur.Add ("19:00");
			LOYBusMonThur.Add ("19:30");
			LOYBusMonThur.Add ("20:00");
			LOYBusMonThur.Add ("20:15");
			LOYBusMonThur.Add ("20:30");
			LOYBusMonThur.Add ("20:45");
			LOYBusMonThur.Add ("21:10");
			LOYBusMonThur.Add ("21:35");
			LOYBusMonThur.Add ("22:00");
			LOYBusMonThur.Add ("22:30");
			LOYBusMonThur.Add ("23:00");
		
			return LOYBusMonThur;
		}

		public List<String> InitializeSGWBusMonThur(){

			SGWBusMonThur.Add ("7:45");
			SGWBusMonThur.Add ("8:30");
			SGWBusMonThur.Add ("8:45");
			SGWBusMonThur.Add ("9:00");
			SGWBusMonThur.Add ("9:15");
			SGWBusMonThur.Add ("9:30");
			SGWBusMonThur.Add ("9:45");
			SGWBusMonThur.Add ("10:00");
			SGWBusMonThur.Add ("10:15");
			SGWBusMonThur.Add ("10:30");
			SGWBusMonThur.Add ("11:00");
			SGWBusMonThur.Add ("11:45");
			SGWBusMonThur.Add ("From 11:30 to 19:00\n\nA bus leaves each campus every 20 – 25 minutes.");
			SGWBusMonThur.Add ("19:00");
			SGWBusMonThur.Add ("19:30");
			SGWBusMonThur.Add ("19:45");
			SGWBusMonThur.Add ("20:00");
			SGWBusMonThur.Add ("20:15");
			SGWBusMonThur.Add ("20:45");
			SGWBusMonThur.Add ("21:00");
			SGWBusMonThur.Add ("21:10");
			SGWBusMonThur.Add ("21:35");
			SGWBusMonThur.Add ("22:00");
			SGWBusMonThur.Add ("22:30");
			SGWBusMonThur.Add ("23:00");

			return SGWBusMonThur;
		}


		public List<String> InitializeLOYBusFriday(){

			LOYBusFriday.Add ("7:45");
			LOYBusFriday.Add ("8:15");
			LOYBusFriday.Add ("8:45");
			LOYBusFriday.Add ("9:00");
			LOYBusFriday.Add ("9:15");
			LOYBusFriday.Add ("9:45");
			LOYBusFriday.Add ("10:00");
			LOYBusFriday.Add ("10:15");
			LOYBusFriday.Add ("11:00");
			LOYBusFriday.Add ("11:15");
			LOYBusFriday.Add ("11:45");
			LOYBusFriday.Add ("12:00");
			LOYBusFriday.Add ("12:15");
			LOYBusFriday.Add ("12:45");
			LOYBusFriday.Add ("13:15");
			LOYBusFriday.Add ("13:30");
			LOYBusFriday.Add ("13:45");
			LOYBusFriday.Add ("14:15");
			LOYBusFriday.Add ("14:30");
			LOYBusFriday.Add ("14:45");
			LOYBusFriday.Add ("15:15");
			LOYBusFriday.Add ("15:30");
			LOYBusFriday.Add ("15:45");
			LOYBusFriday.Add ("16:15");
			LOYBusFriday.Add ("16:30");
			LOYBusFriday.Add ("16:45");
			LOYBusFriday.Add ("17:15");
			LOYBusFriday.Add ("18:15");
			LOYBusFriday.Add ("18:45");
			LOYBusFriday.Add ("19:15");
			LOYBusFriday.Add ("19:45");

			return LOYBusFriday;
		}


		public List<String> InitializeSGWBusFriday(){

			SGWBusFriday.Add ("7:45");
			SGWBusFriday.Add ("8:15");
			SGWBusFriday.Add ("8:45");
			SGWBusFriday.Add ("9:15");
			SGWBusFriday.Add ("9:30");
			SGWBusFriday.Add ("9:45");
			SGWBusFriday.Add ("10:15");
			SGWBusFriday.Add ("10:30");
			SGWBusFriday.Add ("10:45");
			SGWBusFriday.Add ("11:30");
			SGWBusFriday.Add ("11:45");
			SGWBusFriday.Add ("12:15");
			SGWBusFriday.Add ("12:45");
			SGWBusFriday.Add ("13:00");
			SGWBusFriday.Add ("13.15");
			SGWBusFriday.Add ("13:45");
			SGWBusFriday.Add ("14:00");
			SGWBusFriday.Add ("14:15");
			SGWBusFriday.Add ("14:45");
			SGWBusFriday.Add ("15:00");
			SGWBusFriday.Add ("15:15");
			SGWBusFriday.Add ("15:45");
			SGWBusFriday.Add ("16:00");
			SGWBusFriday.Add ("16:15");
			SGWBusFriday.Add ("16:45");
			SGWBusFriday.Add ("17:15");
			SGWBusFriday.Add ("17:45");
			SGWBusFriday.Add ("18:15");
			SGWBusFriday.Add ("18:45");
			SGWBusFriday.Add ("19:15");
			SGWBusFriday.Add ("19:45");

			return SGWBusFriday;
		}
	}
}