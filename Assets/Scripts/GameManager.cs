using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] BattleUnit[] Mouses;
    [SerializeField] BattleUnit[] Enemys;
    enum Phase
    {
        StartPhase,
        CommandPhase,
        ExecutePhase,
        Result,
        End,
    }
    Phase _phase;

    void Start()
    {
        _phase = Phase.StartPhase;
        StartCoroutine(Battle());
    }

    // Update is called once per frame
    void Update()
    {
        
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
                    _phase = Phase.CommandPhase;
                    break;
                case Phase.CommandPhase:
                    yield return new WaitUntil(() =>Input.GetKeyDown(KeyCode.Space));
                    _phase = Phase.ExecutePhase;
                    break;
                case Phase.ExecutePhase:
                    foreach(var m in Mouses)
                    {
                        m.SelectCommand.Execute(m,m.Target);
                    }
                    foreach(var enemy in Enemys)
                    {
                        enemy.SelectCommand.Execute(enemy,enemy.Target);
                    }
                    _phase = Phase.Result;
                    break;
                case Phase.Result:
                    _phase = Phase.End;
                    break;
                case Phase.End:
                    break;
            }
        }
    }
}
