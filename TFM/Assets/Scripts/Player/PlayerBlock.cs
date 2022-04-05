using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private PlayerAnimation playerAnimation;
    [HideInInspector] public bool isBlocking;
    public float costOfStamina;

    #endregion

    #region UNITY METHODS
    void Update()
    {
        if (InputController.instance.isBlocking)
        {
            Block();
        }
        else
        {
            Unblock();
        }
    }
    #endregion

    #region CUSTOM METHODS
    private void Unblock()
    {
        isBlocking = false;
        playerAnimation.BlockAnim(false);
    }

    private void Block()
    {
        isBlocking = true;
        playerAnimation.BlockAnim(true);
    }
    #endregion
}
