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
	[Activity (Label = "BusScheduleActivity")]			
	public class BusScheduleActivity : LeftDrawerActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle,Resource.Layout.BusSchedule);
		}
	}
}