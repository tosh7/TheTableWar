using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.iOS;

public class FinalScore : MonoBehaviour
{
    public Text playerScoreText;
    // Use this for initialization
    void Start()
    {
        playerScoreText.text = StageManager.GetFinalScore().ToString();
    }
}
