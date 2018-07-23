using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour {

	// Use this for initialization
	private DatabaseReference timeRankDB;
	ArrayList leaderBoard = new ArrayList ();
	public const int MaxScores = 5;
	public Text[] nameLabelArray = new Text[5];
	public Text[] scoreLabelArray = new Text[5];
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
							//for文などとは違い一回しか呼ばれていない
							// Debug.Log ("Leaders entry : " +
							// 	childSnapshot.Child ("email").Value.ToString () + " - " +
							// 	childSnapshot.Child ("score").Value.ToString ());

							leaderBoard.Insert (1, childSnapshot.Child ("score").Value.ToString () +
								"  " + childSnapshot.Child ("email").Value.ToString ());
							var array = leaderBoard.ToArray ();
							Debug.Log ("配列の長さは" + array.Length);
							for (int i = 0; i < array.Length; i++) {
								Debug.Log (i + "番目は" + array[i]);
							}

							if (array.Length == 6) {
								for (int i = 0; i < 6; i++) {

									nameLabelArray[i].text = returnString (array[i + 1].ToString (), 0);
									scoreLabelArray[i].text = returnString(array[i+1].ToString(), 1);
								}
							}
						}

					}
				}
			};
	}

	string returnString (string str, int num) {
		string reStr;
		char[] charArray = str.ToCharArray ();
		char[] newCharArray　 = new char[10];
		int j = 0;

		for (int i = 0; i < charArray.Length; i++) {
			if (num == 0) {
				if (!char.IsNumber (charArray[i])) {
					newCharArray[j] = charArray[i];
					j++;
				}
			} else if (num == 1) {
				if (char.IsNumber (charArray[i])) {
					newCharArray[j] = charArray[i];
					j++;
				}
			}
		}
		reStr = new string (newCharArray);

		return reStr;
	}
}