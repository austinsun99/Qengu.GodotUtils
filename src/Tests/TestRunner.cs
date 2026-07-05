using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Godot;

using ClassTestMethodsSuite =
(Qengu.GodotUtils.Tests.TestMethodInfo? setup,
Qengu.GodotUtils.Tests.TestMethodInfo? teardown,
System.Collections.Generic.IEnumerable<Qengu.GodotUtils.Tests.TestMethodInfo> testMethods);

namespace Qengu.GodotUtils.Tests;

/// <summary>
/// A default implementation of a test runner.
/// </summary>
public sealed class TestRunner(Node parent)
{

    /// <summary>
    /// Finds and runs the tests specified by the test attribute.
    /// </summary>
    /// <param name="parent">The parent root node for tests that require instantiating nodes</param>
    /// <returns>An async enumerable after each test completion</returns>
    public async IAsyncEnumerable<TestResult> RunTests()
    {
        IEnumerable<(Type, ClassTestMethodsSuite)> testClasses = GetTestMethods();
        foreach (var (type, testSuite) in testClasses)
        {
            var (setup, teardown, testMethods) = testSuite;
            var instance = Activator.CreateInstance(type);
            foreach (var testMethodInfo in testMethods)
            {
                TestResult testResult;
                try
                {
                    if (setup is not null) await InvokeAndAwaitMethodCall(setup.MethodInfo, instance, GetParamsFromInjectionType(setup.InjectionType));
                    await InvokeAndAwaitMethodCall(testMethodInfo.MethodInfo, instance, GetParamsFromInjectionType(testMethodInfo.InjectionType));
                    testResult = new TestResult(true, type.Name, testMethodInfo.MethodInfo.Name, string.Empty);
                }
                catch (TargetInvocationException ex)
                {
                    testResult = new TestResult(false, type.Name, testMethodInfo.MethodInfo.Name, ex.InnerException?.ToString() ?? string.Empty);
                }
                finally
                {
                    if (teardown is not null) await InvokeAndAwaitMethodCall(teardown.MethodInfo, instance, GetParamsFromInjectionType(teardown.InjectionType));
                }
                yield return testResult;
            }
        }
    }

    private static IEnumerable<(Type classType, ClassTestMethodsSuite methods)> GetTestMethods()
    {

        List<(Type classType, ClassTestMethodsSuite methods)> methods = [];
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                types = ex.Types.Where(t => t != null).Select(t => t!).ToArray();
            }

            foreach (var type in types)
            {
                ClassTestMethodsSuite suite = GetTestMethods(type);
                if (suite.testMethods.Any())
                {
                    methods.Add(new(type, suite));
                }
            }
        }

        return methods;
    }

    private static ClassTestMethodsSuite GetTestMethods(Type type)
    {
        TestMethodInfo? setup = null;
        TestMethodInfo? teardown = null;

        List<TestMethodInfo> methods = new();
        foreach (var methodInfo in type.GetMethods())
        {
            TestAttribute? attribute = methodInfo.GetCustomAttribute<TestAttribute>();
            SetupAttribute? setupAttribute = methodInfo.GetCustomAttribute<SetupAttribute>();
            TeardownAttribute? teardownAttribute = methodInfo.GetCustomAttribute<TeardownAttribute>();
            if (attribute is not null)
            {
                methods.Add(new TestMethodInfo(methodInfo, attribute.InjectionType));
            }

            if (setupAttribute is not null)
            {
                setup = new TestMethodInfo(methodInfo, setupAttribute.InjectionType);
            }

            if (teardownAttribute is not null)
            {
                teardown = new TestMethodInfo(methodInfo, teardownAttribute.InjectionType);
            }
        }

        return (setup, teardown, methods.AsEnumerable());
    }

    private static async Task InvokeAndAwaitMethodCall(MethodInfo methodInfo, object? instance, object?[]? @params)
    {
        object? result = methodInfo.Invoke(instance, @params);
        if (result is Task task)
        {
            await task;
        }
    }

    private object?[]? GetParamsFromInjectionType(InjectionType injectionType)
    {
        return injectionType == InjectionType.ParentNode ? [parent] : null;
    }

}
