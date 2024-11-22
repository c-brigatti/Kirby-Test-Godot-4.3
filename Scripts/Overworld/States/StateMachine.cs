using Godot;
using System;
using System.Security;
using Godot.Collections;

public partial class StateMachine : Node
{
	[Export] public State InitialState { get; set; }
	
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
				childState.Transitioned += _on_child_transition; // State calls Transitioned when transitioning
			}
		}

		InitialState?.Enter();
		_currentState = InitialState;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_currentState?.Update(delta);
	}

	public override void _PhysicsProcess(double delta)
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
