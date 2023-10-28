using System.Net.Mail;

namespace Nbs.Config;

public class Config
{
    private List<Property> properties = new List<Property>();
    public IReadOnlyList<Property> Properties
    {
        get => properties.AsReadOnly();
    }

    public Config() { }

    /// <summary>
    /// Adds a property to the config.
    /// </summary>
    /// <param name="property">The property to add.</param>
    public void Add(Property property)
    {
        if (Properties.Any(p => p.Name == property.Name))
        {
            NbsManager.Log(
                $"Skipped adding [{property.Name}] - A property with that name already exists."
            );
        }
        else
        {
            properties.Add(property);
            NbsManager.Log($"Added [{property.Name}]");
        }
    }

    /// <summary>
    /// Removes a property from the config based on its name, if it exists. Will not throw an error if the property doesn't exist.
    /// </summary>
    /// <param name="propertyName">The property name to remove.</param>
    public void Remove(string propertyName)
    {
        properties.RemoveAll(p => p.Name == propertyName);
        NbsManager.Log($"Removed [{propertyName}] if it existed.");
    }

    /// <summary>
    /// Prints a list of all current properties defined in the config, and their values.
    /// </summary>
    public override string ToString()
    {
        var lines = properties.Select(p => p.ToString()).ToList();
        lines.Insert(0, "nbs.config-------------------------------------");
        lines.Add("-----------------------------------------------");

        return string.Join('\n', lines);
    }
}
