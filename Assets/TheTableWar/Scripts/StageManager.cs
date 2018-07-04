using System;
using System.Collections.Generic;

namespace UnityEngine.XR.iOS {
    public class StageManager : MonoBehaviour {
        public Transform m_HitTransform;
        public float maxRayDistance = 30.0f;
        public LayerMask collisionLayer = 1 << 10; //ARKitPlane layer
        public GameObject ruinedHouse; //配列の入れ物
        public int num = 0;

        void CreateObj (Vector3 atPosition) {
            GameObject floor = Instantiate (ruinedHouse, atPosition, Quaternion.identity);
            floor.transform.LookAt (ruinedHouse.transform);
            floor.transform.rotation = Quaternion.Euler (0.0f, floor.transform.rotation.eulerAngles.y, floor.transform.rotation.z);
        }

        void Update () {
            if (Input.touchCount > 0 && m_HitTransform != null) {
                var touch = Input.GetTouch (0);
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) {
                    var screenPosition = Camera.main.ScreenToViewportPoint (touch.position);
                    ARPoint point = new ARPoint {
                        x = screenPosition.x,
                        y = screenPosition.y
                    };

                    List<ARHitTestResult> hitresults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, ARHitTestResultType.ARHitTestResultTypeFeaturePoint);

                    if (hitresults.Count > 0) {
                        foreach (var hitResult in hitresults) {
                            Vector3 position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);

                            if (num == 0) {
                                CreateObj (new Vector3 (position.x, position.y, position.z));
                                num++;
                            }
                            break;
                        }
                    }
                }
            }
        }
    }
}