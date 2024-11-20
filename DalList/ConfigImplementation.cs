namespace Dal;
using DalApi;
using System;

internal class ConfigImplementation : IConfig // realizing the original methhods to the serfce
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