using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TextAttack : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Color _healCol;
    [SerializeField] Color _buffCol;
    [SerializeField] Color _defCol;
    [SerializeField] Color _atkCol;

    public async void AttackAnimation(Vector3 startPos, Vector3 enemyPos, Attack attack)
    {
        transform.position = startPos;
        transform.localScale = Vector3.zero;
        _text.text = attack.name;

        switch (attack.type)
        {
            case Attack.AtkType.Offense:
                _text.color = _atkCol;
                break;
            case Attack.AtkType.Defense: 
                _text.color = _defCol;
                break;
            case Attack.AtkType.Buff:
                _text.color = _buffCol;
                break;
            case Attack.AtkType.Heal:
                _text.color = _healCol; 
                break;
        }

        for (float timer = 0; timer < 1; timer += 0.016f) 
        {
            transform.position = Vector3.Lerp(startPos, startPos + new Vector3(0, 200, 0), timer);
            transform.localScale = Vector3.Lerp(Vector3.zero,Vector3.one, timer);
            await Task.Delay(16); // should give 60fps
        }
        if (attack.targetSelf)
        {
            startPos = transform.position;
            float alpha = 1.0f;
            for (float timer = 0; timer < 1; timer += 0.016f) 
            {
                transform.position = Vector3.Lerp(startPos, startPos + new Vector3(0, 200, 0), timer);
                transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one*2, timer);
                alpha = Mathf.Lerp(1f, 0f, timer);
                _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, alpha);
                await Task.Delay(16); // should give 60fps
            }
        }
        else
        {
            startPos = transform.position;
            for (float timer = 0; timer < 1; timer += 0.016f)
            {
                transform.position = Vector3.Lerp(startPos, enemyPos, timer);
                await Task.Delay(16); // should give 60fps
            }
        }
        Destroy(gameObject);
    }
}
