public class RobotD : IRobotD
{
    public void Walk()
    {
        Console.WriteLine("Walking");
    }

    public void Talk()
    {
        Console.WriteLine("Talking");
    }
}


public class RobotTalking : ITalkRobotD
{
    public void Talk()
    {
        throw new NotImplementedException();
    }
}


public interface IRobotD
{
    void Walk();
    void Talk();
}

public interface IWalkRobotD
{

}
public interface ITalkRobotD
{

}