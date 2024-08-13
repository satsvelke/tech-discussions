
public class WalkingRobotC//super class
{
    public virtual void Walk()
    {
        Console.WriteLine("Walking");
    }
}

public class FastWalkingRobotC : WalkingRobotC
{
    override public void Walk()
    {
        Console.WriteLine("Fast Walking");
    }
}
