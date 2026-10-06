using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public bool activated = false;
    public GameObject door;
    public Transform button;
    public int doorID = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()

    {
        button = transform;
        door = gameObject;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            Destroy(door);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy (collision.gameObject);
            activated = true;

        }
    }
}
