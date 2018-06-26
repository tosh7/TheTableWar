using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject floor;
    public int num = 0;

    void Start()
    {

    }

    void Update()
    {
        if (num == 0)
        {
            if (Input.touchCount > 0)
            {
                GameObject fllor = Instantiate(floor);
                fllor.transform.position = new Vector3(0, 0, 0);
                fllor.AddComponent<Rigidbody>();
            }
            num++;
        }
    }
}