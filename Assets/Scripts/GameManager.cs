using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] _mouse;
    public List<GameObject> _cat = new List<GameObject>();
    [SerializeField] UnityEvent _startEvent;
    // Start is called before the first frame update
    void Start()
    {
        _startEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
