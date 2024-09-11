using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AttackCommandBase : CommandBase
{
    [SerializeField] int AttackPoint;
    [SerializeField] bool _isPlayable;

    public override void Execute(BattleUnit user, List<BattleUnit> targets)
    {
        foreach (BattleUnit target in targets)
        {
            if (_isPlayable)
            {
                target.Health -= AttackPoint * GameManager.AttackRatio;
            }
            else
            {
                target.Health -= AttackPoint;
            }
            Debug.Log($"{user.name}Ç™{target.name}Ç…çUåÇÅBécÇËHP{target.Health}");
        }
    }
}
