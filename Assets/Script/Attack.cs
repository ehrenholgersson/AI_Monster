using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="New Attack", menuName ="Monster/Attack")]
public class Attack : ScriptableObject
{

    public bool permanent;
    public bool targetSelf;
    public int cost;
    public int health;
    public int ap;
    public List<Modifier> modifiers;
    public string description;
}
