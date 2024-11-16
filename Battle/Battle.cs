using Godot;
using System;

public partial class Battle : Control
{
	private WindowBox _options;
	private WindowBox _choices;
	private Menu _optionsMenu;
	private Menu _choicesMenu;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_options = GetNode<WindowBox>("MarginContainer/VBoxContainer/Bottom/OptionsBox");
		_choices = GetNode<WindowBox>("MarginContainer/VBoxContainer/Bottom/ChoicesBox");
		_optionsMenu = GetNode<Menu>("MarginContainer/VBoxContainer/Bottom/OptionsBox/OptionsScroller/Options");
		_choicesMenu = GetNode<Menu>("MarginContainer/VBoxContainer/Bottom/ChoicesBox/ChoicesScroller/Choices");

		_optionsMenu.FocusButton(0);
	}

	void _on_options_button_focused(BaseButton button)
	{
		if (button is Button castButton)
			GD.Print(castButton.Text + " focused");
	}
	
	void _on_options_button_pressed(BaseButton button)
	{
		if (button is Button castButton)
			GD.Print(castButton.Text + " pressed");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
