using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealCommandBase : CommandBase
{
    [SerializeField] int HealPoint;

    public override void Execute(BattleUnit user,BattleUnit target)
    {
        target.Health += 5;
        if (target.Health < target.MaxHealth)
        {
            target.Health = target.MaxHealth;
        }
        Debug.Log($"{name}‚ª{target.name}‚ð‰ñ•œBŽc‚èHP {target.Health}");
    }
}
