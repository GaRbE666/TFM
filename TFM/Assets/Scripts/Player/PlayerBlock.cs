using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private PlayerAnimation playerAnimation;

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
        playerAnimation.BlockAnim(false);
    }

    private void Block()
    {
        playerAnimation.BlockAnim(true);
    }
    #endregion
}
