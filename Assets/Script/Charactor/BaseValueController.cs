using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseValueController : MonoBehaviour
{
    /// <summary>Level</summary>
    [SerializeField] protected int _level = 1;

    /// <summary>çUåÇóÕ</summary>
    [SerializeField] public float _attackPower = 1;

    /// <summary>çUåÇë¨ìx</summary>
    [SerializeField] public float _attackInterval = 1;

    /// <summary>çUåÇãóó£</summary>
    [SerializeField] public float _attackTransitionDistance = 2;

    /// <summary>ñhå‰óÕ</summary>
    [SerializeField] public float _defensePower = 1;

    /// <summary>ÉXÉsÅ[Éh</summary>
    [SerializeField] public float _speed = 1;

    /// <summary>HP</summary>
    [SerializeField] public float _healthPoint = 1;

    /// <summary>HP</summary>
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
        _healthPoint -= hp;
    }

    
}
