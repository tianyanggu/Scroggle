using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scroggle
{
    class Program
    {
        //-----------------RULES----------------------------

        //Words greater than 4 chars
        //must be adjacent cells, e.g. horizontal, vertical, and diagonal
        //each letter cub only used once

        //Points are as follows:
        //length: 4-1, 5-2, 6-3, 7-4, 8+-5
        //multipliers: q-3x, z&k-2x
        //cell multipliers: 2x or 3x only any cell, if word has letter on that cell. e.g. quiz on 2x multiplier cell is (1*2*3)*2=12 points

        //-----------------INPUT----------------------------
        private string[] dictionary = {"bred", "yore", "byre", "abed", "oread", "bore", "orby", "robed", "broad", "byroad", "robe", "bored", "derby", "bade", "aero", "read", "orbed", "verb", "aery", "bead", "bread", "very", "road", "dove", "robbed", "robber"};

        private char[,] board = { {'y', 'o', 'x'},
                                  {'r', 'b', 'a'},
                                  {'v', 'e', 'd'} };

        //-----------------CODE----------------------------


        //-----------------OUTPUT----------------------------
        static void Main(string[] args)
        {
            Console.WriteLine("Words","Points");
            Console.ReadLine();
        }
    }
}
