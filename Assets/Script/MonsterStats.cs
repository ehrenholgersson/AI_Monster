using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterStats : MonoBehaviour
{
    Monster _monster;
    string _name;

    [SerializeField] TextMeshProUGUI _hpDisplay;
    [SerializeField] TextMeshProUGUI _apDisplay;
    [SerializeField] TextMeshProUGUI _defDisplay;

    private void Start()
    {
        _monster = GetComponent<Monster>();


    }
    private void FixedUpdate()
    {
        if (_hpDisplay != null)
        {
            _hpDisplay.text = _monster.Health.ToString();
        }
        if (_apDisplay != null) 
        {
            _apDisplay.text = _monster.AP.ToString();
        }
        if (_defDisplay != null)
        {
            _defDisplay.text = (_monster.Defence*10).ToString() + "%";
        }
    }
}
