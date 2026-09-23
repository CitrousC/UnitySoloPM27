using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{

    public PlayerController player;

    public Image healthBar;
    public Image fireMode;

    public Sprite bullet;
    public Sprite knockback;

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI clipText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();


        healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
        fireMode = GameObject.Find("FireMode").GetComponent<Image>();

        ammoText = GameObject.Find("AmmoText").GetComponent<TextMeshProUGUI>();
        clipText = GameObject.Find("ClipText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = (float)player.health / (float)player.maxHealth;
        if(player.currentWeapon)
        {
            ammoText.text = "Ammo: " + player.currentWeapon.ammo + "/" + player.currentWeapon.maxAmmo;
            clipText.text = "Clip: " + player.currentWeapon.clip + "/" + player.currentWeapon.clipSize;

            fireMode.enabled = true;

            if (player.currentWeapon.currentFireMode == 0)
            {
                fireMode.sprite = bullet;
            }
            else
            {
                fireMode.sprite = knockback;
            }
        }
        else
        {
            ammoText.text = "";
            clipText.text = "";
            fireMode.enabled = false;
        }

       
        
    }
}
