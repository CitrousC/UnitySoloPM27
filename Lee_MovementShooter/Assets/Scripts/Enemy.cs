using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    AudioSource speaker;

    [Header("Basic Stats")]
    public bool isFollowing = false;
    public int Enemyhealth = 3;
    public int EnemymaxHealth = 3;
    public float detectionRange = 5;
    public NavMeshAgent agent;
    public PlayerController player;

    [Header("Melee")]
    public float attackDuration = 1f;
    public Transform attackhitbox;
   

    [Header("Ranged")]
    public bool EnemyFiring = false;
    public Transform EfirePoint;
    public Transform EfiringDirection;
    public float EprojLifespan;
    public float EprojVelocity;
    public GameObject Enemyprojectile;
    public Transform EnemyGun;
    public Transform GunPoint;

    [Header("Boss")]
    public bool Boss = false;
    public bool Summoning = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        speaker = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Boss)
        {
            float targetDistance = Vector3.Distance(player.transform.position, transform.position);

            isFollowing = targetDistance <= detectionRange;

            if (isFollowing)
            {
                agent.destination = player.transform.position;
            }


        }
        if (Enemyhealth <= (0))
        {
            speaker.Play();
            Destroy(gameObject);
       
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Enemyhealth--;
            Destroy(collision.gameObject);
        }
    }

}
