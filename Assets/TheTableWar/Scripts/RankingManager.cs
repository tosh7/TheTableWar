using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;

public class RankingManager : MonoBehaviour {

	// Use this for initialization
	private DatabaseReference timeRankDB;
	void Start () {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://thetablewar-14053.firebaseio.com/");
		timeRankDB = FirebaseDatabase.DefaultInstance.GetReference ("time ranks");

		//bossNoのノードからtimeで昇順ソートして最大10件を取る（非同期)
		timeRankDB.Child("-LHvsVG0sasa_0YOsCZh".ToString ()).OrderByChild ("time").LimitToFirst (1).GetValueAsync ().ContinueWith (task => {
			Debug.Log ("hey");
			if (task.IsFaulted) { //取得失敗
				//Handle the Error
				Debug.Log ("error");
			} else if (task.IsCompleted) { //取得成功
				Debug.Log ("hello");
				DataSnapshot snapshot = task.Result; //結果取得
				Debug.Log(snapshot);
				// IEnumerator<DataSnapshot> en = snapshot.Children.GetEnumerator (); //結果リストをenumeratorで処理
				int rank = 0;

				// while (en.MoveNext ()) { //１件ずつ処理
					Debug.Log (1);
					// DataSnapshot data = en.Current; //データ取る
					Debug.Log (2);
					string name = (string) snapshot.Child ("name").GetValue (true); //名前取る
					string time = (string) snapshot.Child ("time").GetValue (true); //時間を取る
					Debug.Log (3);
					//順位のuGUIに値を設定
					// GameObject rankItem = rankList.transform.GetChild (rank).gameObject;
					// TimeRank timeRank = rankItem.GetComponent<TimeRank> ();
					// timeRank.SetText (rank + 1, name, getTimeStr (time)); //順位1位から
					rank++;
					Debug.Log (time);
				// }
			}
		});
	}

	// Update is called once per frame
	void Update () {

	}
}