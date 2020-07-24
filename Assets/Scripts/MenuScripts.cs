using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{

    public void PlayGame() {
        SceneManager.LoadScene("Solve");    
    }

    public void QuitGame() {
        Debug.Log("Quit!");
        Application.Quit();
    }

}
