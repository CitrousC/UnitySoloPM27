using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    public bool isFollowing = false;

    public int Enemyhealth = 3;
    public int EnemymaxHealth = 3;

    public float detectionRange = 5;
    public float attackCooldown = 1.5f;
    public float attackDuration = 1f;
    public NavMeshAgent agent;
    public PlayerController player;
    public Transform attackhitbox;
    public bool Enemyattacking = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        attackhitbox = GameObject.FindGameObjectWithTag("AttackHitbox").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float targetDistance = Vector3.Distance(player.transform.position, transform.position);

        isFollowing = targetDistance <= detectionRange;

        if (isFollowing)
        {
            agent.destination = player.transform.position;
        }

        if (Enemyhealth <= (0))
        {
            Destroy(gameObject);
        }

        if (Enemyattacking == true && attackhitbox.transform)
            {
            player.health--;
        }
    }
 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.tag =="AttackHitbox")
        {
            isFollowing = false;
            StartCoroutine("AttackCooldown");
            isFollowing = true; 
           
            // Run a coroutine for a cooldown so the enemy doesn't keep trying to attack the player every instance
            // Move if they have to get back to player

            // Set attacking boolean to true
            // In update, make enemy attack and do damage to player while attacking
        }
        if (collision.gameObject.tag == "Projectile")
        {
            Enemyhealth--;
            Destroy(collision.gameObject);
        }
       
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Enemyattacking = false;
        }
    }

    IEnumerator AttackCooldown()
    {
     yield return new WaitForSeconds(attackCooldown);
        Enemyattacking = true;
        yield return new WaitForSeconds(attackDuration);
        Enemyattacking = false;
    } 
}
