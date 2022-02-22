using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private PlayerAnimation playerAnimation;

    [SerializeField] private Transform handOfArms;
    [SerializeField] private int selectedWeapon;

    private bool _canSwitch;
    [HideInInspector] public Weapon activeWeapon;
    #endregion

    #region UNITY METHODS
    private void Start()
    {
        _canSwitch = true;
        selectedWeapon = 0;
        TouringTheWeapon();
    }

    private void Update()
    {
        if (InputController.instance.isSwitchingWeapon && _canSwitch)
        {
            SwitchWeapon();
        }
    }
    #endregion

    #region CUSTOM METHODS

    private void SwitchWeapon()
    {
        _canSwitch = false;
        InputController.instance.isSwitchingWeapon = false;
        playerAnimation.SwitchWeaponAnim();
    }

    public void ChangeWeapon() //Method called by AnimatorEvent Armed-WeaponSwitch-R-Back
    {
        if (selectedWeapon >= handOfArms.childCount - 1)
        {
            selectedWeapon = 0;
        }
        else
        {
            selectedWeapon++;
        }

        TouringTheWeapon();
    }

    private void TouringTheWeapon()
    {
        int i = 0;

        foreach (Transform weapon in handOfArms)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                activeWeapon = weapon.GetComponent<Arma>().weaponScriptable;
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    public void CanSwitchWeaponAgain()
    {
        _canSwitch = true;
    }
    #endregion

}
