using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject panel;
    Scene sCene;
    
    void Start()
    {
        Application.targetFrameRate = 60; //limit fps to 60
        Cursor.visible = true;
        sCene = SceneManager.GetActiveScene();
        if (sCene.name != "Credits" && sCene.name != "Instructions" && sCene.name != "MainMenu" && sCene.name != "Options" && sCene.name != "PlayerChoice") {panel.SetActive(false);}
    }

    void Update()
    {
        if (sCene.name != "Level1") Cursor.visible = true;
        else Cursor.visible = false;
    }

    void OnPause() //controlar o menu de pausa em todas as scenes exceto main menu
    {
        if (sCene.name != "Credits" && sCene.name != "Instructions" && sCene.name != "MainMenu" && sCene.name != "Options" && sCene.name != "PlayerChoice"){
            
            panel.SetActive(!panel.activeSelf);

            if (panel.activeSelf) {
                Cursor.visible = true;
                Time.timeScale = 0f;
            }
            else {
                Cursor.visible = false;
                Time.timeScale = 1f; 
            }
        }
    }

    public void LoadScene(string sceneName) //serve para carregar as scenes
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame() //serve para terminar o processo onde o jogo e executado
    {
        Application.Quit();
    }    
}

