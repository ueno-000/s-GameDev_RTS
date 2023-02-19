using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Set
{
    blue = 1,
    red = -1,
    none = 0,
}
public class GroundScript : MonoBehaviour
{
    private Set _groundSetting = Set.none;
    private Collider _col;

    void Start()
    {
        _col = this.gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
