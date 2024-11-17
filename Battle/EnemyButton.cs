using Godot;
using System;

public partial class EnemyButton : TextureButton
{
    private Texture2D _originalTexture;
    private Texture2D _focusedTexture;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _originalTexture = (Texture2D)TextureNormal;
        _focusedTexture = AddBorderToTexture(_originalTexture);
        TextureFocused = _focusedTexture;
        
        QueueRedraw();
    }

    private Texture2D AddBorderToTexture(Texture2D original)
    {
        // Convert Texture2D to Image
        var image = original.GetImage();

        // Add a border (example: 10px border in red color)
        var borderSize = 10;
        var width = image.GetWidth() + borderSize * 2;
        var height = image.GetHeight() + borderSize * 2;

        // Create a new image with a border
        var newImage = Image.Create(width, height, false, Image.Format.Rgba8);

        // Fill the new image with a border color (red in this case)
        newImage.Fill(new Color(1, 0, 0, 1)); // red border

        // Paste the original image inside the new image, offset by border size
        newImage.BlitRect(image, new Rect2I(0, 0, image.GetWidth(), image.GetHeight()), new Vector2I(borderSize, borderSize));

        // Create an instance of ImageTexture (non-static method call)
        var focusedTexture = ImageTexture.CreateFromImage(newImage); // Correct non-static method call

        return focusedTexture;
    }
}