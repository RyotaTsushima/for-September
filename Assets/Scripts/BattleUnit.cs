using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] public int MaxHealth;
    [HideInInspector] public int Health;
    [SerializeField] float AnimTime;
    [SerializeField] public CommandBase[] Commands;
    [SerializeField] public Text CommandText;
    [SerializeField] public float AnimTimeDelta;
    [SerializeField] GameObject AttackEffect;
    [SerializeField] public Text HealthText;
    [SerializeField] public GameObject HealEffect;
    [HideInInspector] public List<BattleUnit> Target = new List<BattleUnit>();
    [HideInInspector] public CommandBase SelectCommand;
    [HideInInspector] public bool IsDead;
    [HideInInspector] GameManager _gameManager;
    [HideInInspector] public Animator Anim;
    [HideInInspector] public bool IsAttacked;

    private void Start()
    {
        Health = MaxHealth;
        HealthText.text = $"{MaxHealth} / {MaxHealth}";
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Anim = GetComponent<Animator>();
        IsDead = false;
    }

    private void Update()
    {
        if (gameObject.tag == "Player")
        {
            Anim.SetInteger("Health", Health);
        }
        if(Health <= 0 && IsDead == false)
        {
            IsDead = true;
            Debug.Log($"{name} died");
            if (this.gameObject.tag == "player")
            {
                _gameManager.NumberOfDead += 1;
                switch (_gameManager.NumberOfDead)
                {
                    case 0:
                        GameManager.AttackRatio = 1;
                        break;
                    case 1:
                        GameManager.AttackRatio = 5;
                        break;
                    case 2:
                        GameManager.AttackRatio = 25;
                        break;
                    case 3:
                        _gameManager.IsGameOver = true;
                        break;
                }
                _gameManager.SelectablePlayers.Remove(this);
            }
            else if(this.gameObject.tag == "Enemy")
            {
                Debug.Log("Clear!");
                _gameManager.IsGameOver = true;
                Anim.Play("Dead");
            }
        }

        if(IsDead && Health > 0)
        {
            IsDead = false;
            _gameManager.NumberOfDead -= 1;
            switch (_gameManager.NumberOfDead)
            {
                case 0:
                    GameManager.AttackRatio = 1;
                    break;
                case 1:
                    GameManager.AttackRatio = 5;
                    break;
                case 2:
                    GameManager.AttackRatio = 25;
                    break;
                case 3:
                    _gameManager.IsGameOver = true;
                    break;
            }
            Anim.Play("Return");
            _gameManager.SelectablePlayers.Add(this);
        }
    }

    public void EnemyCommandSet()
    {
        int rd = Random.Range(0, 2);
        if(rd == 0)
        {
            SelectCommand = Commands[0];
            int rd2 = Random.Range(0, _gameManager.SelectablePlayers.Count);
            Target.Add(_gameManager.SelectablePlayers[rd2]);
        }
        if(rd == 1)
        {
            SelectCommand = Commands[1];
            foreach(BattleUnit unit in _gameManager.SelectablePlayers)
            {
                Target.Add(unit);
            }
        }
    }

    public void Provacated()
    {
        if (SelectCommand == Commands[0])
        {
            int rnd = Random.Range(1, _gameManager.Provocations.Count);
            Target.Clear();
            Target.Add(_gameManager.Provocations[rnd - 1]);
        }
    }

    public IEnumerator PlayerAction()
    {
        if (!IsDead)
        {
            if (SelectCommand == Commands[0])
            {
                float time = 0.5f;
                Vector3 pos = transform.position;
                Anim.Play("Run");
                transform.DOMove(new Vector3(-0.7f, 0.4f, 0f), time);
                yield return new WaitForSeconds(time);
                Anim.Play("Attack");
                SelectCommand.Execute(this, Target);
                yield return new WaitForSeconds(AnimTimeDelta);
                Instantiate(AttackEffect);
                yield return new WaitForSeconds(AnimTime - AnimTimeDelta);
                transform.DOMove(pos, time);
                yield return new WaitForSeconds(time);
                transform.localScale = new Vector3(-4, 4, 4);
                Anim.Play("Idle");
            }
            else if (SelectCommand == Commands[1])
            {
                yield return new WaitForSeconds(0.5f);
                Anim.Play("Heal");
                yield return null;
                SelectCommand.Execute(this, Target);
                yield return new WaitForSeconds(2.1f);
            }
        }
        IsAttacked = true;
    }

    public IEnumerator EnemyAction()
    {
        yield return new WaitForSeconds(0.3f);
        SelectCommand.Execute(this, Target);
        foreach(var target in Target)
        {
            if (SelectCommand == Commands[0])
            {
                Instantiate(AttackEffect,target.transform.position, target.transform.rotation);
            }
            else if (SelectCommand == Commands[1])
            {
                Instantiate(HealEffect, target.transform.position, target.transform.rotation);
            }
        }
        yield return new WaitForSeconds(0.3f);
        IsAttacked = true;
    }
}
