using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBlock : MonoBehaviour
{
    bool earth = false;

    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    if ( !earth)
    //    {
    //        if (col.gameObject.tag == "Player")
    //        {
    //            col.gameObject.SetActive(false);
    //            UnityEditor.EditorApplication.isPlaying = false;
    //        }

    //        if (col.gameObject.tag == "E_Earth") //임시코드, 추후수정 예정
    //        {
    //            earth = true;
    //            transform.GetComponent<BoxCollider2D>().isTrigger = false;
    //            transform.GetComponent<SpriteRenderer>().color = Color.gray;
    //        }

    //    }
        
    //}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!earth)
        {
            if (col.gameObject.tag == "Player")
            {
                col.gameObject.SetActive(false);
                UnityEditor.EditorApplication.isPlaying = false;
            }

            if (col.gameObject.tag == "Bullet_Earth") //임시코드, 추후수정 예정
            {
                earth = true;
                transform.GetComponent<BoxCollider2D>().isTrigger = false;
                transform.GetComponent<SpriteRenderer>().color = Color.gray;
            }

            if (col.gameObject.tag == "Bullet_Water")
            {
                gameObject.SetActive(false);
                col.gameObject.SetActive(false);
            }

        }

    }
}
