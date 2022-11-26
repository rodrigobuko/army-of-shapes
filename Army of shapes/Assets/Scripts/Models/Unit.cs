using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeOfAttack
{
    AttackClosest,
    AttackLessHealth
    
}

public enum TypeOfShape
{
    Cube,
    Sphere
    
}
public class Unit
{
    private readonly string _id;
    private float _health;
    private float _attack;
    private float _speed;
    private float _attackSpeed;
    
    // Special Attributes
    private TypeOfAttack _typeOfAttack;
    private TypeOfShape _shape;
    private Color _color;
    private float _sizeModifier;

    public Unit(float health, float attack, float speed, float attackSpeed)
    {
        _id = System.Guid.NewGuid().ToString();
        _health = health;
        _attack = attack;
        _speed = speed;
        _attackSpeed = attackSpeed;
        
        // default values
        _typeOfAttack = TypeOfAttack.AttackClosest;
        _shape = TypeOfShape.Cube;
        _sizeModifier = 1.0f;
        _color = Color.gray;
    }

    public string Id => _id;
    public float Health => _health;
    public float Attack => _attack;
    public float Speed => _speed;
    public float AttackSpeed => _attackSpeed;
    
    // Special Attributes
    public TypeOfAttack TypeOfAttack => _typeOfAttack;
    public TypeOfShape Shape => _shape;
    public Color Color =>  _color;
    public float Size => _sizeModifier;

    public void AddBuff(float health, float attack, float speed, float attackSpeed)
    {
        _health += health;
        _attack += attack;
        _speed += speed;
        _attackSpeed += attackSpeed;
    }

    public void ChangeAttack(TypeOfAttack typeOfAttack)
    {
        _typeOfAttack = typeOfAttack;
    }
    
    public void ChangeShape(TypeOfShape shape)
    {
        _shape = shape;
    }
    
    public void ChangeColor(Color color)
    {
        _color = color;
    }
    
    public void ChangeSize(float size)
    {
        _sizeModifier = size;
    }
    
    
}
