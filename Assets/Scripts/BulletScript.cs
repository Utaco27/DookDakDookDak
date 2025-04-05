using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float speed = 5f;
    public float direction = 1f;
    public void SetElement(string element)
    {
        gameObject.tag = "Bullet_" + element;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "B_Earth")
        {
            Destroy(this.gameObject);
        }

    }
    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, 0f, 0f);
    }
}
