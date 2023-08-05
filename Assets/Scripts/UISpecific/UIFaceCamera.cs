using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFaceCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void LateUpdate()
    {
        gameObject.transform.rotation = Camera.main.transform.rotation;
    }
}
