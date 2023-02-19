using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSetController : MonoBehaviour,ICardSeted
{
    [SerializeField] private GameObject[] _cardList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CardSet(bool isSet,int num)
    {
        var child = this.transform.GetChild(num).gameObject;
        child.GetComponent<Transform>().SetSiblingIndex(_cardList.Length);

    }
}
