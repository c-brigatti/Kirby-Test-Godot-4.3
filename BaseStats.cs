using Godot;

namespace CSharp.Stats;

[GlobalClass] public partial class BaseStats : Resource
{
    [Export] public int BaseHP { get; private set; } = 100;
    [Export] public int BaseMP { get; private set; } = 30;
    [Export] public int BaseSTR { get; private set; } = 10;
    [Export] public int BaseMAG { get; private set; } = 10;
    [Export] public int BaseVIT { get; private set; } = 10;
    [Export] public int BaseAGI { get; private set; } = 10;
    [Export] public int BaseLUK { get; private set; } = 10;
    [Export] public int BaseEXP { get; private set; } = 20;
    [Export] public int BaseMON { get; private set; } = 50;

    [Export] public double RateHP { get; private set; } = 0.15;
    [Export] public double FlatHP { get; private set; } = 50;
    
    [Export] public double RateMP { get; private set; } = 0.1;
    [Export] public double FlatMP { get; private set; } = 10;
    
    [Export] public double RateSTR { get; private set; } = 0.05;
    [Export] public double FlatSTR { get; private set; } = 2.5;
    
    [Export] public double RateMAG { get; private set; } = 0.05;
    [Export] public double FlatMAG { get; private set; } = 2.5;
    
    [Export] public double RateVIT { get; private set; } = 0.05;
    [Export] public double FlatVIT { get; private set; } = 2.5;
    
    [Export] public double RateAGI { get; private set; } = 0.05;
    [Export] public double FlatAGI { get; private set; } = 2.5;
    
    [Export] public double RateLUK { get; private set; } = 0.05;
    [Export] public double FlatLUK { get; private set; } = 2.5;
    
    [Export] public double RateEXP { get; private set; } = 0.15;
    [Export] public double FlatEXP { get; private set; } = 50;
    
    [Export] public double RateMON { get; private set; } = 0.15;
    [Export] public double FlatMON { get; private set; } = 50;

    public static int Scale(int baseStat, double rate, double flat, int level)
    {
        return (int)(baseStat * (1 + (level - 1) * rate) + flat * (level - 1));
    }
}