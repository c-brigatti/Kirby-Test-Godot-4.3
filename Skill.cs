using Godot;
using System;
using CSharp.Battle;

[GlobalClass] public partial class Skill : Ability
{
	public override string ToString()
	{
		return $"Name: {Name}, Damage: {Damage}, MpCost: {MpCost}";
	}
}
