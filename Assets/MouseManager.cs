using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField] GameObject[] _mouse;
    int _mouseIndex;
    // Start is called before the first frame update
    void Start()
    {
        _mouseIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
