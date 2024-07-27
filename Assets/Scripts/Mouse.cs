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
    
    // Start is called before the first frame update
    void Start()
    {
        _alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //éÄñSèàóù
        if (_health <= 0)
        {
            _alive = false;
        }
    }
}
