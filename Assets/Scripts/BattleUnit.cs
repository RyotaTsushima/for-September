using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] public int MaxHealth;
    [HideInInspector] public int Health;

    [SerializeField] public CommandBase[] Commands;
    [SerializeField] public Text CommandText;
    [HideInInspector] public List<BattleUnit> Target = new List<BattleUnit>();
    [HideInInspector] public CommandBase SelectCommand;
    [HideInInspector] bool _isDead;
    [HideInInspector] GameManager _gameManager;

    private void Start()
    {
        Health = MaxHealth;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if(Health <= 0 && _isDead == false)
        {
            _isDead = true;
            Debug.Log($"{name} died");
            if (this.gameObject.tag == "player")
            {
                GameManager.NumberOfKilled += 1;
                
            }
            else if(this.gameObject.tag == "Enemy")
            {
                Debug.Log("Clear!");
                _gameManager.IsCleared = true;
            }
        }
    }

    public void EnemyCommandSet()
    {
        int rd = Random.Range(0, 1);
        if(rd == 0)
        {
            SelectCommand = Commands[0];
            int rd2 = Random.Range(1, _gameManager.Players.Length);
            Target.Add(_gameManager.Players[rd2 - 1]);
        }
        if(rd == 1)
        {
            SelectCommand = Commands[1];
            foreach(BattleUnit unit in _gameManager.Players)
            {
                Target.Add(unit);
            }
        }
    }

    public void Provacated()
    {
        if (SelectCommand == Commands[1])
        {
            int rnd = Random.Range(1, _gameManager.Provocations.Count);
            Target.Clear();
            Target.Add(_gameManager.Provocations[rnd - 1]);
        }
    }
}
