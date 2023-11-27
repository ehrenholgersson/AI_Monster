using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

//public enum Turn {Player, AI }

public class GameController : MonoBehaviour
{
    [SerializeField] Monster _player;
    [SerializeField] Monster _opponent;
    public static GameController main;
    [SerializeField] List<Attack> _permanentActions;
    [SerializeField] List<Attack> _tempActions;
    [SerializeField] GameObject _menu;
    bool _playerTurn;
    bool _gameOver;
    public Monster Turn { get => _playerTurn?_player : _opponent; }
    public static Action OnNewTurn;



    // Start is called before the first frame update
    void Awake()
    {
        _gameOver = false;
        if (main == null)
            main = this;
        else if (main!=this)
            Destroy(this);

        if (UnityEngine.Random.Range(0, 2) > 0) 
        {
            _playerTurn = true;
        }
    }

    private void Start()
    {
        OnNewTurn += AnnounceTurn;
        DelayedStart();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
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
        if (_player.Health <= 0)
            GameOver("Opponent");
        else if (_opponent.Health <= 0)
            GameOver("Player");

        if (requestor == Turn && !_gameOver)
        {
            _playerTurn = !_playerTurn;
            OnNewTurn?.Invoke();
        }
    }

    async void GameOver(string winner)
    {
        _gameOver = true;
        await Task.Delay(2000);
        UIText.DisplayText("Game Over",2);
        await Task.Delay(2000);
        UIText.DisplayText(winner +" Wins!", 2);
        await Task.Delay(2000);
        ToggleMenu();
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

    public void ToggleMenu()
    {
        
        if (_gameOver)
        {
            _menu.SetActive(true);
            _menu.transform.Find("Continue").gameObject.SetActive(false);
        }
        else
            _menu.SetActive(!_menu.activeSelf);
    }
}
