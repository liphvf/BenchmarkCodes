// See https://aka.ms/new-console-template for more information
using BenchmarkCodes.BenchmarkTests;
using BenchmarkDotNet.Running;

// var summary = BenchmarkRunner.Run(typeof(Program).Assembly);

BenchmarkRunner.Run<TasksTests>();

Console.ReadKey();