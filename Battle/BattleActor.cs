using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

public partial class BattleActor : Resource
{
    private Data _dataReference;

    [Signal]
    public delegate void HpChangedEventHandler(int hp, int change);
    
    public String Name { get; private set; }
    //public Dictionary

    public int MaxHp { get; private set; } = 100;
    public int CurrentHp { get; private set; } = 50;

    public int MaxMp { get; private set; } = 100;
    public int CurrentMp { get; private set; } = 70;

    public int Level { get; private set; } = 5;
    
    public Array<Skill> Skills { get; private set; }
    public Array<Magic> Magic { get; private set; }

    public BattleActor(Data dataReference)
    {
        _dataReference = dataReference;
        Skills = new Array<Skill>();
        Magic = new Array<Magic>();
        
        List<string> skillList = new List<string>(_dataReference.Skills.Keys);
        List<string> magicList = new List<string>(_dataReference.Magic.Keys);
        Skills.Add(_dataReference.Skills[skillList[GD.RandRange(0,3)]]);
        Skills.Add(_dataReference.Skills[skillList[GD.RandRange(0,3)]]);
        
        Magic.Add(_dataReference.Magic[magicList[GD.RandRange(0,3)]]);
        Magic.Add(_dataReference.Magic[magicList[GD.RandRange(0,3)]]);
    }

    public void ChangeHp(int value)
    {
        int startHp = CurrentHp;
        CurrentHp += value;
        CurrentHp = Mathf.Clamp(CurrentHp, 0, MaxHp);
        int change = CurrentHp - startHp;
        EmitSignal("HpChanged", CurrentHp, change);
    }

    public new void SetName(string name)
    {
        Name = name;
    }
}
