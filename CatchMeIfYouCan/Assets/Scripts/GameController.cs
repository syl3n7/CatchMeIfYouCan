using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public void LoadScene(string scenename) //serve para carregar as scenes
    {
        SceneManager.LoadScene(scenename);
    }

    public void QuitGame() //serve para terminar o processo onde o jogo e executado
    {
        Application.Quit();
    }
}

