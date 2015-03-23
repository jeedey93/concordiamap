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


		public static List<Event> GetClassesOfDay(List<Event> EventList){
			List<Event> classesOfDay = new List<Event> ();
			if (EventList.Count > 0) {
				DateTime now = DateTime.Now.ToLocalTime ();
				Event nextEvent=null;
				TimeSpan delta = new TimeSpan ();
				TimeSpan closestDelta = new TimeSpan (1, 0, 0); //hh,mm,ss
				foreach (Event e in EventList) {
					//make sure the event time is after now.
					if ((e.mDtStart.CompareTo (now)) > 0) {
						delta = e.mDtStart - now;
						if(delta< closestDelta){
							classesOfDay.Add (e);
						}
					}
				}
				return classesOfDay;
			}else{
				return null;
			}
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
}

