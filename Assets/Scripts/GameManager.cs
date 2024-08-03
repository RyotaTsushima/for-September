using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] _mouse;
    public List<GameObject> _cat = new List<GameObject>();
    [SerializeField] UnityEvent _startEvent;
    int _target;
    bool _commanadPhase;
    int _mouseSelect;
    bool _select;
    int _comSelect;
    bool _battlePhase;
    Animator _anim;
    
    void Start()
    {
        _startEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (_commanadPhase)
        {
            if (_select == false)
            {
                _mouseSelect = 0;
                if(Input.GetKey(KeyCode.S) && _mouseSelect < 0) 
                {
                    _mouseSelect++;
                }

                if(Input.GetKey(KeyCode.W) && _mouseSelect > _mouse.Length - 1)
                {
                    _mouseSelect--;
                }

                if(Input.GetKey(KeyCode.Return)) 
                { 
                    _select = true;
                }
            }
            else
            {
                _comSelect = 0;
                if (Input.GetKeyDown(KeyCode.S) && _comSelect>0)
                {
                    _comSelect++;
                }

                if (Input.GetKeyDown(KeyCode.W) && _comSelect<2) 
                {
                    _comSelect--;
                }
            }
        }
    }
}
