using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// オブジェクトをドラッグして移動させる
/// </summary>

public class SelectedDragScript : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private float _dragSpeed = 10f;
    private const float OffsetY = 2;
    private Plane _plane = new Plane(Vector3.up, new Vector3(0, OffsetY, 0));

    private Rigidbody _rb;
    private Transform _transform;
    private Camera _camera;

    private bool _isKinematic;

    private BaseMoveController _moveController;
    private BaseValueController _valueController;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _transform = transform;
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
            Vector3 pos = _plane.Raycast(ray, out float hit) ? ray.GetPoint(hit) : (hit < -1.0f ? ray.GetPoint(-hit) : Vector3.zero);

            if (pos != Vector3.zero)
            {
                _transform.position = Vector3.Lerp(_transform.position, pos, _dragSpeed * Time.deltaTime);
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log(12345);
            if (_rb != null)
            {
                _rb.isKinematic = _isKinematic;
            }

            _moveController.enabled = true;
            _valueController.enabled = true;

            this.enabled= false;

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        var ray = _camera.ScreenPointToRay(eventData.position);
        //Vector3 pos = _plane.Raycast(ray, out float hit) ? ray.GetPoint(hit) : (hit < -1.0f ? ray.GetPoint(-hit) : Vector3.zero);

        //if (pos != Vector3.zero)
        //{
        //    _transform.position = Vector3.Lerp(_transform.position, pos, _dragSpeed * Time.deltaTime);
        //}
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //if (_rb != null)
        //{
        //    _rb.isKinematic = _isKinematic;
        //}

        //_moveController.enabled = true;
        //_valueController.enabled = true;

        //Destroy(this);
    }
}
