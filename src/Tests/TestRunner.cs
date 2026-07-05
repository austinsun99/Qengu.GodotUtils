using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Godot;

namespace Qengu.GodotUtils.Tests;

public sealed class TestRunner
{

    public async IAsyncEnumerable<TestResult> RunTests(Node parent)
    {
        IEnumerable<(Type type, IEnumerable<TestMethodInfo> testMethods)> testClasses = GetTestMethods();
        foreach (var (type, testMethods) in testClasses)
        {
            var instance = Activator.CreateInstance(type);
            foreach (var testMethodInfo in testMethods)
            {
                TestResult testResult;
                try
                {
                    object? result = testMethodInfo.MethodInfo.Invoke(instance, testMethodInfo.InjectionType == InjectionType.ParentNode ? [parent] : null);

                    if (result is Task task)
                    {
                        await task;
                    }
                    testResult = new TestResult(true, type.Name, testMethodInfo.MethodInfo.Name, string.Empty);
                }
                catch (TargetInvocationException ex)
                {
                    testResult = new TestResult(false, type.Name, testMethodInfo.MethodInfo.Name, ex.InnerException?.Message ?? string.Empty);
                }
                yield return testResult;
            }
        }
    }

    private static IEnumerable<(Type classType, IEnumerable<TestMethodInfo> methods)> GetTestMethods()
    {
        List<(Type classType, IEnumerable<TestMethodInfo> methods)> methods = [];

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
                IEnumerable<TestMethodInfo> testMethods = GetTestMethods(type);
                if (testMethods.Any())
                {
                    methods.Add(new(type, testMethods));
                }
            }
        }

        return methods;
    }

    private static IEnumerable<TestMethodInfo> GetTestMethods(Type type)
    {
        List<TestMethodInfo> methods = new();
        foreach (var methodInfo in type.GetMethods())
        {
            TestAttribute? attribute = methodInfo.GetCustomAttribute<TestAttribute>();
            if (attribute is not null)
            {
                methods.Add(new TestMethodInfo(methodInfo, attribute.InjectionType));
            }
        }
        return methods.AsEnumerable();
    }

}
