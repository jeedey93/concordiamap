using System;

namespace GoogleApiTest
{
	public class GooglePlace
	{
		GooglePlaceLocation location;
		string name;

		public GooglePlace(GooglePlaceLocation loc, string name){
			location = loc;
			this.name = name;
		}

		public GooglePlaceLocation getLocation(){
			return location;
		}

		public string getName(){
			return name;
		}

		public void setLocation(GooglePlaceLocation loc){
			location = loc;
		}

		public void setName(string name){
			this.name = name;
		}

		public string ToString(){
			return "---------------------------------------------\nPlace - " + name + "\n" + location.ToString ();
		}

		public GooglePlace clone(){
			return new GooglePlace (location, name);
		}

	}
}

