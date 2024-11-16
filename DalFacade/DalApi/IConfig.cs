namespace DalApi;
using DO;

public interface IConfig // revealing the methhods to the serfce
{
    DateTime Clock { get; set; } 
    void Rest();
}
