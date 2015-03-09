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
		String ActivityName;
		private static List<Activity> activities = new List<Activity>();

		protected void OnCreate (Bundle bundle, int layout){
		
			base.OnCreate (bundle);
			activities.Add (this);

			SetContentView (layout);

			mDrawerLayout = FindViewById<DrawerLayout> (Resource.Id.drawer_layout);
			mLeftDrawer = FindViewById<ListView> (Resource.Id.left_drawer);

			mLeftItem.Add ("Map");
			mLeftItem.Add ("Explore");
		

//			mDrawerToggle = new MyActionBarDrawerToggle (
//				this,
//				mDrawerLayout, 
//				Resource.Drawable.ic_navigation_drawer, 
//				Resource.String.open_drawer,
//				Resource.String.close_drawer);

			mLeftAdapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, mLeftItem);
			mLeftDrawer.Adapter = mLeftAdapter;

//
			mLeftDrawer.ItemClick += LeftDrawerItemClick; 

//			mDrawerLayout.SetDrawerListener (mDrawerToggle);
//			ActionBar.SetDisplayHomeAsUpEnabled (true);
//			ActionBar.SetHomeButtonEnabled (true);

		}
			

		void LeftDrawerItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{

			if (e.Position == 0) {
//				System.Threading.Thread.Sleep (5000);
//				StartActivity (new Intent (this, typeof(MapActivity)));
				Finish ();

			}
			else if (e.Position == 1) 

				StartActivity(new Intent (this, typeof(ExploreActivity)));
			}
	

//		protected override void OnPostCreate (Bundle savedInstanceState) 
//		{
//
//			this.OnPostCreate (savedInstanceState);
//			mDrawerToggle.SyncState ();					
//		}
//
//		public override bool OnOptionsItemSelected(IMenuItem item){
//
//			if(mDrawerToggle.OnOptionsItemSelected(item))
//				return true;
//			return base.OnOptionsItemSelected(item);
//		}

	}
}






