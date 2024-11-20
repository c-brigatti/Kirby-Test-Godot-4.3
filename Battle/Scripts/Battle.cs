using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CSharp.Battle;

public partial class Battle : Control
{
	[Signal]
	public delegate void ActionFocusedEventHandler(BattleActor activeActor, int actionEnum);
	[Signal]
	public delegate void ActionPressedEventHandler(BattleActor activeActor, int actionEnum);
	
	public enum States
	{
		ACTIONS,
		CHOICES,
		PLAYERS,
		ENEMIES,
	}

	public enum Actions
	{
		SKILLS,
		MAGIC,
		ITEMS,
		RUN,
	}

	private States _state;
	private Actions _action;
	
	private Menu _actions;
	private Menu _choices;
	private Menu _enemyIcons;
	private Menu _players;
	private Menu _playerIcons;

	private PlayerInfoBar _playerReference;
	
	private Control _actionsContainer;
	private Control _choicesContainer;
	private Control _enemyIconsContainer;
	private Control _playersContainer;
	private Control _playerIconsContainer;

	private Queue<BattleActor> _battleQueue;
	private BattleActor _activeActor;
	private Ability _activeAbility;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_actions = GetNode<Menu>("%ActionsBox");
		_choices = GetNode<Menu>("%ChoicesBox");
		_enemyIcons = GetNode<Menu>("%EnemyIcons");
		_players = GetNode<Menu>("%PlayersBox");
		_playerIcons = GetNode<Menu>("%PlayerIcons");
		
		_actionsContainer = _actions.ButtonsContainer;
		_choicesContainer = _choices.ButtonsContainer;
		_enemyIconsContainer = _enemyIcons.ButtonsContainer;
		_playersContainer = _players.ButtonsContainer;
		_playerIconsContainer = _playerIcons.ButtonsContainer;
		
		_battleQueue = new Queue<BattleActor>();

		foreach (var battleActor in Data.Instance.PartyMembers)
		{
			_battleQueue.Enqueue(battleActor);
		}
		foreach (var button in _enemyIcons.GetButtons())
		{
			var enemyButton = (EnemyButton)button;
			_battleQueue.Enqueue(enemyButton.EnemyData);
		}

		_activeActor = _battleQueue.Dequeue();
		_battleQueue.Enqueue(_activeActor);
		
		_state = States.ACTIONS;
		_action = Actions.SKILLS;

		_actions.FocusButton(0);
	}

	private void _unhandled_input(InputEvent _event)
	{
		if (_event.IsActionPressed("ui_cancel"))
		{
			switch (_state)
			{
				case States.ACTIONS:
					break;
				case States.CHOICES:
					_state = States.CHOICES;
					_actions.FocusButton();
					break;
				case States.PLAYERS:
				case States.ENEMIES:
					_state = States.CHOICES;
					_choices.FocusButton();
					break;
			}
		}
	}

	private void _on_actions_button_focused(BaseButton button)
	{
		GD.Print(button.Name + " focused");
		switch (button.Name)
		{
			case "Skills":
				_action = Actions.SKILLS;
				break;
			case "Magic":
				_action = Actions.MAGIC;
				break;
			case "Item":
				_action = Actions.ITEMS;
				break;
			case "Run":
				_action = Actions.RUN;
				break;
		}
		
		// Emits signal giving info on active player focusing on an action
		EmitSignal("ActionFocused", _activeActor, (int)_action);
	}

	// receiver for ButtonPressed in ActionsBox
	private void _on_actions_button_pressed(BaseButton button) 
	{
		GD.Print(button.Name + " pressed");
		_state = States.CHOICES;
		
		// Emits signal giving info on active player selecting an action
		EmitSignal("ActionPressed", _activeActor, (int)_action);
		_choices.FocusButton();
	}

	// receiver for ButtonPressed in ChoicesBox
	private void _on_choices_button_pressed(BaseButton button) 
	{
		ChoiceButton choiceButton = (ChoiceButton)button; // forced cast is allowed here
		GD.Print(choiceButton.Text + " pressed");
		_state = States.ENEMIES;

		_activeAbility = choiceButton.Ability;
		
		_enemyIcons.FocusButton();
	}

	// receiver for ButtonPressed in EnemyIcons
	private void _on_enemies_button_pressed(BaseButton button)
	{
		EnemyButton enemyButton = (EnemyButton)button; // forced cast is allowed here
		BattleActor enemy = enemyButton.EnemyData;

		RunPlayerEvent(_activeActor, enemy, _action, _activeAbility);
	}

	// receiver for ButtonPressed in PlayerIcons
	private void _on_players_button_pressed(BaseButton button) 
	{
		
	}

	// TODO
	public async void RunPlayerEvent(BattleActor player, BattleActor target, Actions action, Ability ability)
	{
		_enemyIcons.UnfocusButtons();
		// TODO: change wait timeout to simulate animation instead
		await ToSignal(GetTree().CreateTimer(0.25f), "timeout");
		
		switch (action)
		{
			case Actions.SKILLS:
			case Actions.MAGIC:
				target.ChangeHp(-ability.Damage);
				player.ChangeMp(-ability.MpCost);
				break;
			case Actions.ITEMS:
				break;
			case Actions.RUN:
				break;
		}
		await ToSignal(GetTree().CreateTimer(0.5f), "timeout");

		StartNextEvent();
	}

	public void StartNextEvent()
	{
		_activeActor = _battleQueue.Dequeue();
		_battleQueue.Enqueue(_activeActor);

		if (_activeActor is PlayerActor)
		{
			_actions.FocusButton();
		}
		else if (_activeActor is EnemyActor)
		{
			// TODO
		}
	}
}
