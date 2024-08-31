using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] public int MaxHealth;
    public int Health;

    [SerializeField] CommandBase[] Commands;
    public BattleUnit Target = default;
    public CommandBase SelectCommand;

    private void Start()
    {
        Health = MaxHealth;
    }

    
}
