using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGoo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Environment" || other.gameObject.tag == "GummyBear")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "GummyBear" || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
