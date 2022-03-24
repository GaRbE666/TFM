using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Weapon scriptableWeapon;
    [SerializeField] private GameObject sprite;

    private void OnEnable()
    {
        sprite.SetActive(true);
    }

    private void OnDisable()
    {
        sprite.SetActive(false);
    }
}
