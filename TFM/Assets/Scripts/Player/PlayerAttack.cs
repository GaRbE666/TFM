using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [HideInInspector] public int combo;

    public void StartCombo()
    {
        InputController.instance.isAttacking = false;
        if (combo < 3)
        {
            combo++;
        }
    }

    public void FinishCombo()
    {
        InputController.instance.isAttacking = false;
        combo = 0;
    }

}
