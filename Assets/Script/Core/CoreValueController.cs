using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreValueController : MonoBehaviour
{
    [SerializeField] private float _subCoreHitPoint = 100f;
    public float SubCoreHitPoint
    {
       get => _subCoreHitPoint;
       set => _subCoreHitPoint = value;
    }

    [SerializeField] private float _mainCoreHitPoint = 500f;
    public float MainCoreHitPoint
    {
        get => _mainCoreHitPoint;
        set => _mainCoreHitPoint = value;
    }

    [SerializeField] private float _subCoreAttackSpeed = 5f;
    public float SubCoreAttackSpeed
    {
        get => _subCoreAttackSpeed;
        set => _subCoreAttackSpeed = value;
    }

    [SerializeField] private float _mainCoreAttackSpeed = 5f;
    public float MainCoreAttackSpeed
    {
        get => _mainCoreAttackSpeed;
        set => _mainCoreAttackSpeed = value;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
