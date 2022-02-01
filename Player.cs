using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolDesktopApp
{
    class Player
    {

        public int PlayerId { get; set; }
        public string BallType { get; set; }
        public string Name { get; set; }



        public Player(int playerId, string ballType, string name)
        {
            PlayerId = playerId;
            BallType = ballType;
            Name = name;
        }
    }
}
