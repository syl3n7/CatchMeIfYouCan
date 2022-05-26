using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	public float speed = 3f;


    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag == "disk")
        {
            transform.Rotate(0f, speed * Time.deltaTime / 0.01f, 0f, Space.Self);
        }
        if(gameObject.tag == "Hcapsule")
        {
            transform.Rotate(0f, -speed * Time.deltaTime / 0.01f, 0f, Space.Self);
        }
		
	}
}
