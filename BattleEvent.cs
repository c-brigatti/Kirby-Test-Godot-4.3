using Godot;

public partial class BattleEvent : Resource
{
    public Menu PlayerDetails { get; set; }
    public BattleActor Enemy { get; set; }
    public Battle.Actions Action { get; set; }

    public BattleEvent(Menu playerDetails, BattleActor enemy, Battle.Actions action)
    {
        PlayerDetails = playerDetails;
        Enemy = enemy;
        Action = action;
    }
}