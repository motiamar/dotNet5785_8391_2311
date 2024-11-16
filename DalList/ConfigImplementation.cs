namespace Dal;
using DalApi;
using System;

public class ConfigImplementation : IConfig // realizing the original methhods to the serfce
{
    public DateTime Clock
    {
        get => Config.Clock;
        set => Config.Clock = value;
    }

    public void Rest()
    {
        Config.Rest();
    }
}