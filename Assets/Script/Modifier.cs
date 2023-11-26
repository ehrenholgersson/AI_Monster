using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(fileName = "New Modifier", menuName = "Monster/Modifier")]
[System.Serializable]
public class Modifier 
{
    public int _hp;
    public int _ap;
    public int _def;
    public int _duration;

    public Modifier(Modifier Original)
    {
        _hp = Original._hp;
        _ap = Original._ap;
        _def = Original._def;
        _duration = Original._duration;
    }
}
