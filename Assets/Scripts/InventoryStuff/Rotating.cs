using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    public bool ifSword = false;
    // Update is called once per frame
    void Update()
    {
        if (ifSword)
        {
            transform.Rotate(0, 0, -.05f);

        }
        else
        {
            transform.Rotate(0, -.05f, 0);
        }
        
    }
}
