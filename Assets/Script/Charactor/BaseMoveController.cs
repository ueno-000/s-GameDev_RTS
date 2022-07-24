using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveAction
{
    AdvanceMove = 1,//�O�i
    CoreAttack = 2,//�R�A�ɑ΂��Ă̍U��
    CaraAttack = 3,//�L�����N�^�[�ɑ΂��Ă̍U��
    Chase = 4,//�Ǐ]
    EvasionMove = -1,//���
    Return = -2//���_�ɖ߂�
}

public class BaseMoveController : MonoBehaviour
{
    /// <summary>�ړ����x</summary>
    [SerializeField] protected float _speed = 5;

    /// <summary>Level</summary>
    protected int _level = 1;

    /// <summary>Action��Type</summary>
    [SerializeField] public MoveAction _actionType = MoveAction.AdvanceMove;


    [SerializeField] protected GameObject[] _targets;

    private Vector3 _thisPos;
    private Vector3 _targetPos;

    private float _currentTargetDistance = 0f;

    void Start()
    {
      
    }

    void Update()
    {
        ActionChange();
    }

    /// <summary>
    /// Acrion�̐ؑ�
    /// </summary>
    private void ActionChange()
    {
        switch (_actionType)
        {
            case MoveAction.AdvanceMove:

                //y���̓v���C���[�Ɠ����ɂ���
                _targetPos.y = transform.position.y;
                // �v���C���[�Ɍ�������
                transform.LookAt(_targetPos);

                //�I�u�W�F�N�g��O�����Ɉړ�����
                transform.position = transform.position + transform.forward * _speed * Time.deltaTime;

                break;
            case MoveAction.CoreAttack:
                Debug.Log("�U��");
               
                break;
        }
    }

    private void FixedUpdate()
    {
        LookDistanse(2);
    }

    /// <summary>
    /// �R�A�Ƃ̋������w�肵���l�ɒB������Action��ς���
    /// </summary>
    /// <param name="value"></param>
    private void LookDistanse(float value)
    {
        _thisPos = this.gameObject.transform.position;

        _currentTargetDistance = Mathf.Sqrt(Mathf.Pow(_thisPos.x - _targetPos.x, 2) + Mathf.Pow(_thisPos.z - _targetPos.z, 2));

        if (_currentTargetDistance <= value)
        {
            _actionType = MoveAction.CoreAttack;
        }
        else
        {
            _actionType = MoveAction.AdvanceMove;
        }
    }

}