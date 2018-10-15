using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS;
using SteeringCS.world;

namespace SteeringCS.controllers
{
    public class KeyboardController
    {
        public World World { get; private set; }
        
        public KeyboardController() { World = WorldSingleton.Instance; }

        /// <summary>
        /// Handle a given key.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleKey(object sender, EventArgs e)
        {

        }
    }
}
