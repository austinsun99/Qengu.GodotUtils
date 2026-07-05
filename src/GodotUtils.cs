using System.Collections.Generic;
using Godot;

namespace Qengu.GodotUtils;

public static class GodotUtils
{
    /// <summary>
    /// Obtain a list of relative (to res://) file names.
    /// </summary>
    /// <param name="path">The path to search</param>
    /// <param name="recursive">Whether or not to search recursively</param>
    /// <returns>An error if the directory access failed, and the list of file names obtained</returns>
    public static (bool err, List<string> files) GetFilesInDirectory(string path, bool recursive = false)
    {
        List<string> files = [];
        using var dir = DirAccess.Open(path);

        if (dir == null) return (true, files);

        dir.ListDirBegin();
        string fileName = dir.GetNext();
        while (fileName != "")
        {
            var fullPathName = string.Format("{0}{2}{1}", path, fileName,
                            path.EndsWith("\\") || path.EndsWith("/") ? "" : "/");
            if (dir.CurrentIsDir() && recursive)
            {
                var (err, recursiveFiles) = GetFilesInDirectory(fullPathName, recursive = true);

                if (err) return (err, files);
                else files.AddRange(recursiveFiles);
            }
            else
            {
                files.Add(fullPathName);
            }
            fileName = dir.GetNext();
        }

        return (false, files);
    }

    /// <summary>
    /// Attemps to instantiate a scene, and return the value.
    /// </summary>
    /// <param name="path">The path to the scene resource</param>
    /// <typeparam name="T">The type to cast the scene to</typeparam>
    /// <returns>Whether an error occured, and the object instantiated</returns>
    public static (bool err, T? obj) InstantiateScene<T>(string path) where T : Node
    {
        PackedScene scene = GD.Load<PackedScene>(path);
        if (scene == null) return (true, null);

        T obj = scene.InstantiateOrNull<T>();
        if (obj == null) return (true, null);

        return (false, obj);
    }

}
