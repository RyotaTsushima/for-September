using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackCommandBase : CommandBase
{
    [SerializeField] int AttackPoint;

    public override void Execute(BattleUnit user, BattleUnit target)
    {
        target.Health -= 3;
        Debug.Log($"{user.name}‚ª{target.name}‚ÉUŒ‚Bc‚èHP{target.Health}");
    }
}
