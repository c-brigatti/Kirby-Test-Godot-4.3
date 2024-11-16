using Godot;
using System;
using System.Linq;

public partial class Menu : Container
{
	[Signal]
	public delegate void ButtonFocusedEventHandler(BaseButton button);

	[Signal]
	public delegate void ButtonPressedEventHandler(BaseButton button);

	private int index = 0;
	
	public override void _Ready()
	{
		foreach (var button in GetButtons())
		{
			button.FocusEntered += () => OnButtonFocused(button);
			button.Pressed += () => OnButtonPressed(button);
		}
	}

	// Retrieve all Button children
	private Godot.Collections.Array<BaseButton> GetButtons()
	{
		return new Godot.Collections.Array<BaseButton>(GetChildren().OfType<BaseButton>());
	}

	// Connect signals to all buttons
	private void ConnectToButtons()
	{

	}

	// Focus a button by index
	public void FocusButton(int focusIndex = 0)
	{
		var buttons = GetButtons();
		buttons[focusIndex].GrabFocus();
	}

	// Emit signal when a button gains focus
	private void OnButtonFocused(BaseButton button)
	{
		EmitSignal("ButtonFocused", button);
	}

	// Emit signal when a button is pressed
	private void OnButtonPressed(BaseButton button)
	{
		EmitSignal("ButtonPressed", button);
	}
}
