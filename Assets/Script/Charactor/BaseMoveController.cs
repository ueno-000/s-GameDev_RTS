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
}


public class BaseMoveController : MonoBehaviour,IDamage
{
    /// <summary>移動速度</summary>
    [SerializeField] protected float _speed = 5;

    /// <summary>攻撃距離</summary>
    [SerializeField] protected float _attackTransitionDistance = 2;

    /// <summary>ActionのType</summary>
    [SerializeField] public MoveAction ActionType = MoveAction.AdvanceMove;

    private GameObject _enemy;

    private Vector3 _targetCorePos;
    private Vector3 _thisPos;

    private Vector3 _basePos;

    /// <summary>現在のコアとの距離</summary>
    private float _currentTargetDistance = 0f;

    /// <summary>動きの判定</summary>
    private bool isMoving = false;

    /// <summary>攻撃対象がコアかどうかの判定</summary>
    private bool isTargetCore = false;

    /// <summary>ValueController</summary>
    protected BaseValueController _valueController;

    private float _time;

    void Start()
    {
       StartCoroutine(MoveStart(1));

        ValueSetting();

        ActionType = MoveAction.AdvanceMove;
        _enemy = null;
        isTargetCore = true;

        //拠点のポジションを格納する
        _basePos = this.transform.position;
    }

    private void ValueSetting()
    {
        _valueController = GetComponent<BaseValueController>();

        _speed = _valueController.MoveSpeed;
        _attackTransitionDistance = _valueController.AttackTransitionDistance;
    }

    void Update()
    {
        if (isMoving)
        {
            ActionChange();
        }

        if (_enemy != null)
        {
            isTargetCore = false;

            ActionType = MoveAction.EnemyAttack;
        }


    }

    /// <summary>
    /// Acrionの切替
    /// </summary>
    protected virtual void ActionChange()
    {
        switch (ActionType)
        {
            case MoveAction.AdvanceMove:

                if (isTargetCore)
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
                DefaultAttack(this.transform.GetChild(0).GetComponent<TargetSerchScript>().TargetCore);
                Debug.Log("攻撃");
                break;

            case MoveAction.EnemyAttack:

                if (!isTargetCore)
                {
                    this.transform.LookAt(_enemy.transform);
                }
                AttackEnemy();
                break;
        }
    }

    protected virtual void FixedUpdate()
    {
        _enemy = transform.GetChild(0).GetComponent<TargetSerchScript>().TargetEnemy;
        _targetCorePos = this.transform.GetChild(0).GetComponent<TargetSerchScript>().TargetCore.transform.position;
        LookDistanse(_attackTransitionDistance);
    }

    /// <summary>
    /// コアとの距離が指定した値に達したらActionを変える
    /// </summary>
    /// <param name="value">検知する距離</param>
    protected virtual void LookDistanse(float value)
    {
        _thisPos = this.gameObject.transform.position;

        _currentTargetDistance = Mathf.Sqrt(Mathf.Pow(_thisPos.x - _targetCorePos.x, 2) + Mathf.Pow(_thisPos.z - _targetCorePos.z, 2));

        if (_currentTargetDistance <= value)
        {
            ActionType = MoveAction.CoreAttack;
  
        }
        else
        {
            Debug.Log("Move");
            ActionType = MoveAction.AdvanceMove;
        }
    }

    /// <summary>
    ///敵キャラクターに遭遇した時の処理 
    /// </summary>
    protected virtual void AttackEnemy()
    {
        var terget = _enemy.gameObject.GetComponent<IDamage>();

        if(terget != null)
        {
            DefaultAttack(_enemy);
        }

    }

    /// <summary>
    /// 通常攻撃
    /// </summary>
    private void DefaultAttack(GameObject _enm)
    {
        _time += Time.deltaTime;
        var interval = _valueController.AttackInterval;
        var damage = _valueController._attackPower;
        if (_time >= interval)
        {
            Debug.Log("敵に攻撃");
            _enm.GetComponent<IDamage>().ReceiveDamage(damage);
            _time = 0.0f;
        }

    }

    /// <summary>
    /// HPの変化
    /// </summary>
    /// <param name="value"></param>
    public void ReceiveDamage(float value)
    {
        _valueController.HP(value);
    }



    /// <summary>
    /// Game開始までの処理
    /// </summary>
    /// <param name="second">秒数指定</param>
    /// <returns></returns>
    protected IEnumerator MoveStart(float second)
    {
        Debug.Log("待機ターン");
        isMoving = false;
        yield return new WaitForSeconds(second);
        Debug.Log("行動開始");
        isMoving = true;
        yield break;
    }
}
