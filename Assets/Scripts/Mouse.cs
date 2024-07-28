using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField] int _maxHealth;
    int _health;
    [SerializeField] int _power;
    float _attackRatio;
    float _defenceRatio;
    bool _alive;
    enum _state{attack,hate,item }
    int _target;
    
    // Start is called before the first frame update
    void Start()
    {
        _alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //€–Sˆ—
        if (_health <= 0)
        {
            _alive = false;
        }
    }

    public void Attack(int _index)
    {

    }
}
