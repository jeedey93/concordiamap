using System;
using System.Collections.Generic;


namespace GoogleApiTest
{
	public class Event
	{
		int mCalendarId;
		int mEventId;
		string mTitle;
		string mDtStart;
		string mEventLocation;

		public Event (int CalendarId, int EventId, string Title, string DtStart, string EventLocation)
		{
			mCalendarId = CalendarId;
			mEventId = EventId;
			mTitle = Title;
			mDtStart = DtStart;
			mEventLocation = EventLocation;
		}


		public static Event GetNextEvent(List<Event> EventList){


			if (EventList.Count > 0) {
				DateTime now = DateTime.Now.ToLocalTime ();
				Event nextEvent;
				TimeSpan delta = new TimeSpan ();
				TimeSpan closestDelta = new TimeSpan (1000000, 50, 50); //hh,mm,ss
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
}

