using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("PlayerStats")]
    public int health = 5;
    public int maxHealth = 5;
    public float speed = 5.0f;
    public float jumpHeight = 10.0f;
    public float stamina = 100f;
    public float maxStamina = 100f;
    public float sprintCost = 25;
    public float staminaregen = 15;
    public float sprintBoost = 2.0f;

    [Header("Meta Stats")]
    public float sprintCD = 1;
    public float jumpDetectDistance = 1f;
    public float interactDistance = 5f;
    public float fusionDmgInterval = 1;
    public bool onGround = true;
    public bool sprinting = false;
    public bool canSprint = true;
    public bool regenStamina = false;
    public bool toggleSprint = true;
    public bool sprintlock = false;


    public float EnemyattackCooldown = 2;

    public bool Enemyattacking = false;
    public bool attacking = false;
    public bool fusionDmg = false;

    Ray jumpRay;
    Ray interactRay;
    RaycastHit interactHit;
    Vector2 moveInput = Vector2.zero;
    Ray firingRay;
    public Weapons currentWeapon;

    Camera playerCam;
    public Transform weaponSlot;
    PlayerInput input;
    Rigidbody rb;
    public GameObject pickupObj;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
        jumpRay = new Ray();
        playerCam = Camera.main;
        firingRay = new Ray();
        interactRay = new Ray();
        weaponSlot = playerCam.transform.GetChild(0);

        
        
    }

    private void FixedUpdate()
    {
        Quaternion playerRotation = Quaternion.identity;
        playerRotation.y = playerCam.transform.rotation.y;
        playerRotation.w = playerCam.transform.rotation.w;
        transform.rotation = playerRotation;
    }

    // Update is called once per frame
    void Update()
    {
        jumpRay.origin = transform.position;
        jumpRay.direction = -transform.up;
        onGround = Physics.Raycast(jumpRay, jumpDetectDistance);

        interactRay.origin = playerCam.transform.position;
        interactRay.direction = playerCam.transform.forward;

        if (Physics.Raycast(interactRay, out interactHit, interactDistance))
        {
            if (interactHit.collider.tag == "Weapon" || interactHit.collider.tag == "Ammo")
            {
                pickupObj = interactHit.collider.gameObject;
            }
            else pickupObj = null;
        }
        else pickupObj = null;

        if (currentWeapon)
            if (currentWeapon.holdToAttack && attacking)
                currentWeapon.fire();
        if (onGround)
        {
            Vector3 tempMove = rb.linearVelocity;

            tempMove.x = moveInput.x * speed;
            tempMove.z = moveInput.y * speed;

            rb.linearVelocity = (tempMove.x * transform.right) +
                        (tempMove.y * transform.up) +
                        (tempMove.z * transform.forward);
           
            if (sprinting)
            {
                if (stamina > 0)
                {
                    tempMove.z *= sprintBoost;

                    stamina -= sprintCost * Time.deltaTime;

                    StopCoroutine("sprintReset");
                    regenStamina = false;
                    if (stamina <= 0)
                    {
                        canSprint = false;
                        sprinting = false;
                        stamina = 0;
                    }
                }
                if (moveInput.y < .75f)
                {
                    canSprint = false;
                    sprinting = false;
                }
            }

            if (!sprinting)
            {
                if (!canSprint && !sprintlock)
                    StartCoroutine("sprintReset");
                if (canSprint && !regenStamina)
                    regenStamina = true;
                if (regenStamina)
                {
                    stamina += staminaregen * Time.deltaTime;
                    if (stamina >= maxStamina)
                    {
                        stamina = maxStamina;
                        regenStamina = false;
                    }
                }
            }
        }
    }

    public void sprint(InputAction.CallbackContext context)
    {
        if(canSprint && (moveInput.y >= .75f) && onGround)
        {
            if (!toggleSprint)
            {
                if (context.ReadValueAsButton())
                    sprinting = true;
                else
                {
                    sprinting = false;
                    canSprint = false;

                }
            }
            else
                if (context.performed)
                    sprinting = !sprinting;
        }

    }
    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Jump()
    {
        if (onGround)
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
    }
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
        {
            if (pickupObj)
            {
                if (pickupObj.tag == "Weapon")
                {
                    pickupObj.GetComponent<Weapons>().equip(this);
                }

                // Interact to pickup ammo

                if (pickupObj.tag == "Ammo" && currentWeapon && currentWeapon.ammo < currentWeapon.maxAmmo)
                {
                    int refillAmt = currentWeapon.ammo + currentWeapon.ammoRefill;

                    if (refillAmt >= currentWeapon.maxAmmo)
                    {
                        currentWeapon.ammo = currentWeapon.maxAmmo;
                    }
                    else
                        currentWeapon.ammo += currentWeapon.ammoRefill;

                    Destroy(pickupObj);
                }


                pickupObj = null;
            }
            else if (currentWeapon)
                Reload();
        }
    }

    public void SwitchFireMode()
    {
        if(currentWeapon)
            currentWeapon.SwitchFireMode();
    }

    public void Reload()
    {
        if (currentWeapon)
            if (!currentWeapon.reloading)
                currentWeapon.reload();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (currentWeapon)
        {
            if (currentWeapon.holdToAttack)
            {
                if (context.ReadValueAsButton())
                    attacking = true;
                else
                    attacking = false;
            }
            else if (context.ReadValueAsButton())
                currentWeapon.fire();
        }
    }
    public void DropWeapon()
    {
        if (currentWeapon)
        {
            currentWeapon.GetComponent<Weapons>().unequip();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ammo" && currentWeapon && currentWeapon.ammo < currentWeapon.maxAmmo)
        {
            int refillAmt = currentWeapon.maxAmmo - currentWeapon.ammo;
            if (refillAmt >= currentWeapon.maxAmmo)
            {
                currentWeapon.ammo = currentWeapon.maxAmmo;

            }
            Destroy(pickupObj);
        }
        if (collision.gameObject.tag == "Hazard")
        {
            health--;
        }

    

        if (collision.gameObject.tag == "Health" && health < maxHealth)
        {
            health++;

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "LAva")
        {
            health=0;
        }
            
     
       }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LevelEnd")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().LoadNextLevel();
        }
        if (other.gameObject.tag == "EnemyProjectile")
        {
            Destroy(other.gameObject);
            health--;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "AttackHitbox")
        {
            if (!Enemyattacking)
            {
                StartCoroutine("AttackCooldown");
            }
        }
    }


    private void  OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "AttackHitbox")
        {
            Enemyattacking = false;
            StopCoroutine("AttackCooldown");

        }
    }


    IEnumerator AttackCooldown()
    {
        Enemyattacking = true;
        yield return new WaitForSeconds(EnemyattackCooldown);
        health--;
        Enemyattacking = false;

    }

    IEnumerator sprintReset()
    {
        sprintlock = true;
        regenStamina = false;

        yield return new WaitForSeconds(sprintCD);

        canSprint = true;
        regenStamina = true;
        sprintlock = false;

           
    }

}

/*
private void OnCollisionStay(Collision collision)

For if you need A Fusion HAzard
{
if (collision.gameobject.tag == "FusionHazard")
{
if(!fusionDmg)
{
    StartCoroutine("fusionDmgCooldown");
}
}

}


private void OnCollisionExit(Collision collision)
{
if (collision.gameObject.tag == "FusionHazard")
{  if(fusionDmg)
{
    StopCoroutine("fusionDmgCooldown");
    fusionDmg = false;

}
}
IEnumerator fusionDmgCooldown()
{
fusionDmg = true;
yield return new WaitForSeconds(fusionDmgInterval);
health--;
fusionDmg = false;
}
*/


//For whatever reason the game changes and you can drop weapons

 
