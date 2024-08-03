using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] GameObject _manager;
    [SerializeField] int _maxHealth;
    public int _health;
    bool _alive;
    // Start is called before the first frame update
    void Start()
    {
        _alive = true;

        //リストへの登録
        GameManager gm=_manager.GetComponent<GameManager>();
        gm._cat.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 攻撃する際に呼び出す
    /// </summary>
    public void Attack()
    {

    }
}
