using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerAnimation playerAnimation;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        if (InputController.instance.isAttacking)
        {
            Attack();
        }

        if (InputController.instance.isBlocking)
        {
            Block();
        }
        else
        {
            Unblock();
        }

    }

    private void Attack()
    {
        playerAnimation.AttackAnim();
        InputController.instance.isAttacking = false;
    }

    private void Unblock()
    {
        playerAnimation.BlockAnim(false);
    }

    private void Block()
    {
        playerAnimation.BlockAnim(true);
    }
}
