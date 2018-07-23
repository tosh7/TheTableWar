using System.Collections;
using System.Collections.Generic;
using System;
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
		RegisterAgain();
	}
	public void InputLogger() {
        inputValue = textField.text;
        Debug.Log(inputValue);
        textField.text = "";
    }

	void RegisterAgain(){
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Leaders");
		reference.RunTransaction(AddScoreTransaction)
      .ContinueWith(task => {
      if (task.Exception != null) {
        Debug.Log(task.Exception.ToString());
      } else if (task.IsCompleted) {
        Debug.Log("Transaction complete.");
      }
    });
	}


	TransactionResult AddScoreTransaction(MutableData mutableData) {
    List<object> leaders = mutableData.Value as List<object>;

    if (leaders == null) {
      leaders = new List<object>();
    } else if (mutableData.ChildrenCount >= RankingManager.MaxScores) {
    // If the current list of scores is greater or equal to our maximum allowed number,
    // we see if the new score should be added and remove the lowest existing score.
      long minScore = long.MaxValue;
      object minVal = null;
      foreach (var child in leaders) {
        if (!(child is Dictionary<string, object>))
          continue;
        long childScore = (long)((Dictionary<string, object>)child)["score"];
        if (childScore < minScore) {
          minScore = childScore;
          minVal = child;
        }
      }
      // If the new score is lower than the current minimum, we abort.
      if (minScore > StageManager.GetFinalScore()) {
        return TransactionResult.Abort();
      }
      // Otherwise, we remove the current lowest to be replaced with the new score.
      leaders.Remove(minVal);
    }

    // Now we add the new score as a new entry that contains the email address and score.
    Dictionary<string, object> newScoreMap = new Dictionary<string, object>();
    newScoreMap["score"] = StageManager.GetFinalScore();
    newScoreMap["email"] = inputValue;
    leaders.Add(newScoreMap);

    // You must set the Value to indicate data at that location has changed.
    mutableData.Value = leaders;
    return TransactionResult.Success(mutableData);
  }
}