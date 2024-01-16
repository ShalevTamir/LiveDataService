using System.Collections.Generic;
using System;

namespace LiveDataService.LiveParameters.Models.Dtos
{
    public class TelemetryFrameDto
    {
        public int FrameId { get; set; }
        public DateTime TimeStamp { get; set; }
        public IEnumerable<TelemetryParameterDto> Parameters { get; set; }
    }
}
