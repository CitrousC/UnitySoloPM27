using System.Collections;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public bool Enemyattacking = false;
    public PlayerController player;
    public float attackCooldown = 1.5f;
    // Update is called once per frame
    void Update()
    {
    if (Enemyattacking == true)
        {
            player.health--;
        }
    }
    private void OnInstanceStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Enemyattacking = true;
            StartCoroutine("AttackCooldown");
        }
    }
    private void OnInstanceExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Enemyattacking = false;
        }
    }


    IEnumerator AttackCooldown()
    {
        Enemyattacking = false;
        yield return new WaitForSeconds(attackCooldown);

    }

}


