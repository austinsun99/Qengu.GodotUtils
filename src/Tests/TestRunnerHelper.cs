using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Qengu.GodotUtils.Tests;

public static class TestRunnerHelper
{
    public static IEnumerable<(Type classType, IEnumerable<MethodInfo> methods)> GetTestMethods(string pathToTestFile)
    {
        List<(Type classType, IEnumerable<MethodInfo> methods)> methods = [];

        IEnumerable<Type> classes =
            Assembly.LoadFrom(pathToTestFile)
            .GetTypes()
            .Where(x => x.IsClass && x.IsPublic);

        foreach (var type in classes)
        {
            IEnumerable<MethodInfo> testMethods = GetTestMethods(type);
            if (testMethods.Any())
            {
                methods.Add((type, testMethods));
            }
        }

        return methods;
    }

    public static IEnumerable<MethodInfo> GetTestMethods(Type type)
    {
        return type
            .GetMethods()
            .Where(m => m.CustomAttributes
            .Any(a => a.AttributeType == typeof(TestAttribute)));
    }

}
