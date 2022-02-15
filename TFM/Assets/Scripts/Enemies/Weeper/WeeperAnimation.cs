using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeeperAnimation : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private Animator animator;

    private int walk = Animator.StringToHash("walk");
    #endregion 

    public void WalkAnim()
    {
        animator.SetBool(walk, true);
    }

    public void StopWalkAnim()
    {
        animator.SetBool(walk, false);
    }
}
