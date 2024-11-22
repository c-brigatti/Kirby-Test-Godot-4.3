using Godot;
using System;
using System.Xml.Schema;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public string CurrentDirection = "none";

	[Export] public AnimatedSprite2D Animation;

	public override void _Ready()
	{
		Animation.Play("idle_down");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		if (Input.IsActionPressed("right"))
		{
			CurrentDirection = "right";
			PlayAnimation(true);
			velocity.X = Speed;
			velocity.Y = 0;
		}
		else if (Input.IsActionPressed("left"))
		{
			CurrentDirection = "left";
			PlayAnimation(true);
			velocity.X = -Speed;
			velocity.Y = 0;
		}
		else if (Input.IsActionPressed("down"))
		{
			CurrentDirection = "down";
			PlayAnimation(true);
			velocity.X = 0;
			velocity.Y = Speed;
		}
		else if (Input.IsActionPressed("up"))
		{
			CurrentDirection = "up";
			PlayAnimation(true);
			velocity.X = 0;
			velocity.Y = -Speed;
		}
		else
		{
			PlayAnimation(false);
			velocity.X = 0;
			velocity.Y = 0;
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	// TODO: Use a state machine here instead
	public void PlayAnimation(bool moving)
	{
		if (CurrentDirection == "right")
		{
			Animation.FlipH = false;
			Animation.Play(moving ? "walk_horizontal" : "idle_horizontal");
		}
		else if (CurrentDirection == "left")
		{
			Animation.FlipH = true;
			Animation.Play(moving ? "walk_horizontal" : "idle_horizontal");
		}
		else if (CurrentDirection == "down")
		{
			Animation.Play(moving ? "walk_down" : "idle_down");
		}
		else if (CurrentDirection == "up")
		{
			Animation.Play(moving ? "walk_up" : "idle_up");
		}
	}
}
