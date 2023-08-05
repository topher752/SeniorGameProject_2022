using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


// This script must be able to get LoreScript numbers 1, 3, and 5

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;

    // Booleans for next Text
    public bool fruitBossDefeated;
    public bool dairyBossDefeated;

    // Getting Lore Pages
    public LorePickups loreCode;

    //Audio
    public AudioSource audioSource;
    public AudioClip nPCTalkingShort;
    public CharacterController playerController;

    //Black Screen
    public GameObject blackScreen;
    public GameObject blackScreen2;

    //Door Blockers
    public GameObject fruitVegBlockers;
    public GameObject dairyBlockers;

    //Animations
    public Animator animator;
    public PlayerAttack disableAttack;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        fruitBossDefeated = false;
        dairyBossDefeated = false;
        loreCode = GameObject.FindGameObjectWithTag("Player").GetComponent<LorePickups>();
        index = 0;
    }

    public void Called()
    {
        gameObject.SetActive(true);
        Cursor.visible = true;
        textComponent.text = string.Empty;
        
        if (index != 0)
        {
            index ++;
        }

        StartCoroutine(TypeLine());
        playerController.enabled = false;
        animator.SetFloat("Speed", 0.0f);
        disableAttack.canUseMouseButton = false;
    }

    public void Next()
    {
        if (textComponent.text == lines[index])
        {
            NextLine();
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            if (index < 5)
            {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
                loreCode.Unlock1();
                audioSource.PlayOneShot(nPCTalkingShort);
            }
            else if (fruitBossDefeated && !dairyBossDefeated)
            {
                if (index < 7)
                {
                    index++;
                    textComponent.text = string.Empty;
                    StartCoroutine(TypeLine());
                    audioSource.PlayOneShot(nPCTalkingShort);
                }
                else
                {
                    gameObject.SetActive(false);
                    Cursor.visible = false;
                    StopAllCoroutines();
                    textComponent.text = lines[index + 1];
                    loreCode.Unlock3();
                    playerController.enabled = true;
                    blackScreen2.SetActive(true);
                    dairyBlockers.SetActive(false);
                    disableAttack.canUseMouseButton = true;
                }
            }
            else if (dairyBossDefeated)
            {
                index++;
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
                loreCode.Unlock5();
                audioSource.PlayOneShot(nPCTalkingShort);
            }
            else if (index != lines.Length - 1)
            {
                gameObject.SetActive(false);
                Cursor.visible = false;
                StopAllCoroutines();
                textComponent.text = lines[index];
                playerController.enabled = true;
                blackScreen.SetActive(true);
                fruitVegBlockers.SetActive(false);
                disableAttack.canUseMouseButton = true;
            }
        }
        else
        {
            SceneManager.LoadScene("WinGameScene");
        }
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void setDairyBoss(bool change)
    {
        dairyBossDefeated = change;
    }

    public void setFruitBoss(bool change)
    {
        fruitBossDefeated = change;
    }
}
