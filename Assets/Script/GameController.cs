using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Turn {Player, AI }

public class GameController : MonoBehaviour
{
    [SerializeField] Monster player;
    [SerializeField] Monster opponent;
    static GameController main;
    [SerializeField] List<Action> permanentActions;
    [SerializeField] List<Action> tempActions;
    public Turn turn { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (main == null)
            main = this;
        else if (main!=this)
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Monster GetPlayer()
    {
        return main.player;
    }

    public static Monster GetOpponent()
    {
        return main.opponent;
    }

    public static List<Action> GetFixedActions()
    {
        if (main.permanentActions.Count > 0)
            return main.permanentActions;
        return null;
    }

    public static Action GetNewAction()
    {
        if (main.tempActions.Count > 0)
            return (main.tempActions[Random.Range(0, main.tempActions.Count)]);
        return null;
    }
}
