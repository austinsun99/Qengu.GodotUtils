using Qengu.GodotUtils.Tests;
using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

public partial class TestRunner : Node
{
    public async override void _Ready()
    {
        await NodeTestHelper.AwaitProcessFrame(GetTree());
        IEnumerable<(Type type, IEnumerable<TestMethodInfo> testMethods)> testClasses = TestRunnerHelper.GetTestMethods();
        foreach (var (type, testMethods) in testClasses)
        {
            var instance = Activator.CreateInstance(type);
            foreach (var testMethodInfo in testMethods)
            {
                try
                {
                    object result = testMethodInfo.MethodInfo.Invoke(instance, testMethodInfo.AttributeType == typeof(NodeTestAttribute) ? [this] : null);
                    if (result is Task task)
                    {
                        await task;
                    }
                    GD.Print($"Success: {testMethodInfo.MethodInfo.Name}");
                }
                catch (TargetInvocationException ex)
                {
                    GD.Print($"Failure: {ex.InnerException.Message}");
                }
            }
        }
    }

    public override void _Process(double delta)
    {

    }

}
