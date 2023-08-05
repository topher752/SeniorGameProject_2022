using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObjectBlockingObject : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform player;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float fadeAlpha = 0.33f;

    [SerializeField] private float checksPerSecond = 10;
    [SerializeField] private int fadeFPS = 30;
    [SerializeField] private float fadeSpeed = 1;
    [SerializeField] private FadeMode fadingMode;

    [Header("Read Only Data")]
    [SerializeField] private List<FadingObject> objectsBlockingView = new List<FadingObject>();
    private List<int> indexesToColor = new List<int>();
    private Dictionary<FadingObject, Coroutine> runningCoroutines = new Dictionary<FadingObject, Coroutine>();

    private RaycastHit[] Hits = new RaycastHit[10];

    private void Start()
    {
        StartCoroutine(CheckForObjects());
    }

    private IEnumerator CheckForObjects()
    {
        WaitForSeconds Wait = new WaitForSeconds(1f / checksPerSecond);

        while (true)
        {
            int hits = Physics.RaycastNonAlloc(mainCamera.transform.position, (player.transform.position - mainCamera.transform.position).normalized, Hits, Vector3.Distance(mainCamera.transform.position, player.transform.position), layerMask);
            if (hits > 0)
            {
                for (int i = 0; i < hits; i++)
                {
                    FadingObject fadingObject = GetFadingObjectFromHit(Hits[i]);
                    if (fadingObject != null && !objectsBlockingView.Contains(fadingObject))
                    {
                        if (runningCoroutines.ContainsKey(fadingObject))
                        {
                            if (runningCoroutines[fadingObject] != null) // may be null if it's already ended
                            {
                                StopCoroutine(runningCoroutines[fadingObject]);
                            }

                            runningCoroutines.Remove(fadingObject);
                        }

                        runningCoroutines.Add(fadingObject, StartCoroutine(FadeObjectOut(fadingObject)));
                        objectsBlockingView.Add(fadingObject);
                    }
                }
            }

            FadeObjectsNoLongerBeingHit();

            ClearHits();

            yield return Wait;
        }
    }

    private void FadeObjectsNoLongerBeingHit()
    {
        for (int i = 0; i < objectsBlockingView.Count; i++)
        {
            bool objectIsBeingHit = false;
            for (int j = 0; j < Hits.Length; j++)
            {
                FadingObject fadingObject = GetFadingObjectFromHit(Hits[j]);
                if (fadingObject != null && fadingObject == objectsBlockingView[i])
                {
                    objectIsBeingHit = true;
                    break;
                }
            }

            if (!objectIsBeingHit)
            {
                if (runningCoroutines.ContainsKey(objectsBlockingView[i]))
                {
                    if (runningCoroutines[objectsBlockingView[i]] != null)
                    {
                        StopCoroutine(runningCoroutines[objectsBlockingView[i]]);
                    }
                    runningCoroutines.Remove(objectsBlockingView[i]);
                }

                runningCoroutines.Add(objectsBlockingView[i], StartCoroutine(FadeObjectIn(objectsBlockingView[i])));
                objectsBlockingView.RemoveAt(i);
            }
        }
    }

    private IEnumerator FadeObjectOut(FadingObject FadingObject)
    {
        float waitTime = 1f / fadeFPS;
        WaitForSeconds Wait = new WaitForSeconds(waitTime);
        int ticks = 1;

        for (int i = 0; i < FadingObject.materials.Count; i++)
        {
            FadingObject.materials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha); // affects both "Transparent" and "Fade" options
            FadingObject.materials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha); // affects both "Transparent" and "Fade" options
            FadingObject.materials[i].SetInt("_ZWrite", 0); // disable Z writing
            if (fadingMode == FadeMode.Fade)
            {
                FadingObject.materials[i].EnableKeyword("_ALPHABLEND_ON");
            }
            else
            {
                FadingObject.materials[i].EnableKeyword("_ALPHAPREMULTIPLY_ON");
            }

            FadingObject.materials[i].renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        }

        if (FadingObject.materials[0].HasProperty("_Color"))
        {
            while (FadingObject.materials[0].color.a > fadeAlpha)
            {
                for (int i = 0; i < FadingObject.materials.Count; i++)
                {
                    if (FadingObject.materials[i].HasProperty("_Color"))
                    {
                        FadingObject.materials[i].color = new Color(
                            FadingObject.materials[i].color.r,
                            FadingObject.materials[i].color.g,
                            FadingObject.materials[i].color.b,
                            Mathf.Lerp(FadingObject.initialAlpha, fadeAlpha, waitTime * ticks * fadeSpeed)
                        );
                    }
                }

                ticks++;
                yield return Wait;
            }
        }

        if (runningCoroutines.ContainsKey(FadingObject))
        {
            StopCoroutine(runningCoroutines[FadingObject]);
            runningCoroutines.Remove(FadingObject);
        }
    }

    private IEnumerator FadeObjectIn(FadingObject FadingObject)
    {
        float waitTime = 1f / fadeFPS;
        WaitForSeconds Wait = new WaitForSeconds(waitTime);
        int ticks = 1;

        if (FadingObject.materials[0].HasProperty("_Color"))
        {
            while (FadingObject.materials[0].color.a < FadingObject.initialAlpha)
            {
                for (int i = 0; i < FadingObject.materials.Count; i++)
                {
                    if (FadingObject.materials[i].HasProperty("_Color"))
                    {
                        FadingObject.materials[i].color = new Color(
                            FadingObject.materials[i].color.r,
                            FadingObject.materials[i].color.g,
                            FadingObject.materials[i].color.b,
                            Mathf.Lerp(fadeAlpha, FadingObject.initialAlpha, waitTime * ticks * fadeSpeed)
                        );
                    }
                }

                ticks++;
                yield return Wait;
            }
        }

        for (int i = 0; i < FadingObject.materials.Count; i++)
        {
            if (fadingMode == FadeMode.Fade)
            {
                FadingObject.materials[i].DisableKeyword("_ALPHABLEND_ON");
            }
            else
            {
                FadingObject.materials[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
            }
            FadingObject.materials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            FadingObject.materials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            FadingObject.materials[i].SetInt("_ZWrite", 1); // re-enable Z Writing
            FadingObject.materials[i].renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
        }

        if (runningCoroutines.ContainsKey(FadingObject))
        {
            StopCoroutine(runningCoroutines[FadingObject]);
            runningCoroutines.Remove(FadingObject);
        }
    }

    private FadingObject GetFadingObjectFromHit(RaycastHit Hit)
    {
        return Hit.collider != null ? Hit.collider.GetComponent<FadingObject>() : null;
    }

    private void ClearHits()
    {
        RaycastHit hit = new RaycastHit();
        for (int i = 0; i < Hits.Length; i++)
        {
            Hits[i] = hit;
        }
    }

    public enum FadeMode
    {
        Transparent,
        Fade
    }
}
