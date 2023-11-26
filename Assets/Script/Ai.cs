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
    // Start is called before the first frame update
    void Start()
    {
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
            int attackWeight = 1;
            // weight each state based on factors (health, opponent health, buffs)
            if (_self.Health <= 50)
            {
                healWeight += 2 + (5 - _self.Health / 10);
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
        for (int i = 0; i < _hand.Length; i++)
        {
            if (_hand[i] == null)
            {
                _hand[i] = GameController.GetNewAction();
            }
        }
    }

    async void PlayCards()
    {
        Debug.Log("Monster Playing");
        //placeholder - just pick random cards
        switch (state)
        {
            case AIStates.Buff:
                while (true)
                {
                    await Task.Delay(1000);
                    UIText.LogText("Your opponent looks to increase their strength");
                Begin:
                    await Task.Delay(3000);

                    Attack.AtkType[] toPlay = new Attack.AtkType[3] { Attack.AtkType.Buff, Attack.AtkType.Defense, Attack.AtkType.Heal };
                    int card = Random.Range(0, _hand.Length);

                    for (int i = 0; i < toPlay.Length; i++)
                        for (int j = 0; j < _hand.Length; j++)
                        {
                            int newcard = (card + j) % _hand.Length;
                            if (_self != null && _hand[newcard] != null && _hand[newcard].type == toPlay[i])
                                if (_self.ActionCost(_hand[newcard].cost))
                                {
                                    UIText.LogText("Opponent Used " + _hand[newcard].name);
                                    if (_hand[(card + j) % _hand.Length].targetSelf)
                                    {
                                        _self.ApplyAction(_hand[newcard]);
                                        if (!_hand[newcard].permanent)
                                            _hand[newcard] = null;

                                    }
                                    else
                                        GameController.GetPlayer().Defend(_hand[newcard]);
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

            case AIStates.Attack:
                while (true)
                {
                    await Task.Delay(1000);
                    UIText.LogText("Your opponent goes on the attack!");
                Begin:
                    await Task.Delay(3000);

                    Attack.AtkType[] toPlay = new Attack.AtkType[1] { Attack.AtkType.Offense };
                    int card = Random.Range(0, _hand.Length);

                    for (int i = 0; i < toPlay.Length; i++)
                        for (int j = 0; j < _hand.Length; j++)
                        {
                            int newcard = (card + j) % _hand.Length;
                            if (_self != null && _hand[newcard] != null && _hand[newcard].type == toPlay[i])
                                if (_self.ActionCost(_hand[newcard].cost))
                                {
                                    UIText.LogText("Opponent Used " + _hand[newcard].name);
                                    if (_hand[(card + j) % _hand.Length].targetSelf)
                                    {
                                        _self.ApplyAction(_hand[newcard]);
                                        if (!_hand[newcard].permanent)
                                            _hand[newcard] = null;

                                    }
                                    else
                                        GameController.GetPlayer().Defend(_hand[newcard]);
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
            case AIStates.Defend:
                while (true)
                {
                    await Task.Delay(1000);
                    UIText.LogText("Your opponent goes on the defensive");
                Begin:
                    await Task.Delay(3000);

                    Attack.AtkType[] toPlay = new Attack.AtkType[3] { Attack.AtkType.Defense,Attack.AtkType.Heal,Attack.AtkType.Buff };
                    int card = Random.Range(0, _hand.Length);

                    for (int i = 0; i < toPlay.Length; i++)
                        for (int j = 0; j < _hand.Length; j++)
                        {
                            int newcard = (card + j) % _hand.Length;
                            if (_self != null && _hand[newcard] != null && _hand[newcard].type == toPlay[i])
                                if (_self.ActionCost(_hand[newcard].cost))
                                {
                                    UIText.LogText("Opponent Used " + _hand[newcard].name);
                                    if (_hand[(card + j) % _hand.Length].targetSelf)
                                    {
                                        _self.ApplyAction(_hand[newcard]);
                                        if (!_hand[newcard].permanent)
                                            _hand[newcard] = null;

                                    }
                                    else
                                        GameController.GetPlayer().Defend(_hand[newcard]);
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
            case AIStates.Heal:
                while (true)
                {
                    await Task.Delay(1000);
                    UIText.LogText("Your opponent licks their wounds!");
                Begin:
                    await Task.Delay(3000);

                    Attack.AtkType[] toPlay = new Attack.AtkType[2] { Attack.AtkType.Heal, Attack.AtkType.Offense };
                    int card = Random.Range(0, _hand.Length);

                    for (int i = 0; i < toPlay.Length; i++)
                        for (int j = 0; j < _hand.Length; j++)
                        {
                            int newcard = (card + j) % _hand.Length;
                            if (_self != null && _hand[newcard] != null && _hand[newcard].type == toPlay[i])
                                if (_self.ActionCost(_hand[newcard].cost))
                                {
                                    UIText.LogText("Opponent Used " + _hand[newcard].name);
                                    if (_hand[(card + j) % _hand.Length].targetSelf)
                                    {
                                        _self.ApplyAction(_hand[newcard]);
                                        if (!_hand[newcard].permanent)
                                            _hand[newcard] = null;

                                    }
                                    else
                                        GameController.GetPlayer().Defend(_hand[newcard]);
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

}
