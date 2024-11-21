using Godot;
using System;

public partial class PlayerInfoBar : HBoxContainer
{
	private Label _name;
	private TextureProgressBar _hpBar;
	private Label _hp;
	private Label _mp;
	private Label _level;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_name = GetNode<Label>("Name");
		_hpBar = GetNode<TextureProgressBar>("HpBar");
		_hp = GetNode<Label>("HP");
		_mp = GetNode<Label>("MP");
		_level = GetNode<Label>("Level");
	}

	
	// required because each PlayerInfoBar has a PlayerInfoButton wrapper that receives a unique playerData using GetIndex()
	public void SetPlayerData(BattleActor actor)
	{
		_name.Text = actor.Name;
		_hpBar.MaxValue = actor.MaxHp;
		GD.Print($"{actor.Name}'s Max HP: {actor.MaxHp} and Current HP: {actor.CurrentHp}");
		_hpBar.Value = actor.CurrentHp;
		_hp.Text = actor.CurrentHp.ToString();
		_mp.Text = actor.CurrentMp.ToString();
		_level.Text = actor.Level.ToString();
	}

	public void SetHpBar(int hp)
	{
		_hpBar.Value = hp;
	}

	public void SetHpText(int hp)
	{
		_hp.Text = hp.ToString();
	}

	public void SetMpText(int mp)
	{
		_mp.Text = mp.ToString();
	}
	
}
