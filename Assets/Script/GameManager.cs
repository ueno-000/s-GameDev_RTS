using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] float _gameTime;
    float _currentTime;

    private void Awake()
    {
        instance = this;
            
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime = _gameTime - Time.deltaTime;

        if (_currentTime == 0)
        {
            Debug.LogError("GameSet");
        }


    }
}
