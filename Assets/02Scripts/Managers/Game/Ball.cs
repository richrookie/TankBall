using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private int _number = 1;
    public int Number
    {
        get => _number;
        private set { _number = value; }
    }


    public void Init()
    {
        _number = 1;
    }

    public void IncreaseNumber(int value)
    {
        Number = value;
    }
}
