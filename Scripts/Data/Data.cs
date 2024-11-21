using Godot;
using System;
using System.IO;
using System.Linq;
using CSharp.Battle;
using Godot.Collections;

public partial class Data : Node
{
	public static Data Instance { get; private set; } // allows reference to Data without node path
	
	[Export] public Dictionary<string, PlayerActor> Players;
	[Export] public Dictionary<string, EnemyActor> Enemies;
	public Dictionary<String, Skill> Skills;
	public Dictionary<String, Magic> Magic;
	[Export] public Array<PlayerActor> PartyMembers;

	public override void _Ready()
	{
		Instance = this;
		
		String filePath;
		DirAccess directory;
		
		Skills = new Dictionary<string, Skill>();
		filePath = "res://Resources/Abilities/Skills/";
		directory = DirAccess.Open(filePath);
		foreach (var file in directory.GetFiles())
		{
			GD.Print("Loading " + file);
			Skill skill = ResourceLoader.Load<Skill>(filePath + file);
			Skills.Add(skill.Name, skill);
		}
		
		Magic = new Dictionary<string, Magic>();
		filePath = "res://Resources/Abilities/Magic/";
		directory = DirAccess.Open(filePath);
		foreach (var file in directory.GetFiles())
		{
			Magic magic = ResourceLoader.Load<Magic>(filePath + file);
			Magic.Add(magic.Name, magic);
		}
		
		// TODO: Make Actors resources too, and load them in like Skills/Magic
		Players = new Dictionary<string, PlayerActor>();
		filePath = "res://Resources/Actors/Players/";
		directory = DirAccess.Open(filePath);
		foreach (var file in directory.GetFiles())
		{
			PlayerActor actor = ResourceLoader.Load<PlayerActor>(filePath + file);
			actor.InitialiseStats();
			Players.Add(actor.Name, actor);
		}

		Enemies = new Dictionary<string, EnemyActor>();
		filePath = "res://Resources/Actors/Enemies/";
		directory = DirAccess.Open(filePath);
		foreach (var file in directory.GetFiles())
		{
			EnemyActor actor = ResourceLoader.Load<EnemyActor>(filePath + file);
			actor.InitialiseStats();
			Enemies.Add(actor.Name, actor);
		}

		PartyMembers = new Array<PlayerActor>(Players.Values);
		
		//SetActorNamesToKeys(Players);
		//SetActorNamesToKeys(Enemies);

		foreach (var player in Players.Values)
		{
			SetRandomAbilities(player, 3);
		}

		foreach (var enemy in Enemies.Values)
		{
			SetRandomAbilities(enemy, 3);
		}
	}
	
	/*public void SetActorNamesToKeys<[MustBeVariant] T>(Dictionary<string, T> actors) where T : BattleActor
	{
		var keys = actors.Keys.ToArray();
		
		foreach (var key in keys)
		{
			actors[key].SetName(key);
		}
	}*/

	public void SetRandomAbilities<[MustBeVariant] T>(T actor, int number) where T : BattleActor
	{
		Array<string> skillArray = new Array<string>(Skills.Keys);
		Array<string> magicArray = new Array<string>(Magic.Keys);
		for (int i = 0; i < number; i++)
		{
			Skill skill = Skills[skillArray[GD.RandRange(0, 3)]];
			Magic magic = Magic[magicArray[GD.RandRange(0, 3)]];
			actor.Skills.Add(skill);
			actor.Magic.Add(magic);

			GD.Print($"Skill \"{skill.Name}\" and Magic \"{magic.Name}\" added to {actor.Name}");
		}
	}
}
