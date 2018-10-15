using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.world;

namespace SteeringCS.util.spatial_partitioning
{
    public class Grid
    {
        public int CellSize { get; private set; }
        public Bucket[,] Buckets { get; private set; }
        private World World { get; set; }

        public int ColumnCount
        {
            get
            {
                return World.Width / CellSize;
            }
        }
        public int RowCount
        {
            get
            {
                return World.Height / CellSize;
            }
        }

        public Grid(World w)
        {
            World = w;
            CellSize = 50;
            CreateGrid(width: World.Width, height: World.Height);
        }

        public void AdjustCellSize(int cellSize)
        {
            if (cellSize >= 5 && ((World.Width / cellSize)) > 2)
            {
                CellSize = cellSize;
                CreateGrid(width: World.Width, height: World.Height);
            }
            else
            {
                throw new ArgumentOutOfRangeException("The grid should consist of at least 2 buckets and cellSize should be >= 5.");
            }

        }

        public List<BaseGameEntity> GetAllAdjacentEntities(BaseGameEntity entity, Type type = null)
        {
            var entities = new List<BaseGameEntity>();
            var buckets = GetAllAdjacentBuckets(entity);
            if (type != null)
            {
                /*
                 * We still have a double forloop, but now this loop is over a max of 9 buckets
                 * instead of: (Columns * CellSize) + (Rows * CellSize) buckets.
                 */
                lock(buckets)
                {
                    foreach (var bucket in buckets)
                    {
                        lock (bucket.Entities)
                        {
                            for (int i = 0; i < bucket.Entities.Count; i++)
                            {
                                if (i >= bucket.Entities.Count)
                                    return entities;
                                if (i < bucket.Entities.Count)
                                {
                                    var e = bucket.Entities.ElementAt(i);
                                    if (e.GetType() == type)
                                        entities.Add(e);
                                }
                            }
                        }
                    }
                }    
            }
            else
            {
                lock(buckets)
                {
                    for(int i = 0; i < buckets.Count; i++)
                    {
                        var bucket = buckets[i];

                        lock(entity)
                        {
                            try
                            {
                                for (int j = 0; j < bucket.Entities.Count; j++)
                                {
                                    if (j > bucket.Entities.Count)
                                        break;
                                    var e = bucket.Entities[j];
                                    entities.Add(e);
                                }
                            }catch(Exception e)
                            {
                                Console.WriteLine("Error: {0}", e.Message);
                            }
                            
                        }
                    }
                }
            }

            return entities;
        }

        public List<BaseGameEntity> GetAllAdjacentEntities(int row, int column)
        {
            var entities = new List<BaseGameEntity>();
            var buckets = GetAllAdjacentBuckets(row, column);

            buckets.ForEach(b => entities.AddRange(b.Entities));

            return entities;
        }

        public List<Bucket> GetAllAdjacentBuckets(int row, int column)
        {
            var buckets = new List<Bucket>();
            Bucket bucket;

            if (IsValidBucket(new Tuple<int, int>(row, column)))
                bucket = GetBucket(row, column);
            else
                return new List<Bucket>();

            var tuples = new List<Tuple<int, int, string>>
            {
                // current
                new Tuple<int, int, string>(row, column, "current"),

                // top
                new Tuple<int, int, string>(row - 1, column, "top"),
                // topleft
                new Tuple<int, int, string>(row - 1, column - 1, "top\nleft"),
                // topright
                new Tuple<int, int, string>(row - 1, column + 1, "top\nright"),

                // left
                new Tuple<int, int, string>(row, column - 1, "left"),
                // right
                new Tuple<int, int, string>(row, column + 1, "right"),

                // bottom
                new Tuple<int, int, string>(row + 1, column, "bottom"),
                // bottomleft
                new Tuple<int, int, string>(row + 1, column - 1, "bottom\nleft"),
                // bottomright
                new Tuple<int, int, string>(row + 1, column + 1, "bottom\nright"),
            };

            try
            {
                foreach (var tuple in tuples)
                {
                    var valid = IsValidBucket(new Tuple<int, int>(tuple.Item1, tuple.Item2));
                    if (valid)
                    {
                        var tupleBucket = GetBucket(tuple.Item1, tuple.Item2);
                        tupleBucket.Name = tuple.Item3;
                        buckets.Add(tupleBucket);
                    }
                    else
                    {
                        Console.WriteLine(string.Format("@ {0};{1} is not a valid bucket", tuple.Item1, tuple.Item2));
                    }
                }
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Console.WriteLine("Column or row doesn't exist: {0}", exception.ToString());
            }

            return buckets;
        }

        public List<Bucket> GetAllAdjacentBuckets(BaseGameEntity entity)
        {
            var buckets = new List<Bucket>();
            var column = (int)entity.Pos.X / CellSize;
            var row = (int)entity.Pos.Y / CellSize;
            Bucket bucket;

            if (IsValidBucket(new Tuple<int, int>(row, column)))
                bucket = GetBucket(row, column);
            else
                return new List<Bucket>();

            var tuples = new List<Tuple<int, int, string>>
            {
                // current
                new Tuple<int, int, string>(row, column, "current"),

                // top
                new Tuple<int, int, string>(row - 1, column, "top"),
                // topleft
                new Tuple<int, int, string>(row - 1, column - 1, "top\nleft"),
                // topright
                new Tuple<int, int, string>(row - 1, column + 1, "top\nright"),

                // left
                new Tuple<int, int, string>(row, column - 1, "left"),
                // right
                new Tuple<int, int, string>(row, column + 1, "right"),

                // bottom
                new Tuple<int, int, string>(row + 1, column, "bottom"),
                // bottomleft
                new Tuple<int, int, string>(row + 1, column - 1, "bottom\nleft"),
                // bottomright
                new Tuple<int, int, string>(row + 1, column + 1, "bottom\nright"),
            };

            try
            {
                foreach (var tuple in tuples)
                {
                    var valid = IsValidBucket(new Tuple<int, int>(tuple.Item1, tuple.Item2));
                    if (valid)
                    {
                        var tupleBucket = GetBucket(tuple.Item1, tuple.Item2);
                        tupleBucket.Name = tuple.Item3;
                        buckets.Add(tupleBucket);
                    }
                    else
                    {
                        Console.WriteLine(string.Format("@ {0};{1} is not a valid bucket", tuple.Item1, tuple.Item2));
                    }
                }
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Console.WriteLine("Column or row doesn't exist: {0}", exception.ToString());
            }

            return buckets;
        }

        public void AddToBucket(Point point, BaseGameEntity entity)
        {
            GetBucket(point.X, point.Y).AddEntity(entity);
        }

        /// <summary>
        /// Add an entity to it's specific bucket.
        /// </summary>
        /// <param name="entity"></param>
        public void AddToBucket(BaseGameEntity entity)
        {
            int row = (int)entity.Pos.Y / CellSize;
            int col = (int)entity.Pos.X / CellSize;

            if (IsValidBucket(new Tuple<int, int>(row, col)))
            {
                var bucket = GetBucket(row, col);
                bucket.AddEntity(entity);
            }
        }

        private bool IsValidBucket(Tuple<int, int> tuple)
        {
            if (tuple.Item1 < 0 || tuple.Item1 >= RowCount)
                return false;
            if (tuple.Item2 < 0 || tuple.Item2 >= ColumnCount)
                return false;

            return true;
        }

        public void ResetGrid()
        {
            // Reset all buckets
            for(int row = 0; row < RowCount; row++)
            {
                for(int column = 0; column < ColumnCount; column++)
                {
                    Buckets[row, column].Reset();
                }
            }

            CreateGrid(World.Width, World.Height);
        }

        /// <summary>
        /// Create a grid for the Game World.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void CreateGrid(int width, int height)
        {
            Buckets = new Bucket[RowCount, ColumnCount];

            for (int row = 0; row < RowCount; row++)
            {
                for (int column = 0; column < ColumnCount; column++)
                {
                    Buckets[row, column] = new Bucket(new Tuple<int, int>(row, column));
                }
            }
        }

        /// <summary>
        /// Get the bucket for a specified entity.
        /// </summary>
        /// <param name="entity"></param>
        public Bucket GetBucket(BaseGameEntity entity)
        {
            int column = (int)entity.Pos.X / CellSize;
            int row = (int)entity.Pos.Y / CellSize;

            if (IsValidBucket(new Tuple<int, int>(column, row)))
            {
                if (column <= Buckets.GetLength(0) && row <= Buckets.GetLength(1))
                    return Buckets[column, row];
                else
                    throw new ArgumentOutOfRangeException("Row or column does not exist.");
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Get the bucket for a specified row & column.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public Bucket GetBucket(int row, int column)
        {
            if (row <= Buckets.GetLength(0) && column <= Buckets.GetLength(1))
                return Buckets[row, column];
            else
                throw new ArgumentOutOfRangeException("Row or column does not exist.");
        }
    }
}
