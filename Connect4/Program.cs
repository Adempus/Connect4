using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class ConnectFour
    {   //create basic visual pattern
        public static String[,] DrawBoard()
        {
            String[,] f = new String[7, 15];

            //Loop over each row from up to down
            for (int i = 0; i < f.GetLength(0); i++)
            {
                for (int j = 0; j < f.GetLength(1); j++)
                {
                    if (j % 2 == 0) f[i, j] = "|";
                    else f[i, j] = " ";
                    //now make the lowest row
                    if (i == 6) f[i, j] = "-";
                }
            }
            return f;
        }

        public static void PrintPattern(String[,] f)
        {
            for (int i = 0; i < f.GetLength(0); i++)
            {
                for (int j = 0; j < f.GetLength(1); j++)
                {
                    if (f[i, j].Equals("R"))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else if (f[i, j].Equals("Y"))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write(f[i, j]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        public static void DropRedPattern(String[,] f)
        {   //Prompt user to enter column to drop color into:
            Console.Write("Drop a red piece at column (1-7): ");
            try
            {
                int column = (2 * Convert.ToInt32(Console.ReadLine()) - 1);
                for (int i = 5; i >= 0; i--)
                {
                    if (f[i, column].Equals(" "))
                    {
                        f[i, column] = "R";
                        break;
                    }
                }
            }
            catch (System.IndexOutOfRangeException)
            {
                Console.WriteLine("Out of bounds. Try again. \n");
                DropRedPattern(f);
            }
            catch(System.FormatException)
            {
                Console.WriteLine("Invalid column. Try again. \n");
                DropRedPattern(f);
            }
        }

        public static void DropYellowPattern(String[,] f)
        {   // same method above implemented here, but for yellow.
            Console.Write("Drop a yellow piece at column: (1-7): ");
            try
            {
                int column = (2 * Convert.ToInt32(Console.ReadLine()) - 1);
                for (int i = 5; i >= 0; i--)
                {
                    if (f[i, column].Equals(" "))
                    {
                        f[i, column] = "Y";
                        break;
                    }
                }
            }
            catch (System.IndexOutOfRangeException)
            {
                Console.WriteLine("Out of bounds. Try again. \n");
                DropYellowPattern(f);
            }
            catch (System.FormatException)
            {
                Console.WriteLine("Invalid column. Try again. \n");
                DropYellowPattern(f);
            }
        }

        public static String checkWinner(String[,] f)
        {   //check the first winning line
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j += 2)
                {
                    if ((f[i, j + 1] != " ")
                        && (f[i, j + 3] != " ")
                        && (f[i, j + 5] != " ")
                        && (f[i, j + 7] != " ")
                        && ((f[i, j + 1] == f[i, j + 3])
                        && (f[i, j + 3] == f[i, j + 5])
                        && (f[i, j + 5] == f[i, j + 7])))

                        return f[i, j + 1];
                }
            }

            for (int i = 1; i < 15; i += 2)
            {
                for (int j = 0; j < 3; j++)
                {
                    if ((f[j, i] != " ")
                        && (f[j + 1, i] != " ")
                        && (f[j + 2, i] != " ")
                        && (f[j + 3, i] != " ")
                        && ((f[j, i] == f[j + 1, i])
                        && (f[j + 1, i] == f[j + 2, i])
                        && (f[j + 2, i] == f[j + 3, i])))

                        return f[j, i];
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 1; j < 9; j += 2)
                {
                    if ((f[i, j] != " ")
                        && (f[i + 1, j + 2] != " ")
                        && (f[i + 2, j + 4] != " ")
                        && (f[i + 3, j + 6] != " ")
                        && ((f[i, j] == f[i + 1, j + 2])
                        && (f[i + 1, j + 2] == f[i + 2, j + 4])
                        && (f[i + 2, j + 4] == f[i + 3, j + 6])))

                        return f[i, j];
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 7; j < 15; j += 2)
                {
                    if ((f[i, j] != " ")
                        && (f[i + 1, j - 2] != " ")
                        && (f[i + 2, j - 4] != " ")
                        && (f[i + 3, j - 6] != " ")
                        && ((f[i, j] == f[i + 1, j - 2])
                        && (f[i + 1, j - 2] == f[i + 2, j - 4])
                        && (f[i + 2, j - 4] == f[i + 3, j - 6])))

                        return f[i, j];
                }
            } return null;
        }

        static void Main(String[] args)
        {
            var rematch = 'Y';
            int YellowTally = 0, RedTally = 0;

            do
            {
                String[,] f = DrawBoard();
                PrintPattern(f);
                int count = 0;

                Boolean gameLoop = true;
                while (gameLoop)
                {
                    if (count % 2 == 0) DropRedPattern(f);
                    else DropYellowPattern(f);
                    count++; PrintPattern(f);

                    if (checkWinner(f) != null)
                    {
                        if ((checkWinner(f)).Equals("R"))
                        {
                            Console.Write("Red player wins!\n");
                            RedTally++;
                        }
                        else if ((checkWinner(f)).Equals("Y"))
                        {
                            Console.Write("Yellow player wins!\n");
                            YellowTally++;
                        }
                        gameLoop = false;
                    }
                }

                Console.WriteLine("\n----------Score------------" +
                    "\n  Red: " + RedTally + "    |" + "  Yellow: " + YellowTally);
                Console.Write("\n      Rematch? Y/N: ");
                rematch = Convert.ToChar(Console.ReadLine());
                Console.WriteLine("---------------------------\n");

            } while (rematch.Equals('y') || rematch.Equals('Y'));
        }
    }
}