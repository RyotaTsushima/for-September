using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public BattleUnit[] Players;
    [SerializeField] BattleUnit Enemy;
    [SerializeField] Text[] CommandText;
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

    void Start()
    {
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

    public void SelectTatget(int index)
    {
        Target = Players[index];
        _targetIsSelected[index] = true;
        if (_targetIsSelected[0] && _targetIsSelected[1] && _targetIsSelected[2])
        {
            _isSelected = true;
        }
    }

    public void Provocation()
    {
        Provocations.Add(Target);
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
                    yield return new WaitUntil(() => Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0));
                    _phase = Phase.CommandPhase;
                    Enemy.EnemyCommandSet();
                    break;
                case Phase.CommandPhase:
                    yield return new WaitUntil(() => _isSelected);
                    _phase = Phase.ExecutePhase;
                    break;
                case Phase.ExecutePhase:
                    Enemy.Provacated();
                    foreach (var m in Players)
                    {
                        m.SelectCommand.Execute(m,m.Target);
                        m.Target.Clear();
                    }
                    Enemy.SelectCommand.Execute(Enemy, Enemy.Target);
                    Enemy.Target.Clear();
                    if (_isGameOver || IsCleared)
                    {
                        _phase = Phase.End;
                    }
                    else
                    {
                        Enemy.EnemyCommandSet();
                        _phase = Phase.CommandPhase;
                    }
                    break;
                case Phase.Result:
                    SceneManager.LoadScene("Result");
                    _phase = Phase.End;
                    break;
                case Phase.End:
                    break;
            }
        }
    }
}
