using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseValueController : MonoBehaviour
{
    /// <summary>Level</summary>
    [SerializeField] protected int _level = 1;

    /// <summary>�U����</summary>
    [SerializeField] public float _attackPower = 1;

    /// <summary>�U�����x</summary>
    [SerializeField] private float _attackInterval = 1;
    public float AttackInterval
    {
        get => _attackInterval;
        set => _attackInterval = value;
    }

    /// <summary>�U������</summary>
    [SerializeField] private float _attackTransitionDistance = 2;
    public float AttackTransitionDistance
    {
        get => _attackTransitionDistance;
        set => _attackTransitionDistance = value;
    }

    /// <summary>�h���</summary>
    [SerializeField] public float _defensePower = 1;

    /// <summary>�ړ����x</summary>
    [SerializeField] private float _moveSpeed = 1;
    public float MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }

    /// <summary>HP</summary>
    [SerializeField] private float _healthPoint = 1;
    public float HealthPoint
    {
            get
            {
                return _healthPoint; 
            }
            set
            {
                _healthPoint = value; 
            } 
    }

    /// <summary>MaxHP</summary>
    [HideInInspector] public float _maxHealthPoint = 1;


    [SerializeField] private SliderController _sliderCon;
    
    void Start()
    {
        _sliderCon = _sliderCon.gameObject.GetComponent<SliderController>();
        _sliderCon._slider.maxValue = _healthPoint;
        _maxHealthPoint = _healthPoint;
    }

    // Update is called once per frame
    void Update()
    {
        _sliderCon.UpdateSlider(_healthPoint);

        if (_healthPoint <= 0)
        {
            Debug.Log(this.gameObject.name + ": Death");
            this.gameObject.SetActive(false);
        }

    }

    public void HP(float hp)
    {
        HealthPoint -= hp;
    }

    
}
