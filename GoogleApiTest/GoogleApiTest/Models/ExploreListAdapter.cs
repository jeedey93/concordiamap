
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
			row.FindViewById<TextView> (Resource.Id.exploreLTTitle).Text = gWebPlace.GetName ().ToString ().Replace ("\"", "");


			row.FindViewById<TextView> (Resource.Id.exploreLTDistance).TextSize = 9;
			if (gWebPlace.GetDistance () > 1000.0) {
				double distance = gWebPlace.GetDistance ()/1000.0;

				row.FindViewById<TextView> (Resource.Id.exploreLTDistance).Text = String.Format("{0:0.0}" ,distance)+"Km";

			} else {
				row.FindViewById<TextView> (Resource.Id.exploreLTDistance).Text = String.Format("{0:0,0.00}" , gWebPlace.GetDistance())+"m";

			}

			//row.FindViewById<TextView> (Resource.Id.exploreLType).TextSize = 9;
			//row.FindViewById<TextView> (Resource.Id.exploreLType).Text = gWebPlace.GetTypes ().Replace("\"", "");

			row.FindViewById<TextView> (Resource.Id.exploreLAdress).TextSize = 10;
			row.FindViewById<TextView> (Resource.Id.exploreLAdress).Text = gWebPlace.GetAdress ();

			Button btn = row.FindViewById<Button> (Resource.Id.exploreLButton);

			btn.Click += (sender, e) => {
				var mapActivity = new Intent (context, typeof(MapActivity));
				mapActivity.PutExtra ("lat", gWebPlace.GetLat());
				mapActivity.PutExtra("lng", gWebPlace.GetLng());
				mapActivity.PutExtra("name", gWebPlace.GetName());
				mapActivity.PutExtra("adress",gWebPlace.GetAdress());
				context.StartActivity (mapActivity);
			};

			return row;
		}
	}
}

