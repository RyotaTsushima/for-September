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
        //ƒŠƒXƒg‚Ö‚Ì“o˜^
        GameManager gm=_manager.GetComponent<GameManager>();
        gm._cat.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// UŒ‚‚·‚éÛ‚ÉŒÄ‚Ño‚·
    /// </summary>
    public void Attack()
    {

    }
}
