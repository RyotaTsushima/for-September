using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] GameObject _manager;
    [SerializeField] int _maxHealth;
    public int _health;
    // Start is called before the first frame update
    void Start()
    {
        //���X�g�ւ̓o�^
        GameManager gm=_manager.GetComponent<GameManager>();
        gm._cat.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// �U������ۂɌĂяo��
    /// </summary>
    public void Attack()
    {

    }
}
