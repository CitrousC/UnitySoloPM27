using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public PlayerController player;

    public Image healthBar;
    public Image fireMode;
    public Image staminaBar;
    public Image sprintStatus;
    public Sprite bullet;
    public Sprite knockback;

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI clipText;

    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public bool paused = false;
    public bool gameover = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            pauseMenu = GameObject.FindGameObjectWithTag("Pause");
            pauseMenu.SetActive(paused);

            gameOverMenu = GameObject.FindGameObjectWithTag("GameOver");
            gameOverMenu.SetActive(gameover);

            staminaBar = GameObject.Find("SprintBar").GetComponent<Image>();
            healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
            fireMode = GameObject.Find("FireMode").GetComponent<Image>();
            ammoText = GameObject.Find("AmmoText").GetComponent<TextMeshProUGUI>();
            clipText = GameObject.Find("ClipText").GetComponent<TextMeshProUGUI>();

        }


    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            healthBar.fillAmount = (float)player.health / (float)player.maxHealth;
            staminaBar.fillAmount = player.stamina / player.maxStamina;
            sprintStatus.enabled = player.canSprint;
            if (player.currentWeapon)
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
       
        if (player.health <= 0)
        {
            paused = false;
            gameover = true;
            pauseMenu.SetActive(paused);
            gameOverMenu.SetActive(gameover);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        
    }

    public void Pause()
    {
        if (!gameover)
        { 
        paused = !paused;

        pauseMenu.SetActive(paused);

        Cursor.visible = paused;

        if (paused)
        {
            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;

            Time.timeScale = 1;
        }
    }
    }

    public void LoadLevel(int levelID)
    {
        if (levelID >= SceneManager.sceneCountInBuildSettings)
            Debug.Log("Level ID is too high: " + levelID);
        else
            SceneManager.LoadScene(levelID);
    }

    public void LoadNextLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainMenu()
    {
        LoadLevel(0);
    }

    public void Restart()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}