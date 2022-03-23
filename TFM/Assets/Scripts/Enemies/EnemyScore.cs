using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScore : MonoBehaviour
{
    [SerializeField] private int score;

    public int GetScore()
    {
        return score;
    }
}
