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
    EvasionMove = -1,//���
    Return = -2//���_�ɖ߂�
}

public class BaseMoveController : MonoBehaviour,IDamage
{
    /// <summary>�ړ����x</summary>
    [SerializeField] protected float _speed = 5;

    /// <summary>�U������</summary>
    [SerializeField] protected float _attackTransitionDistance = 2;

    /// <summary>Action��Type</summary>
    [SerializeField] public MoveAction _actionType = MoveAction.AdvanceMove;

    private GameObject _enemy;

    private Vector3 _targetCorePos;
    private Vector3 _thisPos;

    /// <summary>���݂̃R�A�Ƃ̋���</summary>
    private float _currentTargetDistance = 0f;

    /// <summary>�����̔���</summary>
    private bool IsMoving = false;

    /// <summary>�U���Ώۂ��R�A���ǂ����̔���</summary>
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
    /// Acrion�̐ؑ�
    /// </summary>
    protected virtual void ActionChange()
    {
        switch (_actionType)
        {
            case MoveAction.AdvanceMove:

                if (IsTargetCore)
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
                Debug.Log("�U��");
                break;

            case MoveAction.EnemyAttack:

                if (!IsTargetCore)
                {
                    Debug.Log("�G�L�����N�^�[�ɍU��");
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
    /// �R�A�Ƃ̋������w�肵���l�ɒB������Action��ς���
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
    ///�G�L�����N�^�[�ɑ����������̏��� 
    /// </summary>
    protected virtual void AttackEnemy()
    {
        _actionType = MoveAction.EnemyAttack;

    }

    /// <summary>
    /// HP�̕ω�
    /// </summary>
    /// <param name="value"></param>
    public void ReceiveDamage(float value)
    {
        _valueController = GetComponent<BaseValueController>();
        _valueController._healthPoint = value--;
    }



    /// <summary>
    /// Game�J�n�܂ł̏���
    /// </summary>
    /// <param name="second">�b���w��</param>
    /// <returns></returns>
    protected IEnumerator MoveStart(float second)
    {
        Debug.Log("�ҋ@�^�[��");
        IsMoving = false;
        yield return new WaitForSeconds(second);
        Debug.Log("�s���J�n");
        IsMoving = true;
        yield break;
    }
}
