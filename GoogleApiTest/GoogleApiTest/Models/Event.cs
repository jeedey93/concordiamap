using System;
using System.Collections.Generic;


namespace GoogleApiTest
{
	public class Event
	{
		public int mCalendarId;
		public int mEventId;
		public string mTitle;
		public DateTime mDtStart;
		public string mEventLocation;

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
				TimeSpan closestDelta = new TimeSpan (1000000, 50, 50); //hh,mm,ss
				foreach (Event e in EventList) {
					//make sure the event time is after now.
					if ((e.mDtStart.CompareTo (now)) > 0) {
						nextEvent = e;
						break;
					}
				}
				return nextEvent;
			}else{
				return null;
			}

		}
	}
}

