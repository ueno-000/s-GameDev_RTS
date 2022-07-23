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
    [SerializeField] private float _healthPoint = 5;
    [SerializeField] public Type CoreType = Type.SubCore;

    MeshRenderer _meshRenderer;
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        Change();
    }

    private void Change()
    {
        switch (CoreType)
        {
            case Type.SubCore:
                _healthPoint = 5;
                break;
            case Type.MainCore:
                _healthPoint = 100;

                break;
        }
    }

    private void OnValidate()
    {
        Change();
    }
}
