using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployCaractor : MonoBehaviour
{
    [SerializeField] GameObject _caraPrefab;

    public void OnClick()
    {
        Instantiate(_caraPrefab,new Vector3(0,8,-12),Quaternion.identity);
        //this.gameObject.SetActive(false);
    }

    public void OnClickExit()
    {
        Debug.Log(this.transform.parent);
        //var seted = TryGetComponent(out ICardSeted sr) ? this.transform.parent.gameObject.GetComponentInParent<ICardSeted>() : null;

        var seted = this.transform.parent.gameObject.GetComponentInParent<ICardSeted>();

        if (seted != null)
        {
            seted.CardSet(true, this.transform.GetSiblingIndex());
            this.gameObject.SetActive(false);
            Debug.Log(this.transform.GetSiblingIndex());
        }
        else
        {
            Debug.LogError("aaaaaaaaaaaaaa");
        }
    }

}
