using Godot;

namespace Qengu.GodotUtils.Extensions;

public static class TimerExtensions
{
    /// <summary>
    /// Gets the time elapsed. Based on time left and wait time.
    /// </summary>
    /// <returns>The time elapsed</returns>
    public static double GetTimeElapsed(this Timer timer)
    {
        return timer.WaitTime - timer.TimeLeft;
    }
}
