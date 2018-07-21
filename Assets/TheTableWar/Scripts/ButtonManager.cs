using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
    public void BackButton () {
        SceneManager.LoadScene ("Title");
        // SceneManager.UnloadSceneAsync("score");
    }

    public void RegisterButton () {
        SceneManager.LoadScene ("Ranking");
        // SceneManager.UnloadSceneAsync("score");
    }
}