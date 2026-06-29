namespace Qengu.GodotUtils.SourceGenerators;

/// <summary>
/// An attribute which implements a generator providing statically typed Instantiate function.
/// This generator will expose a static Instantiate method to other classes, which instantiates an instance
/// of the scene with snake case of this class's name. The class will may have Init function with parameters
/// which will be called automatically (with arguments) when instantiating
/// The parameter <paramref name="init"/> is used to specify the name of the Init function
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
sealed public class InstantiableAttribute(string init = "Init") : Attribute
{
    public string Init { get; } = init;
}
