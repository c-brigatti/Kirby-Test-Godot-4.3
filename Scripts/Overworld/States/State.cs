using Godot;
using System;
using System.Diagnostics;

public partial class State : Node
{
    [Signal] public delegate void TransitionedEventHandler(State state, string newStateName);
    
    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void Update(double delta)
    {
        
    }

    public void PhysicsUpdate(double delta)
    {
        
    }
}
