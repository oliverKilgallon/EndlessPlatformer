using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenu : MonoBehaviour {

    public void Quit()
    {
        if (!Application.isEditor)
            Application.Quit();
        #if UNITY_EDITOR
        else
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
