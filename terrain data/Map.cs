namespace TrainLineDesigner
{
    public class Map<T> where T: struct
    {

        /*
        public int Get1DCoordinate(int x, int y, int MapWidth)
        {
            return MapWidth * y + x;
        }
        */

        public Dictionary<Location, T> ChunkMap {get; set;}
        public int MapWidth {get; set;}
        public Map(int MapWidth)
        {
            ChunkMap = new Dictionary<Location, T>();
            this.MapWidth = MapWidth;
        }

        public Map()
        {
            ChunkMap = new Dictionary<Location, T>();
        }

        public void WriteChunk(Location L, T data)
        {
            ChunkMap.Add(L, data);
        }

        public T ReadChunk(Location L)
        {
            if(ChunkMap.ContainsKey(L))
            {
                return ChunkMap[L];
            }else{
                return default(T);
            }
        }


        public void WriteMap(Location[] L, T[] data, int MapWidth = 0)
        {
					if(MapWidth != 0){
						this.MapWidth = MapWidth;
					}
            if (L.Length==data.Length)
            {
                for (int i = 0; i < L.Length; i++)
                {
                    WriteChunk(L[i], data[i]);
                }
            }
            else
            {
                throw new Exception("WriteMap array params must be equal in length!");
            }
        }
    }
}