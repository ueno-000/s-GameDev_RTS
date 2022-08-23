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
    [SerializeField] public float _attackInterval = 1;

    /// <summary>�U������</summary>
    [SerializeField] public float _attackTransitionDistance = 2;

    /// <summary>�h���</summary>
    [SerializeField] public float _defensePower = 1;

    /// <summary>�X�s�[�h</summary>
    [SerializeField] public float _speed = 1;

    /// <summary>HP</summary>
    [SerializeField] public float _healthPoint = 1;

    [SerializeField] private SliderController _sliderCon;
    
    void Start()
    {
        _sliderCon = _sliderCon.gameObject.GetComponent<SliderController>();
        _sliderCon._slider.maxValue = _healthPoint;
    }

    // Update is called once per frame
    void Update()
    {
        _sliderCon.UpdateSlider(_healthPoint);
    }

    public void HP(float hp)
    {
        _healthPoint = hp--;
    }

}
