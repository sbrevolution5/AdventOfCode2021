// See https://aka.ms/new-console-template for more information
using AdventOfCode2021.Code;
using AdventOfCode2021.inputs;

Console.WriteLine(RadarReader.HowManyIncreases(Day1Input.Radar));
Console.WriteLine(RadarReader.HowManySlidingIncreases(Day1Input.Radar));
Console.WriteLine(SubmarineNavigation.NavigateSub(Day2.NavigationInstructions));
Console.WriteLine(SubmarineNavigation.AimSub(Day2.NavigationInstructions));
Console.WriteLine("Power consumption is:");
Console.WriteLine(PowerConsumption.GetPowerConsumption(Inputs.Day3));
Console.WriteLine("Life support Rating is:");
Console.WriteLine(PowerConsumption.GetLifeSupportRating(Inputs.Day3));
Console.WriteLine(Bingo.GetBingoWinnerScore(Inputs.Day4));
Console.WriteLine(Bingo.GetBingoLoserScore(Inputs.Day4));
Console.WriteLine("Hydrothermal Overlap number is:");
Console.WriteLine(HydrothermalVents.CountOverlaps(Inputs.Day5));
Console.WriteLine(Lanternfish.HowManyFishColumns(Inputs.Day6, 256));
Console.WriteLine("Fuel needed to align subs horizontally");
Console.WriteLine(CrabSubs.FuelForOneLine(Inputs.Day7));
Console.WriteLine("Easy outputs (1,4,7,8) found on display:");
Console.WriteLine(SevenSegmentDisplay.CountEasyOutputs(Inputs.Day8));
Console.WriteLine("Seven Segment display Sum = ");
Console.WriteLine(SevenSegmentDisplay.GetSumOfOutputs(Inputs.Day8));
Console.WriteLine("Total Risk Factor for lowest points:");
Console.WriteLine(LavaTunnel.FindSumOfLowRiskLevel(Inputs.Day9));
Console.WriteLine("Product of the size of 3 largest lava basins");
Console.WriteLine(LavaTunnel.Find3LargestBasins(Inputs.Day9));
Console.WriteLine("Error score for corrupted syntax lines:");
Console.WriteLine(SyntaxFixer.FindSyntaxErrorScore(Inputs.Day10));