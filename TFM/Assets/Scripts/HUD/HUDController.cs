using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    private static HUDController _instance;
    public static HUDController instance { get { return _instance; } }

    [SerializeField] private Text soulsCounterText;
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private Slider playerStaminaBar;
    [SerializeField] private float timeToCanRecoveryStamina;
    [SerializeField] private float staminaPerSecond;
    private float currentStamina;
    private bool canRestoreStamina;
    private int totalSouls;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        totalSouls = 0;
        soulsCounterText.text = totalSouls.ToString();
    }

    private void Update()
    {
        if (currentStamina >= playerStaminaBar.maxValue)
        {
            return;
        }
        if (canRestoreStamina)
        {
            currentStamina += Time.deltaTime * staminaPerSecond;
            playerStaminaBar.value = currentStamina;
        }
    }

    public void SetSoulsInCounter(int numSouls)
    {
        totalSouls += numSouls;
        soulsCounterText.text = totalSouls.ToString();
    }

    public void SetPlayerHealthValue(float health)
    {
        playerHealthBar.value = health;
    }

    public void SetPlayerHealthMaxValueSlider(float maxHealth)
    {
        playerHealthBar.maxValue = maxHealth;
        playerHealthBar.value = maxHealth;
    }

    public void SetMaxStamina(float maxStamina)
    {
        playerStaminaBar.maxValue = maxStamina;
        playerStaminaBar.value = maxStamina;
        currentStamina = maxStamina;
    }
    public void ConsumeStamina(float stamina)
    {
        currentStamina -= stamina;
        if (currentStamina < 0)
        {
            currentStamina = 0;
        }
        playerStaminaBar.value = currentStamina;
        StopAllCoroutines();
        StartCoroutine(WaitToStartRestoreStamina());
    }

    private IEnumerator WaitToStartRestoreStamina()
    {
        canRestoreStamina = false;
        yield return new WaitForSeconds(timeToCanRecoveryStamina);
        canRestoreStamina = true;
    }

    public float GetCurrentValueOfHealthBar()
    {
        return playerHealthBar.value;
    }

    public float GetCurrentValueOfStaminaBar()
    {
        return playerStaminaBar.value;
    }
}
