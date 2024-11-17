using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public partial class Battle : Control
{
	public enum States
	{
		ACTIONS,
		CHOICES,
		PLAYERS,
		ENEMIES,
	}

	public enum Actions
	{
		IDLE,
		SKILLS,
		MAGIC,
		ITEMS,
		RUN,
	}
	
	private States _state = States.ACTIONS;
	private Actions _action = Actions.IDLE;
	
	private Menu _actions;
	private Menu _choices;
	private Menu _enemyIcons;
	private Menu _playerDetails;
	private Menu _playerIcons;

	private PlayerInfoBar _playerReference;
	
	private Control _actionsContainer;
	private Control _choicesContainer;
	private Control _enemyIconsContainer;
	private Control _playerDetailsContainer;
	private Control _playerIconsContainer;

	private Queue<BattleActor> _battleQueue = new Queue<BattleActor>();
	private BattleActor _activeActor;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_actions = GetNode<Menu>("MarginContainer/VBoxContainer/Bottom/ActionsBox");
		_choices = GetNode<Menu>("MarginContainer/VBoxContainer/Bottom/ChoicesBox");
		_enemyIcons = GetNode<Menu>("MarginContainer/VBoxContainer/MainArea/EnemyIcons");
		_playerDetails = GetNode<Menu>("MarginContainer/VBoxContainer/Bottom/PlayerInfo");
		_playerIcons = GetNode<Menu>("MarginContainer/VBoxContainer/MainArea/PlayerIcons");
		
		_actionsContainer = _actions.ButtonsContainer;
		_choicesContainer = _choices.ButtonsContainer;
		_enemyIconsContainer = _enemyIcons.ButtonsContainer;
		_playerDetailsContainer = _playerDetails.ButtonsContainer;
		_playerIconsContainer = _playerIcons.ButtonsContainer;

		foreach (var battleActor in GetNode<Data>("/root/Data").PartyMembers)
		{
			_battleQueue.Enqueue(battleActor);
		}

		foreach (var button in _enemyIcons.GetButtons())
		{
			var enemyButton = (EnemyButton)button;
			_battleQueue.Enqueue(enemyButton.EnemyData);
		}

		_activeActor = _battleQueue.Dequeue();

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
					_state = States.ACTIONS;
					_actions.FocusButton();
					break;
				case States.PLAYERS:
					_state = States.CHOICES;
					_choices.FocusButton();
					break;
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
	}

	private void _on_actions_button_pressed(BaseButton button)
	{
		GD.Print(button.Name + " pressed");
		switch (button.Name)
		{
			case "Skills":
				_state = States.CHOICES;
				_action = Actions.SKILLS;
				_choices.FocusButton();
				break;
			case "Magic":
				_state = States.PLAYERS;
				_action = Actions.MAGIC;
				_playerDetails.FocusButton();
				break;
			case "Item":
				_state = States.CHOICES;
				_action = Actions.ITEMS;
				_choices.FocusButton();
				break;
		}
	}

	private void _on_choices_button_pressed(BaseButton button)
	{
		if (button is Button castButton)
		{
			GD.Print(castButton.Text + " pressed");
			_state = States.ENEMIES;
			_enemyIcons.FocusButton();
		}
	}

	private void _on_enemies_button_pressed(EnemyButton button)
	{
		BattleActor enemy = button.EnemyData;

		RunPlayerEvent(_activeActor, enemy, _action);
	}

	private void _on_players_button_pressed(PlayerButton button)
	{
		
	}

	public void RunPlayerEvent(BattleActor player, BattleActor target, Actions action)
	{
		switch (action)
		{
			case Actions.SKILLS:
				break;
			case Actions.MAGIC:
				break;
			case Actions.ITEMS:
				break;
			case Actions.RUN:
				break;
			
		}
	}
}
