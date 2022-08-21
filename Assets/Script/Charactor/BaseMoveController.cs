using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveAction
{
    Idle = 0,//静止
    AdvanceMove = 1,//前進
    CoreAttack = 2,//コアに対しての攻撃
    EnemyAttack = 3,//キャラクターに対しての攻撃
    Chase = 4,//追従
    EvasionMove = -1,//回避
    Return = -2//拠点に戻る
}

public class BaseMoveController : MonoBehaviour,IDamage
{
    /// <summary>移動速度</summary>
    [SerializeField] protected float _speed = 5;

    /// <summary>攻撃距離</summary>
    [SerializeField] protected float _attackTransitionDistance = 2;

    /// <summary>ActionのType</summary>
    [SerializeField] public MoveAction _actionType = MoveAction.AdvanceMove;

    private GameObject _enemy;

    private Vector3 _targetCorePos;
    private Vector3 _thisPos;

    /// <summary>現在のコアとの距離</summary>
    private float _currentTargetDistance = 0f;

    /// <summary>動きの判定</summary>
    private bool IsMoving = false;

    /// <summary>攻撃対象がコアかどうかの判定</summary>
    private bool IsTargetCore = false;

    /// <summary>ValueController</summary>
    protected BaseValueController _valueController;

    void Start()
    {
        StartCoroutine(MoveStart(5));
        
        _actionType = MoveAction.AdvanceMove;
        _enemy = null;
        IsTargetCore = true;

    }

    void Update()
    {
        if (IsMoving)
        {
            ActionChange();
        }


        _enemy = transform.GetChild(0).GetComponent<TargetSerchScript>()._targetEnemy;

        if (_enemy != null)
        {
            IsTargetCore = false;
            AttackEnemy();
        }
        else
        {
            _actionType = MoveAction.AdvanceMove;
        }
    }

    /// <summary>
    /// Acrionの切替
    /// </summary>
    protected virtual void ActionChange()
    {
        switch (_actionType)
        {
            case MoveAction.AdvanceMove:

                if (IsTargetCore)
                {
                    //y軸はプレイヤーと同じにする
                    _targetCorePos.y = transform.position.y;
                    // プレイヤーに向かせる
                    transform.LookAt(_targetCorePos);

                    //オブジェクトを前方向に移動する
                    transform.position = transform.position + transform.forward * _speed * Time.deltaTime;
                }
                break;
            case MoveAction.CoreAttack:
                Debug.Log("攻撃");
                break;

            case MoveAction.EnemyAttack:

                if (!IsTargetCore)
                {
                    Debug.Log("敵キャラクターに攻撃");
                    this.transform.LookAt(_enemy.transform);
                }
                break;
        }
    }

    protected virtual void FixedUpdate()
    {
        _targetCorePos = transform.GetChild(0).GetComponent<TargetSerchScript>()._targetCore.transform.position;
        LookDistanse(2);
    }

    /// <summary>
    /// コアとの距離が指定した値に達したらActionを変える
    /// </summary>
    /// <param name="value"></param>
    protected virtual void LookDistanse(float value)
    {
        _thisPos = this.gameObject.transform.position;

        _currentTargetDistance = Mathf.Sqrt(Mathf.Pow(_thisPos.x - _targetCorePos.x, 2) + Mathf.Pow(_thisPos.z - _targetCorePos.z, 2));

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
    ///敵キャラクターに遭遇した時の処理 
    /// </summary>
    protected virtual void AttackEnemy()
    {
        _actionType = MoveAction.EnemyAttack;

    }

    /// <summary>
    /// HPの変化
    /// </summary>
    /// <param name="value"></param>
    public void ReceiveDamage(float value)
    {
        _valueController = GetComponent<BaseValueController>();
        _valueController._healthPoint = value--;
    }



    /// <summary>
    /// Game開始までの処理
    /// </summary>
    /// <param name="second">秒数指定</param>
    /// <returns></returns>
    protected IEnumerator MoveStart(float second)
    {
        Debug.Log("待機ターン");
        IsMoving = false;
        yield return new WaitForSeconds(second);
        Debug.Log("行動開始");
        IsMoving = true;
        yield break;
    }
}
