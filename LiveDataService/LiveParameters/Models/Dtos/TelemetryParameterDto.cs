namespace LiveDataService.LiveParameters.Models.Dtos
{
    public class TelemetryParameterDto
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Units { get; set; }
        public TelemetryParameterDto(string name, string value, string units)
        {
            Name = name;
            Value = value;
            Units = units;
        }
    }
}
