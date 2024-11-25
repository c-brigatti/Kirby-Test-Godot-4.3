using Godot;
using System;
using System.Xml.Schema;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public string CurrentDirection = "none";
	
	private StateMachine _stateMachine;

	public override void _Ready()
	{
		_stateMachine = GetNode<StateMachine>("State Machine");
		//Animation.Play("idle_down");
	}

	public override void _Process(double delta)
	{
		//_stateMachine.Process(delta);
	}

	public override void _PhysicsProcess(double delta)
	{
		//_stateMachine?.PhysicsProcess(delta);
	}
}
