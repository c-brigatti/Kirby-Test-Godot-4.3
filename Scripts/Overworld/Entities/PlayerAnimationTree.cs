using Godot;
using System;

public partial class PlayerAnimationTree : Node2D
{
	[Export] public AnimationTree Animation;
	[Export] public CharacterBody2D Player;

	private Vector2 _lastFacingDirection;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_lastFacingDirection = Player.Velocity.Normalized();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		var idle = Player.Velocity.IsZeroApprox();
		
		// Used to flip sprite when moving left.
		// If the direction of the player changes, and the player is not idle.
		if (_lastFacingDirection != Player.Velocity.Normalized() && !idle)
		{
			GD.Print("Player Velocity: " + Player.Velocity.Normalized());
			// If the player is moving left, flip the sprite around.
			if (Player.Velocity.Normalized() == Vector2.Left)
			{
				Player.Scale = new Vector2(Player.Scale.X, -Player.Scale.Y);
				Player.RotationDegrees = 180;
			}
			// Else if the last direction was left, flip it back to normal.
			else if(_lastFacingDirection == Vector2.Left)
			{
				Player.Scale = new Vector2(Player.Scale.X, Math.Abs(Player.Scale.Y));
				Player.RotationDegrees = 0;
			}
		}
		
		// If the player is not idle, update the last facing direction to the current direction.
		if (!idle)
		{
			_lastFacingDirection = Player.Velocity.Normalized();
			Animation.Set("parameters/Walk/blend_position", _lastFacingDirection);
			Animation.Set("parameters/Idle/blend_position", _lastFacingDirection);
		}
		
		
	}
}
