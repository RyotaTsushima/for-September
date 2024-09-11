using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CommandBase : ScriptableObject
{

    public virtual void Execute(BattleUnit user, List<BattleUnit> target)
    {

    }
}
