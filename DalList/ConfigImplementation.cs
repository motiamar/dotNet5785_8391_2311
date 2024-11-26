namespace Dal;
using DalApi;
using System;


// realizing the original methhods to the serfce
internal class ConfigImplementation : IConfig 
{
    public DateTime Clock
    {
        get => Config.Clock;
        set => Config.Clock = value;
    }

    public void Reset()
    {
        Config.Reset();
    }

    public TimeSpan RiskRnge
    {
        get => Config.RiskRnge;
        set => Config.RiskRnge = value;
    }

}