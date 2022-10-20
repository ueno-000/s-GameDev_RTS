using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// オブジェクトをドラッグして移動させる
/// </summary>

public class SelectedDragScript : MonoBehaviour,/*IBeginDragHandler,*/IDragHandler,IEndDragHandler
{
    /// <summary>ドラッグの速度</summary>
    private const float _dragSpeed = 10f;

    /// <summary>オブジェクト地面から浮かせる高さ </summary>
    private const float OffsetY = 2;

    private Plane plane = new Plane(Vector3.up, new Vector3(0, OffsetY, 0));

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

    /// <summary>
    /// ドラッグの開始
    /// </summary>
    /// <param name = "eventData" ></ param >
    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    ドラッグ中は物理演算が働かないようにする
    //    if (_rb != null)
    //    {
    //        isKinematic = _rb.isKinematic;
    //        _rb.isKinematic = true;
    //    }
    //}

    /// <summary>
    /// ドラッグ中
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
    /// ドラッグの終了
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        // ドラッグ後は物理演算を元に戻す
        if (_rb != null)
        {
            _rb.isKinematic = isKinematic;
        }

        _moveController.enabled = true;
        _valueController.enabled = true;

        Destroy(this);
    }
}
