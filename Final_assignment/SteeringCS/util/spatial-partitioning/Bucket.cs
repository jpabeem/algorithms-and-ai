using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.util.spatial_partitioning
{
    public class Bucket
    {
        public List<BaseGameEntity> Entities { get; private set; }

        // holds the Column & Row position
        public Tuple<int, int> Coords;

        public string Name { get; set; }

        public int Column
        {
            get
            {
                return Coords.Item1;
            }
        }

        public int Row
        {
            get
            {
                return Coords.Item2;
            }
        }

        public Bucket(Tuple<int, int> coords)
        {
            Coords = coords;
            Entities = new List<BaseGameEntity>();
        }

        public void Reset()
        {
            Entities.Clear();
        }

        public void AddEntity(BaseGameEntity entity)
        {
            if (Entities == null)
                Entities = new List<BaseGameEntity>();

            Entities.Add(entity);
        }
    }
}
