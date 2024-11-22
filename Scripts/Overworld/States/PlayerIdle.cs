using Godot;
using System;

public partial class PlayerIdle : State
{
	[Export] public CharacterBody2D Player;
	
	private Vector2 _moveDirection;

	public new void Enter()
	{
		
	}
}
