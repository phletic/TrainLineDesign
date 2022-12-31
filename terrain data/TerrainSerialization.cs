using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace TrainLineDesigner{
    
    class TerrainSerializer{
        Terrain? terrain;
        string fileName;

        public TerrainSerializer(string fileName, Terrain terrain){
            this.fileName = fileName;
            this.terrain = terrain;
        }
        public TerrainSerializer(string fileName){
            this.fileName = fileName;
        }

        public Terrain Read(){
            string jsonString = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<Terrain>(jsonString);
        }

        public void Write(){
            Console.WriteLine(fileName);
            using FileStream Stream = File.Create(fileName);
            JsonSerializer.SerializeAsync(Stream, terrain/*,serializeOptions*/);
            Stream.DisposeAsync();
        }
    }

    // TODO: Add a converter for Dictionary<Location, T>
    public class LocationJSONConverter : JsonConverter<Location>
    {
        public override Location Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) => Location.Create(reader.GetString()!);

        public override void Write(
            Utf8JsonWriter writer,
            Location val,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(val.position.ToString());
    }

    /*
                var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters =
                {
                    new LocationJSONConverter()
                }
            };
    */

    
}

// output:
//{"Date":"2019-08-01T00:00:00-07:00","TemperatureCelsius":25,"Summary":"Hot"}