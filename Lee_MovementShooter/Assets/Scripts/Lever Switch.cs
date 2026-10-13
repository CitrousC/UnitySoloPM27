using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public bool activated = false;
    public bool pressurePlate = false;
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
            if (pressurePlate)
            {
                Destroy(transform);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile" && !pressurePlate)
        {
            Destroy (collision.gameObject);
            activated = true;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            if (pressurePlate)
            {
                Destroy(door);
                Destroy(transform);
            }
        }
    }
}
