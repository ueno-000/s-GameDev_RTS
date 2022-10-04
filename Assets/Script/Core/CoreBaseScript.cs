using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type
{
    SubCore,
    MainCore
}

public class CoreBaseScript : MonoBehaviour,IDamage
{
   [SerializeField] private float _healthPoint = 5;
    private float _attackSpeed = 5;
    private float _damage = 5;
    private float _time;

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

    public void ReceiveDamage(float value)
    {
        _healthPoint -= value;
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.GetComponent<IDamage>() != null)
            {
                _time += Time.deltaTime;

                if(_time >= _attackSpeed)
                {
                    other.gameObject.GetComponent<IDamage>().ReceiveDamage(_damage);
                    Debug.Log($"{other}Ç…{_damage}É_ÉÅÅ[ÉWÇÃçUåÇ");
                    _time = 0.0f;
                }

            }
        }
    }
}
