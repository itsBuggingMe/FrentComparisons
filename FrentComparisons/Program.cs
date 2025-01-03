using Arch.Core;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using Frent;
using Friflo.Engine.ECS;
using ArchWorld = Arch.Core.World;
using World = Frent.World;

//[assembly: HardwareCounters(HardwareCounter.CacheMisses, HardwareCounter.BranchMispredictions)]
namespace FrentComparisons;

public class Program
{
    static void Main(string[] args)
    {
        BenchmarkRunner.Run<CreateBenchmarks>();
        BenchmarkRunner.Run<SystemBenchmarks>();
        //BenchmarkRunner.Run<CacheSample>(DefaultConfig.Instance.WithOptions(ConfigOptions.DisableOptimizationsValidator));
    }
}

[MemoryDiagnoser]
public class CreateBenchmarks
{
    private const int EntityCount = 100_000;

    [Benchmark]
    [BenchmarkCategory("Create1")]
    public void CreateArch_1()
    {
        var world = ArchWorld.Create();
        for (int i = 0; i < EntityCount; i++)
            world.Create<Component1>();
        ArchWorld.Destroy(world);
    }

    [Benchmark]
    [BenchmarkCategory("Create1")]
    public void CreateFriflo_1()
    {
        var world = new EntityStore();
        for (int i = 0; i < EntityCount; i++)
            world.CreateEntity<Component1>(default);
    }

    [Benchmark]
    [BenchmarkCategory("Create1")]
    public void CreateFrent_1()
    {
        var world = new World();
        for (int i = 0; i < EntityCount; i++)
            world.Create<Component1>(default);
        world.Dispose();
    }

    [Benchmark]
    [BenchmarkCategory("Create2")]
    public void CreateArch_2()
    {
        var world = ArchWorld.Create();
        for (int i = 0; i < EntityCount; i++)
            world.Create<Component1, Component2>();
        ArchWorld.Destroy(world);
    }

    [Benchmark]
    [BenchmarkCategory("Create2")]
    public void CreateFriflo_2()
    {
        var world = new EntityStore();
        for (int i = 0; i < EntityCount; i++)
            world.CreateEntity<Component1, Component2>(default, default);
    }

    [Benchmark]
    [BenchmarkCategory("Create2")]
    public void CreateFrent_2()
    {
        var world = new World();
        for (int i = 0; i < EntityCount; i++)
            world.Create<Component1, Component2>(default, default);
        world.Dispose();
    }
}

[MemoryDiagnoser]
public class SystemBenchmarks
{
    private ArchWorld _arch;
    private QueryDescription _d1;
    private QueryDescription _d2;
    private System1 _system1;
    private System2 _system2;

    private EntityStore _friflo;
    private ArchetypeQuery<Component1> _f1;
    private ArchetypeQuery<Component1, Component2> _f2;

    private World _frent;

    [GlobalSetup]
    public void Setup()
    {
        _arch = ArchWorld.Create();
        _friflo = new EntityStore();
        _frent = new World();
        _d1 = new QueryDescription().WithAll<Component1>();
        _d2 = new QueryDescription().WithAll<Component1, Component2>();

        _f1 = _friflo.Query<Component1>();
        _f2 = _friflo.Query<Component1, Component2>();

        for (int i = 0; i < 100_000; i++)
        {
            _arch.Create<Component1>();
            _friflo.CreateEntity<Component1>(default);
            _frent.Create<Component1>(default);

            _arch.Create<Component1, Component2>();
            _friflo.CreateEntity<Component1, Component2>(default, default);
            _frent.Create<Component1, Component2>(default, default);
        }
    }

    [Benchmark]
    [BenchmarkCategory("System1")]
    public void SystemArch_1()
    {
        _arch.InlineQuery<System1, Component1>(in _d1, ref _system1);
    }

    [Benchmark]
    [BenchmarkCategory("System1")]
    public void SystemFriflo_1()
    {
        _f1.Each(_system1);
    }

    [Benchmark]
    [BenchmarkCategory("System1")]
    public void SystemFrent_1()
    {
        _frent.InlineQuery<System1, Component1>(_system1);
    }

    [Benchmark]
    [BenchmarkCategory("System2")]
    public void SystemArch_2()
    {
        _arch.InlineQuery<System2, Component1, Component2>(in _d2, ref _system2);
    }

    [Benchmark]
    [BenchmarkCategory("System2")]
    public void SystemFriflo_2()
    {
        _f2.Each(_system2);
    }

    [Benchmark]
    [BenchmarkCategory("System2")]
    public void SystemFrent_2()
    {
        _frent.InlineQuery<System2, Component1, Component2>(_system2);
    }

    internal struct System1 : IForEach<Component1>, IEach<Component1>, IQuery<Component1>
    {
        public void Execute(ref Component1 c1) => c1.Value++;
        public void Run(ref Component1 arg) => arg.Value++;
        public void Update(ref Component1 t0) => t0.Value++;
    }

    internal struct System2 : IForEach<Component1, Component2>, IEach<Component1, Component2>, IQuery<Component1, Component2>
    {
        public void Execute(ref Component1 c1, ref Component2 c2) => c2.Value += c1.Value;
        public void Run(ref Component1 arg1, ref Component2 arg2) => arg2.Value += arg1.Value;
        public void Update(ref Component1 t0, ref Component2 t1) => t1.Value += t0.Value;
    }
}

internal record struct Component1(int Value) : IComponent;
internal record struct Component2(long Value) : IComponent;
