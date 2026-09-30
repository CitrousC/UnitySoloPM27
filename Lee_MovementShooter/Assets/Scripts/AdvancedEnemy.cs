using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AdvancedEnemy : Enemy
{

    public Vector3 FiringDirection; 
    AdvancedEnemy GunEnemy;
    public bool CanEnemyFire  = false;

    public float EnemyfireCooldown = 2;
    
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
        float firingRotation = 0;


        transform.GetChild(0).LookAt(player.transform);
        transform.GetChild(0).rotation.ToAngleAxis(out firingRotation, out FiringDirection);

        isFollowing = targetDistance <= detectionRange;
        if (isFollowing)
        {
           
            agent.destination = player.transform.position;


            if (!EnemyFiring)
            {
                EnemyFiring = true;
                GameObject e = Instantiate(Enemyprojectile, EfirePoint.position, EfirePoint.rotation);
                e.GetComponent<Rigidbody>().AddForce(transform.forward * EprojVelocity);
                Destroy(e, EprojLifespan);
                StartCoroutine("EnemyFireCooldown");

            }
        }
        if (Enemyhealth <= (0))
        {
            Destroy(gameObject);
        }


    }
    IEnumerator EnemyFireCooldown()
    {
        yield return new WaitForSeconds(EnemyfireCooldown);
        EnemyFiring = false;
    }    
}
