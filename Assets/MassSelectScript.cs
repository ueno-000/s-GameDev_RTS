using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassSelectScript : MonoBehaviour
{
    private Vector3 _mouse;
    private Vector3 _target;
    [SerializeField] private Transform _targetObj;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _mouse = Input.mousePosition;
        _target = Camera.main.ScreenToWorldPoint(new Vector3(_mouse.x, _mouse.y, 10));
        _targetObj.position = _target;
    }
}
