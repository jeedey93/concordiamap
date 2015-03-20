
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Common.Apis;
using Android.Gms.Common;
using Android.Gms.Plus;
using Android.Gms.Plus.Model.People;

namespace GoogleApiTest
{
	[Activity (Label = "CalendarActivity")]			
	public class CalendarActivity : Activity, IGoogleApiClientConnectionCallbacks, IGoogleApiClientOnConnectionFailedListener
	{
		private IGoogleApiClient mGoogleApiClient;
		private SignInButton mGoogleSignIn;

		private bool mIntentInProgress;
		private bool mSignInClicked;
		private bool mInfoPopulated;

		private ConnectionResult mConnectionResult;

		protected override void OnCreate (Bundle bundle)
		{
<<<<<<< HEAD
			base.OnCreate (bundle, Resource.Layout.LeftDrawer);
=======
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Calendar);


			mGoogleSignIn = FindViewById<SignInButton> (Resource.Id.sign_in_button);
			mGoogleSignIn.Click += mGoogleSignIn_Click;

			GoogleApiClientBuilder builder = new GoogleApiClientBuilder (this);
			builder.AddConnectionCallbacks (this);
			builder.AddOnConnectionFailedListener (this);
			builder.AddApi(PlusClass.Api);
			builder.AddScope (PlusClass.ScopePlusProfile);
			builder.AddScope (PlusClass.ScopePlusLogin);

			//Build our IGoogleApiClient
			mGoogleApiClient = builder.Build ();



			//GoogleCalendar calendar = new GoogleCalendar
			//	("Justin Do", "Google account name", "Google account password");
			//CalendarEventObject[] events = calendar.GetEvents ();
			//Console.WriteLine (events);
		}

		void mGoogleSignIn_Click(object sender, EventArgs e){
			//Fire Sign In
			if (!mGoogleApiClient.IsConnecting) {
				mSignInClicked = true;
				ResolveSignInError ();
			}
		}

		private void ResolveSignInError(){
			if (mGoogleApiClient.IsConnected) {
				//No need to resolve errors, already connected
				return;
			}

			if (mConnectionResult.HasResolution) {
				try{
				mIntentInProgress = true;
				StartIntentSenderForResult (mConnectionResult.Resolution.IntentSender, 0, null, 0, 0, 0);
				}
				catch(Android.Content.IntentSender.SendIntentException e){
					// The intent was cancelled before it was sent. Return to the default 
					// state and attempt to connect to get an updated ConnectionResult
					mIntentInProgress = false;
					mGoogleApiClient.Connect (); 
				}
			}
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			if (requestCode == 0) {
				if (resultCode != Result.Ok) {
					mSignInClicked = false;
				}

				mIntentInProgress = false;

				if (!mGoogleApiClient.IsConnecting) {
					mGoogleApiClient.Connect ();
				}
			}
		}

		protected override void OnStart ()
		{
			base.OnStart ();
			mGoogleApiClient.Connect ();
		}

		protected override void OnStop(){
			base.OnStop ();
			if (mGoogleApiClient.IsConnected) {
				mGoogleApiClient.Disconnect ();
			}
		}
<<<<<<< Updated upstream
=======
>>>>>>> origin/master
>>>>>>> Stashed changes

		public void OnConnected (Bundle connectionHint)
		{
			//Successful log in
			mSignInClicked = false;

			if (PlusClass.PeopleApi.GetCurrentPerson (mGoogleApiClient) != null) {
				IPerson plusUser = PlusClass.PeopleApi.GetCurrentPerson (mGoogleApiClient);
				Console.WriteLine (plusUser.DisplayName);
			}
		}

		public void OnConnectionSuspended (int cause)
		{
			throw new NotImplementedException ();
		}

		public void OnConnectionFailed (ConnectionResult result)
		{
			if (!mIntentInProgress) {
				//Store the ConnectionResults so that we can use it later when the user click sign in
				mConnectionResult = result;
			}

			if (mSignInClicked) {
				//The user has already clicked sign in so we attempt to resolve all errors until the user is signed in or the cancel
				ResolveSignInError ();
			}
		}
	}
}

