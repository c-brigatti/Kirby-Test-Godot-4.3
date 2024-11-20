using Godot;
using System;

public partial class PlayerInfoBar : HBoxContainer
{
	public readonly PackedScene HIT_TEXT = (PackedScene)ResourceLoader.Load("res://Scenes/HitText.tscn");
	
	public BattleActor PlayerData;
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
		PlayerData = actor;
		
		_name.Text = actor.Name;
		_hpBar.MaxValue = actor.MaxHp;
		_hpBar.Value = actor.CurrentHp;
		_hp.Text = actor.CurrentHp.ToString();
		_mp.Text = actor.CurrentMp.ToString();
		_level.Text = "5";

		PlayerData.HpChanged += _on_data_hp_changed;
		PlayerData.MpChanged += _on_data_mp_changed;
	}

	public void _on_data_hp_changed(int hp, int change)
	{
		_hpBar.Value = hp;

		Label hitText = HIT_TEXT.Instantiate<Label>();
		AddChild(hitText);
        
		hitText.Text = change.ToString();
		hitText.Position = new Vector2(0, -24);
	}

	public void _on_data_mp_changed(int mp, int change)
	{
		_mp.Text = mp.ToString();
	}
}
