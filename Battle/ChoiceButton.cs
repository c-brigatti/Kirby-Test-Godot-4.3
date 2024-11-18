using Godot;

namespace CSharp.Battle;

public partial class ChoiceButton : Button
{
    public Ability Ability { get; private set; }

    public ChoiceButton(Ability ability)
    {
        Ability = ability;
    }
}