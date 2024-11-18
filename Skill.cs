using Godot;
using System;
using CSharp.Battle;

[GlobalClass] public partial class Skill : Ability
{
	public Skill() { }
	
	public Skill(string name = "default", int damage = 10, int mpCost = 10)
	{
		Name = name;
		Damage = damage;
		MpCost = mpCost;
	}
	
	public override string ToString()
	{
		return $"Name: {Name}, Damage: {Damage}, MpCost: {MpCost}";
	}
}
