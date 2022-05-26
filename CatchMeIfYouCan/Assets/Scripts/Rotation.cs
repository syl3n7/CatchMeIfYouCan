using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    public float turnSpeed = 5f;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float y = Input.GetAxis("Horizontal") * turnSpeed;
        //float x = Input.GetAxis("Mouse Y") * turnSpeed;
        //player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y + y, 0);
        player.transform.Rotate(0, player.transform.rotation.y + y, 0);
        //cam.transform.eulerAngles = new Vector3(cam.transform.eulerAngles.x + x, 0, 0);
    }
}
