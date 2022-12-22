using System.Text.Json;
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
            using FileStream Stream = File.Create(fileName);
            JsonSerializer.SerializeAsync(Stream, terrain);
            Stream.DisposeAsync();
        }
    }
}

// output:
//{"Date":"2019-08-01T00:00:00-07:00","TemperatureCelsius":25,"Summary":"Hot"}