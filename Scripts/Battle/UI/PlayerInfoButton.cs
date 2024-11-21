using Godot;
using System;

public partial class PlayerInfoButton : Button
{
	public PlayerInfoBar InfoBar;
	public PlayerActor PlayerData;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		InfoBar = GetNode<PlayerInfoBar>("PlayerInfoBar");
		
		if (GetIndex() < Data.Instance.PartyMembers.Count)
		{
			PlayerData = Data.Instance.PartyMembers[GetIndex()];
			InfoBar.SetPlayerData(PlayerData);
			
			PlayerData.HpChanged += _on_data_hp_changed;
			PlayerData.MpChanged += _on_data_mp_changed;
		}
		else
		{
			Color m = GetModulate();
			m.A = 0;
			SetModulate(m);
			FocusMode = FocusModeEnum.None;
		}
	}
	
	public void _on_data_hp_changed(int hp, int change)
	{
		InfoBar.SetHpBar(hp);
		InfoBar.SetHpText(hp);
	}

	public void _on_data_mp_changed(int mp, int change)
	{
		InfoBar.SetMpText(mp);
	}
}
