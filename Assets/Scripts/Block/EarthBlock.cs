using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthBlock : MonoBehaviour
{
    private bool ground = false;
    //private void OnCollisionEnter2D(Collision2D col)
    //{

    //    Debug.Log("�浹����");
    //    if (!ground)
    //    {
    //        if (col.gameObject.tag == "E_Water") //�ӽ��ڵ�, ���ļ��� ����
    //        {
    //            ground = true;
    //            transform.GetComponent<BoxCollider2D>().isTrigger = false;
    //            transform.GetComponent<SpriteRenderer>().color = Color.green;
    //        }

    //    }

    //}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!ground)
        {
            if (col.gameObject.tag == "Bullet_Water") //�ӽ��ڵ�, ���ļ��� ����
            {
                ground = true;
                transform.GetComponent<BoxCollider2D>().isTrigger = false;
                transform.GetComponent<SpriteRenderer>().color = Color.green;
                gameObject.tag = "B_Blocked_Earth";
            }

        }
    }
}
