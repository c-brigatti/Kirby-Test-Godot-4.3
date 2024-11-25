using Godot;
using System;
using System.Security;
using Godot.Collections;

public partial class StateMachine : Node
{
	[Export] public State InitialState { get; set; }
	[Export] public CharacterBody2D Entity { get; set; }
	[Export] public AnimatedSprite2D Animation { get; set; }
	
	private State _currentState;
	private Dictionary<string, State> _states = new ();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (var child in GetChildren())
		{
			if (child is State childState)
			{
				_states.Add(child.Name, childState);
				childState.SetEntity(Entity, Animation);
				childState.Transitioned += _on_child_transition; // State calls Transitioned when transitioning
			}
		}

		_currentState = InitialState;
		InitialState?.Enter();
	}
	
	public void Process(double delta)
	{
		_currentState?.Update(delta);
	}

	public void PhysicsProcess(double delta)
	{
		_currentState?.PhysicsUpdate(delta);
	}

	public void _on_child_transition(State state, string newStateName)
	{
		if (state != _currentState)
		{
			return;
		}
		
		State newState = _states[newStateName];
		
		_currentState?.Exit();
		
		newState?.Enter();
		_currentState = newState;
	}
}
