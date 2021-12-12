using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    internal static class FlashingOctopuses
    {
        public static int HowManyFlashesAfter100(string input)
        {
            Octo[,] octos = parseInput(input);
            var count = 0;
            for (int step = 0; step < 100; step++)
            {
                count += PerformStep(octos);
            }
            return count;
        }
        public static int WhenIsFirstSyncedFlash(string input)
        {
            Octo[,] octos = parseInput(input);
            for (int step = 0; step < 500; step++)
            {
                if (PerformStep(octos) == 100)
                {
                    return step+1;
                }
            }
            return 0;
        }

        private static int PerformStep(Octo[,] octos)
        {
            var oldCount = -1;
            var newCount = 0;

            IncrementAll(octos);
            while (newCount != oldCount)
            {
                oldCount = newCount;
                //Do count

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (StepOcto(i, j, octos))
                        {
                            newCount++;
                        }
                    }
                }
            }
            AllUnflash(octos);
            ResetNines(octos);
            return newCount;
        }

        private static void ResetNines(Octo[,] octos)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (octos[i, j].EnergyLevel > 9)
                    {
                        octos[i,j].EnergyLevel = 0;
                    }
                }
            }
        }

        private static void AllUnflash(Octo[,] octos)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    octos[i, j].Flashed = false;
                }
            }
        }

        private static void IncrementAll(Octo[,] octos)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    octos[i, j].EnergyLevel++;
                }
            }
        }

        private static bool StepOcto(int i, int j, Octo[,] octos)
        {
            if (octos[i, j].EnergyLevel > 9 && !octos[i, j].Flashed)
            {
                octos[i, j].Flashed = true;
                var xMin = i > 0;
                var xMax = i < 9;
                var yMin = j > 0;
                var yMax = j < 9;
                if (xMin)
                {
                    octos[i - 1, j].EnergyLevel++;
                    if (yMin)
                    {
                        octos[i - 1, j - 1].EnergyLevel++;
                    }
                    if (yMax)
                    {
                        octos[i - 1, j + 1].EnergyLevel++;
                    }
                }
                if (xMax)
                {
                    octos[i + 1, j].EnergyLevel++;
                    if (yMin)
                    {
                        octos[i + 1, j - 1].EnergyLevel++;
                    }
                    if (yMax)
                    {
                        octos[i + 1, j + 1].EnergyLevel++;
                    }
                }
                if (yMin)
                {
                    octos[i, j - 1].EnergyLevel++;

                }
                if (yMax)
                {
                    octos[i, j + 1].EnergyLevel++;
                }

                return true;
            }
            return false;
        }

        private static Octo[,] parseInput(string input)
        {
            var result = new Octo[10, 10];
            var lines = input.Split('\n');
            var nums = lines.Select(x => x.Trim().ToCharArray().Select(y => int.Parse(char.ToString(y))).ToArray()).ToArray();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    result[i, j] = new()
                    {

                        EnergyLevel = nums[i][j],
                        Flashed = false
                    };
                }
            }
            return result;
        }
    }

    internal class Octo
    {
        public int EnergyLevel { get; set; }
        public bool Flashed { get; set; }
    }
}
