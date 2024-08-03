using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    [SerializeField] GameObject _manager;
    [SerializeField] GameObject[] _cat;
    [SerializeField] int _maxHealth;
    int _health;
    [SerializeField] int _power;
    float _attackRatio;
    float _defenceRatio;
    bool _alive;
    bool _select;
    enum _state{attack,hate,item }
    int _targetIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        _alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        //死亡処理
        if (_health <= 0)
        {
            _alive = false;
        }
    }

    /// <summary>
    /// `攻撃する際に呼び出す。
    /// </summary>
    /// <param name="_index"></param>
    public void Attack()
    {
        if (_cat[_targetIndex] != null)
        {
            var cat = _cat[_targetIndex].GetComponent<Cat>();
            cat._health -= _power;
        }
    }

    /// <summary>
    /// アイテムを使用する際に呼び出す。引数にアイテム名を入れる
    /// </summary>
    /// <param name="itemName"></param>
    public void UseItem(string itemName)
    {
        if (itemName == "チーズ")
        {

        }
    }
}
