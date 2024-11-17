using Godot;
using System;

public partial class Skill : Resource
{
    public string Name;
    public int Damage;

    public Skill(string name, int damage)
    {
        Name = name;
        Damage = damage;
    }
}
