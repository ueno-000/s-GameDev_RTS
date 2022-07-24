using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerColor
{
    Blue = 0,
    Red = 1
}

public class TargetSerchScript : MonoBehaviour
{
    [SerializeField] protected GameObject[] _targets;

    /// <summary>Player‚ÌType</summary>
    [SerializeField] public PlayerColor _playerType = PlayerColor.Blue;
    // Start is called before the first frame update
    void Start()
    {
        TargetSerch();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TargetSerch()
    {
        switch (_playerType)
        {
            case 0:
                _targets = GameObject.FindGameObjectsWithTag("Red");
                break;
            case (PlayerColor)1:
                _targets = GameObject.FindGameObjectsWithTag("Blue");
                break;
        }
        //foreach (var obj in _targets)
        //{
        //    _targetPos = obj.transform.position;
        //}
    }

}
