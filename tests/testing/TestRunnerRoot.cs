using Qengu.GodotUtils.Tests;
using Godot;
using System;
using System.Runtime.InteropServices.JavaScript;

public partial class TestRunnerRoot : Node3D
{

    [Export] private Node parent;

    public async override void _Ready()
    {
        TestRunner testRunner = new TestRunner(parent);
        await foreach (TestResult testResult in testRunner.RunTests())
        {
            GD.PrintRich(string.Format("[{0}] [b]({1} {2})[/b]{3}",
                        testResult.Success ? "[color=green]✔[/color]" : "[color=red]✘[/color]",
                        testResult.FileName,
                        testResult.MethodName,
                        testResult.Success ? string.Empty : $" {testResult.ExceptionMessage}"));
        }
    }

}
