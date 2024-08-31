using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CommandBase : ScriptableObject
{
    public string Name;

    public virtual void Execute(BattleUnit user, BattleUnit target)
    {

    }
}
