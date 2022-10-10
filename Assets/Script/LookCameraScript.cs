using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCameraScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //this.transform.LookAt(Camera.main.transform);

       // this.transform.rotation = Quaternion.LookRotation(Camera.main.transform.position);

        this.transform.rotation = Camera.main.transform.rotation;   
    }
}
