using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="New action", menuName ="Monster/Action")]
public class Action : ScriptableObject
{

    public bool permanent;
    public bool targetSelf;
    public int cost;
    public int health;
    public int ap;
    public List<Modifier> modifiers;
    public string description;
}
