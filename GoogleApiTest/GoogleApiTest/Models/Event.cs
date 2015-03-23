using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace GoogleApiTest
{
	public class Event
	{
		public int mCalendarId;
		public int mEventId;
		public string mTitle;
		public DateTime mDtStart;
		public string mEventLocation;
		BuildingManager BuildingManager = new BuildingManager ();


		public Event (int CalendarId, int EventId, string Title, double DtStart, string EventLocation)
		{
			mCalendarId = CalendarId;
			mEventId = EventId;
			mTitle = Title;
			DateTime date = new DateTime (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds (DtStart).ToLocalTime ();
			mDtStart = date;
			mEventLocation = EventLocation;
		}


		public static Event GetNextEvent(List<Event> EventList){

			if (EventList.Count > 0) {
				DateTime now = DateTime.Now.ToLocalTime ();
				Event nextEvent=null;
				TimeSpan delta = new TimeSpan ();
				//choose timespan that will be greater than the next event.
				TimeSpan closestDelta = new TimeSpan (10000,23, 50, 50); //dd,hh,mm,ss
				foreach (Event e in EventList) {
					//make sure the event time is after now.
					if ((e.mDtStart.CompareTo (now)) > 0) {
						delta = e.mDtStart - now;
						if (delta.CompareTo (closestDelta) < 0) {
							closestDelta = delta;
							nextEvent = e;
						}

					}
				}
				return nextEvent;
			}else{
				return null;
			}
		}

		public static Building GetNextEventBuilding(List<Event> EventList){


			if (EventList.Count > 0) {
				DateTime now = DateTime.Now.ToLocalTime ();
				Event nextEvent=null;
				TimeSpan delta = new TimeSpan ();
				//choose timespan that will be greater than the next event.
				TimeSpan closestDelta = new TimeSpan (10000,23, 50, 50); //dd,hh,mm,ss
				foreach (Event e in EventList) {
					//make sure the event time is after now.
					if ((e.mDtStart.CompareTo (now)) > 0) {
						delta = e.mDtStart - now;
						if (delta.CompareTo (closestDelta) < 0) {
							closestDelta = delta;
							nextEvent = e;
						}

					} else {
						//remove list items that are past
						EventList.Remove (e);
					}
				}
				//Check that event corresponds to a building
				Building nextBuilding = nextEvent.AbbreviationToBuilding (LocationToBuildingAbbreviation (nextEvent.mEventLocation));
				//If the abbreviation does correspond, return that building
				if (nextBuilding != null) {
					return nextBuilding;
				} else {
				//event doesn't correspond to a building remove from list and try again
					EventList.Remove (nextEvent);
					return GetNextEventBuilding (EventList);
				}

			}else{
				//Couldn't locate next Building from list
				return null;
			}
		}



		static String LocationToBuildingAbbreviation(String eventLocation){

			String abbreviation =  Regex.Replace (eventLocation, @"[\d-]",string.Empty);
			if (abbreviation.Length < 3 && abbreviation.Length > 0) {
				return abbreviation;
			} else {
				return null;
			}

		}

		Building AbbreviationToBuilding(String buildingAbbreviation){
			BuildingManager.InitializeLoyolaBuildings ();
			BuildingManager.InitializeSGWBuildings ();

			foreach (Building b in BuildingManager.GetSGWBuildings()) {
				if (b.Abbreviation == buildingAbbreviation) {
					return b;
				}
			}

			foreach (Building b in BuildingManager.GetLoyolaBuildings()) {
				if (b.Abbreviation == buildingAbbreviation) {
					return b;
				}
			}
			return null;


		}


	}
}

