using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
	public Camera camera;
	public GameObject stageManager;
	public PlayerScript ps;

	// Use this for initialization
	void Start () {
		Debug.Log("here");
	}
	
	// Update is called once per frame
	void Update () {
		shot();
	}

	void shot() {
		int distance = 10;
		Vector3 center = new Vector3(Screen.width/2, Screen.height/2, 0);
		Ray ray = camera.ScreenPointToRay(center);
		RaycastHit hitInfo;

		if(Physics.Raycast(ray, out hitInfo, distance)) {
			Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
			Debug.Log("ok" + hitInfo.collider.name);
			stageManager.GetComponent<UnityEngine.XR.iOS.StageManager>().DestoryObject(hitInfo.collider.gameObject);
		}
	}
}
