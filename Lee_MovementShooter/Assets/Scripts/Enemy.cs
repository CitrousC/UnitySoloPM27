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
 
    public float attackDuration = 1f;
    public NavMeshAgent agent;
    public PlayerController player;
    public Transform attackhitbox;
   


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        attackhitbox = gameObject.transform.GetChild(0);
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
