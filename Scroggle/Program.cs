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
        //each letter can only used once

        //Points are as follows:
        //length: 4-1, 5-2, 6-3, 7-4, 8+-5
        //multipliers: q-3x, z&k-2x
        //cell multipliers: 2x or 3x only any cell, if word has letter on that cell. e.g. quiz on 2x multiplier cell is (1*2*3)*2=12 points

        //-----------------INPUT----------------------------
        //Hashset used so Contains only uses O(1) time
        private static HashSet<string> dictionary = new HashSet<string> {"bred", "yore", "byre", "abed", "oread", "bore", "orby", "robed", "broad", "byroad", "robe", "bored", "derby", "bade", "aero", "read", "orbed", "verb", "aery", "bead", "bread", "very", "road", "dove", "robbed", "robber"};

        private static char[,] board = { {'y', 'o', 'x'},
                                         {'r', 'b', 'a'},
                                         {'v', 'e', 'd'} };

        private static int[,] multiplierBoard = { {1, 1, 2},
                                                  {1, 1, 1},
                                                  {1, 1, 1} };

        //-----------------CODE----------------------------
        //Call Solver and access these two values if part of larger program
        public HashSet<string> solvedWords = new HashSet<string>();
        public int totalScore;

        private int width = 0;
        private int height = 0;
        private int size = 0;

        //Gets dimensions of the board and recurses through the board in every direction until a word is found or size of array is reached
        //If word is found, adds the value of the points into the total
        public void Solver (char[,] board, int[,] multiplierBoard)
        {
            width = board.GetLength(0);
            height = board.GetLength(1);
            size = width * height;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    string firstLetter = board[i, j].ToString();
                    SolverHelper(i, j, firstLetter, 1, new bool[width, height]);
                }
            }

            Display(solvedWords, totalScore);
        }

        private void SolverHelper (int i, int j, string currWord, int multiplier, bool[,] visited)
        {
            visited[i, j] = true;

            //for each visited letter, recurse through each direction
            for (int vi = 0; vi < width; vi++)
            {
                for (int vj = 0; vj < height; vj++)
                {
                    if (visited[vi, vj])
                    {
                        //values of i and j for the 8 letters around a given letter
                        //starts from upper left to right, central left and right, bottom left to right
                        int[] directionsi = { vi - 1, vi - 1, vi - 1, vi, i, vi + 1, vi + 1, vi + 1 };
                        int[] directionsj = { vj - 1, vj, vj + 1, vj - 1, vj + 1, vj - 1, vj, vj + 1 };

                        //for each of the 8 directions, add the letter to the existing word and see if it produces a word in the dictionary
                        for (int n = 0; n < 8; n++)
                        {
                            if (directionsi[n] >= 0 && directionsi[n] < width && directionsj[n] >= 0 && directionsj[n] < height)
                            {
                                //checks if letter has been visited before, if so then do nothing.
                                if (!visited[directionsi[n], directionsj[n]])
                                {
                                    //clone the visited array for recursion
                                    bool[,] newVisited = (bool[,]) visited.Clone();
                                    newVisited[directionsi[n], directionsj[n]] = true;

                                    //determine multiplier
                                    int newMultiplier = multiplier;
                                    if (board[directionsi[n], directionsj[n]] == 'q')
                                    {
                                        newMultiplier *= 3;
                                    } else if (board[directionsi[n], directionsj[n]] == 'z' || board[directionsi[n], directionsj[n]] == 'k')
                                    {
                                        newMultiplier *= 2;
                                    }
                                    newMultiplier *= multiplierBoard[directionsi[n], directionsj[n]];

                                    string newWord = currWord + board[directionsi[n], directionsj[n]];

                                    //if newWord is in Dictionary, calculate points and add to totalScore and add to the solvedWords hashset
                                    if (newWord.Length >= 4 && dictionary.Contains(newWord))
                                    {
                                        if (!solvedWords.Contains(newWord))
                                        {
                                            solvedWords.Add(newWord);
                                            Console.WriteLine("...");
                                            if (newWord.Length < 8)
                                            {
                                                totalScore += (newWord.Length - 3) * newMultiplier;
                                            }
                                            else
                                            {
                                                totalScore += 5 * newMultiplier;
                                            }
                                        }
                                        SolverHelper(i, j, newWord, newMultiplier, newVisited);
                                    } else
                                    {
                                        SolverHelper(i, j, newWord, newMultiplier, newVisited);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //Prints out all the solved words from the hashset solvedWords
        private void Display(HashSet<string> solvedWords, int points)
        {
            Console.WriteLine("Words:");
            foreach (string i in solvedWords)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine(Environment.NewLine + Environment.NewLine + "Points:" + Environment.NewLine + totalScore);
        }

        //-----------------OUTPUT----------------------------
        static void Main(string[] args)
        {
            Program solverInstance = new Program();
            solverInstance.Solver(board, multiplierBoard);
            Console.ReadLine();
        }
    }
}
