using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField] Color PermCol;
    [SerializeField] Color HealCol;
    [SerializeField] Color BuffCol;
    [SerializeField] Color DefCol;
    [SerializeField] Color AtkCol;

    [SerializeField] Button _button;
    [SerializeField] Attack _action;
    public void Click()
    {
        Monster player = GameController.GetPlayer();
        if (GameController.main.Turn == player &&  player.ActionCost(_action.cost))
        {
            UIText.LogText("Player used " + _action.name);
            if (_action.targetSelf)
                player.ApplyAction(_action);
            else
                GameController.GetOpponent().Defend(_action);

            if (!_action.permanent)
                Destroy(this.gameObject);
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

        if (_action != null)
        {
            Image background = GetComponent<Image>();
            if (_action.permanent)
                background.color = PermCol;
            else
            {
                switch (_action.type)
                {
                    case Attack.AtkType.Offense:
                        background.color = AtkCol;
                        break;
                    case Attack.AtkType.Defense:
                        background.color = DefCol;
                        break;
                    case Attack.AtkType.Buff:
                        background.color = BuffCol;
                        break;
                    case Attack.AtkType.Heal:
                        background.color = HealCol;
                        break;
                }
            }
        }
    }
}
