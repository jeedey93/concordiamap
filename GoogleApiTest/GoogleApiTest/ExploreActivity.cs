using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content;


namespace GoogleApiTest
{
	[Activity (Label = "ExploreActivity")]			
	public class ExploreActivity : Activity
	{
		//List of buttons wihtin gridview
		private List<Button> mButtons = new List<Button>();

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ExploreView);

			//Creat all buttons and add to list
			//mButtons = makeButtons ();

			//var gridView = (GridView) FindViewById<GridView> (Resource.Id.exploreMenu);
			//gridView.Adapter = new ExploreMButtonAdapter (this, mButtons);
			Button retaurantBtn = (Button)FindViewById<Button> (Resource.Id.exploreMBRestaurant);
			Button coffeeBtn = (Button)FindViewById<Button> (Resource.Id.exploreMBCoffee);
			Button barBtn = (Button)FindViewById<Button> (Resource.Id.exploreMBBar);
			Button convinienceBtn = (Button)FindViewById<Button> (Resource.Id.exploreMBConvinience);

			retaurantBtn.Click += (sender, e) => {
				//Do Something
				Console.WriteLine ("Restaurant");

				var exploreListActivity = new Intent (this, typeof(ExploreListActivity));
				StartActivity (exploreListActivity);
			};

			coffeeBtn.Click += (sender, e) => {
				//Do Something
				Console.WriteLine ("Coffee");

				var exploreListActivity = new Intent (this, typeof(ExploreListActivity));
				StartActivity (exploreListActivity);
			};

			barBtn.Click += (sender, e) => {
				//Do Something
				Console.WriteLine ("Bar");

				var exploreListActivity = new Intent (this, typeof(ExploreListActivity));
				StartActivity (exploreListActivity);
			};

			convinienceBtn.Click += (sender, e) => {
				//Do Something
				Console.WriteLine ("Convinience");

				var exploreListActivity = new Intent (this, typeof(ExploreListActivity));
				StartActivity (exploreListActivity);
			};
		}
			
	}
}

