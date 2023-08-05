using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public Transform attackPoint;
    public GameObject objectToThrow;
    public GameObject throwBurntObject;
    public WeaponClass throwingStars;

    public float throwForce;
    public float throwUpwardForce;
    private GameObject projectile;

    // Start is called before the first frame update
    public void Throw()
    {
        StartCoroutine(Strike());
    }

    IEnumerator Strike()
    {
        yield return new WaitForSeconds(0.3f);

        if (throwingStars.isBurnt)
        {
            projectile = Instantiate(throwBurntObject, attackPoint.position, attackPoint.rotation);
        }
        else
        {
            projectile = Instantiate(objectToThrow, attackPoint.position, attackPoint.rotation);
        }

        Destroy(projectile, 3.0f);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
        Vector3 forceToAdd = attackPoint.transform.forward * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
    }
}
