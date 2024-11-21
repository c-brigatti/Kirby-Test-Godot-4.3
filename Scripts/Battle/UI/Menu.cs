using Godot;
using System;
using System.Linq;
using CSharp;
using Godot.Collections;


public partial class Menu : Control
{
    [Signal]
    public delegate void ButtonFocusedEventHandler(BaseButton button);

    [Signal]
    public delegate void ButtonPressedEventHandler(BaseButton button);

    [Signal]
    public delegate void ClosedEventHandler();

    [Export] 
    public Control ButtonsContainer { get; set; } = null;
    
    [Export]
    public bool HideOnFocusExit = false;

    

    private int _index = 0;
    private bool _exiting = false;

    public override void _Ready()
    {
        TreeExiting += () => OnTreeExiting();
        
        // Ensure we restrict focus only to buttons
        SetButtonsEmit(GetButtons());
        SetButtonsFocus(GetButtons());
    }

    // Retrieve all Button children inside this Menu container
    public Array<BaseButton> GetButtons()
    {
        var buttons = ButtonsContainer.GetChildren().OfType<BaseButton>();
        return new Array<BaseButton>(buttons);
    }

    public async void UnfocusButtons()
    {
        await ToSignal(GetTree(), "process_frame");
        
        var buttons = GetButtons();
        if (buttons.Count > 0)
        {
            ButtonEnableFocus(false);
        }
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

    protected void ButtonEnableFocus(bool on)
    {
        var mode = on ? FocusModeEnum.All : FocusModeEnum.None;
        foreach (var button in GetButtons())
        {
            button.FocusMode = mode;
        }

        if (HideOnFocusExit)
            Visible = on;
    }

    protected async void OnButtonFocusExited(BaseButton button)
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
    protected void OnButtonFocused(BaseButton button)
    {
        _index = button.GetIndex();
        EmitSignal("ButtonFocused", button);
    }

    // Emit signal when a button is pressed
    protected void OnButtonPressed(BaseButton button)
    {
        EmitSignal("ButtonPressed", button);
    }

    /*protected void OnButtonTreeExiting(BaseButton button)
    {
        if (!GetButtons().Contains(GetViewport().GuiGetFocusOwner()))
        {
            FocusButton(_index - 1);
        }
    }*/

    protected void OnTreeExiting()
    {
        _exiting = true;
        GD.Print("Tree exited: " + ButtonsContainer.Name);
    }

    protected void SetButtonsEmit(Array<BaseButton> buttons)
    {
        if (buttons.Count <= 0) return;

        foreach (var button in buttons)
        {
            button.FocusExited += () => OnButtonFocusExited(button);
            button.FocusEntered += () => OnButtonFocused(button);
            button.Pressed += () => OnButtonPressed(button);
            //button.TreeExiting += () => OnButtonTreeExiting(button);
        }
    }
    
    protected void SetButtonsFocus(Array<BaseButton> buttons)
    {
        if(buttons.Count <= 0) return;
        
        if (ButtonsContainer is VBoxContainer)
        {
            var topButton = buttons.First();
            var bottomButton = buttons.Last();
            topButton.FocusNeighborTop = bottomButton.GetPath();
            bottomButton.FocusNeighborBottom = topButton.GetPath();
        }
        else if (ButtonsContainer is HBoxContainer)
        {
            var firstButton = buttons.First();
            var lastButton = buttons.Last();
            firstButton.FocusNeighborLeft = lastButton.GetPath();
            lastButton.FocusNeighborRight = firstButton.GetPath();
        }
        else if (ButtonsContainer is GridContainer gridContainer)
        {
            int cols = gridContainer.Columns;
            int rows = Mathf.CeilToInt((float)buttons.Count / cols);

            var topRow = new Array<BaseButton>();
            var bottomRow = new Array<BaseButton>();

            for (int x = 0; x < cols; x++)
            {
                if (x < buttons.Count)
                    topRow.Add(buttons[x]);
            }

            for (int x = rows * cols - cols; x < rows * cols; x++)
            {
                if (x < buttons.Count)
                    bottomRow.Add(buttons[x]);
            }

            for (int x = 0; x < cols; x++)
            {
                if (x < topRow.Count && x < bottomRow.Count)
                {
                    var topButton = topRow[x];
                    var bottomButton = bottomRow[x];
                    if (topButton != bottomButton)
                    {
                        topButton.FocusNeighborTop = bottomButton.GetPath();
                        bottomButton.FocusNeighborBottom = topButton.GetPath();
                    }
                }
            }

            for (int i = 0; i < buttons.Count; i += cols)
            {
                if (i + cols - 1 < buttons.Count)
                {
                    var leftButton = buttons[i];
                    var rightButton = buttons[i + cols - 1];
                    leftButton.FocusNeighborLeft = rightButton.GetPath();
                    rightButton.FocusNeighborRight = leftButton.GetPath();
                }
            }
        }
            
        ButtonEnableFocus(false);
    }

    protected void _on_action_pressed(BattleActor activeActor, int actionEnum)
    {
        
    }
}
