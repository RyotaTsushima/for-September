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
            if(target.Health < 0)
            {
                target.Health = 0;
            }
            target.HealthText.text = $"{target.Health} / {target.MaxHealth}";
            target.Anim.Play("Damaged");
            Debug.Log($"{user.name}‚ª{target.name}‚ÉUŒ‚BŽc‚èHP{target.Health}");
        }
    }
}
