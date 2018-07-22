using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class tttt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		NCMBObject scoreClass = new NCMBObject("ScoreClass");
		//これ以降にniftyCloudでの動作を書くよ
		//まずはscoreClassに保存内容を入れて
		scoreClass["name"] = 3;
		// scoreClass["score"] = 1;

		// ここでpush
		
		scoreClass.SaveAsync();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
