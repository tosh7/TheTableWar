using System;
using System.Collections.Generic;

namespace UnityEngine.XR.iOS
{
    public class UnityARHitTestExample : MonoBehaviour
    {

        public Camera cam;
		public GameObject monster;

        void Update()
        {
            if (Input.touchCount > 0 && cam != null)
            {
                //CreatePrimitiveで動的にGameObjectであるCubeを生成する
                // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                GameObject cube = Instantiate(monster);
                //Cubeに適用するランダムな色を生成する
                // Material material = new Material(Shader.Find("Diffuse"))
                // {
                //     color = new Color(Random.value, Random.value, Random.value)
                // };
                //ランダムに変化する色をCubeに適用する
                // cube.GetComponent<Renderer>().material = material;
                //Android端末をタップして、ランダムな色のCubeを認識した平面上に投げ出すように追加していく
                //Cubeの大きさも0.2fとして指定している
                cube.transform.position = cam.transform.TransformPoint(0, 0, 0.5f);
                cube.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                //CubeにはRigidbodyを持たせて重力を与えておかないと、床の上には配置されないので注意が必要。Rigidbodyで重力を持たせないとCubeは宙に浮いた状態になる
                cube.AddComponent<Rigidbody>();
                cube.GetComponent<Rigidbody>().AddForce(cam.transform.TransformDirection(0, 1f, 2f), ForceMode.Impulse);

				
				// monster.transform.position = cam.transform.TransformPoint(0, 0, 0.5f);
				// monster.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

				// monster.AddComponent<Rigidbody>();
				// monster.GetComponent<Rigidbody>().AddForce(cam.transform.TransformDirection(0, 1f, 2f), ForceMode.Impulse);
            }
        }
        // public Transform m_HitTransform;
        // public float maxRayDistance = 30.0f;
        // public LayerMask collisionLayer = 1 << 10;  //ARKitPlane layer

        // bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes)
        // {
        //     List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
        //     if (hitResults.Count > 0) {
        //         foreach (var hitResult in hitResults) {
        //             Debug.Log ("Got hit!");
        //             m_HitTransform.position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
        //             m_HitTransform.rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);
        //             Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", m_HitTransform.position.x, m_HitTransform.position.y, m_HitTransform.position.z));
        //             return true;
        //         }
        //     }
        //     return false;
        // }

        // // Update is called once per frame
        // void Update () {
        // 	#if UNITY_EDITOR   //we will only use this script on the editor side, though there is nothing that would prevent it from working on device
        // 	if (Input.GetMouseButtonDown (0)) {
        // 		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        // 		RaycastHit hit;

        // 		//we'll try to hit one of the plane collider gameobjects that were generated by the plugin
        // 		//effectively similar to calling HitTest with ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent
        // 		if (Physics.Raycast (ray, out hit, maxRayDistance, collisionLayer)) {
        // 			//we're going to get the position from the contact point
        // 			m_HitTransform.position = hit.point;
        // 			Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", m_HitTransform.position.x, m_HitTransform.position.y, m_HitTransform.position.z));

        // 			//and the rotation from the transform of the plane collider
        // 			m_HitTransform.rotation = hit.transform.rotation;
        // 		}
        // 	}
        // 	#else
        // 	if (Input.touchCount > 0 && m_HitTransform != null)
        // 	{
        // 		var touch = Input.GetTouch(0);
        // 		if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
        // 		{
        // 			var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
        // 			ARPoint point = new ARPoint {
        // 				x = screenPosition.x,
        // 				y = screenPosition.y
        // 			};

        //             // prioritize reults types
        //             ARHitTestResultType[] resultTypes = {
        //                 ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
        //                 // if you want to use infinite planes use this:
        //                 //ARHitTestResultType.ARHitTestResultTypeExistingPlane,
        //                 ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
        //                 ARHitTestResultType.ARHitTestResultTypeFeaturePoint
        //             }; 

        //             foreach (ARHitTestResultType resultType in resultTypes)
        //             {
        //                 if (HitTestWithResultType (point, resultType))
        //                 {
        //                     return;
        //                 }
        //             }
        // 		}
        // 	}
        // 	#endif

        // }


    }
}

