using DG.Tweening;
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
    [SerializeField] Text TurnText;
    [SerializeField] Image FadePanel;
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] GameObject _tokoButton;
    int _turn;
    public int NumberOfDead;
    static public int AttackRatio;
    [HideInInspector] public bool _isGameOver;
    [HideInInspector] public bool IsCleared;
    [HideInInspector] public BattleUnit Target;
    [HideInInspector] public List<BattleUnit> Provocations;
    [HideInInspector] bool[] _targetIsSelected = new bool[3];
    [HideInInspector] public List<BattleUnit> SelectablePlayers;
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
    int _index;

    void Start()
    {
        AttackRatio = 1;
        _turn = 0;
        TurnText.text = "Turn : 1";
        NumberOfDead = 0;
        _isGameOver = false;
        IsCleared = false;
        _phase = Phase.StartPhase;
        StartCoroutine(Battle());
        FadePanel.DOFade(0f, 0.3f);
        FadePanel.enabled = false;
        _ub.ButtonActivate(false);
        SelectablePlayers.Clear();
        foreach(var player in Players)
        {
            SelectablePlayers.Add(player);
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
        _targetIsSelected[_index] = true;
        if (_targetIsSelected[0] && _targetIsSelected[1] && _targetIsSelected[2])
        {
            _isSelected = true;
        }
    }

    public void SelectCommand(int index)
    {
        Target.SelectCommand = Target.Commands[index];
        _targetIsSelected[_index] = true;
    }

    public void PhaseEnd()
    {
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
                    _ub.ButtonActivate(true);
                    yield return null;
                    _eventSystem.SetSelectedGameObject(_tokoButton);
                    _turn++;
                    TurnText.text = $"Turn : {_turn}";
                    NumberReset();
                    yield return new WaitUntil(() => _isSelected);
                    _ub.ButtonActivate(false);
                    _phase = Phase.ExecutePhase;
                    break;
                case Phase.ExecutePhase:
                    if (Provocations.Count != 0)
                    {
                        Enemy.Provacated();
                    }
                    foreach (var m in Players)
                    {
                        m.IsAttacked = false;
                        m.StartCoroutine(m.PlayerAction());
                        m.Target.Clear();
                        yield return new WaitUntil(() => m.IsAttacked);
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
                    break;
                case Phase.End:
                    FadePanel.DOFade(1f, 0.3f);
                    yield return new WaitForSeconds(0.3f);
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
