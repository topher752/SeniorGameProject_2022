using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSpawner : MonoBehaviour
{
    public GameObject[] ingredients;
    public bool isFalling = false;

    void Update()
    {
        if (!isFalling)
        {
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        SetParent(gameObject.transform);
        isFalling = true;
        yield return new WaitForSeconds(3.0f);
        isFalling = false;
    }

    private void SetParent(Transform newParent)
    {
        GameObject go = Instantiate(ingredients[Random.Range(0, ingredients.Length)], gameObject.transform.position, gameObject.transform.rotation);
        go.transform.SetParent(newParent);
        Destroy(go, 10.0f);
    }
}
