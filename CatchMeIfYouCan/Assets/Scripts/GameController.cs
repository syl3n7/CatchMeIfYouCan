using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private Scene sCene;
    void Start()
    {
        sCene = GetComponent<Scene>(); // get current scene uppon script load
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {

    }
}
