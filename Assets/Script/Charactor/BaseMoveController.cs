using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveAction
{
    Idle = 0,//�Î~
    AdvanceMove = 1,//�O�i
    CoreAttack = 2,//�R�A�ɑ΂��Ă̍U��
    EnemyAttack = 3,//�L�����N�^�[�ɑ΂��Ă̍U��
    Chase = 4,//�Ǐ]
}


public class BaseMoveController : MonoBehaviour,IDamage
{
    /// <summary>�ړ����x</summary>
    [SerializeField] protected float _speed = 5;

    /// <summary>�U������</summary>
    [SerializeField] protected float _attackTransitionDistance = 2;

    /// <summary>Action��Type</summary>
    [SerializeField] public MoveAction ActionType = MoveAction.AdvanceMove;

    private GameObject _enemy;

    private Vector3 _targetCorePos;
    private Vector3 _thisPos;

    private Vector3 _basePos;

    /// <summary>���݂̃R�A�Ƃ̋���</summary>
    private float _currentTargetDistance = 0f;

    /// <summary>�����̔���</summary>
    private bool isMoving = false;

    /// <summary>�U���Ώۂ��R�A���ǂ����̔���</summary>
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

        //���_�̃|�W�V�������i�[����
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
    /// Acrion�̐ؑ�
    /// </summary>
    protected virtual void ActionChange()
    {
        switch (ActionType)
        {
            case MoveAction.AdvanceMove:

                if (isTargetCore)
                {
                    //y���̓v���C���[�Ɠ����ɂ���
                    _targetCorePos.y = transform.position.y;
                    // �v���C���[�Ɍ�������
                    transform.LookAt(_targetCorePos);

                    //�I�u�W�F�N�g��O�����Ɉړ�����
                    transform.position = transform.position + transform.forward * _speed * Time.deltaTime;
                }
                break;

            case MoveAction.CoreAttack:
                DefaultAttack(this.transform.GetChild(0).GetComponent<TargetSerchScript>().TargetCore);
                Debug.Log("�U��");
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
    /// �R�A�Ƃ̋������w�肵���l�ɒB������Action��ς���
    /// </summary>
    /// <param name="value">���m���鋗��</param>
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
    ///�G�L�����N�^�[�ɑ����������̏��� 
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
    /// �ʏ�U��
    /// </summary>
    private void DefaultAttack(GameObject _enm)
    {
        _time += Time.deltaTime;
        var interval = _valueController.AttackInterval;
        var damage = _valueController._attackPower;
        if (_time >= interval)
        {
            Debug.Log("�G�ɍU��");
            _enm.GetComponent<IDamage>().ReceiveDamage(damage);
            _time = 0.0f;
        }

    }

    /// <summary>
    /// HP�̕ω�
    /// </summary>
    /// <param name="value"></param>
    public void ReceiveDamage(float value)
    {
        _valueController.HP(value);
    }



    /// <summary>
    /// Game�J�n�܂ł̏���
    /// </summary>
    /// <param name="second">�b���w��</param>
    /// <returns></returns>
    protected IEnumerator MoveStart(float second)
    {
        Debug.Log("�ҋ@�^�[��");
        isMoving = false;
        yield return new WaitForSeconds(second);
        Debug.Log("�s���J�n");
        isMoving = true;
        yield break;
    }
}
