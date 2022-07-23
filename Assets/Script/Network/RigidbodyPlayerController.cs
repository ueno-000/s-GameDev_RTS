using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.AI;   // Navmesh Agent を使うために必要

/// <summary>
/// Rigidbody を使ってプレイヤーを動かすコンポーネント
/// 入力を受け取り、それに従ってオブジェクトを動かす
/// ControlType を設定することで、オールドタイプ（ラジコン型）と現代的なタイプの操作系を切り替えられる
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class RigidbodyPlayerController : MonoBehaviour
{
    /// <summary>動く速さ</summary>
    [SerializeField] float _movingSpeed = 5f;
    /// <summary>ターンの速さ</summary>
    [SerializeField] float _turnSpeed = 3f;
    /// <summary>ジャンプ力</summary>
    [SerializeField] float _jumpPower = 5f;
    /// <summary>接地判定の際、中心 (Pivot) からどれくらいの距離を「接地している」と判定するかの長さ</summary>
    [SerializeField] float _isGroundedLength = 1.1f;
    /// <summary>キャラクターの Animator</summary>
    [SerializeField] Animator _anim;


    /// <summary>移動先となる位置情報</summary>
    [SerializeField] Transform _maker;
    /// <summary>移動先座標を保存する変数</summary>
    Vector3 _cachedTargetPosition;

    /// <summary> NavMesh Agent コンポーネントを格納する変数</summary>
    NavMeshAgent _agent = default;

    Rigidbody _rb;
    PhotonView _view;


    void Start()
    {
        _view = GetComponent<PhotonView>();

        if (_view)
        {
            if (_view.IsMine)
            {
                // 同期元（自分で操作して動かす）オブジェクトの場合のみ Rigidbody を使う
                _rb = GetComponent<Rigidbody>();
            }
        }
        _agent = GetComponent<NavMeshAgent>();
        // 初期位置を保存する（※）
        _cachedTargetPosition = _maker.position;
    }

    void Update()
    {
        if (_view && !_view.IsMine) return;  // 同期先のオブジェクトだった場合は何もしない
        {
            // _target が移動したら Navmesh Agent を使って移動させる
            if (Vector3.Distance(_cachedTargetPosition, _maker.position) > Mathf.Epsilon) // _target が移動したら
            {
                _cachedTargetPosition = _maker.position; // 移動先の座標を保存する
                _agent.SetDestination(_cachedTargetPosition);  // Navmesh Agent に目的地をセットする
            }
        }
    }


}
