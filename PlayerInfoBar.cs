using Godot;
using System;

public partial class PlayerInfoBar : HBoxContainer
{
	private BattleActor _playerData;
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

	public void SetPlayerData(BattleActor actor)
	{
		_playerData = actor;
		
		_name.Text = actor.Name;
		_hpBar.SetMax(actor.MaxHp);
		_hpBar.SetValue(actor.CurrentHp);
		_hp.Text = actor.CurrentHp.ToString();
		_mp.Text = actor.CurrentMp.ToString();
		_level.Text = "5";
	}
}
