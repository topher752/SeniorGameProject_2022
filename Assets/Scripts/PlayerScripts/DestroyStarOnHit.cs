using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyStarOnHit : MonoBehaviour
{
    public GameObject splat;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Environment")
        {
            Instantiate(splat, transform.position, transform.rotation);
        }
    }
}
