namespace HelloCore;

public interface IClock
{
    DateTime GetNow();
    int GetRandomNumber();
}

public class FakeClock : IClock
{
    public DateTime GetNow() => new DateTime(2021, 1, 1, 9, 0, 0);

    public int GetRandomNumber()
    {
        throw new NotImplementedException();
    }
}

public class RealClock : IClock
{
    private int randomNumber;
    public RealClock()
    {
        randomNumber = new Random().Next(1, 100);
    }
    public DateTime GetNow() => DateTime.Now;

    public int GetRandomNumber()
    {
        return randomNumber;
    }
}


public interface IWelcome
{
    string SayHello();
}

//public class Welcome : IWelcome
//{
//    public string SayHello() => "Benvenuti al corso";
//}

public class Welcome : IWelcome
{
    private readonly IClock clock;
    private readonly IConfiguration configuration;

    public Welcome(IClock clock, IConfiguration configuration)
    {
        //IClock clock = new RealClock();
        this.clock = clock;
        this.configuration = configuration;
    }

    public string SayHello()
    {
        var date = clock.GetNow();
        if (date.Hour < 12)
        {
            return $"{configuration["SalutoComplesso:B"]}   Buongiorno, corso! {date.ToString()} {clock.GetRandomNumber()}";
        } else
        {
            return $"{configuration["SalutoComplesso:B"]} Buonasera, corso! {date.ToString()} {clock.GetRandomNumber()}";

        }
    }
}


public class WelcomeBis : IWelcome
{
    public string SayHello()
    {
        return "Buonasera, corso!";
    }
}