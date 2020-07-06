using System;
using System.IO;
using System.Reflection;

public static class Env
{
    public static void Load()
    {
        var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        var env = Path.GetFullPath(Path.Combine(path, ".env"));

        if (File.Exists(env))
        {            
            DotNetEnv.Env.Load(env);
        }
    }

    public static string GetString(string key)
    {
        return Environment.GetEnvironmentVariable(key);
    }

    public static int GetInteger(string key)
    {
        int result = -1;
        int.TryParse(Environment.GetEnvironmentVariable(key), out result);

        return result;
    }
}
