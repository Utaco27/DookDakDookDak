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

        pos.x = Mathf.Clamp(pos.x, -1.82f, 47.42f);
        pos.y = Mathf.Clamp(pos.y, -0.4f, 22.2f);
        transform.position = pos;
    }

    private void Update()
    {
        FollowPlayer();
    }

}
