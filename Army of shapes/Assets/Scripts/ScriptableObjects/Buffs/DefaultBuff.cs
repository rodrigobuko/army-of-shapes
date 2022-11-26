using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Army of Shapes/DefaultBuff")]
public class DefaultBuff : ScriptableObject
{
    public float ChangeOnHealth;
    public float ChangeOnAttack;
    public float ChangeOnSpeed;
    public float ChangeOnAttackSpeed;
}
