/// <summary>
/// Represents a single property.
/// </summary>
public class Property
{
    /// <summary>
    /// The name of the property.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The value of the property.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Creates a new property.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="value">The value of the property.</param>
    /// <exception cref="ArgumentException"></exception>
    public Property(string name, string value)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Name cannot be null or empty.", nameof(name));

        if (string.IsNullOrEmpty(value))
            throw new ArgumentException("Value cannot be null or empty.", nameof(value));

        Name = name.Trim();
        Value = value.Trim();
    }

    /// <summary>
    /// The string representation of the property.
    /// </summary>
    /// <returns>A correctly formatted property as a string.</returns>
    public override string ToString()
    {
        return $"{Name}={Value}";
    }
}
