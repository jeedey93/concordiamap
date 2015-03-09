
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace GoogleApiTest
{
	public class ExploreListAdapter : BaseAdapter<GooglePlace> {

		List<GooglePlace> gWebPlaces;
		Context context;

		public ExploreListAdapter(Context context, List<GooglePlace> place){
			this.context = context;
			gWebPlaces = place;
		}
		public override long GetItemId(int position)
		{
			return position;
		}
		public override GooglePlace this[int position] {  
			get { return gWebPlaces.ToArray()[position]; }
		}
		public override int Count {
			get { return gWebPlaces.Count; }
		}
		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View row = convertView;

			if (row == null) {
				row = LayoutInflater.From (context).Inflate (Resource.Layout.ExploreListView_Row, null, false); 
			}

			TextView countText = (TextView)row.FindViewById<TextView> (Resource.Id.exploreLTCount);
			TextView nameText = (TextView)row.FindViewById<TextView> (Resource.Id.exploreLTTitle);
			TextView distanceText = (TextView)row.FindViewById<TextView> (Resource.Id.exploreLTDistance);

			countText.Text = position.ToString ();
			nameText.Text = gWebPlaces.ToArray () [position].getName();
			distanceText.Text = gWebPlaces.ToArray () [position].getDistance().ToString();

			return row;
		}
	}
}

