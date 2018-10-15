using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.world
{
    /// <summary>
    /// Singleton class for "World".
    /// </summary>
    public sealed class WorldSingleton
    {
        public static DBPanel Panel { get; set; }
        private static volatile World instance;
        private static object syncRoot = new Object();

        private WorldSingleton() { }

        public static World Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new World(Panel.Width, Panel.Height);
                    }
                }

                return instance;
            }
        }
    }
}
