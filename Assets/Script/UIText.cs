using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    float _alpha;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI logText;
    static UIText main;
    static List<string> gameLog = new List<string>();
    int logLength = 20;
    float _defaultSpeed = 5;
    float _fadeSpeed;

    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            Debug.Log("This text hadler is a duplicate of " + main.gameObject.name);
            Destroy(this);
        }
        //_fadeSpeed = _defaultSpeed;
        _alpha = 0;
    }
    void Start()
    {

        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (_alpha > 0)
        {
            _alpha -= Time.deltaTime / _fadeSpeed;
            text.color = new Color(text.color.r, text.color.g, text.color.b, _alpha);
        }
    }

    void UpdateText(string newText)
    {
        _fadeSpeed = _defaultSpeed;
        text.text = newText;
        _alpha = 1;
        text.color = new Color(text.color.r, text.color.g, text.color.b, _alpha);
    }

    void UpdateText(string newText, float speed)
    {
        _fadeSpeed = speed;
        text.text = newText;
        _alpha = 1;
        text.color = new Color(text.color.r, text.color.g, text.color.b, _alpha);
    }

    public static void DisplayText(string newText)
    {
        main.UpdateText(newText);
    }
    public static void DisplayText(string newText, float speed)
    {
        if (main != null)
            main.UpdateText(newText, speed);
        else
            Debug.Log("UI Text Handler is broken :(");
    }
    public static void LogText(string text)
    {
        gameLog.Add(text);
        if (gameLog.Count > main.logLength)
            gameLog.RemoveAt(0);
        if (main.logText!=null)
        {
            string output = "";
            for(int i = gameLog.Count-1;i >=0;i--)
            {
                output += (gameLog[i] + "<br>");
            }
            main.logText.text = output;
        }
    }
}
