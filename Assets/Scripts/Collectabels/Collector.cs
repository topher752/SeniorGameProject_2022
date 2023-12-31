using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public void OnTriggerEnter(Collider other) 
    {
        ICollectable collectible = other.GetComponent<ICollectable>();

        if (collectible != null)
        {
            collectible.Collect();
        }
    }
}
