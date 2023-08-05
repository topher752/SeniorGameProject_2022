using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float damage;
    private float lastAttackTime = 0;
    private float attackCoolDown = 1.5f;

    //Booleans
    private bool isAttacking = false;
    private bool isWalking = false;
    private bool isIdle = false;

    //Navmesh
    private NavMeshAgent agent;

    //Target Object
    private GameObject target;

    //Distance Variables
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float seeingDistance;
    [SerializeField] private float stopAttackingDistance;

    //Player Health
    private PlayerHealth pHealth;

    //Enemy Health
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject userInterface;
    public Slider slider;

    //Enemy Drops
    public GameObject[] itemDrop;
    public GameObject healthDrop;
    public GameObject loreDrop;
    public WeaponClass throwingStar;
    [Tooltip("Number entered here has a 1 in X chance. (Ex. if 4 is input there is a 1 in 4 chance to recieve this drop)")]
    public int dropChance = 4;

    //Lore Spawners
    private GameObject kingdom1LoreSpawner;
    private GameObject kingdom2LoreSpawner;

    //Patrolling Stuff
    public GameObject[] patrollingPoints;
    private bool canSeePlayer = false;
    private bool walkingToWayPoint = false;

    //Audio
    private GameObject sFXGO;
    private AudioSource audioSource;
    public AudioClip playerTakeDamage;

    //Animator
    private Animator animator;

    private void Awake()
    {
        if (gameObject.tag == "FruitEnemy" || gameObject.tag == "FruitBoss")
        {
            patrollingPoints = GameObject.FindGameObjectsWithTag("LKWaypoints");
        }

        if (gameObject.tag == "DairyEnemy" || gameObject.tag == "DairyBoss")
        {
            patrollingPoints = GameObject.FindGameObjectsWithTag("RKWaypoints");
        }

        sFXGO = GameObject.FindGameObjectWithTag("SFX");
        audioSource = sFXGO.GetComponent<AudioSource>();

        animator = gameObject.GetComponentInChildren<Animator>();

        kingdom1LoreSpawner = GameObject.FindGameObjectWithTag("Kingdom1LoreSpawner");
        kingdom2LoreSpawner = GameObject.FindGameObjectWithTag("Kingdom2LoreSpawner");
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        pHealth = target.GetComponent<PlayerHealth>();
        currentHealth = maxHealth;
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "ThrowingStar")
        {
            TakeDamage((int)throwingStar.damange);
        }
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);

        //Player is within the enemies range to see
        if (dist <= seeingDistance)
        {
            canSeePlayer = true;
            if (canSeePlayer)
            {
                GoToTarget();
            }
        }

        if(dist > seeingDistance)
        {
            canSeePlayer = false;
            if (!canSeePlayer && !walkingToWayPoint)
            {
                StartCoroutine(Patrol());
            }
        }

        //Enemy is within attacking distance
        if (dist < stoppingDistance)
        {
            StopEnemy();
            Attack();
        }
        else
        {
            isWalking = true;
            isIdle = false;
        }

        //Animations

        if (isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if (isAttacking)
        {
            if(dist < stoppingDistance)
            {
                animator.SetBool("isAttacking", true);
                isWalking = false;
                isIdle = false;
            }

            if (dist > stoppingDistance)
            {
                animator.SetBool("isAttacking", false);
                isWalking = true;
                isIdle = false;
            }
        }

        if (isIdle)
        {
            animator.SetBool("isIdle", true);
        }
        else
        {
            animator.SetBool("isIdle", false);
        }
    }

    private void LateUpdate()
    {
        userInterface.transform.rotation = Camera.main.transform.rotation;
    }

    private void GoToTarget()
    {
        agent.isStopped = false;
        agent.SetDestination(target.transform.position);

        if(agent.isStopped == false && !isAttacking)
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
    }

    private void StopEnemy()
    {
        agent.isStopped = true;

        if(agent.isStopped == true && !isAttacking)
        {
            isIdle = true;
        }
        else
        {
            isIdle = false;
        }
    }

    private void Attack()
    {
        if (Time.time - lastAttackTime >= attackCoolDown)
        {
            isAttacking = true;
            lastAttackTime = Time.time;
            pHealth.playerHealth -= damage;
            audioSource.PlayOneShot(playerTakeDamage);
        }
    }

    IEnumerator Patrol()
    {
        walkingToWayPoint = true;
        agent.isStopped = false;
        isWalking = true;
        isAttacking = false;

        int targetNum;
        targetNum = Random.Range(0, patrollingPoints.Length);
        agent.SetDestination(patrollingPoints[targetNum].transform.position);

        yield return new WaitForSeconds(7.0f);

        walkingToWayPoint = false;
        agent.isStopped = true;
        isWalking = false;
        isAttacking = false;
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
            Vector3 offset = new Vector3(1.0f, 0.0f, 1.0f);
            Instantiate(healthDrop, gameObject.transform.position + offset, gameObject.transform.rotation);
        }
        // Die animation
        Destroy(gameObject);

        if (gameObject.tag == "FruitBoss")
        {
            Vector3 offset = new Vector3(2.0f, 0.0f, 2.0f);
            Instantiate(loreDrop, kingdom2LoreSpawner.transform.position, kingdom2LoreSpawner.transform.rotation);
        }

        if (gameObject.tag == "DairyBoss")
        {
            Vector3 offset = new Vector3(2.0f, 0.0f, 2.0f);
            Instantiate(loreDrop, kingdom1LoreSpawner.transform.position, kingdom1LoreSpawner.transform.rotation);
        }

        Destroy(gameObject);
    }
}
