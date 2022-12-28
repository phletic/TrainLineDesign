namespace TrainLineDesigner
{
    public class Map<T> where T: struct
    {
        public int Get1DCoordinate(int x, int y, int MapWidth)
        {
            return MapWidth * y + x;
        }

        public Dictionary<int, T> ChunkMap {get; set;}
        public int MapWidth;
        public Map(int MapWidth)
        {
            ChunkMap = new Dictionary<int, T>();
            this.MapWidth = MapWidth;
        }

        public Map()
        {
            ChunkMap = new Dictionary<int, T>();
        }

        public void WriteChunk(int x, int y, T data)
        {
            ChunkMap.Add(Get1DCoordinate(x,y,MapWidth), data);
        }

        public T ReadChunk(int x, int y)
        {
            int target = Get1DCoordinate(x,y,MapWidth);
            if(ChunkMap.ContainsKey(target))
            {
                return ChunkMap[target];
            }else{
                return default(T);
            }
        }

			        public T ReadChunk(int xy)
        {
            if(ChunkMap.ContainsKey(xy))
            {
                return ChunkMap[xy];
            }else{
                return default(T);
            }
        }


        public void WriteMap(int[] x, int[] y, T[] data, int MapWidth = 0)
        {
					if(MapWidth != 0){
						this.MapWidth = MapWidth;
					}
            if (x.Length == y.Length && y.Length == data.Length)
            {
                for (int i = 0; i < x.Length; i++)
                {
                    WriteChunk(x[i], y[i], data[i]);
                }
            }
            else
            {
                throw new Exception("WriteMap array params must be equal in length!");
            }
        }
    }
}