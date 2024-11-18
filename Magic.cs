using Godot;
using System;
using CSharp.Battle;

[GlobalClass] public partial class Magic : Ability
{
    public Magic() { }
    
    public Magic(string name = "default", int damage = 10, int mpCost = 10)
    {
        Name = name;
        Damage = damage;
        MpCost = mpCost;
    }
    
    public override string ToString()
    {
        return $"Name: {Name}, Damage: {Damage} MpCost: {MpCost}";
    }
}
