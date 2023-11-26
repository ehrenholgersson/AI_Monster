using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    GameObject[] _hand =   new GameObject[4];
    [SerializeField] GameObject _cardPrefab;
    // Start is called before the first frame update
    void Start()
    {
        {
            int i = 0;
            foreach (Attack action in GameController.GetFixedActions())
            {
                if (i >= _hand.Length)
                    break;
                _hand[i] = Instantiate(_cardPrefab);
                _hand[i].transform.SetParent(transform, false);
                _hand[i].GetComponent<ActionButton>().SetAction(action);
                RectTransform rect = _hand[i].GetComponent<RectTransform>();
                rect.localPosition = new Vector3(rect.localPosition.x + (Mathf.Abs(rect.rect.width) * i), rect.localPosition.y, rect.localPosition.z);
                //_hand[i].transform.position = new Vector3(transform.position.x/* + (i * 781.4f)*/,transform.position.y,transform.position.z);
                i++;
            }
            
        }
        for (int i = 0; i < _hand.Length; i++) 
        {
            if (_hand[i] == null)
            {
                _hand[i] = Instantiate(_cardPrefab);
                _hand[i].transform.SetParent(transform, false);
                RectTransform rect = _hand[i].GetComponent<RectTransform>();
                rect.localPosition = new Vector3(rect.localPosition.x + (Mathf.Abs(rect.rect.width) * i), rect.localPosition.y, rect.localPosition.z);
            }
        }
    }
}
