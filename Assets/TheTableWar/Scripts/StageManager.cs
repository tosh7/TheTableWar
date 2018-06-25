using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {
    public GameObject floor;

    void Start() {
        
    }

    void Update() {
        if(Input.touchCount > 0) {
            Instantiate(floor, new Vector3(0, 0, 0), Quaternion.identity );
            floor.AddComponent<Rigidbody>();
        }
    }
}