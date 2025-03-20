using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameManager gManager;

    Transform playerTransform;
    Rigidbody2D playerRigidbody;

    float moveSpeed = 4.5f;
    float jumpPower = 300f;

    Transform firePoint;
    public GameObject bulletPrefab;

    private bool water = false;
    private bool earth = false;
    private bool wind = false;
    private bool fire = false;


    void p_Move_Top()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        playerTransform.Translate(horizontal * moveSpeed * Time.deltaTime, 0f, 0f);
        playerTransform.Translate(0f, vertical * moveSpeed * Time.deltaTime, 0f);
    }

    void p_Move_2D()
    {
        float horizontal = Input.GetAxis("Horizontal");
        playerTransform.Translate(horizontal * moveSpeed * Time.deltaTime, 0f, 0f);

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            playerRigidbody.AddForce(Vector3.up * jumpPower);
        }
    }


    void Change_View()
    {
        if (gManager.is_topView)
        {
            playerRigidbody.gravityScale = 0f;
            p_Move_Top();
            //playerRigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        if (!gManager.is_topView)
        {
            playerRigidbody.gravityScale = 1f;
            p_Move_2D();
            //playerRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    void Shoot_Element()
    {
        if(water)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                BulletScript bulletScript = bullet.GetComponent<BulletScript>();
                if(bulletScript != null)
                {
                    bulletScript.SetElement("Water");
                    if(playerTransform.localScale.x <= 0f)
                    {
                        bulletScript.direction = -1f;
                    }
                }
            }
        }
    }
    void Start()
    {
        playerTransform = this.GetComponent<Transform>();
        playerRigidbody = this.GetComponent<Rigidbody2D>();
        firePoint = transform.GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Change_View();
        Shoot_Element();
    }

    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    Debug.Log("충돌감지");
    //    if(col.gameObject.tag == "E_Water")
    //    {
    //        water = true;
    //    }
    //    if (col.gameObject.tag == "E_Earth")
    //    {

    //    }
    //    if (col.gameObject.tag == "E_Wind")
    //    {

    //    }
    //    if (col.gameObject.tag == "E_Fire")
    //    {

    //    }
    //}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "E_Water")
        {
            water = true;
        }
        if (col.gameObject.tag == "E_Earth")
        {

        }
        if (col.gameObject.tag == "E_Wind")
        {

        }
        if (col.gameObject.tag == "E_Fire")
        {

        }
    }
}
