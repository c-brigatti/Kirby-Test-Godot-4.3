using Godot;

[GlobalClass] public partial class Ability : Resource
{
	[Export] public string Name { get; set; } = "default";
	[Export] public int Damage { get; set; } = 10;
	[Export] public int MpCost { get; set; } = 10;
}
