using System.ComponentModel.DataAnnotations.Schema;

namespace slidingpuzzleconsole
{
    internal class Program
    {
        static bool Swappable(int[,] matris, int number)
        {
            int numRow = -1, numCol = -1;
            int zeroRow = -1, zeroCol = -1;

            for(int i = 0;i < 3; i++)
            {
                for(int j = 0;j < 3;j++)
                {
                    if (matris[i,j] == number)
                    {
                        numRow = i;
                        numCol = j;
                    }
                    if (matris[i,j] == 0)
                    {
                        zeroRow = i;
                        zeroCol = j;
                    }
                }
            }

            if((Math.Abs(numRow - zeroRow) == 1 && numCol == zeroCol ) ||
                (Math.Abs(numCol - zeroCol) == 1 && numRow == zeroRow))
            {
                return true;
            }

            return false;
        }
        static bool DogruMu(int[,] matris)
        {
            int sayi = 0;

            for(int i = 0; i < 3;i++)
            {
                for(int j = 0; j < 3;j++)
                {
                    if(i== 2 && j == 2)
                    {
                        return true;
                    }
                    if (matris[i,j] == sayi)
                    {
                        return false;
                    }
                    sayi++;
                }
            }
            return true;
        }
        static void Main(string[] args)
        {
            int[] sayilar = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            Random rnd = new Random();
            int numCol = -1, numRow = -1, zeroCol = -1, zeroRow = -1;

            for (int i = sayilar.Length - 1; i > 0; i--)
            {
                int r = rnd.Next(0, i + 1);

                int temp = sayilar[i];
                sayilar[i] = sayilar[r];
                sayilar[r] = temp;
            }

            int[,] matris = new int[3, 3];
            int index = 0;

            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    matris[i, j] = sayilar[index];
                    index++;
                }
            }
            do
            {
                for(int i = 0;i < 3;i++)
                {
                    Console.Write("{ ");
                    for(int j = 0; j < 3;j++)
                    {
                        Console.Write(matris[i, j] + " ");
                        if (matris[i,j] == 0)
                        {
                            int zero = matris[i, j];
                            zeroRow = i;
                            zeroCol = j;
                        }
                    }
                    Console.WriteLine("}");
                }
                Console.WriteLine();
                Console.Write("Please enter the number u wanna move: ");
                int number = int.Parse(Console.ReadLine());
                
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (matris[i,j] == number)
                        {
                            numRow = i;
                            numCol = j;
                        }
                    }
                }



                if (Swappable(matris, number))
                {
                    int temp = matris[zeroRow,zeroCol];
                    matris[zeroRow, zeroCol] = matris[numRow, numCol];
                    matris[numRow, numCol] = 0;

                }

            } while (!DogruMu(matris));


        }
    }
}
