using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveAction
{
    AdvanceMove = 1,//前進
    Attack = 2,//攻撃
    //Chase = 2,//追従
    //RecessionMove = -1,//後退

}
public class BaseMoveController : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] protected float _speed = 5;

    /// <summary>HP</summary>
    protected int _helthPoint = 5;

    /// <summary>ActionのType</summary>
    [SerializeField] public MoveAction _ActionType = MoveAction.AdvanceMove;

    [SerializeField] protected GameObject _target; 
    
    void Start()
    {
        TargetSerch();

    }

    private void TargetSerch()
    {
        _target = GameObject.FindWithTag("BlueCore");
    }

    void Update()
    {
        ActionChange();
    }

    private void ActionChange()
    {
        switch (_ActionType)
        {
            case MoveAction.AdvanceMove:

                Vector3 _targetPos = _target.transform.position;
                //y軸はプレイヤーと同じにする
                _targetPos.y = transform.position.y;
                // プレイヤーに向かせる
                transform.LookAt(_targetPos);

                //オブジェクトを前方向に移動する
                transform.position = transform.position + transform.forward * _speed * Time.deltaTime;

                break;
            case MoveAction.Attack:
                Debug.Log("攻撃");
                break;
        }
    }
}
