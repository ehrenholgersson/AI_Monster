using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    int count = 0;
    public void TestText()
    {
        count++;
        UIText.LogText("This is a test!!"+count);
    }
}
