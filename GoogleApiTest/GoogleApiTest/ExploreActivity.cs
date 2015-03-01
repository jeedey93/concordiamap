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
		protected override void OnCreate (Bundle bundle)
		{

			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ExploreView);

			// Get reference to each button within exploreMenu Layout
			Button retaurantBtn = (Button)FindViewById<Button> (Resource.Id.exploreMBRestaurant);
			Button coffeeBtn = (Button)FindViewById<Button> (Resource.Id.exploreMBCoffee);
			Button barBtn = (Button)FindViewById<Button> (Resource.Id.exploreMBBar);
			Button pharmacyBtn = (Button)FindViewById<Button> (Resource.Id.exploreMBPharmacy);


			// Set all onClick events to above buttons
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

			pharmacyBtn.Click += (sender, e) => {
				//Do Something
				Console.WriteLine ("Convinience");

				var exploreListActivity = new Intent (this, typeof(ExploreListActivity));
				StartActivity (exploreListActivity);
			};
		}
			
	}
}

