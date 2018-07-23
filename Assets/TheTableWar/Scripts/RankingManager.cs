using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;

public class RankingManager : MonoBehaviour {

	// Use this for initialization
	private DatabaseReference timeRankDB;
	ArrayList leaderBoard = new ArrayList ();
	private const int MaxScores = 5;
	void Start () {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://thetablewar-14053.firebaseio.com/");
		timeRankDB = FirebaseDatabase.DefaultInstance.GetReference ("time ranks");

		leaderBoard.Clear ();
		leaderBoard.Add ("Firebase Top " + MaxScores.ToString () + " Scores");

		StartListener ();

	}

	protected void StartListener () {

		FirebaseDatabase.DefaultInstance
			.GetReference ("Leaders").OrderByChild ("score")
			.ValueChanged += (object sender2, ValueChangedEventArgs e2) => {

				if (e2.DatabaseError != null) {

					Debug.LogError (e2.DatabaseError.Message);
					return;
				}
				Debug.Log ("Received values for Leaders.");
				string title = leaderBoard[0].ToString ();
				leaderBoard.Clear ();
				leaderBoard.Add (title);
				if (e2.Snapshot != null && e2.Snapshot.ChildrenCount > 0) {
					foreach (var childSnapshot in e2.Snapshot.Children) {
						if (childSnapshot.Child ("score") == null ||
							childSnapshot.Child ("score").Value == null) {
							Debug.LogError ("Bad data in sample.  Did you forget to call SetEditorDatabaseUrl with your project id?");
							break;
						} else {
							Debug.Log ("Leaders entry : " +
								childSnapshot.Child ("email").Value.ToString () + " - " +
								childSnapshot.Child ("score").Value.ToString ());
							leaderBoard.Insert (1, childSnapshot.Child ("score").Value.ToString () +
								"  " + childSnapshot.Child ("email").Value.ToString ());
						}
					}
				}
			};
	}

	// Update is called once per frame
	void Update () {

	}
}