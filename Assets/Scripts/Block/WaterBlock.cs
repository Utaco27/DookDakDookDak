using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBlock : MonoBehaviour
{
    //private void OnCollisionEnter2D(Collision2D col)
    //{

    //    Debug.Log("충돌감지");

    //    if (col.gameObject.tag == "Player")
    //    {
    //        col.gameObject.SetActive(false);
    //        UnityEditor.EditorApplication.isPlaying = false;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.SetActive(false);
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
