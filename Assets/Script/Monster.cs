using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] int _health = 100;
    [SerializeField] int _maxHealth { get; } = 100;
    [SerializeField] int _actions  = 5;
    [SerializeField] int _maxActions { get; } = 10;
    List<Modifier> _modifiers = new List<Modifier>();

    public int Health { get { return _health; } }
    public int AP { get { return _actions; } }
    string _name;

    // Start is called before the first frame update
    void Start()
    {
        if (GameController.GetPlayer() == this)
            _name = "player";
        else
            _name = "Opponent";
    }

    public bool ActionCost(int cost)
    {
        if (_actions >= cost)
        {
            _actions -= cost;
            return true;
        }
        return false;
    }

    public void ApplyAction(Action action)
    {
        if (action != null)
        {
            {
                _health += action.health;
                _actions += action.ap;
                foreach (Modifier modifier in action.modifiers)
                {
                    _modifiers.Add(modifier);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
