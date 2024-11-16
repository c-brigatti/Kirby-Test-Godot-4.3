using Godot;
using System;

public partial class Battle : Control
{
	private WindowBox _options;
	private WindowBox _choices;
	private Menu _options_menu;
	private Menu _choices_menu;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_options = GetNode<WindowBox>("MarginContainer/VBoxContainer/Bottom/OptionsBox");
		_choices = GetNode<WindowBox>("MarginContainer/VBoxContainer/Bottom/ChoicesBox");
		_options_menu = GetNode<Menu>("MarginContainer/VBoxContainer/Bottom/OptionsBox/OptionsScroller/Options");
		_choices_menu = GetNode<Menu>("MarginContainer/VBoxContainer/Bottom/ChoicesBox/ChoicesScroller/Choices");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
