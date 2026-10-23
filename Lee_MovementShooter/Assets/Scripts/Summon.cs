using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Summon : MonoBehaviour
{
    public GameObject summoned;
    public bool summoning = false;
       
    public bool destroyable = false;
    public float summonCDtime = 3f;
    public int summonerhealth = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame

   
    private void Update()
    {
      if (summonerhealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            if (!summoning)
            {
                summoning = true;
                StartCoroutine("SummonThing");
            }
        }
    }
  
    public void OnTriggerEnter (Collider collision)
    {
        if (collision.gameObject.tag == "Projectile" && destroyable)
        {
            Destroy(collision.gameObject);
            summonerhealth--;
        }
    }
    IEnumerator SummonThing()
    {

        GameObject s = Instantiate(summoned, transform.position, transform.rotation);
        s = summoned;
        yield return new WaitForSeconds(summonCDtime);
        summoning = false;
    }
}
