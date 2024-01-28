using LiveDataService.LiveParameters.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace LiveDataService.LiveParameters.Services
{
    public class JsonUtilsService
    {
        public IEnumerable<IcdParameter> DeserializeIcdFile()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "LiveParameters/Documents/FlightBox.json");
            string jsonText = File.ReadAllText(path);
            var deserializedList = JsonConvert.DeserializeObject<IcdParameter[]>(jsonText);
            return deserializedList;
        }
    }
}
