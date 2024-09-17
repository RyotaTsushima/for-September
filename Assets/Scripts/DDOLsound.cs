using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOLsound : MonoBehaviour
{
    float _time;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        _time += Time.deltaTime;
        if (_time == 1)
        {
            Destroy(gameObject);
        }
    }
}
