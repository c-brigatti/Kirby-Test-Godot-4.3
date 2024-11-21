using Godot;

public partial class EnemyButton : Button
{
    public readonly PackedScene HIT_TEXT = (PackedScene)ResourceLoader.Load("res://Scenes/Battle/UI/HitText.tscn");
    
    public TextureProgressBar HpBar;
    public BattleActor EnemyData; 
    
    public override void _Ready()
    {
        HpBar = GetNode<TextureProgressBar>("HpBar");
        

        EnemyData = (BattleActor)Data.Instance.Enemies["Goofball"].Duplicate();
        EnemyData.InitialiseStats(); // needed because duplicate only takes export properties

        HpBar.MaxValue = EnemyData.MaxHp;
        HpBar.Value = EnemyData.CurrentHp;

        EnemyData.HpChanged += _on_data_hp_changed;
    }

    public void _on_data_hp_changed(int hp, int change)
    {
        HpBar.Value = hp;

        Label hitText = HIT_TEXT.Instantiate<Label>();
        AddChild(hitText);
        
        hitText.Text = change.ToString();
        hitText.Position = new Vector2(0, -24);
    }
}