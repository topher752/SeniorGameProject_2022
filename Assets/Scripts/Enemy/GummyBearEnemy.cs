using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GummyBearEnemy : MonoBehaviour
{
    [SerializeField] private float damage;
    private float lastAttackTime = 0;
    private float attackCoolDown = 1.3f;

    private GameObject target;

    [SerializeField] private float seeingDistance;

    // Enemy Health
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject userInterface;
    public Slider slider;

    //Enemy Drops
    public GameObject[] itemDrop;
    public GameObject healthDrop;
    public WeaponClass throwingStar;
    [Tooltip("Number entered here has a 1 in X chance. (Ex. if 4 is input there is a 1 in 4 chance to recieve this drop)")]
    public int dropChance = 4;

    private bool canSeePlayer = false;

    //Audio
    private GameObject sFXGO;
    public AudioClip playerTakeDamage;

    //Enemy Projectile
    public EnemyProjectile enemyProjectile;

    //Animator
    private Animator animator;

    private void Awake()
    {
        sFXGO = GameObject.FindGameObjectWithTag("SFX");
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
    }

    void Update()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);

        //Player is within the enemies range to see
        if (dist <= seeingDistance)
        {
            canSeePlayer = true;
            if (canSeePlayer)
            {
                animator.SetBool("isIdle", false);
                var lookPosition = target.transform.position - transform.position;
                lookPosition.y = 0;
                var rotation = Quaternion.LookRotation(lookPosition);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1.0f);
                Attack();
            }
        }

        if (!canSeePlayer)
        {
            animator.SetBool("isIdle", true);
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "ThrowingStar")
        {
            TakeDamage((int)throwingStar.damange);
        }
    }

    public void Attack()
    {
        if (Time.time - lastAttackTime >= attackCoolDown)
        {
            lastAttackTime = Time.time;
            animator.SetBool("isAttacking", true);
            enemyProjectile.Throw();
            //PLAYER DAMAGE ANIMATION HERE
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        slider.value = currentHealth;

        // hurt animation
        // hurt sound

        if (currentHealth <= 0)
        {
            Die();
            Spawner.enemiesKilledCurrently += 1;
            Spawner.enemiesLeftCurrently -= 1;
        }
    }

    private void Die()
    {
        // Lines 100 - 101 is for dropping all weapons
        int randomItem = Random.Range(0, 3);
        Instantiate(itemDrop[randomItem], gameObject.transform.position, gameObject.transform.rotation);

        /* This is just for one item
        Instantiate(itemDrop, gameObject.transform.position, gameObject.transform.rotation);
        */

        int random = Random.Range(0, dropChance); // 0 - 4 means a 1 in 4 chance because it will look for 0, 1, 2, 3 and only 1 will return a drop
        Debug.Log(random.ToString());
        if (random == 1)
        {
            Vector3 offset = new Vector3(1.0f, 1.0f, 1.0f);
            Instantiate(healthDrop, gameObject.transform.position + offset, gameObject.transform.rotation);
        }
        // Die animation
        Destroy(gameObject);
    }
}
