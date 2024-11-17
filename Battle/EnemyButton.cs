using Godot;

public partial class EnemyButton : TextureButton
{
    public override void _Ready()
    {
        // Create a StyleBoxFlat for the focused state (with a border)
        var focusStyle = new StyleBoxFlat
        {
            BorderWidthTop = 10,
            BorderWidthBottom = 10,
            BorderWidthLeft = 10,
            BorderWidthRight = 10,
            BorderColor = new Color(1, 0, 0) // Red border
        };

        // Apply the style override for the focused state
        AddThemeStyleboxOverride("focus", focusStyle);
    }
}