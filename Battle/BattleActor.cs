using Godot;
using System;
using System.Collections.Generic;
using CSharp;
using Godot.Collections;

[GlobalClass] public partial class BattleActor : Resource
{
    [Signal] public delegate void HpChangedEventHandler(int hp, int change);
    
    [Signal] public delegate void MpChangedEventHandler(int mp, int change);
    
    [Export] public String Name { get; private set; }

    [Export] public int MaxHp { get; private set; } = 100;
    public int CurrentHp { get; private set; }

    [Export] public int MaxMp { get; private set; } = 100;
    public int CurrentMp { get; private set; }

    [Export] public int Level { get; private set; } = 5;
    
    public Array<Skill> Skills { get; private set; }
    public Array<Magic> Magic { get; private set; }

    public BattleActor()
    {
        // assign Data node here
        Skills = new Array<Skill>();
        Magic = new Array<Magic>();
        
        CurrentHp = MaxHp;
        CurrentMp = MaxMp;
    }
    
    public void ChangeHp(int value)
    {
        int startHp = CurrentHp;
        CurrentHp += value;
        CurrentHp = Mathf.Clamp(CurrentHp, 0, MaxHp);
        int change = CurrentHp - startHp;
        GD.Print("BattleActor Start HP: " + startHp + " - Change to: " + CurrentHp);
        EmitSignal("HpChanged", CurrentHp, change);
    }

    public void ChangeMp(int value)
    {
        int startMp = CurrentMp;
        CurrentMp += value;
        CurrentMp = Mathf.Clamp(CurrentMp, 0, MaxMp);
        int change = CurrentMp - startMp;
        EmitSignal("MpChanged", CurrentMp, change);
    }

    public new void SetName(string name)
    {
        Name = name;
    }
}
