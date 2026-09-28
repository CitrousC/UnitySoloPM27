using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AdvancedEnemy : Enemy
{
     public GameObject projectile;
    void Start()
    {
      
       player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        EfirePoint = transform.GetChild(0);
       
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float targetDistance = Vector3.Distance(player.transform.position, transform.position);

        isFollowing = targetDistance <= detectionRange;

        if (isFollowing)
        {
            agent.destination = player.transform.position;
      


           /* if (!EnemyFiring)
            {
                GameObject p = Instantiate(Enemyprojectile, EfirePoint.position, EfirePoint.rotation);
                p.GetComponent<Rigidbody>().AddForce(player.transform.forward * EprojVelocity);
                Destroy(p, EprojLifespan);
                StartCoroutine("EnemyFireCooldown");

            }*/
        }
        if (Enemyhealth <= (0))
        {
            Destroy(gameObject);
        }


    }
}
