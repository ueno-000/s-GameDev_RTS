using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseValueController : MonoBehaviour/*,IDamage*/
{
    /// <summary>Level</summary>
    [SerializeField] protected int _level = 1;

    /// <summary>UŒ‚—Í</summary>
    [SerializeField] public float _attackPower = 1;

    /// <summary>UŒ‚‘¬“x</summary>
    [SerializeField] private float _attackInterval = 1;
    public float AttackInterval
    {
        get => _attackInterval;
        set => _attackInterval = value;
    }

    /// <summary>UŒ‚‹——£</summary>
    [SerializeField] private float _attackTransitionDistance = 2;
    public float AttackTransitionDistance
    {
        get => _attackTransitionDistance;
        set => _attackTransitionDistance = value;
    }

    /// <summary>–hŒä—Í</summary>
    [SerializeField] public float _defensePower = 1;

    /// <summary>ˆÚ“®‘¬“x</summary>
    [SerializeField] private float _moveSpeed = 1;
    public float MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }

    /// <summary>HP</summary>
    [SerializeField] private float _hitPoint = 1;
    public float HealthPoint
    {
        get =>_hitPoint;
        set => _hitPoint = value;
    }

    /// <summary>MaxHP</summary>
    [HideInInspector] public float _maxHealthPoint = 1;


    [SerializeField] private SliderController _sliderCon;
    
    void Start()
    {
        _sliderCon = _sliderCon.gameObject.GetComponent<SliderController>();
        _sliderCon.HPSlider.maxValue = _hitPoint;
        _maxHealthPoint = _hitPoint;
    }

    // Update is called once per frame
    void Update()
    {
        _sliderCon.UpdateSlider(_hitPoint);

        if (_hitPoint <= 0)
        {
            Debug.Log(this.gameObject.name + ": Death");
            this.gameObject.SetActive(false);
        }

    }

    public void HP(float hp)
    {
        HealthPoint -= hp;
    }

    //public void ReceiveDamage(float value)
    //{ 
    //    HP(value);
    //}


}
