using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public BattleUnit[] Players;
    [SerializeField] BattleUnit Enemy;
    [SerializeField] UnitButtonControler _ub;
    static public int NumberOfKilled;
    static public int AttackRatio;
    bool _isGameOver;
    [HideInInspector] public bool IsCleared;
    [HideInInspector] public BattleUnit Target;
    [HideInInspector] public List<BattleUnit> Provocations;
    [HideInInspector] bool[] _targetIsSelected = new bool[3];
    enum Phase
    {
        StartPhase,
        CommandPhase,
        ExecutePhase,
        Result,
        End,
    }
    Phase _phase;
    bool _isSelected = false;
    [SerializeField] UnityEvent _startCommandPhase;
    int _index;

    void Start()
    {
        NumberOfKilled = 0;
        _isGameOver = false;
        IsCleared = false;
        _phase = Phase.StartPhase;
        StartCoroutine(Battle());
    }

    // Update is called once per frame
    void Update()
    {
        switch (NumberOfKilled)
        {
            case 0:
                AttackRatio = 1;
                break;
            case 1:
                AttackRatio = 5;
                break;
            case 2:
                AttackRatio = 50;
                break;
            case 3:
                _isGameOver = true;
                break;
        }
    }

    public void SelectTarget(int index)
    {
        Target = Players[index];
        Debug.Log($"target is {Target.name}");
        Target.Target.Clear();
        Target.Target.Add(Enemy);
        _index = index;
    }

    public void ChangeTarget(int index)
    {
        Target.Target.Clear();
        Target.Target.Add(Players[index]); 
        if (_targetIsSelected[0] && _targetIsSelected[1] && _targetIsSelected[2])
        {
            _isSelected = true;
        }
    }

    public void SelectCommand(int index)
    {
        Target.SelectCommand = Target.Commands[index];
        _targetIsSelected[_index] = true;
        if (_targetIsSelected[0] && _targetIsSelected[1] && _targetIsSelected[2])
        {
            _isSelected = true;
        }
    }

    public void Provocation()
    {
        Provocations.Add(Target);
    }

    public void SetCommandText(string condition)
    {
        Target.CommandText.text = condition;
    }

    IEnumerator Battle()
    {
        while(_phase != Phase.End)
        {
            yield return null;
            Debug.Log(_phase);
            switch (_phase)
            {
                case Phase.StartPhase:
                    NumberReset();
                    yield return new WaitUntil(() => Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0));
                    _phase = Phase.CommandPhase;
                    Enemy.EnemyCommandSet();
                    break;
                case Phase.CommandPhase:
                    NumberReset();
                    _startCommandPhase.Invoke();
                    yield return new WaitUntil(() => _isSelected);
                    _phase = Phase.ExecutePhase;
                    break;
                case Phase.ExecutePhase:
                    if (Provocations.Count != 0)
                    {
                        Enemy.Provacated();
                    }
                    foreach (var m in Players)
                    {
                        m.SelectCommand.Execute(m,m.Target);
                        m.Target.Clear();
                    }
                    Enemy.SelectCommand.Execute(Enemy, Enemy.Target);
                    Enemy.Target.Clear();
                    _phase = Phase.Result;
                    break;
                case Phase.Result:
                    if (_isGameOver || IsCleared)
                    {
                        _phase = Phase.End;
                    }
                    else
                    {
                        Enemy.EnemyCommandSet();
                        _phase = Phase.CommandPhase;
                    }
                    _phase = Phase.End;
                    break;
                case Phase.End:
                    SceneManager.LoadScene("Result");
                    break;
            }
        }
    }

    void NumberReset()
    {
        _isSelected = false;
        _targetIsSelected[0] = false;
        _targetIsSelected[1] = false;
        _targetIsSelected[2] = false;
        Players[0].CommandText.text = "";
        Players[1].CommandText.text = "";
        Players[2].CommandText.text = "";
    }
}
