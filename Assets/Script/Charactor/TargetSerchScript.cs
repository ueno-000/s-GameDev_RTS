using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public enum PlayerColor
{
    Blue = 0,
    Red = 1
}

public class TargetSerchScript : MonoBehaviour
{
    [Header("�^�[�Q�b�g")]
    /// <summary>�^�[�Q�b�g�ɂ���R�A</summary>
    [Tooltip("�^�[�Q�b�g�ɂ���R�A"),SerializeField] public GameObject _targetCore;

    /// <summary>�^�[�Q�b�g�ɂ���Enemy</summary>
    [Tooltip("�^�[�Q�b�g�ɂ���Enemy"), SerializeField] public GameObject _targetEnemy;
    
    [Header("Type")]
    /// <summary>Player��Type</summary>
    [SerializeField] public PlayerColor _playerType = PlayerColor.Blue;
    
    /// <summary>�R�A�̃^�O</summary>
    private string[] _coreTag = {"BlueSubCore","RedSubCore", "BlueMainCore", "RedMainCore" };

    [Header("�T�u�R�A")]
    /// <summary>�����w�n�T�u�R�A�̔z��</summary>
    [SerializeField] private GameObject[] _alliesSubCores;

    /// <summary>�G�w�n�T�u�R�A�̔z��</summary>
    [SerializeField] private GameObject[] _enemySubCores;

    [SerializeField] bool isEnemy = false;



    void Start()
    { 
        TargetSerch();
        DecideCoreObject();
    }

    // Update is called once per frame
    void Update()
    {
        var isBreakSubCore = false;
        if (isBreakSubCore)
        {
            
        }

        if (!isEnemy) _targetEnemy = null;
    }

    private void TargetSerch()
    {
        switch (_playerType)
        {
            case 0://Blue
                //�����̃T�u�R�A��z��Ɋi�[����
                _alliesSubCores = GameObject.FindGameObjectsWithTag(_coreTag[0]);//Blue
                //�G�̃T�u�R�A��z��Ɋi�[����
                _enemySubCores = GameObject.FindGameObjectsWithTag(_coreTag[1]);//Red
                break;
            case (PlayerColor)1://Red
                //�����̃T�u�R�A��z��Ɋi�[����
                _alliesSubCores = GameObject.FindGameObjectsWithTag(_coreTag[1]);//Red
                //�G�̃T�u�R�A��z��Ɋi�[����
                _enemySubCores = GameObject.FindGameObjectsWithTag(_coreTag[0]);//Blue
                break;
        }
    }

    /// <summary>
    /// ��ԋ߂������ɂ���G�̃R�A���U���Ώۂɂ���
    /// </summary>
    void DecideCoreObject()
    {
        var enemyCoreArray = _enemySubCores.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToArray();

        _targetCore = enemyCoreArray[0];

    }

    private void OnTriggerStay(Collider other)
    {
        isEnemy = true;

        switch (_playerType)
        {
            case 0://Blue
                if (other.gameObject.tag == "Red")
                {
                    _targetEnemy = other.gameObject;
                }
                break;
            case (PlayerColor)1://Red
                if (other.gameObject.tag == "Blue")
                {
                    _targetEnemy = other.gameObject;
                }
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (_playerType)
        {
            case 0://Blue
                if (other.gameObject.tag == "Red")
                {
                    isEnemy = false;
                    Debug.Log("RedEnemy���U���ΏۊO�ɂȂ�܂���");
                }
                break;
            case (PlayerColor)1://Red
                if (other.gameObject.tag == "Blue")
                {
                    isEnemy = false;
                    Debug.Log("BlueEnemy���U���ΏۊO�ɂȂ�܂���");
                }
                break;
        }
    }

}
