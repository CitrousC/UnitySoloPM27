using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AdvancedEnemy : Enemy
{
     public GameObject projectile;
    public Vector3 FiringDirection; 
    void Start()
    {
      
      player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
      EfirePoint = transform.GetChild(0);
//target -  enemy position (Y First)
      agent = GetComponent<NavMeshAgent>();
       
    }

    // Update is called once per frame
    void Update()
    {
        float targetDistance = Vector3.Distance(player.transform.position, transform.position);
        float firingRotation = 0;

        isFollowing = targetDistance <= detectionRange;
       
        if (isFollowing)
        {
            transform.GetChild(0).LookAt(player.transform);
            transform.GetChild(0).rotation.ToAngleAxis(out firingRotation, out FiringDirection);

            agent.destination = player.transform.position;
           // Mathf.Atan2(player.transform.position.z - transform.position.z, player.transform.position.x - transform.position.x * Mathf.Rad2Deg);

            if (!EnemyFiring)
            {
                
                GameObject = Instantiate(Enemyprojectile, EfirePoint.position, EfirePoint.rotation);
                GetComponent<Rigidbody>().AddForce(FiringDirection.transform.forward * EprojVelocity);
                Destroy(p, EprojLifespan);
                StartCoroutine("EnemyFireCooldown");

            }
        }
        if (Enemyhealth <= (0))
        {
            Destroy(gameObject);
        }


    }
}
