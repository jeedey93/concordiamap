using System.Text;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V4.App;
using Android.Views;
using System.Collections.Generic;

namespace GoogleApiTest
{
	[Activity (Label = "BusScheduleActivity")]			
	public class BusScheduleActivity : LeftDrawerActivity
	{
		BusManager BusManager = new BusManager();
		List<string> LOYMonThur = new List<string>();
		List<string> LOYFriday = new List<string>();
		List<string> SGWMonThur = new List<string>();
		List<string> SGWFriday = new List<string>();
		Button MonThurButton;
		Button FridayButton;
		Button SGWButton;
		Button LOYButton;


		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle,Resource.Layout.BusSchedule);

			LOYFriday = BusManager.InitializeLOYBusFriday ();
			LOYMonThur = BusManager.InitializeLOYBusMonThur ();
			SGWFriday = BusManager.InitializeSGWBusFriday ();
			SGWMonThur = BusManager.InitializeSGWBusMonThur ();
		
			MonThurButton = (Button)FindViewById (Resource.Id.buttonMonThur);
			FridayButton = (Button)FindViewById (Resource.Id.buttonFriday);
			SGWButton = (Button)FindViewById (Resource.Id.buttonSGWBus);
			LOYButton = (Button)FindViewById (Resource.Id.buttonLOYBus);

			MonThurButton.SetBackgroundColor (new Android.Graphics.Color(146,35,56,255));
			FridayButton.SetBackgroundColor (new Android.Graphics.Color (146,35,56,100));
			SGWButton.SetBackgroundColor (new Android.Graphics.Color(203,181,118,255));
			LOYButton.SetBackgroundColor (new Android.Graphics.Color (203,181,118,100));

			ArrayAdapter<string> itemsAdapter = new ArrayAdapter<string>(this,Android.Resource.Layout.SimpleListItem1, SGWMonThur);
			ArrayAdapter<string> itemsAdapter1 = new ArrayAdapter<string>(this,Android.Resource.Layout.SimpleListItem1, LOYMonThur);


			ListView listViewSGW = (ListView) FindViewById(Resource.Id.SGWBusListView);
			listViewSGW.SetAdapter (itemsAdapter);

			ListView listViewLOY = (ListView) FindViewById(Resource.Id.LOYBusListView);
			listViewLOY.SetAdapter (itemsAdapter1);

			listViewSGW.SetBackgroundColor(new Android.Graphics.Color(235,221,183,0));

			MonThurButton.Click += (sender, e) => {
				//Do Something
				MonThurButtonClick(itemsAdapter, itemsAdapter1);

				MonThurButton.SetBackgroundColor (new Android.Graphics.Color(146,35,56,255));
				FridayButton.SetBackgroundColor (new Android.Graphics.Color (146,35,56,100));

			};

			FridayButton.Click += (sender, e) => {
				//Do Something
				FridayButtonClick(itemsAdapter, itemsAdapter1);

				MonThurButton.SetBackgroundColor (new Android.Graphics.Color(203,181,118,255));
				FridayButton.SetBackgroundColor (new Android.Graphics.Color (203,181,118,100));
			}; 

			SGWButton.Click += (sender, e) => {

				listViewSGW.SetBackgroundColor(new Android.Graphics.Color(255,255,255,60));
				listViewLOY.SetBackgroundColor(new Android.Graphics.Color(255,255,255,0));

				SGWButton.SetBackgroundColor (new Android.Graphics.Color(203,181,118,255));
				LOYButton.SetBackgroundColor (new Android.Graphics.Color (203,181,118,100));

			};

			LOYButton.Click += (sender, e) => {

				listViewLOY.SetBackgroundColor(new Android.Graphics.Color(255,255,255,80));
				listViewSGW.SetBackgroundColor(new Android.Graphics.Color(255,255,255,0));

				SGWButton.SetBackgroundColor (new Android.Graphics.Color(203,181,118,100));
				LOYButton.SetBackgroundColor (new Android.Graphics.Color (203,181,118,255));
			};



			}
			

			void MonThurButtonClick(ArrayAdapter<string> itemsAdapter, ArrayAdapter<string> itemsAdapter1 ){

				itemsAdapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleListItem1, SGWMonThur);
				itemsAdapter1 = new ArrayAdapter<string>(this,Android.Resource.Layout.SimpleListItem1, LOYMonThur);
				
				ListView listViewSGW = (ListView) FindViewById(Resource.Id.SGWBusListView);
				listViewSGW.SetAdapter (itemsAdapter);

				ListView listViewLOY = (ListView) FindViewById(Resource.Id.LOYBusListView);
				listViewLOY.SetAdapter (itemsAdapter1);

			}

			void FridayButtonClick( ArrayAdapter<string> itemsAdapter, ArrayAdapter<string> itemsAdapter1 ){

				itemsAdapter = new ArrayAdapter<string> (this, Android.Resource.Layout.SimpleListItem1, SGWFriday);
				itemsAdapter1 = new ArrayAdapter<string>(this,Android.Resource.Layout.SimpleListItem1, LOYFriday);

				ListView listViewSGW = (ListView) FindViewById(Resource.Id.SGWBusListView);
				listViewSGW.SetAdapter (itemsAdapter);

				ListView listViewLOY = (ListView) FindViewById(Resource.Id.LOYBusListView);
				listViewLOY.SetAdapter (itemsAdapter1);

			}

	}
}