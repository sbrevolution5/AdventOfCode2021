// See https://aka.ms/new-console-template for more information
using AdventOfCode2021.Code;
using AdventOfCode2021.inputs;

Console.WriteLine(RadarReader.HowManyIncreases(Day1Input.Radar));
Console.WriteLine(RadarReader.HowManySlidingIncreases(Day1Input.Radar));
Console.WriteLine(SubmarineNavigation.NavigateSub(Day2.NavigationInstructions));
Console.WriteLine(SubmarineNavigation.AimSub(Day2.NavigationInstructions));
Console.WriteLine("Power consumption is:");
Console.WriteLine(PowerConsumption.GetPowerConsumption(Inputs.Day3));