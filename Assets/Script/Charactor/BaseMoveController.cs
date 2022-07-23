using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveAction
{
    AdvanceMove = 1,//前進
    Attack = 2,//攻撃
    //Chase = 2,//追従
    RecessionMove = -1,//後退
}
public enum PlayerColor
{
    Blue = 0,
    Red = 1
}
public class BaseMoveController : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] protected float _speed = 5;

    /// <summary>Level</summary>
    protected int _level = 1;

    /// <summary>ActionのType</summary>
    [SerializeField] public MoveAction _actionType = MoveAction.AdvanceMove;
    /// <summary>PlayerのType</summary>
    [SerializeField] public PlayerColor _playerType = PlayerColor.Blue;

    [SerializeField] protected GameObject[] _targets;

    private Vector3 _thisPos;
    private Vector3 _targetPos;

    private float _currentTargetDistance = 0f;

    void Start()
    {
        TargetSerch();
    }

    private void TargetSerch()
    {
        switch (_playerType)
        {
            case 0:
                _targets = GameObject.FindGameObjectsWithTag("Red");
                break;
            case (PlayerColor)1:
                _targets = GameObject.FindGameObjectsWithTag("Blue");
                break;
        }
        foreach (var obj in _targets)
        {
            _targetPos =  obj.transform.position;
        }
    }

    void Update()
    {
        ActionChange();
    }

    private void ActionChange()
    {
        switch (_actionType)
        {
            case MoveAction.AdvanceMove:

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

    private void FixedUpdate()
    {
        LookDistanse(2);
    }

    /// <summary>
    /// ターゲットとの距離が指定した値に達したらActionを変える
    /// </summary>
    /// <param name="value"></param>
    private void LookDistanse(float value)
    {
        _thisPos = this.gameObject.transform.position;

        _currentTargetDistance = Mathf.Sqrt(Mathf.Pow(_thisPos.x - _targetPos.x, 2) + Mathf.Pow(_thisPos.z - _targetPos.z, 2));

        if (_currentTargetDistance <= value)
        {
            _actionType = MoveAction.Attack;
        }
        else
        {
            _actionType = MoveAction.AdvanceMove;
        }
    }

}
