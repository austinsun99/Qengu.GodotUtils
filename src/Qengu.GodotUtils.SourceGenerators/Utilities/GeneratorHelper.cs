using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Qengu.GodotUtils.SourceGenerators;

public static class GeneratorHelper
{
    public static ArgumentData[] GetMethodArguments(ClassDeclarationSyntax syntax, string funcName)
    {
        MethodDeclarationSyntax initMethodSymbol =
            syntax.Members
            .OfType<MethodDeclarationSyntax>()
            .FirstOrDefault(m => m.Identifier.Text.Equals(funcName));

        if (initMethodSymbol == null) return Array.Empty<ArgumentData>();

        List<ArgumentData> initParams = [];
        foreach (ParameterSyntax param in initMethodSymbol.ParameterList.Parameters)
        {
            initParams.Add(new ArgumentData(
                        param.ToString(),
                        param.Identifier.Text,
                        param.Type?.ToString(),
                        param.Default?.Value.ToString()
                        ));
        }
        return [.. initParams];
    }

    public static class Godot
    {
        public static string AbsToResPath(string absPath, string projectDir, string? customFileName = null)
        {
            string normalizedRoot = projectDir.Replace('\\', '/').TrimEnd('/');
            string normalizedFile = absPath.Replace('\\', '/');

            if (!normalizedFile.StartsWith(normalizedRoot, StringComparison.OrdinalIgnoreCase)) return normalizedFile;

            string relative = normalizedFile.Substring(normalizedRoot.Length).TrimStart('/');
            string dir = Path.GetDirectoryName(relative);

            string fileName = Path.GetFileNameWithoutExtension(relative);
            if (customFileName != null)
                fileName = customFileName;
            return "res://" + Path.Combine(dir, StringUtilities.ToSnakeCase(fileName) + ".tscn");
        }

        public static IncrementalValueProvider<string?> GetProjectDirectory(IncrementalGeneratorInitializationContext context)
        {
            return context.AnalyzerConfigOptionsProvider
                .Select(static (provider, _) =>
                        {
                            provider.GlobalOptions.TryGetValue("build_property.GodotProjectDir", out var dir);
                            return dir;
                        });
        }

    }
}

public record ArgumentData
{
    public string FullArgument { get; }
    public string Name { get; }
    public string? Type { get; }
    public string? DefaultValue { get; }

    public ArgumentData(string fullArgument, string name, string? type, string? defaultValue)
    {
        FullArgument = fullArgument;
        Name = name;
        Type = type;
        DefaultValue = defaultValue;
    }
}
