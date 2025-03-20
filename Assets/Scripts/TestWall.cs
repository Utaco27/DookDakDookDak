using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWall : MonoBehaviour
{
    public GameManager gManager;

    Rigidbody2D wallRigidbody;
    void change_View()
    {
        if (gManager.is_topView)
        {
            wallRigidbody.gravityScale = 0f;
            //playerRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        if (!gManager.is_topView)
        {
            wallRigidbody.gravityScale = 1f;
            //playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    void Start()
    {
        wallRigidbody = this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        change_View();
    }
}
