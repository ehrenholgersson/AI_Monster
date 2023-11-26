using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] Attack _action;
    public void Click()
    {
        if (GameController.GetPlayer().ActionCost(_action.cost))
        {
            if (_action.targetSelf)
                GameController.GetPlayer().ApplyAction(_action);
            else
                GameController.GetOpponent().ApplyAction(_action);
        }
    }

    public void SetAction(Attack action)
    {
        _action = action;
    }
    public void Start()
    {
        if (_action == null)
            _action = GameController.GetNewAction();
        transform.Find("Title").GetComponentInChildren<TextMeshProUGUI>().text = _action.name;
        transform.Find("Description").GetComponentInChildren<TextMeshProUGUI>().text = _action.description;
    }
}
