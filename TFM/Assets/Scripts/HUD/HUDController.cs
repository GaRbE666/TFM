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
}
