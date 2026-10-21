using System.Collections;
using UnityEngine;

public class Summon : MonoBehaviour
{
    public GameObject summoned;
    public bool summoning = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
    // Update is called once per frame
    void Update()
    {
        if (summoning)
        {
            StartCoroutine("SummonThing");
        }
    }

    IEnumerator SummonThing()
    {
        GameObject s = Instantiate(summoned, transform.position, transform.rotation);
    }
}
