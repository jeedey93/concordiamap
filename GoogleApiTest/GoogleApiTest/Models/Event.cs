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
			return null;
		}

	}
}

