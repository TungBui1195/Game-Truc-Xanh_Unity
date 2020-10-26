using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public
       //Text guessText;
    int a = 10;
    public void goNextScene(string name)
    {
        Debug.Log("we are going to" + name + "scene");
        SceneManager.LoadScene(name);
       // guessText.text = a.ToString();
    }

    public void quitScene()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }
}
