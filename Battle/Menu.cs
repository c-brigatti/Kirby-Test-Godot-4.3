using Godot;
using System;
using System.Linq;

public partial class Menu : Container
{
    [Signal]
    public delegate void ButtonFocusedEventHandler(BaseButton button);

    [Signal]
    public delegate void ButtonPressedEventHandler(BaseButton button);

    [Signal]
    public delegate void ClosedEventHandler();

    [Export] 
    public Container ButtonsContainer = null;
    
    [Export]
    public bool HideOnFocusExit = false;

    private int _index = 0;
    private bool _exiting = false;

    public override void _Ready()
    {
        // Ensure we restrict focus only to buttons
        foreach (var button in GetButtons())
        {
            button.FocusExited += () => OnButtonFocusExited(button);
            button.FocusEntered += () => OnButtonFocused(button);
            button.Pressed += () => OnButtonPressed(button);
            button.TreeExiting += () => OnButtonTreeExiting(button);
        }
        
        var buttons = GetButtons();
        var className = GetClass();

        if (className.StartsWith("VBox"))
        {
            var topButton = buttons.First();
            var bottomButton = buttons.Last();
            topButton.FocusNeighborTop = bottomButton.GetPath();
            bottomButton.FocusNeighborBottom = topButton.GetPath();
        }
        else if (className.StartsWith("HBox"))
        {
            var firstButton = buttons.First();
            var lastButton = buttons.Last();
            firstButton.FocusNeighborLeft = lastButton.GetPath();
            lastButton.FocusNeighborRight = firstButton.GetPath();
        }
        
        ButtonEnableFocus(false);
    }

    // Retrieve all Button children inside this Menu container
    public Godot.Collections.Array<BaseButton> GetButtons()
    {
        return new Godot.Collections.Array<BaseButton>(GetChildren().OfType<BaseButton>());
    }

    // Focus a button by index
    public async void FocusButton(int focusIndex = -1)
    {
        if (focusIndex == -1)
            focusIndex = _index;

        await ToSignal(GetTree(), "process_frame");
        
        var buttons = GetButtons();
        if (buttons.Count > 0)
        {
            ButtonEnableFocus(true);
            focusIndex = Math.Clamp(focusIndex, 0, buttons.Count - 1);
            buttons[focusIndex].GrabFocus();
        }
    }

    private void ButtonEnableFocus(bool on)
    {
        var mode = on ? FocusModeEnum.All : FocusModeEnum.None;
        foreach (var button in GetButtons())
        {
            button.FocusMode = mode;
        }

        if (HideOnFocusExit)
            Visible = on;
    }

    private async void OnButtonFocusExited(BaseButton button)
    {
        await ToSignal(GetTree(), "process_frame");
        if (_exiting)
            return;
        
        if (!GetButtons().Contains(GetViewport().GuiGetFocusOwner()))
        {
            ButtonEnableFocus(false);
        }
    }

    // Emit signal when a button gains focus
    private void OnButtonFocused(BaseButton button)
    {
        _index = button.GetIndex();
        EmitSignal("ButtonFocused", button);
    }

    // Emit signal when a button is pressed
    private void OnButtonPressed(BaseButton button)
    {
        EmitSignal("ButtonPressed", button);
    }

    private void OnButtonTreeExiting(BaseButton button)
    {
        if (!GetButtons().Contains(GetViewport().GuiGetFocusOwner()))
        {
            FocusButton(_index - 1);
        }
    }
}
