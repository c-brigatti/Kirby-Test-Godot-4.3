using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using CSharp;
using CSharp.Stats;
using Godot.Collections;

[GlobalClass] [Tool] public partial class BattleActor : Resource
{
    [Signal] public delegate void HpChangedEventHandler(int hp, int change);
    [Signal] public delegate void MpChangedEventHandler(int mp, int change);

    [Export] public String Name { get; private set; } = "default";

    private BaseStats _baseStats = new BaseStats(); // private backing field
    [Export] public BaseStats BaseStats
    {
        get => _baseStats;
        set
        {
            _baseStats = value;
            CalculateStats();
        }
    }
    
    private int _level = 1; // private backing field
    [Export] public int Level
    {
        get => _level;
        set
        {
            _level = value;
            CalculateStats();
        }
    }
    
    public int MaxHp { get; private set; }
    public int CurrentHp { get; private set; }
    
    public int MaxMp { get; private set; }
    public int CurrentMp { get; private set; }
    
    public int STR { get; private set; }
    public int MAG { get; private set; }
    public int VIT { get; private set; }
    public int AGI { get; private set; }
    public int LUK { get; private set; }
    public int EXP { get; private set; }
    public int MON { get; private set; }
    
    [Export] public Array<Skill> Skills { get; private set; } = new Array<Skill>();
    [Export] public Array<Magic> Magic { get; private set; } = new Array<Magic>();

    public BattleActor()
    {
        CalculateStats();
        
        CurrentHp = MaxHp;
        CurrentMp = MaxMp;
    }

    public void CalculateStats()
    {
        MaxHp = BaseStats.Scale(BaseStats.BaseHP, BaseStats.RateHP, BaseStats.FlatHP, Level);
        MaxMp = BaseStats.Scale(BaseStats.BaseMP, BaseStats.RateMP, BaseStats.FlatMP, Level);
        STR = BaseStats.Scale(BaseStats.BaseSTR, BaseStats.RateSTR, BaseStats.FlatSTR, Level);
        MAG = BaseStats.Scale(BaseStats.BaseMAG, BaseStats.RateMAG, BaseStats.FlatMAG, Level);
        VIT = BaseStats.Scale(BaseStats.BaseVIT, BaseStats.RateVIT, BaseStats.FlatVIT, Level);
        AGI = BaseStats.Scale(BaseStats.BaseAGI, BaseStats.RateAGI, BaseStats.FlatAGI, Level);
        LUK = BaseStats.Scale(BaseStats.BaseLUK, BaseStats.RateLUK, BaseStats.FlatLUK, Level);
        EXP = BaseStats.Scale(BaseStats.BaseEXP, BaseStats.RateEXP, BaseStats.FlatEXP, Level);
        MON = BaseStats.Scale(BaseStats.BaseMON, BaseStats.RateMON, BaseStats.FlatMON, Level);
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
    
    /* NOTE: === FUNCTIONS FOR EDITOR PREVIEW STATS === */
    
    // Customize how properties are displayed in the editor
    public override Array<Dictionary> _GetPropertyList()
    {
        var properties = new Array<Dictionary>();

        // This will show these properties as read-only in the editor
        void AddReadOnlyProperty(string name, Variant.Type type)
        {
            var property = new Dictionary
            {
                { "name", name },
                { "type", (int)type },
                { "usage", (int)PropertyUsageFlags.ReadOnly | (int)PropertyUsageFlags.Editor },
            };
            properties.Add(property);
        }

        // Add read-only properties for stats
        AddReadOnlyProperty("Stats/Max_HP", Variant.Type.Int);
        AddReadOnlyProperty("Stats/Max_MP", Variant.Type.Int);
        AddReadOnlyProperty("Stats/STR", Variant.Type.Int);
        AddReadOnlyProperty("Stats/MAG", Variant.Type.Int);
        AddReadOnlyProperty("Stats/VIT", Variant.Type.Int);
        AddReadOnlyProperty("Stats/AGI", Variant.Type.Int);
        AddReadOnlyProperty("Stats/LUK", Variant.Type.Int);
        AddReadOnlyProperty("Stats/EXP", Variant.Type.Int);
        AddReadOnlyProperty("Stats/MON", Variant.Type.Int);

        return properties;
    }

    // Ensure dynamic updates in the editor
    public override Variant _Get(StringName property)
    {
        return property.ToString() switch
        {
            "Stats/Max_HP" => MaxHp,
            "Stats/Max_MP" => MaxMp,
            "Stats/STR" => STR,
            "Stats/MAG" => MAG,
            "Stats/VIT" => VIT,
            "Stats/AGI" => AGI,
            "Stats/LUK" => LUK,
            "Stats/EXP" => EXP,
            "Stats/MON" => MON,
            _ => base._Get(property)
        };
    }
}
