using Godot;
using System;
using System.Linq;
using CSharp;
using CSharp.Battle;
using Godot.Collections;

public partial class ChoicesMenu : Menu
{
    private Dictionary
        <Battle.Actions, Dictionary
            <BattleActor, Array<BaseButton>>> _choicesDictionary;
    
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        _choicesDictionary = new Dictionary
            <Battle.Actions, Dictionary
                <BattleActor, Array<BaseButton>>>();
	}

	private void _on_action_focused(BattleActor activeActor, int actionEnumInt)
    {
        Battle.Actions actionEnum = (Battle.Actions)actionEnumInt;
        GD.Print("action focused: " + actionEnum.ToString());
        
        // Clear existing children from ButtonsContainer
        foreach (Node child in ButtonsContainer.GetChildren())
        {
            if (child is Button)
            {
                ButtonsContainer.RemoveChild(child);
            }
        }
    
        if (!_choicesDictionary.ContainsKey(actionEnum))
        {
            _choicesDictionary.Add(actionEnum, new Dictionary<BattleActor, Array<BaseButton>>());
        }
        Dictionary<BattleActor, Array<BaseButton>> actorChoicesDictionary = _choicesDictionary[actionEnum];
        
        if(!actorChoicesDictionary.ContainsKey(activeActor))
        {
            Array<BaseButton> choiceButtons = new Array<BaseButton>();
            Theme buttonTheme = (Theme)GD.Load("res://Themes/battle_ui_theme.tres");
    
            switch (actionEnum)
            {
                // TODO: consider a util function for making a button, adding some features, then returning it
                case Battle.Actions.SKILLS:
                    foreach (var skill in activeActor.Skills)
                    {
                        ChoiceButton skillButton = new ChoiceButton(skill);
                        skillButton.Theme = buttonTheme;
                        skillButton.Text = skill.Name;
                        skillButton.MouseFilter = MouseFilterEnum.Ignore;
                        skillButton.Flat = true;
                        choiceButtons.Add(skillButton);
                    }
                    break;
                case Battle.Actions.MAGIC:
                    foreach (var magic in activeActor.Magic)
                    {
                        ChoiceButton magicButton = new ChoiceButton(magic);
                        magicButton.Theme = buttonTheme;
                        magicButton.Text = magic.Name;
                        magicButton.MouseFilter = MouseFilterEnum.Ignore;
                        magicButton.Flat = true;
                        choiceButtons.Add(magicButton);
                    }
                    break;
                case Battle.Actions.ITEMS:
                    break;
                case Battle.Actions.RUN: 
                    break;
            }
            SetButtonsEmit(choiceButtons);
            actorChoicesDictionary.Add(activeActor, choiceButtons);
        }
            
        Array<BaseButton> choiceArray = _choicesDictionary[actionEnum][activeActor];
        
        foreach (var button in choiceArray)
        { 
            ButtonsContainer.AddChild(button);
        }
        
        SetButtonsFocus(choiceArray);
    }
}
