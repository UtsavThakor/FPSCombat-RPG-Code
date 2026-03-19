using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider HealthBar;
    public PlayerStats playerStats;
    public Image damageFlash;
    public Image healthBarFill;
    public TMP_Text ammoText;
    public PlayerShoot playerShoot;
    public Slider staminaBar;

    private float flashTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        HealthBar.value = playerStats.currentHealth;
        staminaBar.value = playerStats.currentStamina;

        float healthPercent = playerStats.currentHealth / playerStats.maxHealth;
        healthBarFill.color = Color.Lerp(Color.red, Color.green, healthPercent);
        ammoText.text = playerShoot.currentAmmo + " / " + playerShoot.maxAmmo;

        if (flashTimer > 0f)
        {
            flashTimer -= Time.deltaTime;
            float alpha = flashTimer / 0.3f;
            damageFlash.color = new Color(1f, 0f, 0f, alpha);
        }
    }

    public void ShowDamageFlash()
    {
        flashTimer = 0.3f;
    }
}
