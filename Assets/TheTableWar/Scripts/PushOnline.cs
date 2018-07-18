using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;

public class PushOnline : MonoBehaviour {

	private DatabaseReference timeRankDB;

	// Use this for initialization
	void Start () {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://thetablewar-14053.firebaseio.com/");
		timeRankDB = FirebaseDatabase.DefaultInstance.GetReference ("time ranks");
		string key = timeRankDB.Child (ToString ()).Push ().Key;
		//登録する１件データをDictionaryで定義(nameとtime)
		Dictionary<string, object> itemMap = new Dictionary<string, object> ();
		itemMap.Add ("name", 12);
		itemMap.Add ("time", 3);
		//１件データをさらにDictionaryに入れる。キーはノード(bossNo/さっき生成したキー)
		Dictionary<string, object> map = new Dictionary<string, object> ();
		map.Add (ToString () + "/" + key, itemMap);
		//データ更新！
		timeRankDB.UpdateChildrenAsync (map);
	}

	// Update is called once per frame
	void Update () {

	}
}