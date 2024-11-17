using Godot;
using System;

public partial class Magic : Resource
{
    public string Name;
    public int Damage;
    public int MpCost;

    public Magic(string name, int damage, int mpCost)
    {
        Name = name;
        Damage = damage;
        MpCost = mpCost;
    }
}
