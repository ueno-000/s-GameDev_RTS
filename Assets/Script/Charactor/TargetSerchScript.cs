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
    [Header("ターゲット")]
    /// <summary>ターゲットにするコア</summary>
    [Tooltip("ターゲットにするコア"),SerializeField] public GameObject TargetCore;

    /// <summary>ターゲットにするEnemy</summary>
    [Tooltip("ターゲットにするEnemy"), SerializeField] public GameObject TargetEnemy;
    
    [Header("Type")]
    /// <summary>PlayerのType</summary>
    [SerializeField] public PlayerColor PlayerType = PlayerColor.Blue;
    
    /// <summary>コアのタグ</summary>
    private string[] _coreTag = {"BlueSubCore","RedSubCore", "BlueMainCore", "RedMainCore" };

    [Header("サブコア")]

    /// <summary>敵陣地サブコアの配列</summary>
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
        if (!isEnemy) TargetEnemy = null;
    }

    private void TargetSerch()
    {
        switch (PlayerType)
        {
            case 0://Blue
                //敵のサブコアを配列に格納する
                _enemySubCores = GameObject.FindGameObjectsWithTag(_coreTag[1]);//Red
                break;
            case (PlayerColor)1://Red
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

        TargetCore = enemyCoreArray[0];

    }

    private void OnTriggerStay(Collider other)
    {
        isEnemy = true;

        switch (PlayerType)
        {
            case 0://Blue
                if (other.gameObject.tag == "Red")
                {
                    TargetEnemy = other.gameObject;
                }
                break;
            case (PlayerColor)1://Red
                if (other.gameObject.tag == "Blue")
                {
                    TargetEnemy = other.gameObject;
                }
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (PlayerType)
        {
            case 0://Blue
                if (other.gameObject.tag == "Red")
                {
                    isEnemy = false;
                    Debug.Log("RedEnemyが攻撃対象外になりました");
                }
                break;
            case (PlayerColor)1://Red
                if (other.gameObject.tag == "Blue")
                {
                    isEnemy = false;
                    Debug.Log("BlueEnemyが攻撃対象外になりました");
                }
                break;
        }
    }

}
