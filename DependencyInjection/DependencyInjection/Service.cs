namespace DependencyInjection;

public interface IService
{
    void DoIt();
}

public class Service : IService
{
    public void DoIt()
    {
        Console.WriteLine("Done!");
    }
}

