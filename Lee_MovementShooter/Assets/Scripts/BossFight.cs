using System.Collections;
using UnityEngine;

public class BossFight : MonoBehaviour
    
{
    [Header("Meta Stats")]
    public GameObject spawnedObject;
    public GameObject physicalAttack;

    [Header("Basic Stats")]
    public int attack = 0;
    public int maxattack = 2;
    public float attackCD = 5f;
    public float summonCD = 2f;
    public bool attacking = false;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    private void Update()
    {
        
        if (attacking)
        {
            StartCoroutine("BossAttacks");
            if (attack == 1) //Summon Attack.
            {
                StartCoroutine("SummonAttack");
     
            }
        }
    }
    
    IEnumerator BossAttacks()
    {
        attacking = false;
       attack = Random.Range(1, maxattack);
        yield return new WaitForSeconds(attackCD);
        attacking = true;
    }
    IEnumerator SummonAttack()
    {
        GameObject s = Instantiate(spawnedObject, transform.GetChild(Random.Range(0, 1)).position, transform.GetChild(Random.Range(0, 1)).rotation);
        s = spawnedObject;
        yield return new WaitForSeconds(summonCD);

    }
}
