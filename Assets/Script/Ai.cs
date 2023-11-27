using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ai : MonoBehaviour
{
    enum AIStates {Attack,Defend,Buff,Heal}

    AIStates state;

    Attack[] _hand = new Attack[4];
    Monster _self;

    Vector3 _blobPos;
    Vector3 _enemyPos;
    GameObject _canvas;
    [SerializeField] GameObject _animatedText;

    // Start is called before the first frame update
    void Start()
    {
        _blobPos = GameObject.Find("EnemyBlob").transform.position;
        _enemyPos = GameObject.Find("PlayerBlob").transform.position;
        _canvas = GameObject.Find("Canvas");

        _self = GameController.GetOpponent();
        int i = 0;
        foreach (Attack action in GameController.GetFixedActions())
        {
            if (i >= _hand.Length)
                break;
            _hand[i] = action;
            i++;
        }
        while (i < _hand.Length)
        {
            _hand[i] = GameController.GetNewAction();
            i++;
        }
        GameController.OnNewTurn += AiTurn;
    }

    public void AiTurn() 
    {
        Debug.Log("New Turn");
        if (GameController.main.Turn == _self)
        {
            Debug.Log("My Turn");
            DrawCards();
            // decide on strategy (state)
            int healWeight = 1;
            int buffWeight = 1;
            int defenceWeight = 1;
            int attackWeight = 4;
            // weight each state based on factors (health, opponent health, buffs)
            if (_self.Health <= 50)
            {
                healWeight += 2 + (5 - _self.Health / 10);
            } else if (_self.Health > 150)
            {
                healWeight = 0;
            }
            if (_self.Modifiers < 2)
            {
                buffWeight += 2;
            }
            if (_self.Defence <2)
            {
                defenceWeight += 2;
            }
            if (GameController.GetPlayer().Health < 50)
            {
                attackWeight += 4;
            }

            int rng = (Random.Range(0, healWeight + buffWeight + attackWeight + defenceWeight));
            if (rng < attackWeight)
            {
                state = AIStates.Attack;
                
            }
            else if (rng < attackWeight + healWeight)
            {
                state = AIStates.Heal;

            }
            else if (rng < attackWeight + healWeight + buffWeight)
            {
                state = AIStates.Buff;

            }
            else
            {
                state = AIStates.Defend;

            }

            
            // do Ai stuff
            PlayCards();
        }
    }

    void DrawCards()
    {
        {
            int i = 0;
            foreach (Attack action in GameController.GetFixedActions())
            {
                if (i >= _hand.Length)
                    break;
                _hand[i] = action;
                i++;
            }
        }
        for (int i = 0; i < _hand.Length; i++)
        {
            if (_hand[i] == null)
            {
                _hand[i] = GameController.GetNewAction();
            }
        }
    }

    void PlayCards()
    {
        Attack.AtkType[] toPlay = new Attack.AtkType[0];
        string announce = "";
        switch (state)
        {
            case AIStates.Buff:
                toPlay = new Attack.AtkType[3] { Attack.AtkType.Buff, Attack.AtkType.Defense, Attack.AtkType.Offense };
                announce = "Your opponent looks to increase their strength";
                break;
            case AIStates.Attack:
                toPlay = new Attack.AtkType[1] { Attack.AtkType.Offense };
                announce = "Your opponent goes on the attack!";
                break;
            case AIStates.Defend:
                toPlay = new Attack.AtkType[3] { Attack.AtkType.Defense, Attack.AtkType.Buff, Attack.AtkType.Offense };
                announce = "Your opponent goes on the defensive";
                break;
            case AIStates.Heal:
                toPlay = new Attack.AtkType[2] { Attack.AtkType.Heal, Attack.AtkType.Offense };
                announce = "Your opponent licks their wounds!";
                break;
        }
        SelectCards(toPlay, announce);
    }

    async void SelectCards(Attack.AtkType[] categories, string announce)
    {
        while (true)
        {
            await Task.Delay(1500);
            UIText.LogText(announce);
        Begin:
            await Task.Delay(3000);

            int card = Random.Range(0, _hand.Length);

            for (int i = 0; i < categories.Length; i++)
                for (int j = 0; j < _hand.Length; j++)
                {
                    int newcard = (card + j) % _hand.Length;
                    if (_self != null && _hand[newcard] != null && _hand[newcard].type == categories[i])
                        if (_self.ActionCost(_hand[newcard].cost))
                        {
                            UIText.LogText("Opponent Used " + _hand[newcard].name);
                            if (_hand[(card + j) % _hand.Length].targetSelf)
                            {
                                _self.ApplyAction(_hand[newcard]);
                            }
                            else
                                GameController.GetPlayer().Defend(_hand[newcard]);

                            GameObject gO = Instantiate(_animatedText, _canvas.transform);
                            gO.GetComponent<TextAttack>().AttackAnimation(_blobPos, _enemyPos, _hand[newcard]);

                            //if (!_hand[newcard].permanent)
                            _hand[newcard] = null;
                            goto Begin;
                        }
                        else
                        {
                            GameController.main.EndTurn(_self);
                            return;
                        }
                }
            GameController.main.EndTurn(_self);
            return;
        }
    }
}
