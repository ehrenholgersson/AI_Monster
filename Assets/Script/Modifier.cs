using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Modifier", menuName = "Monster/Modifier")]
public class Modifier : ScriptableObject
{
    int _hp;
    int _ap;
    int _def;
    int _duration;
}
