using System;
using System.Collections;
using UnityEngine;

public class ObjTimer: MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        StartCoroutine(ShowAndHide());
    }
    private IEnumerator ShowAndHide()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(5); 
        gameObject.SetActive(true);
    }
}
