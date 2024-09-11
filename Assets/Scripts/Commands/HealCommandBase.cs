using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealCommandBase : CommandBase
{
    [SerializeField] int HealPoint;

    public override void Execute(BattleUnit user,List<BattleUnit> targets)
    {
        foreach (BattleUnit target in targets)
        {

            target.Health += HealPoint;
            if (target.Health < target.MaxHealth)
            {
                target.Health = target.MaxHealth;
            }
            Debug.Log($"{name}が{target.name}を回復。残りHP {target.Health}");
        }
    }
}
