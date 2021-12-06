using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Code
{
    public static class Lanternfish
    {
        public static int HowManyFish(string input, int days)
        {
            var fish = ParseInput(input);
            for (int i = 0; i < days; i++)
            {
                SpawnFish(fish);
            }
            return fish.Count();
        }
        public static long HowManyFishColumns(string input, int days)
        {
            var fish = ParseColumns(input);
            var lifespan = new long[9];
            foreach (var i in fish)
            {
                lifespan[i]++;
            }
            for (int i = 0; i < days; i++)
            {
                var buffer = new long[9];
                for (int j = 0; j < 9; j++)
                {

                    if (j == 0)
                    {
                        buffer[6] += lifespan[j];
                        buffer[8] += lifespan[j];

                    }
                    else
                    {
                        buffer[j - 1] += lifespan[j];
                    }
                }
                lifespan = buffer;
            }
            return lifespan.Sum();
        }

        private static List<int> ParseColumns(string input)
        {
            return new List<int>(Array.ConvertAll(input.Split(','), int.Parse));
        }

        private static void SpawnFish(List<int> fishes)
        {
            var oldFishes = new int[fishes.Count()];
            fishes.CopyTo(oldFishes);
            for (int i = 0; i < oldFishes.Count(); i++)
            {
                if (oldFishes[i] == 0)
                {
                    fishes.Add(8);
                    fishes[i] = 6;
                }
                else
                {
                    fishes[i]--;
                }
            }
        }

        private static List<int> ParseInput(string input)
        {
            return input.Split(',').Select(i => int.Parse(i)).ToList();
        }
    }
}
