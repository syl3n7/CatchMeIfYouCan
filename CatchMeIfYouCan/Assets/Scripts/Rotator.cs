using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	public float speed = 3f;


    // Update is called once per frame
    void Update()
    {
        if (!gameObject.CompareTag("disk")) { }
        else transform.Rotate(0f, speed * Time.deltaTime / 0.01f, 0f, Space.Self);

        if (!gameObject.CompareTag("Hcapsule")) { }
        else transform.Rotate(0f, -speed * Time.deltaTime / 0.01f, 0f, Space.Self);
	}
}
