using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private Weapon weaponScriptable;
    [SerializeField] private Transform handOfArms;
    [SerializeField] private int selectedWeapon;

    private bool _canSwitch;
    #endregion

    #region UNITY METHODS
    private void Start()
    {
        _canSwitch = true;
        selectedWeapon = 0;
    }

    private void Update()
    {
        if (InputController.instance.isSwitchingWeapon && _canSwitch)
        {
            _canSwitch = false;
            playerAnimation.SwitchWeaponAnim();
        }
    }
    #endregion

    #region CUSTOM METHODS
    public void SelectedWeapon()
    {
        InputController.instance.isSwitchingWeapon = false;
        if (selectedWeapon >= handOfArms.childCount - 1)
        {
            selectedWeapon = 0;
        }
        else
        {
            selectedWeapon++;
        }

        //if (selectedWeapon == 0)
        //{
        //    selectedWeapon = 1;
        //}else if (selectedWeapon == 1)
        //{
        //    selectedWeapon = 0;
        //}


        int i = 0;

        foreach (Transform weapon in handOfArms)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
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
