using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveAction
{
    Idle = 0,//静止
    AdvanceMove = 1,//前進
    CoreAttack = 2,//コアに対しての攻撃
    CaraAttack = 3,//キャラクターに対しての攻撃
    Chase = 4,//追従
    EvasionMove = -1,//回避
    Return = -2//拠点に戻る
}

public class BaseMoveController : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] protected float _speed = 5;

    /// <summary>攻撃距離</summary>
    [SerializeField] protected float _attackTransitionDistance = 2;


    /// <summary>Level</summary>
    protected int _level = 1;

    /// <summary>ActionのType</summary>
    [SerializeField] public MoveAction _actionType = MoveAction.AdvanceMove;

 
    private Vector3 _targetPos;

    private Vector3 _thisPos;

    private float _currentTargetDistance = 0f;

    private bool _isMoving = false;

    void Start()
    {
        StartCoroutine(MoveStart(5));
        _targetPos = GetComponent<TargetSerchScript>()._target.transform.position;
        _actionType = MoveAction.AdvanceMove;
    }

    void  Update()
    {
        if(_isMoving)
            ActionChange();
    }

    /// <summary>
    /// Acrionの切替
    /// </summary>
    protected virtual void ActionChange()
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
            case MoveAction.CoreAttack:
                Debug.Log("攻撃");
               
                break;
        }
    }

    protected virtual void FixedUpdate()
    {
        LookDistanse(2);
    }

    /// <summary>
    /// コアとの距離が指定した値に達したらActionを変える
    /// </summary>
    /// <param name="value"></param>
    protected virtual void LookDistanse(float value)
    {
        _thisPos = this.gameObject.transform.position;

        _currentTargetDistance = Mathf.Sqrt(Mathf.Pow(_thisPos.x - _targetPos.x, 2) + Mathf.Pow(_thisPos.z - _targetPos.z, 2));

        if (_currentTargetDistance <= _attackTransitionDistance)
        {
            _actionType = MoveAction.CoreAttack;
        }
        else
        {
            _actionType = MoveAction.AdvanceMove;
        }
    }

    /// <summary>
    /// Game開始までの処理
    /// </summary>
    /// <param name="second">秒数指定</param>
    /// <returns></returns>
    protected IEnumerator MoveStart(float second)
    {
        Debug.Log("待機ターン");
        _isMoving = false;
        yield return new WaitForSeconds(second);
        Debug.Log("行動開始");
        _isMoving = true;
        yield break;
    }
}
