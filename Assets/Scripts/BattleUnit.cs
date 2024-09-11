using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] public int MaxHealth;
    [HideInInspector] public int Health;

    [SerializeField] public CommandBase[] Commands;
    [HideInInspector] public List<BattleUnit> Target = new List<BattleUnit>();
    [HideInInspector] public CommandBase SelectCommand;
    [HideInInspector] bool _isDead;
    [HideInInspector] GameManager _gameManager;
    [HideInInspector] public enum Action { attack,heal,provocation,rangeAttack}
    [HideInInspector] public Action _action;

    private void Start()
    {
        Health = MaxHealth;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _action = Action.attack;
    }

    private void Update()
    {
        if(Health < 0 && _isDead == false)
        {
            _isDead = true;
            if(this.gameObject.tag == "player")
            {
                GameManager.NumberOfKilled += 1;
            }
            else if(this.gameObject.tag == "Enemy")
            {
                _gameManager.IsCleared = true;
            }
        }
    }

    public void SetCommand()
    {
        switch (_action)
        {
            case Action.attack:
                SelectCommand = Commands[0];
                break;
            case Action.heal:   
                SelectCommand = Commands[1];
                break;
            case Action.provocation:
                _gameManager.Provocations.Add(gameObject.GetComponent<BattleUnit>());
                break;
            case Action.rangeAttack:
                SelectCommand = Commands[1];
                break;
        }
    }

    public void Provacate()
    {
        _action = Action.provocation;
        _gameManager.Provocations.Add(gameObject.GetComponent<BattleUnit>() );
    }

    public void EnemyCommandSet()
    {
        int rd = Random.Range(0, 1);
        if(rd == 0)
        {
            _action = Action.attack;
            int rd2 = Random.Range(1, _gameManager.Players.Length);
            Target.Add(_gameManager.Players[rd2 - 1]);
        }
        if(rd == 1)
        {
            _action = Action.rangeAttack;
            foreach(BattleUnit unit in _gameManager.Players)
            {
                Target.Add(unit);
            }
        }
    }

    public void Provacated()
    {
        if(_action == Action.attack)
        {
            int rnd = Random.Range(1, _gameManager.Provocations.Count);
            Target.Clear();
            Target.Add(_gameManager.Provocations[rnd - 1]);
        }
    }
}
