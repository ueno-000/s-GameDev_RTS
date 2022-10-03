using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    SubCore,
    MainCore
}

public class CoreBaseScript : MonoBehaviour
{
    private float _healthPoint = 5;
    private float _attackSpeed = 5;

    [SerializeField] public Type CoreType = Type.SubCore;

    [SerializeField] private CoreValueController _coreValueController;
    private void Start()
    {
        _coreValueController = _coreValueController.gameObject.GetComponent<CoreValueController>();

        Change();
    }

    private void Change()
    {
        switch (CoreType)
        {
            case Type.SubCore:
                _healthPoint = _coreValueController.SubCoreHitPoint;
                _attackSpeed = _coreValueController.SubCoreAttackSpeed;
                break;
            case Type.MainCore:
                _healthPoint = _coreValueController.MainCoreHitPoint;
                _attackSpeed = _coreValueController.MainCoreAttackSpeed;
                break;
        }
    }

    private void OnValidate()
    {
        Change();
    }
}
