using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;

    public float xSensitivity = 30f;
    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        //apply this to our camera transform
        cam.transform.localRotation = Quaternion.Euler(20f, 0f, 0f);
        // rotate player to look left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
