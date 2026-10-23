using System.Collections;
using UnityEngine;

public class BossFight : MonoBehaviour

{
    [Header("Meta Stats")]
    public GameObject spawnedObject;
    public GameObject physicalAttack;
    public bool bossfightStart = false;

    [Header("Basic Stats")]
    public int bossHP = 100;
    public int bossMaxHP = 100;
    public int attack = 0;
    public int maxattack = 2;
    public float attackCD = 5f;
    public float summonCD = 2f;
    public bool attacking = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    private void Update()
    {

        if (!attacking && bossfightStart)
        {
            StartCoroutine("BossAttacks");
            if (attack == 1) //Summon Attack.
            {
                StartCoroutine("SummonAttack");

            }
        }
        if (bossHP <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            bossHP--;
            Destroy(collision.gameObject);

        }

        
    }
    IEnumerator BossAttacks()
    {
        attacking = true;
        attack = Random.Range(1, maxattack+1);
        yield return new WaitForSeconds(attackCD);
        attacking = false;
    }
    IEnumerator SummonAttack()
    {
        Vector3 SummonPos = transform.GetChild(Random.Range(0, 2)).position;
        GameObject s = Instantiate(spawnedObject, SummonPos, transform.rotation);
        s = spawnedObject;
        yield return new WaitForSeconds(summonCD);

    }
}
