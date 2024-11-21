using Godot;
using System;

public partial class HitText : Label
{
	public const float SPEED = 0.05f;
	
	public override void _Process(double delta)
	{
		Vector2 pos = Position; // Get a copy of the position
		pos.Y -= SPEED;         // Modify the Y coordinate
		Position = pos;         // Reassign the modified position
	}

	public void _on_free_timeout()
	{
		QueueFree();
	}
}
