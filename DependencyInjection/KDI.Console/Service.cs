namespace KDI.Console;

public interface IService
{
    void DoIt();
}

public class Service : IService
{
    public void DoIt()
    {
        System.Console.WriteLine("Done!");
    }
}

