using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    #region Main Variables

    public CharacterController controller;
    public float speed = 6f;

    // Turning
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    // Jump and Gravity
    public Transform groundCheck;
    public LayerMask groundMask;
    public float gravity = -9.81f;
    public float groundDistance = 0.2f;
    private Vector3 velocity;
    private bool isGrounded;
    public float jump = 3f;

    // Preliminary Inventory
    private bool isInventoryActive = false;

    // Specifically in Baking Area
    private bool inBaking = false;
    public GameObject inventoryPage;
    public GameObject bakingPage;
    public GameObject recipes;
    public GameObject lore;
    public GameObject colours;
    public GameObject settings;
    public GameObject inventory;
    public GameObject buttons;
    public GameObject saveButton;
    public GameObject quitButton;
    public GameObject infoText;

    public Dialogue dialogueCode;
    public GameObject wantsToTalk;

    // Animation Variables
    private Animator anim;

    //Junk
    public GameObject tooManyWeapons;
    public Image primaryDurability;
    public Image secondaryDurability;

    //Particle System
    public ParticleSystem dust;

    // QuickTime UI
    public GameObject qtUI;
    public CharacterController playerController;
    public string weaponToCraft; 
    public QuickTimeScript qtScript;
    public InventoryManager invScript;

    #endregion

    

    // First Frame
    void Start()
    {
        Cursor.visible = false;
        inventory.SetActive(false);
        inventoryPage.SetActive(true);
        bakingPage.SetActive(false);
        anim = GetComponentInChildren<Animator>();
        infoText.SetActive(false);
        tooManyWeapons.SetActive(false);
        qtUI.SetActive(false);
        playerController = gameObject.GetComponent<CharacterController>();
        qtScript = gameObject.GetComponent<QuickTimeScript>();

        // Testing
        // wantsToTalk.SetActive(false);
    }

    // Character Movement
    void Update()
    {
        // Check if Gravity needs to be applied
        if (!playerController.enabled)
        {
            return;
        }
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isGrounded)
        {
            anim.SetBool("IsGrounded", true);
            anim.SetBool("IsFalling", false);
            anim.SetBool("IsJumping", false);
        }

        if (!isGrounded && velocity.y < 0)
        {
            velocity.y = -6f;
            anim.SetBool("IsFalling", true);
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsGrounded", false);
        }
        

        // Movement
        float moveLR = Input.GetAxisRaw("Horizontal");
        float moveFB = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(moveLR, 0f, moveFB).normalized;

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Velocity value is ~7.67
            velocity.y = Mathf.Sqrt(jump * -2.0f * gravity);

            anim.SetBool("IsJumping", true);
            anim.SetBool("IsGrounded", false);
            anim.SetBool("IsFalling", false);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Smooth Turning and Direction
        if (direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
            Walk();

            if (isGrounded)
            {
                CreateDust();
            }
            return;
        }
        else
        {
            Idle();
        }
    }

    #region Particle System
    
    private void CreateDust()
    {
        dust.Play();
    }

    #endregion

    #region UI Methods

    // Checking if we are in the Baking Area, affecting UI elements OUTSIDE UI Script
    private void OnTriggerEnter(Collider other)
    {
        infoText.SetActive(true);
        
        if (other.tag == "Baking")
        {
            inBaking = true;
            buttons.SetActive(false);
            saveButton.SetActive(false);
            quitButton.SetActive(false);
        }

        // Add UI that is the NPC character information
        if (other.tag == "NPC")
        {
            if (wantsToTalk.activeSelf)
            {
                dialogueCode.Called();
                wantsToTalk.SetActive(false);
            }
        }
    }

    // Checking if we are leaving the Baking Area, affecting UI elements OUTSIDE UI Script
    private void OnTriggerExit(Collider other)
    {
        infoText.SetActive(false);

        if (other.tag == "Baking")
        {
            inBaking = false;
            buttons.SetActive(true);
            saveButton.SetActive(true);
            quitButton.SetActive(true);
        }

        // NPC UI automatically turns off after no text left
    }

    // Accessing Inventory based off of if we are in the Baking Area or not
    public void EnableInventory()
    {
        if (inBaking)
        {
            return;
        }

        if (isInventoryActive)
        {
            Cursor.visible = false;

            // Setting Each page back to default state
            inventoryPage.SetActive(true);
            recipes.SetActive(false);
            lore.SetActive(false);
            colours.SetActive(false);
            settings.SetActive(false);

            inventory.SetActive(false);
            isInventoryActive = false;
            Time.timeScale = 1f;
        }

        else
        {
            Cursor.visible = true;
            inventory.SetActive(true);
            isInventoryActive = true;
            Time.timeScale = 0f;
        }
    }

    // Baking UI
    public void Baking()
    {
        if (!inBaking)
        {
            return;
        }

        Cursor.visible = true;

        recipes.SetActive(false);
        lore.SetActive(false);
        colours.SetActive(false);
        settings.SetActive(false);

        if (!isInventoryActive)
        {
            if (primaryDurability.enabled && secondaryDurability.enabled)
            {
                tooManyWeapons.SetActive(true);
            }
            else
            {
                bakingPage.SetActive(true);
            }
            inventory.SetActive(true);
            isInventoryActive = true;
            Time.timeScale = 0f;
        }
        else
        {
            Cursor.visible = false;
            tooManyWeapons.SetActive(false);
            inventory.SetActive(false);
            bakingPage.SetActive(false);
            isInventoryActive = false;
            Time.timeScale = 1f;
        }
    }

    #endregion

    #region Animation Scripts

    public void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    public void Walk()
    {
        anim.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
    }

    public void QuickTimeStart(string weapon)
    {
        Cursor.visible = false;
        tooManyWeapons.SetActive(false);
        inventory.SetActive(false);
        bakingPage.SetActive(false);
        isInventoryActive = false;
        Time.timeScale = 1f;
        playerController.enabled = false;
        qtUI.SetActive(true);
        qtScript.BeginBaking();
        weaponToCraft = weapon;
    }

    public void FinishQuickTime(bool passed)
    {
        qtUI.SetActive(false);
        playerController.enabled = true;
        Debug.Log(weaponToCraft);
        invScript.AddWeapons(weaponToCraft, passed);
    }

    #endregion
}
