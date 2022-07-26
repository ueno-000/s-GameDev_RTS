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
//public enum SerchCoreType
//{
    
//}
public class TargetSerchScript : MonoBehaviour
{
    /// <summary>ターゲットにするコア</summary>
    [SerializeField] public GameObject _target;

    /// <summary>PlayerのType</summary>
    [SerializeField] public PlayerColor _playerType = PlayerColor.Blue;
    
    /// <summary>コアのタグ</summary>
    private string[] _coreTag = {"BlueSubCore","RedSubCore", "BlueMainCore", "RedMainCore" };

    /// <summary>味方陣地サブコアの配列</summary>
    [SerializeField] private GameObject[] _alliesSubCores;

    /// <summary>敵陣地サブコアの配列</summary>
    [SerializeField] private GameObject[] _enemySubCores;



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
    }

    private void TargetSerch()
    {
        switch (_playerType)
        {
            case 0://Blue
                //味方のサブコアを配列に格納する
                _alliesSubCores = GameObject.FindGameObjectsWithTag(_coreTag[0]);//Blue
                //敵のサブコアを配列に格納する
                _enemySubCores = GameObject.FindGameObjectsWithTag(_coreTag[1]);//Red
                break;
            case (PlayerColor)1://Red
                //味方のサブコアを配列に格納する
                _alliesSubCores = GameObject.FindGameObjectsWithTag(_coreTag[1]);//Red
                //敵のサブコアを配列に格納する
                _enemySubCores = GameObject.FindGameObjectsWithTag(_coreTag[0]);//Blue
                break;
        }
    }

    /// <summary>
    /// 一番近い距離にいる敵のコアを攻撃対象にする
    /// </summary>
    void DecideCoreObject()
    {
        var enemyCoreArray = _enemySubCores.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToArray();

        _target = enemyCoreArray[0];

    }

}
