using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private void Start()
    {
        if (main == null)
        {
            main = this;
            //text = GetComponent<TextMeshProUGUI>();
        }
        else Destroy(this);
        _alpha = 0;
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (_alpha > 0)
        {
            _alpha -= 0.2f * Time.deltaTime;
            text.color = new Color(text.color.r, text.color.g, text.color.b, _alpha);
        }
    }

    void UpdateText(string newText)
    {
        text.text = newText;
        _alpha = 1;
        text.color = new Color(text.color.r, text.color.g, text.color.b, _alpha);
    }

    public static void DisplayText(string newText)
    {
        main.UpdateText(newText);
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
