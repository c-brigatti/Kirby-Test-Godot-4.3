using Godot;
using System;

public partial class PlayerInfoButton : Button
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BattleActor playerData = GetNode<Data>("/root/Data").PartyMembers[GetIndex()];
		PlayerInfoBar playerBar = GetNode<PlayerInfoBar>("PlayerInfoBar");
		
		playerBar.SetPlayerData(playerData);
	}
}
