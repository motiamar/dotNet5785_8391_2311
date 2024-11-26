namespace DalApi;
using DO;


// revealing the methhods to the serfce
public interface IConfig 
{
    DateTime Clock { get; set; } 
    void Reset();
    TimeSpan RiskRnge { get; set; }
}
