namespace ProceduralWorldGenerator.Common.Converters
{
    public readonly struct EnumValue
    {
        public EnumValue(string name, object? value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public object? Value { get; }
    }
}