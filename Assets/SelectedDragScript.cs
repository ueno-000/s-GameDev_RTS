using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

/// <summary>
/// �I�u�W�F�N�g���h���b�O���Ĉړ�������
/// </summary>

public class SelectedDragScript : MonoBehaviour,IDragHandler,IEndDragHandler
{
    /// <summary>�h���b�O�̑��x</summary>
    private float _dragSpeed = 10f;

    /// <summary>�I�u�W�F�N�g�n�ʂ��畂�����鍂�� </summary>
    private const float OffsetY = 2;

    private Plane plane= new(Vector3.up, new Vector3(0, OffsetY, 0));

    private Rigidbody _rb;
    private Transform _transform;
    private Camera _camera;

    private bool isKinematic;

    private BaseMoveController _moveController;
    private BaseValueController _valueController;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _transform = this.transform;

        _camera = Camera.main;
        
        _moveController = GetComponent<BaseMoveController>();
        _valueController = GetComponent<BaseValueController>();
    }

    private void Start()
    {
        _rb.isKinematic = true; 
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            Vector3 pos = default;
            if (plane.Raycast(ray, out float hit))
            {
                pos = ray.GetPoint(hit);
            }
            else if (hit < -1.0f)
            {
                pos = ray.GetPoint(-hit);
            }

            if (pos != default)
            {
                _transform.position = Vector3.Lerp(_transform.position, pos, _dragSpeed * Time.deltaTime);
            }
        }
    }

    /// <summary>
    /// �h���b�O��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        var ray = _camera.ScreenPointToRay(eventData.position);
        Vector3 pos = default;
        if (plane.Raycast(ray, out float hit))
        {
            pos = ray.GetPoint(hit);
        }
        else if (hit < -1.0f)
        {
            pos = ray.GetPoint(-hit);
        }

        if (pos != default)
        {
            _transform.position = Vector3.Lerp(_transform.position, pos, _dragSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// �h���b�O�̏I��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        // �h���b�O��͕������Z�����ɖ߂�
        if (_rb != null)
        {
            _rb.isKinematic = isKinematic;
        }

        _moveController.enabled = true;
        _valueController.enabled = true;

        Destroy(this);
    }
}
