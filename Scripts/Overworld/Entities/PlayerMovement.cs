using Godot;
using System;

public partial class PlayerMovement : Node2D
{
	[Export] public CharacterBody2D Player;
	
	public const float Speed = 300.0f;
	public string CurrentDirection = "none";
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Player.Velocity;

		if (Input.IsActionPressed("right"))
		{
			CurrentDirection = "right";
			velocity.X = Speed;
			velocity.Y = 0;
		}
		else if (Input.IsActionPressed("left"))
		{
			CurrentDirection = "left";
			velocity.X = -Speed;
			velocity.Y = 0;
		}
		else if (Input.IsActionPressed("down"))
		{
			CurrentDirection = "down";
			velocity.X = 0;
			velocity.Y = Speed;
		}
		else if (Input.IsActionPressed("up"))
		{
			CurrentDirection = "up";
			velocity.X = 0;
			velocity.Y = -Speed;
		}
		else
		{
			velocity.X = 0;
			velocity.Y = 0;
		}

		Player.Velocity = velocity;
		Player.MoveAndSlide();
	}
}
