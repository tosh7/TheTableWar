using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;

public class PushOnline : MonoBehaviour {

	private DatabaseReference timeRankDB;
	//登録する１件データをDictionaryで定義(nameとtime)
	Dictionary<string, object> itemMap = new Dictionary<string, object> ();
	//１件データをさらにDictionaryに入れる。キーはノード(bossNo/さっき生成したキー)
	Dictionary<string, object> map = new Dictionary<string, object> ();
	public string key;
	InputField textField;
	public GameObject inputField;
	string inputValue;
	FinalScore fs;


	// Use this for initialization
	void Start () {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://thetablewar-14053.firebaseio.com/");
		timeRankDB = FirebaseDatabase.DefaultInstance.GetReference ("time ranks");
		//まずbossNoのノードにレコードを登録(Push)して、生成されたキーを取得（これを１件のノード名に使う）
		key = timeRankDB.Child (ToString ()).Push ().Key;
		textField = inputField.GetComponent<InputField>();
		// Debug.Log("final score is " + StageManager.GetFinalScore());
	}

	// Update is called once per frame
	void Update () {

	}

	public void Register() {
		InputLogger();
		itemMap.Add ("name", inputValue);
		itemMap.Add ("time", StageManager.GetFinalScore());
		map.Add (ToString () + "/" + key, itemMap);
		//ここでpushしてる
		timeRankDB.UpdateChildrenAsync (map);
	}
	public void InputLogger() {
        inputValue = textField.text;
        Debug.Log(inputValue);
        textField.text = "";
    }
}