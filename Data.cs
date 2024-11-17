using Godot;
using System;
using System.Linq;
using Godot.Collections;
using Array = Godot.Collections.Array;

public partial class Data : Node
{
	public Dictionary<string, BattleActor> Players;
	public Dictionary<string, BattleActor> Enemies;
	public Dictionary<String, Skill> Skills;
	public Dictionary<String, Magic> Magic;
	public Array<BattleActor> PartyMembers;

	public override void _Ready()
	{
		Players = new Dictionary<string, BattleActor>
		{
			{"Bread", new BattleActor(this)},
			{"Pillow", new BattleActor(this)},
			{"Wolf", new BattleActor(this)},
			{"Milk", new BattleActor(this)},
		};
		
		Enemies = new Dictionary<string, BattleActor>
		{
			{ "Cocky Roach", new BattleActor(this) },
			{ "Goofball", new BattleActor(this) },
		};

		Skills = new Dictionary<string, Skill>
		{
			{ "Punch", new Skill("Punch", 10) },
			{ "Band-aid", new Skill("Band-aid", -10) },
			{ "Kick", new Skill("Kick", 15) },
			{ "Tackle", new Skill("Tackle", 40) },
		};

		Magic = new Dictionary<string, Magic>
		{
			{ "Fireball", new Magic("Fireball", 25, 10) }, // Moderate damage, moderate MP cost
			{ "Ice Shard", new Magic("Ice Shard", 20, 8) }, // Moderate damage, lower MP cost
			{ "Lightning Bolt", new Magic("Lightning Bolt", 35, 15) }, // High damage, high MP cost
			{ "Heal", new Magic("Heal", -30, 12) }, // Restores health, moderate MP cost
		};

		PartyMembers = new Array<BattleActor>(Players.Values);
		
		SetActorNamesToKeys(Players);
		SetActorNamesToKeys(Enemies);
		GD.Print(Players["Bread"].Name);
	}
	
	public static void SetActorNamesToKeys(Dictionary<string, BattleActor> actors)
	{
		var keys = actors.Keys.ToArray();
		if (actors[keys[0]] is BattleActor)
		{
			foreach (var key in keys)
			{
				actors[key].SetName(key);
			}
		}
	}
}
