using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneraController : MonoBehaviour
{
    public Transform playerTransform;

    private void FollowPlayer()
    {
        Vector3 pos;
        pos.x = Mathf.Lerp(transform.position.x, playerTransform.position.x, 0.3f);
        pos.y = Mathf.Lerp(transform.position.y, playerTransform.position.y, 0.3f);
        pos.z = transform.position.z;

        pos.x = Mathf.Clamp(pos.x, 0f, 61.5f);
        pos.y = Mathf.Clamp(pos.y, 0f, 27.9f);

        transform.position = pos;
    }

    private void Update()
    {
        FollowPlayer();
    }

}
