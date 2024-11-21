using Godot;
using System;

public partial class PlayerButton : Button
{
	public readonly PackedScene HIT_TEXT = (PackedScene)ResourceLoader.Load("res://Scenes/Battle/UI/HitText.tscn");
	
	public BattleActor PlayerData;

	public override void _Ready()
	{
		if (GetIndex() < Data.Instance.PartyMembers.Count)
		{
			PlayerData = Data.Instance.PartyMembers[GetIndex()];
			PlayerData.HpChanged += _on_data_hp_changed;
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
		Label hitText = HIT_TEXT.Instantiate<Label>();
		AddChild(hitText);
        
		hitText.Text = change.ToString();
		hitText.Position = new Vector2(0, -24);
	}
}
