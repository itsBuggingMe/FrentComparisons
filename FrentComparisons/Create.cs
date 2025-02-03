using BenchmarkDotNet.Attributes;
using Friflo.Engine.ECS;

namespace FrentComparisons;

[MemoryDiagnoser]
[DisassemblyDiagnoser]
public class Create
{
    [Params(1_000, 100_000, 1_000_000)]
    public int EntityCount;

    private FrentWorld _frent;
    private FrifloWorld _friflo;
    private ArchWorld _arch;

    [IterationSetup]
    public void Setup()
    {
        _arch = ArchWorld.Create();
        _friflo = new FrifloWorld();
        _frent = new FrentWorld();
    }

    [Benchmark]
    public void CreateArch()
    {
        for(int i = 0; i < EntityCount; i++)
            _arch.Create(new Component1(), new Component2(), new Component3());
    }

    [Benchmark]
    public void CreateFriflo()
    {
        for (int i = 0; i < EntityCount; i++)
            _friflo.CreateEntity(new Component1(), new Component2(), new Component3());
    }

    [Benchmark]
    public void CreateFrent()
    {
        for (int i = 0; i < EntityCount; i++)
            _frent.Create(new Component1(), new Component2(), new Component3());
    }

    [IterationCleanup]
    public void Cleanup()
    {
        _frent.Dispose();
        _arch.Dispose();
        _friflo = null;
        _frent = null;
        _arch = null;
    }
}

internal record struct Component1(int Num) : IComponent;
internal record struct Component2(int Num) : IComponent;
internal record struct Component3(int Num) : IComponent;
