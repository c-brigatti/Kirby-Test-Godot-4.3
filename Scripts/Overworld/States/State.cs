using Godot;
using System;
using System.Diagnostics;

public partial class State : Node
{
    [Signal] public delegate void TransitionedEventHandler(State state, string newStateName);

    protected CharacterBody2D Entity;
    protected AnimatedSprite2D Animation;
    //protected 
    
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

    public void SetEntity(CharacterBody2D entity, AnimatedSprite2D animation)
    {
        Entity = entity;
        Animation = animation;
    }
}
