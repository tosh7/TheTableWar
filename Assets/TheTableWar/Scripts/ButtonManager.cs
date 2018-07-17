using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
    public void BackButton()
    {
        SceneManager.LoadScene("Title");
    }

    public void RegisterButton()
    {
        SceneManager.LoadScene("Ranking");
    }
}
