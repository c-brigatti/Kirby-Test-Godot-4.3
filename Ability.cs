using Godot;

[GlobalClass] public partial class Ability : Resource
{
	[Export] public string Name { get; set; }
	[Export] public int Damage { get; set; }
	[Export] public int MpCost { get; set; }

	public Ability() { }

	public Ability(string name = "default", int damage = 10, int mpCost = 10)
	{
		Name = name;
		Damage = damage;
		MpCost = mpCost;
	}
}
