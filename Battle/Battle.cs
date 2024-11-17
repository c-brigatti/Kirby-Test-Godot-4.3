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
	
	private WindowBox _options;
	private WindowBox _choices;
	private Menu _optionsMenu;
	private Menu _choicesMenu;
	private Menu _enemiesMenu;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_options = GetNode<WindowBox>("MarginContainer/VBoxContainer/Bottom/OptionsBox");
		_choices = GetNode<WindowBox>("MarginContainer/VBoxContainer/Bottom/ChoicesBox");
		_optionsMenu = GetNode<Menu>("MarginContainer/VBoxContainer/Bottom/OptionsBox/OptionsScroller/Options");
		_choicesMenu = GetNode<Menu>("MarginContainer/VBoxContainer/Bottom/ChoicesBox/ChoicesScroller/Choices");
		_enemiesMenu = GetNode<Menu>("MarginContainer/VBoxContainer/MainArea/Enemies/Enemies");

		_optionsMenu.FocusButton(0);
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
					_optionsMenu.FocusButton();
					break;
				case States.PLAYERS:
					_state = States.CHOICES;
					_choicesMenu.FocusButton();
					break;
				case States.ENEMIES:
					_state = States.CHOICES;
					_choicesMenu.FocusButton();
					break;
			}
		}
			
	}

	private void _on_options_button_focused(BaseButton button)
	{
		if (button is Button castButton)
			GD.Print(castButton.Text + " focused");
	}

	private void _on_options_button_pressed(BaseButton button)
	{
		if (button is Button castButton)
		{
			GD.Print(castButton.Text + " pressed");
			switch (castButton.Text)
			{
				case "Skills":
				case "Magic":
				case "Item":
					_state = States.CHOICES;
					_choicesMenu.FocusButton();
					break;
			}
		}
	}

	private void _on_choices_button_pressed(BaseButton button)
	{
		if (button is Button castButton)
		{
			GD.Print(castButton.Text + " pressed");
			_state = States.ENEMIES;
			_enemiesMenu.FocusButton();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
