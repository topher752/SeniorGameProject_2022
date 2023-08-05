using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint; 
    public float attackRange = 1f;
    public LayerMask enemyLayer;
    public GameObject[] weapons = new GameObject[3];
    public Image primaryWeapon;
    private Animator anim;
    public bool isAttacking = false;
    public PlayerInput playerInput;
    public bool canUseMouseButton = true;

    // WeaponClass weapons for accessing damage and durability
    public WeaponClass sword;
    public WeaponClass axe;
    public WeaponClass macron;

    public PlayerProjectile pjscript;

    //SFX Audio
    public AudioSource sFX;
    public AudioClip swingSFX;
    private Collider[] hitEnemies;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        pjscript = gameObject.GetComponent<PlayerProjectile>();
    }

    void Update()
    {
        // Debug.Log(wCrafting.primaryImage.sprite.name);
        // CheckWeapon();

        if (Input.GetMouseButtonDown(0) && canUseMouseButton)
        {
            Debug.Log(primaryWeapon.sprite.name);
            if (primaryWeapon.sprite.name != "WeaponHolder")
            {
                if (primaryWeapon.sprite.name == "CakeSword" || primaryWeapon.sprite.name == "CakeSword_Burnt")
                {
                    anim.SetBool("SwordAttacking", true);
                    sFX.PlayOneShot(swingSFX);
                }
                else if (primaryWeapon.sprite.name == "Axe" || primaryWeapon.sprite.name == "Axe_Burnt")
                {
                    anim.SetBool("AxeAttacking", true);
                    sFX.PlayOneShot(swingSFX);
                }
                else if (primaryWeapon.sprite.name == "StrawMac" || primaryWeapon.sprite.name == "StrawMac_Burnt")
                {
                    anim.SetBool("StarAttacking", true);
                    pjscript.Throw();
                    macron.durability -= 1;
                    sFX.PlayOneShot(swingSFX);
                }

                Debug.Log("Attack");
                // Detect Enemies in attack
                hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

                //Damanging them
                StartCoroutine(DelayDamage());
                /*
                foreach (Collider enemy in hitEnemies)
                {
                    if (primaryWeapon.sprite.name == "CakeSword")
                    {
                        if(enemy.gameObject.tag == "GummyBear")
                        {
                            enemy.GetComponent<GummyBearEnemy>().TakeDamage((int)sword.damange);
                        }
                        else 
                        {
                            enemy.GetComponent<Enemy>().TakeDamage((int)sword.damange);
                        }

                        sword.durability -= 1;
                    }
                    else if (primaryWeapon.sprite.name == "Axe")
                    {
                        if(enemy.gameObject.tag == "GummyBear")
                        {
                            enemy.GetComponent<GummyBearEnemy>().TakeDamage((int)axe.damange);
                        }
                        else
                        {
                            enemy.GetComponent<Enemy>().TakeDamage((int)axe.damange);
                        }
                        axe.durability -= 1;
                    }
                }
                */

                StartCoroutine(StopAttackAnimation());
            }
        }
        else
        {
            anim.SetBool("SwordAttacking", false);
            anim.SetBool("AxeAttacking", false);
            anim.SetBool("StarAttacking", false);
        }
    }

    private IEnumerator StopAttackAnimation()
    {
        canUseMouseButton = false;
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        anim.SetBool("SwordAttacking", false);
        anim.SetBool("AxeAttacking", false);
        anim.SetBool("StarAttacking", false);
        canUseMouseButton = true;
    }

    private IEnumerator DelayDamage()
    {
        yield return new WaitForSeconds(0.4f);

        foreach (Collider enemy in hitEnemies)
        {
            if (primaryWeapon.sprite.name == "CakeSword" || primaryWeapon.sprite.name == "CakeSword_Burnt")
            {
                if(enemy.gameObject.tag == "GummyBear")
                {
                    enemy.GetComponent<GummyBearEnemy>().TakeDamage((int)sword.damange);
                }
                else 
                {
                    enemy.GetComponent<Enemy>().TakeDamage((int)sword.damange);
                }

                sword.durability -= 1;
            }
            else if (primaryWeapon.sprite.name == "Axe" || primaryWeapon.sprite.name == "Axe_Burnt")
            {
                if(enemy.gameObject.tag == "GummyBear")
                {
                    enemy.GetComponent<GummyBearEnemy>().TakeDamage((int)axe.damange);
                }
                else
                {
                    enemy.GetComponent<Enemy>().TakeDamage((int)axe.damange);
                }
                axe.durability -= 1;
            }
        }
    }
    
    void OnDrawGizmosSelected()
    {
        // Debugging mostly, making sure the attack range is perfected
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}