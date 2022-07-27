using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseValueController : MonoBehaviour
{
    /// <summary>Level</summary>
    [SerializeField] protected int _level = 1;

    /// <summary>UŒ‚—Í</summary>
    [SerializeField] public int _attackPower = 1;

    /// <summary>UŒ‚‘¬“x</summary>
    [SerializeField] public float _attackInterval = 1;

    /// <summary>UŒ‚‹——£</summary>
    [SerializeField] public float _attackTransitionDistance = 2;

    /// <summary>–hŒä—Í</summary>
    [SerializeField] public float _defensePower = 1;

    /// <summary>ƒXƒs[ƒh</summary>
    [SerializeField] public float _speed = 1;

    /// <summary>HP</summary>
    [SerializeField] public int _healthPoint = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
