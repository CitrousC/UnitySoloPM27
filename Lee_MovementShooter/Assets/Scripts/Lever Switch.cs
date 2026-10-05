using UnityEngine;

public class Target : MonoBehaviour
{
    public bool activated = false;

    public int doorID = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
