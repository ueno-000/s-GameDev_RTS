using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MassSelectScript : MonoBehaviour
{
    [SerializeField] private Transform _targetObj;

    private Vector3 mouse;
    private Vector3 target;

    void Update()
    {
        mouse = Input.mousePosition;
        target = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, 10));
        _targetObj.position = target;
    }
}
