using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenuEvents : MonoBehaviour
{
    /*
         public void SceneManager() 
    {
        if 
    }

    public void A()
    {

    }
     */

    public void ChangeScene(int i)
    { 
        SceneManager.LoadScene(i);
    }
    public void ExitToDesktop()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }



}
