using System;
using Android.Widget;
using Android.Content;
using Android.Views;
using System.Collections.Generic;

namespace GoogleApiTest
{
	public class ExploreMButtonAdapter : BaseAdapter {  
		private readonly Context context;  
		private List<Button> buttons;

		// Gets the context so it can be used later  
		public ExploreMButtonAdapter(Context c, List<Button> b) {  
			context = c;  
			buttons = b;
		}  

		// Total number of things contained within the adapter  
		public override int Count {  
			get { return buttons.Count; } 
		}  

		// Require for structure, not really used in my code.  
		public override Java.Lang.Object GetItem (int position) {  
			return null;  
		}  

		// Require for structure, not really used in my code. Can  
		// be used to get the id of an item in the adapter for   
		// manual control.   
		public override long GetItemId(int position) {  
			return position;  
		}  

		public override View GetView(int position, View convertView, ViewGroup parent) {  
			Button btn;  
			if (convertView == null) {    
				// if it's not recycled, initialize some attributes  
				btn = buttons.ToArray()[position];  
			}   
			else {  
				btn = (Button) convertView;  
			}  

			return btn;  
		}  
	}  
}

