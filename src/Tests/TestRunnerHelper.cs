using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Qengu.GodotUtils.Tests;

public static class TestRunnerHelper
{
    public static IEnumerable<(Type classType, IEnumerable<MethodInfo> methods)> GetTestMethods()
    {
        List<(Type classType, IEnumerable<MethodInfo> methods)> methods = [];

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
                IEnumerable<MethodInfo> testMethods = GetTestMethods(type);
                if (testMethods.Any())
                {
                    methods.Add((type, testMethods));
                }
            }
        }

        return methods;
    }

    public static IEnumerable<MethodInfo> GetTestMethods(Type type)
    {
        return type
            .GetMethods()
            .Where(m => m.GetCustomAttribute<TestAttribute>() != null);
    }

}
