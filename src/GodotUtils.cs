using System.Collections.Generic;
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
            if (dir.CurrentIsDir() && recursive)
            {
                var (err, recursiveFiles) = GetFilesInDirectory($"{path}/{fileName}", recursive = true);

                if (err) return (err, files);
                else files.AddRange(recursiveFiles);
            }
            else
            {
                files.Add(fileName);
            }
            fileName = dir.GetNext();
        }

        return (false, files);
    }
}
