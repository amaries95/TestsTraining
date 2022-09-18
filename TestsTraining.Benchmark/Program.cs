using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using TestsTraining.Benchmark;

var config = new ManualConfig()
	.WithOptions(ConfigOptions.DisableOptimizationsValidator);
config.Add(DefaultConfig.Instance.GetExporters().ToArray());
config.Add(DefaultConfig.Instance.GetLoggers().ToArray());
config.Add(DefaultConfig.Instance.GetColumnProviders().ToArray());

BenchmarkRunner.Run<Benchmarks>(config);

Console.ReadKey();