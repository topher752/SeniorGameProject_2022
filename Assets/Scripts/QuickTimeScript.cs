using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuickTimeScript : MonoBehaviour
{
    // Lets have a method here that takes in the burnt version of a weapon and the normal version, and
    // depending on what happens in this script, add that weapon into the inventory (which in turn requires Enable burnt)

    // Turn off Player controller
    public Image ring;
    public float QTfillAmount = 0;
    public float timeThreshold = 0;
    public Image bakeFill;
    public TextMeshProUGUI timeLeft;
    public GameObject UI;

    public float currentTime;
    public float startingTime;
    private PlayerMovement playerM;

    // Start is called before the first frame update
    void Start()
    {
        playerM = gameObject.GetComponent<PlayerMovement>();
    }

    public void BeginBaking()
    {
        bakeFill.fillAmount = 0;
        ring.fillAmount = 0;
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (UI.activeSelf)
        {
            if (bakeFill.fillAmount >= 1)
            {
                bakeFill.fillAmount = 0;
                playerM.FinishQuickTime(true);
            }
            
            if (currentTime <= 0f)
            {
                bakeFill.fillAmount = 0;
                playerM.FinishQuickTime(false);
            }

            currentTime -= 1 * Time.deltaTime;
            timeLeft.text = currentTime.ToString("0");
        
            if (Input.GetKeyDown("space"))
            {
                QTfillAmount += .075f;
            }

            timeThreshold += Time.deltaTime;
            if (timeThreshold > .05f)
            {
                timeThreshold = 0;
                QTfillAmount -= .015f;
            }

            if (QTfillAmount < 0)
            {
                QTfillAmount = 0;
            }
            if (QTfillAmount > 1)
            {
                QTfillAmount = 1;
            }

            if (QTfillAmount > 0.68f && QTfillAmount < 0.81f)
            {
                bakeFill.fillAmount += 0.001f;
            }
            else if (QTfillAmount >= 0.81f || QTfillAmount <= 0.68f)
            {
                bakeFill.fillAmount -= 0.005f;
            }
        
            ring.fillAmount = QTfillAmount;

            // Win or Lose condition, turn off QTUI, enable character controller
            // pass a boolean to InventoryManager and weapon type to determine
            // what kind of weapon to enable
        }
    }
}
