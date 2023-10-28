namespace Nbs.Config;

/// <summary>
/// Manages nbs.config files.
/// </summary>
public static class NbsManager
{
    /// <summary>
    /// Whether or not to output the Log to the terminal.
    /// </summary>
    public static bool Output = true;

    /// <summary>
    /// Parses an nbs.config format file and returns a Config object.
    /// </summary>
    /// <param name="targetFilePath">The full path of the file including the extension if one was supplied.</param>
    /// <returns>A Config object containg the key-value pairs found in the config.nbs file. Returns a new Config object if no file is found.</returns>
    public static Config Load(string targetFilePath = "config.nbs")
    {
        var config = new Config();

        if (File.Exists(targetFilePath))
        {
            foreach (var line in File.ReadAllLines(targetFilePath))
            {
                if (line.Trim().StartsWith('#') || line.Trim() == string.Empty)
                {
                    continue;
                }
                else
                {
                    var parts = line.Split('=');
                    if (parts.Length != 2)
                    {
                        Log("Problem reading file, format is incorrect or contains error.");
                    }
                    else
                    {
                        var property = new Property(parts[0].Trim(), parts[1].Trim());
                        Log(property.ToString());
                        config.Add(property);
                    }
                }
            }
            return config;
        }
        else
        {
            Log($"The provided file path was not found: {targetFilePath}");
            Log("Returned a new Config object.");
            return new Config();
        }
    }

    /// <summary>
    /// Saves the file, overwriting the file if it already exists.
    /// </summary>
    /// <param name="config">The config to save.</param>
    /// <param name="path">The path to the file including the file extension, if present.</param>
    /// <returns>True, if the file saved succesfully. False if the file failed to save.</returns>
    public static bool Save(Config config, string path = "./nbs.config")
    {
        if (config == null)
        {
            Log("Cannot save the contents of a null config.");
            return false;
        }
        else
        {
            try
            {
                using (TextWriter tw = File.CreateText(path))
                {
                    foreach (Property property in config.Properties)
                    {
                        tw.WriteLine(property.ToString());
                    }
                }

                Log($"Saved config to: {path}");
                return true;
            }
            catch (IOException)
            {
                Log($"Writing to the file failed. Is the file being used by another program?");
                return false;
            }
        }
    }

    /// <summary>
    /// Formats the ouput string with the NBS tag before outputting it.
    /// </summary>
    /// <param name="text">Message to display.</param>
    internal static void Log(string text)
    {
        if (Output)
        {
            Console.WriteLine($"[NBS]: {text}");
        }
    }
}
