using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSharp.Battle;
using Godot.Collections;
using Array = Godot.Collections.Array;

public partial class Data : Node
{
	public static Data Instance { get; private set; } // allows reference to Data without node path
	
	public Godot.Collections.Dictionary<string, PlayerActor> Players;
	public Godot.Collections.Dictionary<string, EnemyActor> Enemies;
	public Godot.Collections.Dictionary<String, Skill> Skills;
	public Godot.Collections.Dictionary<String, Magic> Magic;
	public Array<PlayerActor> PartyMembers;

	public override void _Ready()
	{
		Instance = this;
		
		String filePath;
		DirAccess directory;
		
		Skills = new Godot.Collections.Dictionary<string, Skill>();
		filePath = "res://Skills/";
		directory = DirAccess.Open(filePath);
		foreach (var file in directory.GetFiles())
		{
			Skill skill = ResourceLoader.Load<Skill>(filePath + file); ;
			Skills.Add(skill.Name, skill);
		}
		
		Magic = new Godot.Collections.Dictionary<string, Magic>();
		filePath = "res://Magic/";
		directory = DirAccess.Open(filePath);
		foreach (var file in directory.GetFiles())
		{
			Magic magic = ResourceLoader.Load<Magic>(filePath + file);
			Magic.Add(magic.Name, magic);
		}
		
		// TODO: Make Actors resources too, and load them in like Skills/Magic
		Players = new Godot.Collections.Dictionary<string, PlayerActor>
		{
			{"Bread", new PlayerActor()},
			{"Pillow", new PlayerActor()},
			{"Wolf", new PlayerActor()},
			{"Milk", new PlayerActor()},
		};
		
		Enemies = new Godot.Collections.Dictionary<string, EnemyActor>
		{
			{ "Cocky Roach", new EnemyActor() },
			{ "Goofball", new EnemyActor() },
		};

		PartyMembers = new Array<PlayerActor>(Players.Values);
		
		SetActorNamesToKeys(Players);
		SetActorNamesToKeys(Enemies);

		foreach (var player in Players.Values)
		{
			SetRandomAbilities(player, 3);
		}

		foreach (var enemy in Enemies.Values)
		{
			SetRandomAbilities(enemy, 3);
		}
	}
	
	public void SetActorNamesToKeys<[MustBeVariant] T>(Godot.Collections.Dictionary<string, T> actors) where T : BattleActor
	{
		var keys = actors.Keys.ToArray();
		
		foreach (var key in keys)
		{
			actors[key].SetName(key);
		}
	}

	public void SetRandomAbilities<[MustBeVariant] T>(T actor, int number) where T : BattleActor
	{
		List<string> skillList = new List<string>(Skills.Keys);
		List<string> magicList = new List<string>(Magic.Keys);
		for (int i = 0; i < number; i++)
		{
			actor.Skills.Add(Skills[skillList[GD.RandRange(0,3)]]);
			actor.Magic.Add(Magic[magicList[GD.RandRange(0,3)]]);
		}
	}
}
