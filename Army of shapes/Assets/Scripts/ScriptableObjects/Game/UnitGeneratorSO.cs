using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Army of Shapes/UnitGenerator")]
public class UnitGeneratorSO : ScriptableObject
{
    [Header("Buffs")]
    [SerializeField] private DefaultBuff _defaultBuff;
    [SerializeField] private List<ShapeBuff> _shapeBuffs;
    [SerializeField] private List<ColorBuff> _colorBuffs;
    [SerializeField] private List<SizeBuff> _sizeBuffs;

    public Unit GenerateUnit()
    {
        Unit newUnit = new Unit(0, 0, 0, 0);
        AddDefaultBuffToUnit(newUnit, _defaultBuff);
        
        System.Random random = new System.Random((int)System.DateTime.Now.Ticks);
        AddShapeBuff(newUnit, random);
        AddColorBuff(newUnit, random);
        AddSizeBuff(newUnit, random);
        
        return newUnit;
    }

    private void AddShapeBuff(Unit unit, System.Random random)
    {
        int index = random.Next(_shapeBuffs.Count);
        ShapeBuff shapeBuff = _shapeBuffs[index];
        AddDefaultBuffToUnit(unit, shapeBuff);
        
        unit.ChangeShape(shapeBuff.Shape);
        unit.ChangeAttack(shapeBuff.TypeOfAttack);
    }
    
    private void AddColorBuff(Unit unit, System.Random random)
    {
        int index = random.Next(_colorBuffs.Count);
        ColorBuff colorBuff = _colorBuffs[index];
        AddDefaultBuffToUnit(unit, colorBuff);
        
        unit.ChangeColor(colorBuff.Color);
       
    }
    
    private void AddSizeBuff(Unit unit, System.Random random)
    {
        int index = random.Next(_sizeBuffs.Count);
        SizeBuff sizeBuff = _sizeBuffs[index];
        AddDefaultBuffToUnit(unit, sizeBuff);
        
        unit.ChangeSize(sizeBuff.Scale);
    }

    private void AddDefaultBuffToUnit(Unit unit, DefaultBuff buff)
    {
        unit.AddBuff(buff.ChangeOnHealth,
            buff.ChangeOnAttack,
            buff.ChangeOnSpeed,
            buff.ChangeOnAttackSpeed);
    }
}
