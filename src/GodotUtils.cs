using System.Collections.Generic;
using System.Threading.Tasks;
using Godot;

namespace Qengu.GodotUtils;

public static class GodotUtils
{
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

    public static (bool err, T? obj) InstantiateScene<T>(string path) where T : Node
    {
        PackedScene scene = GD.Load<PackedScene>(path);
        if (scene == null) return (true, null);

        T obj = scene.InstantiateOrNull<T>();
        if (obj == null) return (true, null);

        return (false, obj);
    }

}
