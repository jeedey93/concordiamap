using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;

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
			mButtons = makeButtons ();

			var gridView = (GridView) FindViewById<GridView> (Resource.Id.exploreMenu);
			gridView.Adapter = new ExploreMButtonAdapter (this, mButtons);

		}

		// Generate list of buttons
		private List<Button> makeButtons(){
			List<Button> tempList = new List<Button> ();

			tempList.Add (makeRestaurantBtn ());
			tempList.Add (makeCoffeeBtn ());
			tempList.Add (makeBarBtn ());

			return tempList;
		}


		private Button makeRestaurantBtn(){
			Button btn = new Button (this);

			btn.Text = "Retaurant";
			btn.SetBackgroundResource (Resource.Drawable.Restaurant);

			//Event Handler
			btn.Click += (sender, e) => {
				//Do Something
				Console.WriteLine ("Restaurant");
			};
			return btn;
		}

		private Button makeCoffeeBtn(){
			Button btn = new Button (this);

			btn.Text = "Coffee";
			btn.SetBackgroundResource (Resource.Drawable.Coffee);

			//Event Handler
			btn.Click += (sender, e) => {
				//Do Something
				Console.WriteLine ("Coffee");
			};

			return btn;
		}

		private Button makeBarBtn(){
			Button btn = new Button (this);

			btn.Text = "Bar";
			btn.SetBackgroundResource (Resource.Drawable.Bar);

			//Event Handler
			btn.Click += (sender, e) => {
				//Do Something
				Console.WriteLine ("Bar");
			};

			return btn;
		}
	}
}

