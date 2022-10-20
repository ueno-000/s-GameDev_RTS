using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployCaractor : MonoBehaviour
{
    [SerializeField] GameObject _caraPrefab;

    public void OnClick()
    {
        Instantiate(_caraPrefab,new Vector3(0,8,-12),Quaternion.identity);
        this.gameObject.SetActive(false);
    }

}
