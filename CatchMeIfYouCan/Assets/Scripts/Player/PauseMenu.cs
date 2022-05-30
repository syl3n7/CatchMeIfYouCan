using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private InputManager inputManager;
    public GameObject painel;

    private void Start()
    {
        Application.targetFrameRate = 60; //limit fps to 60
        inputManager = GetComponent<InputManager>();
        Cursor.lockState = CursorLockMode.Locked;
        painel.SetActive(false);
    }

    private void Update()
    {
        if (inputManager.onFoot.Pause.triggered)
        {
            Cursor.lockState = CursorLockMode.None;
            //Time.timeScale = 0f;
            painel.SetActive(!painel.activeSelf);
        }
    }
}
