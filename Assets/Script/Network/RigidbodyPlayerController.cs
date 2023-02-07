using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;   // Navmesh Agent ���g�����߂ɕK�v

/// <summary>
/// Rigidbody ���g���ăv���C���[�𓮂����R���|�[�l���g
/// ���͂��󂯎��A����ɏ]���ăI�u�W�F�N�g�𓮂���
/// ControlType ��ݒ肷�邱�ƂŁA�I�[���h�^�C�v�i���W�R���^�j�ƌ���I�ȃ^�C�v�̑���n��؂�ւ�����
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPlayerController : MonoBehaviour
{
    /// <summary>��������</summary>
    [SerializeField] float _movingSpeed = 5f;
    /// <summary>�^�[���̑���</summary>
    [SerializeField] float _turnSpeed = 3f;
    /// <summary>�W�����v��</summary>
    [SerializeField] float _jumpPower = 5f;
    /// <summary>�ڒn����̍ہA���S (Pivot) ����ǂꂭ�炢�̋������u�ڒn���Ă���v�Ɣ��肷�邩�̒���</summary>
    [SerializeField] float _isGroundedLength = 1.1f;
    /// <summary>�L�����N�^�[�� Animator</summary>
    [SerializeField] Animator _anim;


    /// <summary>�ړ���ƂȂ�ʒu���</summary>
    [SerializeField] Transform _maker;
    /// <summary>�ړ�����W��ۑ�����ϐ�</summary>
    Vector3 _cachedTargetPosition;

    /// <summary> NavMesh Agent �R���|�[�l���g���i�[����ϐ�</summary>
    NavMeshAgent _agent = default;

    Rigidbody _rb;



    void Start()
    {

        // �������i�����ő��삵�ē������j�I�u�W�F�N�g�̏ꍇ�̂� Rigidbody ���g��
        _rb = GetComponent<Rigidbody>();


        _agent = GetComponent<NavMeshAgent>();
        // �����ʒu��ۑ�����i���j
        _cachedTargetPosition = _maker.position;
    }

    void Update()
    {

        // _target ���ړ������� Navmesh Agent ���g���Ĉړ�������
        if (Vector3.Distance(_cachedTargetPosition, _maker.position) > Mathf.Epsilon) // _target ���ړ�������
        {
            _cachedTargetPosition = _maker.position; // �ړ���̍��W��ۑ�����
            _agent.SetDestination(_cachedTargetPosition);  // Navmesh Agent �ɖړI�n���Z�b�g����
        }

    }


}
