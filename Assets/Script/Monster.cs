using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] int _health = 100;
    [SerializeField] int _maxHealth { get; } = 100;
    [SerializeField] int _actions  = 5;
    [SerializeField] int _maxActions { get; } = 10;
    [SerializeField] int _ApRechargeRate;
    List<Modifier> _modifiers = new List<Modifier>();

    string _name;
    int _defence = 0;

    public int Health { get { return _health; } }
    public int AP { get { return _actions; } }
    public int Modifiers { get => _modifiers.Count; }
    public int Defence { get => _defence; }
    

    // Start is called before the first frame update
    void Start()
    {
        if (GameController.GetPlayer() == this)
            _name = "Player";
        else
            _name = "Opponent";
        GameController.OnNewTurn += NewTurn;
    }

    private void OnDestroy()
    {
        GameController.OnNewTurn -= NewTurn;
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

    void NewTurn()
    {
        if (GameController.main.Turn == this)
        {
            _defence = 0;
            _actions += _ApRechargeRate;
            foreach (Modifier modifier in _modifiers.ToList())
            {
                _health += modifier._hp;
                _actions += modifier._ap;
                _defence += modifier._def;
                modifier._duration--;
                if (modifier._duration <= 0)
                    _modifiers.Remove(modifier);
            }
        }
    }

    public void Defend(Attack action)
    {
        if (_defence <= Random.Range(0, 10))
            ApplyAction(action);
        else
        {
            UIText.DisplayText("Blocked");
            UIText.LogText(_name +" Blocked an Attack");
        }
    }

    public void ApplyAction(Attack action)
    {
        if (action != null)
        {
            {
                _health += action.health;
                _actions += action.ap;
                _defence += action.defence;
                foreach (Modifier modifier in action.modifiers)
                {
                    _modifiers.Add(new Modifier(modifier));
                }
            }
        }
        if (_health <= 0)
        {
            GameController.main.EndTurn(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
