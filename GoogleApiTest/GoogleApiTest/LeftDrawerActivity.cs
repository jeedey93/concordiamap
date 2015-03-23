using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V4.App;
using Android.Views;

namespace GoogleApiTest
{
	[Activity (Label = "LeftDrawer")]			
	public class LeftDrawerActivity: Activity
	{
		DrawerLayout mDrawerLayout;
		List<string> mLeftItem = new List<string> ();
		ArrayAdapter mLeftAdapter;
		ListView mLeftDrawer;
		ActionBarDrawerToggle mDrawerToggle;
		bool MapActivity;

		protected void OnCreate (Bundle bundle, int layout){

			RequestWindowFeature(WindowFeatures.NoTitle);

			base.OnCreate (bundle);

			SetContentView (layout);

			mDrawerLayout = FindViewById<DrawerLayout> (Resource.Id.drawer_layout);
			mLeftDrawer = FindViewById<ListView> (Resource.Id.left_drawer);

			mLeftItem.Add ("Map");
			mLeftItem.Add ("Explore");
			mLeftItem.Add ("My Calendar");
			mLeftItem.Add ("Navigate");
			mLeftItem.Add ("Go to my next class");
			mLeftItem.Add ("Bus Schedule");

			mDrawerToggle = new ActionBarDrawerToggle (
				(Activity) this,
				mDrawerLayout, 
				Resource.Drawable.ic_navigation_drawer, 
				Resource.String.open_drawer,
				Resource.String.close_drawer);

			Console.WriteLine (mDrawerToggle.ToString());

			mLeftAdapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, mLeftItem);
			mLeftDrawer.Adapter = mLeftAdapter;

			MapActivity = IsMapActivity (layout); 

			mLeftDrawer.ItemClick += LeftDrawerItemClick; 

			mDrawerLayout.SetDrawerListener (mDrawerToggle);
			//ActionBar.SetDisplayHomeAsUpEnabled (true);
			//ActionBar.SetHomeButtonEnabled (true);
		}
			

		void LeftDrawerItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{

			if (e.Position == 0 && !MapActivity) {
				Finish ();
			} else if (e.Position == 1) {
				StartActivity (new Intent (this, typeof(ExploreActivity)));
			} else if (e.Position == 2) {
				if (BuildingManager.isDefaultCalendarSelected) {
					var showEvents = new Intent (this, typeof(EventListActivity));
					showEvents.PutExtra ("calId", BuildingManager.DefaultCalendarId);
					StartActivity (showEvents);
				} else
					StartActivity (new Intent (this, typeof(CalendarListActivity)));
			} else if (e.Position == 3) {
				var NavigateActivity = new Intent (this, typeof(NavigateActivity));
				StartActivityForResult (NavigateActivity, 0);
			} else if (e.Position == 4) {
				if (BuildingManager.isDefaultCalendarSelected) {
					var showEvents = new Intent (this, typeof(EventListActivity));
					showEvents.PutExtra ("calId", BuildingManager.DefaultCalendarId);
					showEvents.PutExtra ("isNextClass", true);
					StartActivityForResult (showEvents, 1);
				} else {
					Toast.MakeText (this, "Please choose a default calendar", ToastLength.Short).Show ();
					return;
				}
			} else if (e.Position == 5) {
				var BusSchedule = new Intent (this, typeof(BusScheduleActivity));
				StartActivityForResult (BusSchedule, 0);
			}

		}
	

		protected override void OnPostCreate (Bundle savedInstanceState) 
		{

				base.OnPostCreate (savedInstanceState);
				mDrawerToggle.SyncState ();					
		}

		public override bool OnOptionsItemSelected(IMenuItem item){

			if(mDrawerToggle.OnOptionsItemSelected(item))
				return true;
			return base.OnOptionsItemSelected(item);
		}

		protected override void OnPause ()
		{
			base.OnPause ();

			if (!MapActivity) 
				Finish ();
			else
				mDrawerLayout.CloseDrawers();
		}

		bool IsMapActivity(int Layout){

			if(Layout == Resource.Layout.Main)
				return true;

			return false;
		}
	}
}






