using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGameLogic
{
    public struct MemoryGameCardCords
    {
        //public MemoryGameCardCords CardCords { get; set; }

        public int Y { get; set; }
        public int X { get; set; }

        public MemoryGameCardCords(int i_Y, int i_X)
        {
            Y = i_Y;
            X = i_X;
        }

        // Implicit conversion from MemoryGameCardCords to Pair
        public static implicit operator MemoryGameCardCords((int,int) i_Cords)
        {
            return new MemoryGameCardCords(i_Cords.Item1, i_Cords.Item2);
        }

        // Implicit conversion from Pair to MemoryGameCardCords
        public static implicit operator (int,int)(MemoryGameCardCords i_Cords)
        {
            return (i_Cords.Y, i_Cords.X);
        }
    }
}
