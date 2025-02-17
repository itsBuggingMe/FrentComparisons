using Arch.Core;
using BenchmarkDotNet.Attributes;
using Frent.Systems;
using Friflo.Engine.ECS;
using Frent.Core;
using System.Runtime.CompilerServices;
using Frent;

namespace FrentComparisons;

[MemoryDiagnoser]
[DisassemblyDiagnoser]
public class Systems
{
    const int EntityCount = 10_000;

    private FrentWorld _frent;
    private FrifloWorld _friflo;
    private ArchWorld _arch;

    private ArchetypeQuery<Component1, Component2, Component3> _frifloQuery;
    private QueryDescription _archQuery;
    private Frent.Systems.Query _frentQuery;


    [GlobalSetup]
    public void Setup()
    {
        _arch = ArchWorld.Create();
        _friflo = new FrifloWorld();
        _frent = new FrentWorld();

        for (int i = 0; i < EntityCount; i++)
        {
            _arch.Create(new Component1(), new Component2(), new Component3());
            _friflo.CreateEntity(new Component1(), new Component2(), new Component3());
            _frent.Create(new Component1(), new Component2(), new Component3());
        }

        _frifloQuery = _friflo.Query<Component1, Component2, Component3>();
        _archQuery = new QueryDescription().WithAll<Component1, Component2, Component3>();
        _frentQuery = _frent.Query<With<Component1>, With<Component2>, With<Component3>>();
    }

    [Benchmark]
    public void SystemArch()
    {
        var x = default(QueryFunction);
        _arch.InlineQuery<QueryFunction, Component1, Component2, Component3>(_archQuery, ref x);
    }

    [Benchmark]
    public void SystemFriflo()
    {
        _frifloQuery.Each<QueryFunction, Component1, Component2, Component3>(default);
    }

    [Benchmark]
    public void SystemFrent()
    {
        _frentQuery.Inline<QueryFunction, Component1, Component2, Component3>(default);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _frent.Dispose();
        _arch.Dispose();
        _friflo = null;
        _frent = null;
        _arch = null;
    }

    internal struct QueryFunction :
        IAction<Component1, Component2, Component3>,
        IForEach<Component1, Component2, Component3>,
        IEach<Component1, Component2, Component3>
    {
        public void Execute(ref Component1 c1, ref Component2 c2, ref Component3 c3) => c1.Num += c2.Num + c3.Num;

        public void Run(ref Component1 arg1, ref Component2 arg2, ref Component3 arg3) => arg1.Num += arg2.Num + arg3.Num;

        public void Update(ref Component1 t0, ref Component2 t1, ref Component3 t2) => t0.Num += t1.Num + t2.Num;
    }
}
