using Qengu.GodotUtils.Tests;
using Godot;
using System;

public partial class TestRunnerRoot : Node
{

    public async override void _Ready()
    {
        TestRunner testRunner = new TestRunner(this);
        await foreach (TestResult testResult in testRunner.RunTests())
        {
            GD.Print(string.Format("[{0}] ({1} {2}){3}", testResult.Success ? "Success" : "Failure", testResult.FileName, testResult.MethodName, testResult.Success ? string.Empty : $" {testResult.ExceptionMessage}"));
        }
    }

}
