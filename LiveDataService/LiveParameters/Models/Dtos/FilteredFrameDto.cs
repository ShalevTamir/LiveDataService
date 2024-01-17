using System;
using System.Collections.Generic;

namespace LiveDataService.LiveParameters.Models.Dtos
{
    public class FilteredFrameDto
    {
        public IEnumerable<TelemetryParameterDto> Parameters { get; set; }
        public long TimeStamp { get; set; }

    }
}
