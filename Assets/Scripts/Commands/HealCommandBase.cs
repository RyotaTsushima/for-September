using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealCommandBase : CommandBase
{
    [SerializeField] int HealPoint;
    [SerializeField] GameObject _healEffect;

    public override void Execute(BattleUnit user,List<BattleUnit> targets)
    {
        foreach (BattleUnit target in targets)
        {
            target.Health += HealPoint;
            if (target.Health >= target.MaxHealth)
            {
                target.Health = target.MaxHealth;
            }
            target.HealthText.text = $"{target.Health} / {target.MaxHealth}";
            Instantiate(_healEffect,target.transform.position,target.transform.rotation);
            Debug.Log($"{user.name}‚ª{target.name}‚ğ‰ñ•œBc‚èHP {target.Health}");
        }
    }
}
