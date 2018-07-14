using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class PlayerScript : MonoBehaviour {
    public Camera camera;
    public StageManager stageManager;
    public PlayerScript playerScript;
    // Shotボタンを押した回数
    public int shotCount = 0;

    public void Shot () {
        Debug.Log("Bang!");

        if (stageManager.seconds <= 10.0f && stageManager.seconds >= 0.0f) {
            int distance = 10;
            Vector3 center = new Vector3 (Screen.width / 2, Screen.height / 2, 0);
            Ray ray = camera.ScreenPointToRay (center);
            RaycastHit hitInfo;
            shotCount++;

            if (Physics.Raycast (ray, out hitInfo, distance)) {
                // Debug.DrawLine (ray.origin, hitInfo.point, Color.red);
                // Debug.Log ("ok" + hitInfo.collider.name);
                stageManager.GetComponent<UnityEngine.XR.iOS.StageManager> ().DestoryObject (hitInfo.collider.gameObject);
            }
        }
    }
}