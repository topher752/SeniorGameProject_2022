using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public Transform attackPoint;
    public GameObject objectToThrow;

    public float throwForce;
    public float throwUpwardForce;

    // Start is called before the first frame update
    public void Throw()
    {
        StartCoroutine(Strike());
    }

    IEnumerator Strike()
    {
        yield return new WaitForSeconds(.5f);

        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, attackPoint.rotation);
        Destroy(projectile, 5.0f);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        Vector3 forceToAdd = attackPoint.transform.forward * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
    }
}
