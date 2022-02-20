using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Player/Weapons")]
public class Weapon : ScriptableObject
{
    [Header("References")]
    public GameObject prefab;
    public GameObject fireEffect;
    public GameObject iceEffect;
    public GameObject electricEffect;
    [Header("Data")]
    public string weaponName;
    public string weaponDescription;
    public float damage;
    public float extraPercentageByFlame;
    public float extraPercentageByIce;
    public float extraPercentageByElectric;
}
