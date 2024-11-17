using Godot;
using System;

public partial class PlayerButton : Button
{
	public BattleActor PlayerData;

	public override void _Ready()
	{
		PlayerData = GetNode<Data>("/root/Data").PartyMembers[GetIndex()];
		GD.Print("PlayerData: " + PlayerData.Name);
	}
}
