using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private const int DEFAULT_POOL_COUNT = 20;
    private static ObjectPool instance;
    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                var poolObj = new GameObject("PoolBase");
                instance = poolObj.AddComponent<ObjectPool>();
                instance._objectDic = new Dictionary<string, List<GameObject>>();
                DontDestroyOnLoad(poolObj);
            }
            return instance;
        }
    }
    private Dictionary<string, List<GameObject>> _objectDic = default;
    public void CreatePool(GameObject poolObject, int poolCount = DEFAULT_POOL_COUNT)
    {
        if (_objectDic.ContainsKey(poolObject.name))
        {
            return;
        }
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < poolCount; i++)
        {
            var obj = Instantiate(poolObject, this.transform);
            obj.SetActive(false);
            list.Add(obj);
        }
        _objectDic.Add(poolObject.name, list);
    }
    public GameObject Use(GameObject useObject)
    {
        if (!_objectDic.ContainsKey(useObject.name))
        {
            CreatePool(useObject);
        }
        foreach (var objList in _objectDic[useObject.name])
        {
            if (objList.activeInHierarchy)
            {
                continue;
            }
            objList.SetActive(true);
            return objList;
        }
        var obj = Instantiate(useObject, this.transform);
        _objectDic[useObject.name].Add(obj);
        return obj;
    }
    public GameObject Use(GameObject useObject, Vector3 pos)
    {
        var obj = Use(useObject);
        obj.transform.position = pos;
        return obj;
    }
    public int GetCount(GameObject countObject)
    {
        if (!_objectDic.ContainsKey(countObject.name))
        {
            return 0;
        }
        int activeCount = 0;
        foreach (var objList in _objectDic[countObject.name])
        {
            if (objList.activeInHierarchy)
            {
                activeCount++;
            }
        }
        return activeCount;
    }
}