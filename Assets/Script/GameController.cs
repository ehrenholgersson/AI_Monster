using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

//public enum Turn {Player, AI }

public class GameController : MonoBehaviour
{
    [SerializeField] Monster _player;
    [SerializeField] Monster _opponent;
    public static GameController main;
    [SerializeField] List<Attack> _permanentActions;
    [SerializeField] List<Attack> _tempActions;
    bool _playerTurn;
    public Monster Turn { get => _playerTurn?_player : _opponent; }
    public static Action OnNewTurn;



    // Start is called before the first frame update
    void Awake()
    {
        if (main == null)
            main = this;
        else if (main!=this)
            Destroy(this);
    }

    private void Start()
    {
        OnNewTurn += AnnounceTurn;
        DelayedStart();
    }

    async void DelayedStart()
    {
        await Task.Delay(1000);
        OnNewTurn?.Invoke();
    }

    public static Monster GetPlayer()
    {
        return main._player;
    }

    public static Monster GetOpponent()
    {
        return main._opponent;
    }

    public void EndTurn(Monster requestor)
    {
        if (requestor == Turn)
        {
            _playerTurn = !_playerTurn;
            OnNewTurn?.Invoke();
        }
    }

    public static List<Attack> GetFixedActions()
    {
        if (main._permanentActions.Count > 0)
            return main._permanentActions;
        return null;
    }

    public static Attack GetNewAction()
    {
        if (main._tempActions.Count > 0)
            return (main._tempActions[UnityEngine.Random.Range(0, main._tempActions.Count)]);
        return null;
    }

    async void AnnounceTurn()
    {
        UIText.DisplayText("Ready!", 1);
        await Task.Delay(1000);
        UIText.DisplayText(Turn.name + " Turn");
        UIText.LogText("Its " + Turn.name + "s Turn");
        await Task.Delay(1000);
    }
}
