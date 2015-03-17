
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
		Activity context;

		public ExploreListAdapter(Activity context, List<GooglePlace> places) : base(){
			this.context = context;
			gWebPlaces = places;
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
			var gWebPlace = gWebPlaces.ToArray () [position];

			//Row re-use
			if (row == null) {
				row = context.LayoutInflater.Inflate (Resource.Layout.ExploreListView_Row, null); 
			}


			//Set text fields wihtin row
			row.FindViewById<TextView> (Resource.Id.exploreLTCount).Text = (position+1).ToString()+".";
			row.FindViewById<TextView> (Resource.Id.exploreLTTitle).Text = gWebPlace.GetName();
			row.FindViewById<TextView> (Resource.Id.exploreLTDistance).Text = String.Format("{0:0,0.00}" , gWebPlace.GetDistance())+"m";


			return row;
		}
	}
}

