using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="New Attack", menuName ="Monster/Attack")]
public class Attack : ScriptableObject
{
    public enum AtkType {Offense, Defense, Buff, Heal }

    public bool permanent;
    public bool targetSelf;
    public int cost;
    public int health;
    public int ap;
    public int defence;
    public List<Modifier> modifiers;
    public string description;
    public AtkType type;
}
