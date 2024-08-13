public class RobotOCP
{
    public void Walk()
    {
        Console.WriteLine("Walking");
    }

    public void Talk()
    {
        Console.WriteLine("Talking");
    }

    public void Jump()
    {
        Console.WriteLine("Talking");
    }
}


public abstract class RobotA
{
    public abstract void DoAction();
}

public class WalkingRobotA : RobotA
{
    public override void DoAction()
    {
        Console.WriteLine("Walking");
    }
}

public class TalkingRobotB : RobotA
{
    public override void DoAction()
    {
        Console.WriteLine("Talking");
    }
}

public class JumpingRobotB : RobotA
{
    public override void DoAction()
    {
        Console.WriteLine("Jumping");
    }
}
