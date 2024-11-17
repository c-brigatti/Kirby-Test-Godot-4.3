using Godot;
using System;

public partial class Battle : Control
{
	private enum States
	{
		OPTIONS,
		CHOICES,
		PLAYERS,
		ENEMIES
	}
	
	private States _state = States.OPTIONS;
	
	private Menu _options;
	private Menu _choices;
	private Menu _enemyIcons;
	private Menu _playerDetails;
	private Menu _playerIcons;
	
	private Control _optionsContainer;
	private Control _choicesContainer;
	private Control _enemyIconsContainer;
	private Control _playerDetailsContainer;
	private Control _playerIconsContainer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_options = GetNode<Menu>("MarginContainer/VBoxContainer/Bottom/OptionsBox");
		_choices = GetNode<Menu>("MarginContainer/VBoxContainer/Bottom/ChoicesBox");
		_enemyIcons = GetNode<Menu>("MarginContainer/VBoxContainer/MainArea/EnemyIcons");
		_playerDetails = GetNode<Menu>("MarginContainer/VBoxContainer/Bottom/PlayerInfo");
		_playerIcons = GetNode<Menu>("MarginContainer/VBoxContainer/MainArea/PlayerIcons");
		
		_optionsContainer = _options.ButtonsContainer;
		_choicesContainer = _choices.ButtonsContainer;
		_enemyIconsContainer = _enemyIcons.ButtonsContainer;
		_playerDetailsContainer = _playerDetails.ButtonsContainer;
		_playerIconsContainer = _playerIcons.ButtonsContainer;

		_options.FocusButton(0);
	}

	private void _unhandled_input(InputEvent _event)
	{
		if (_event.IsActionPressed("ui_cancel"))
		{
			switch (_state)
			{
				case States.OPTIONS:
					break;
				case States.CHOICES:
					_state = States.OPTIONS;
					_options.FocusButton();
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

	private void _on_options_button_focused(BaseButton button)
	{
		GD.Print(button.Name + " focused");
	}

	private void _on_options_button_pressed(BaseButton button)
	{
		GD.Print(button.Name + " pressed");
		switch (button.Name)
		{
			case "Skills":
				_state = States.CHOICES;
				_choices.FocusButton();
				break;
			case "Magic":
				_state = States.PLAYERS;
				_playerDetails.FocusButton();
				break;
			case "Item":
				_state = States.CHOICES;
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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
