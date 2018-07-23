using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityEngine.XR.iOS {
    public class StageManager : MonoBehaviour {
        public Transform m_HitTransform;
        public float maxRayDistance = 30.0f;
        public LayerMask collisionLayer = 1 << 10; //ARKitPlane layer
        public GameObject ruinedHouse; //配列の入れ物
        public GameObject monsterA;
        public PlayerScript playerScript;
        public Text timerText, scoreText, accuracyText, messageText, comboText;
        private String comboMessage, accuracyMessage;
        // タイマーとスコア、コンボ
        private int score = 0;
        public static int finalScore = 0;
        public float seconds = 30.0f;
        public float timer;
        private int combo = 0;
        // 敵1体あたりの点数
        private int point = 100;
        // 射撃して敵に当てた回数
        private int hitCount = 0;
        // 射撃精度
        private float accuracy;

        public int num = 0;
        public GameObject bomb;


        public void Start(){
            timer = seconds + 1.5f;
        }
        //指定した場所にオブジェクトを生成
        void CreateObj (Vector3 atPosition, GameObject obj) {
            GameObject floor = Instantiate (obj, atPosition, Quaternion.identity);
            floor.transform.LookAt (obj.transform);
            floor.transform.rotation = Quaternion.Euler (0.0f, floor.transform.rotation.eulerAngles.y, floor.transform.rotation.z);
        }

        void Update () {
            if (num != 0) Game ();
           // Game();

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
                                CreateObj (new Vector3 (position.x, position.y + 0.05f, position.z), ruinedHouse);
                                CreateObj (new Vector3 (position.x, position.y, position.z), monsterA);
                                CreateObj (new Vector3 (position.x + 0.3f, position.y, position.z), monsterA);
                                CreateObj (new Vector3 (position.x + 0.1f, position.y, position.z + 0.8f), monsterA);
                                CreateObj (new Vector3 (position.x, position.y + 0.2f, position.z), monsterA);
                                num++;
                            }
                            break;
                        }
                    }
                }
            }
        }

        public void DestoryObject (GameObject enemy) {
            Debug.Log ("Successed to get a component");
            if (enemy.tag == "enemy") {
                Debug.Log ("if this works, it will be destoroyed");
                hitCount++;
                combo++;
                AddPoint (point, combo);
                if(bomb){
                    Instantiate(bomb, enemy.transform.position, enemy.transform.rotation);
                }
                Destroy (enemy);
            }
            else
            {
                combo = 0;
            }
        }

        void Game () {
            if (timer > -1.6f) timer -= Time.deltaTime;
            if (timer > seconds) messageText.text = "START!";
            if (timer > 0.0f && timer <= seconds) {
                messageText.text = "";
                timerText.text = timer.ToString ("F1");
                if (combo >= 2)
                {
                    comboMessage = combo.ToString() + " combo";
                    comboText.text = comboMessage;
                }
                else
                {
                    comboText.text = "";
                }
                accuracy = hitCount * 100 / playerScript.shotCount;
                //accuracyMessage = (accuracy * 100.0).ToString ("F2") + " %";
                //if (accuracy <= 1.0f) accuracyText.text = accuracyMessage;
                accuracyText.text = accuracy.ToString("F2") + " %";
                finalScore = CalcFinalScore (score, accuracy);
            }
            if (timer > -1.5f && timer <= 0.0f) messageText.text = "FINISH!";
            if (timer <= -1.5f) SceneManager.LoadScene ("Score");
        }

        // 敵の種類によるポイントとコンボを掛け合わせてスコア加算
        public void AddPoint (int point, int combo) {
            if (combo >= 10) combo = 10;
            score += point * combo;
            scoreText.text = score.ToString ();
        }

        // 射撃精度を掛け合わせて最終スコアを計算
        private int CalcFinalScore (int score, float accuracy) {
            return (int) (score * accuracy / 100);
        }

        // Scoreシーンで使用
        public static int GetFinalScore () {
            return finalScore;
        }
    }
}