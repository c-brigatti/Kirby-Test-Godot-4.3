using Godot;
using System;
using System.Linq;

public partial class Menu : Container
{
	Godot.Collections.Array<Button> get_buttons()
	{
		return GetChildren().OfType<Button>;
	}
}
