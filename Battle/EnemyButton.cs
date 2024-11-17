using Godot;

public partial class EnemyButton : Button
{
    public TextureProgressBar HpBar;
    public BattleActor EnemyData; 
    
    public override void _Ready()
    {
        HpBar = GetNode<TextureProgressBar>("HpBar");
        EnemyData = (BattleActor)GetNode<Data>("/root/Data").Enemies["Goofball"].Duplicate();
    }
}