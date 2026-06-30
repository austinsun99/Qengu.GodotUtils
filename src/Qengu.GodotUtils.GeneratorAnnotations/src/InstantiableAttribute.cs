using System;

namespace Qengu.GodotUtils.GeneratorAnnotations;

/// <summary>
/// An attribute which implements a generator providing statically typed Instantiate function.
/// This generator will expose a static Instantiate method to other classes, which instantiates an instance
/// of the scene with snake case of this class's name. The class will may have Init function with parameters
/// which will be called automatically (with arguments) when instantiating
/// <br/>
/// <br/>
/// The parameter <paramref name="init"/> is used to specify the name of the Init function <br/>
/// The parameter <paramref name="sceneName"/> is used to specify the name of the scene. If left
/// uninitialized, it will default to the snake case of the script name. <br/>
/// Note that the scene this script is attached to must be in the same folder as the script.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
sealed public class InstantiableAttribute(string init = "Init", string? sceneName = null) : Attribute
{
    public string Init { get; } = init;
    public string? SceneName { get; } = sceneName;
}
