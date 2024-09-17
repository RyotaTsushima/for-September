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
    [SerializeField] GameObject ButtonSound;
    int _turn;
    public int NumberOfDead;
    static public int AttackRatio;
    [HideInInspector] public bool IsGameOver;
    [HideInInspector] static public bool IsCleared = false;
    [HideInInspector] public BattleUnit Target;
    [HideInInspector] public List<BattleUnit> Provocations;
    [HideInInspector] bool[] _targetIsSelected = new bool[3];
    [HideInInspector] public List<BattleUnit> SelectablePlayers;
    [HideInInspector] AudioSource _as;
    enum Phase
    {
        StartPhase,
        CommandPhase,
        ExecutePhase,
        Result,
        End,
        End2
    }
    Phase _phase;
    bool _isSelected = false;
    int _index;

    void Start()
    {
        _as = GetComponent<AudioSource>();
        _as.mute = false;
        AttackRatio = 1;
        _turn = 0;
        TurnText.text = "Turn : 1";
        NumberOfDead = 0;
        IsGameOver = false;
        IsCleared = false;
        _phase = Phase.StartPhase;
        StartCoroutine(Battle());
        FadePanel.DOFade(0f, 0.5f);
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
        Target.CommandIndex = index;
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
        Target.SelectCommand = null;
        Provocations.Add(Target);
        Target.CommandIndex = 2;
    }

    public void SetCommandText(string condition)
    {
        Target.CommandText.text = condition;
    }

    IEnumerator Battle()
    {
        while(_phase != Phase.End2)
        {
            yield return null;
            Debug.Log(_phase);
            switch (_phase)
            {
                case Phase.StartPhase:
                    yield return new WaitForSeconds(0.5f);
                    FadePanel.enabled = false;
                    NumberReset();
                    yield return new WaitForSeconds(5.0f);
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
                    if (Provocations.Count > 0)
                    {
                        Enemy.Provacated();
                    }
                    foreach (var m in Players)
                    {
                        m.IsAttacked = false;
                        m.StartCoroutine(m.PlayerAction());
                        if(m.CommandIndex == 0)
                        {
                            yield return new WaitForSeconds(m.AnimTimeDelta + 0.5f);
                            m.SelectCommand.Execute(m, m.Target);
                        }
                        else if(m.CommandIndex == 1)
                        {
                            yield return new WaitForSeconds(0.5f);
                            foreach (var t in m.Target)
                            {
                                if (t.Health == 0)
                                {
                                    SelectablePlayers.Add(t);
                                }
                            }
                            m.SelectCommand.Execute(m, m.Target);
                        }
                        yield return new WaitUntil(() => m.IsAttacked);
                    }
                    Enemy.StartCoroutine(Enemy.EnemyAction());
                    yield return new WaitUntil(() => Enemy.IsAttacked);
                    _phase = Phase.Result;
                    break;
                case Phase.Result:
                    yield return null;
                    if (Players[0].IsDead && Players[1].IsDead && Players[2].IsDead)
                    {
                        _phase = Phase.End;
                    }
                    if(IsCleared == true) 
                    {
                        _phase = Phase.End;
                    }
                    if(_phase != Phase.End)
                    {
                        Enemy.EnemyCommandSet();
                        _phase = Phase.CommandPhase;
                    }
                    break;
                case Phase.End:
                    if (IsCleared)
                    {
                        Enemy.Anim.Play("Dead");
                        yield return new WaitForSeconds(2f);
                        Enemy.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                        _as.Play();
                        yield return new WaitForSeconds(0.3f);
                        _as.mute = true;
                        foreach (var player in SelectablePlayers)
                        {
                            player.Anim.Play("Heal");
                        }
                        Debug.Log("Clear!");
                        yield return new WaitForSeconds(2f);
                    }
                    else
                    {
                        yield return new WaitForSeconds(2f);
                    }
                    
                    FadePanel.enabled = true;
                    FadePanel.DOFade(1f, 0.5f);
                    yield return new WaitForSeconds(0.5f);
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

    public void Sound()
    {
        Instantiate(ButtonSound);
    }
}
