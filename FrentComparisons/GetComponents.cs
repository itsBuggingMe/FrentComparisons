using BenchmarkDotNet.Attributes;
using Friflo.Engine.ECS;
using Frent;
using Arch.Core.Extensions;

namespace FrentComparisons;

[MemoryDiagnoser]
[DisassemblyDiagnoser]
public class GetComponents
{
    const int EntityCount = 10_000;

    private FrentWorld _frent;
    private FrifloWorld _friflo;
    private ArchWorld _arch;

    private Frentity[] _frentities;
    private FrifloEntity[] _frifloEntities;
    private ArchEntity[] _archEntities;


    [GlobalSetup]
    public void Setup()
    {
        _arch = ArchWorld.Create();
        _friflo = new FrifloWorld();
        _frent = new FrentWorld();

        _frentities = new Frentity[EntityCount];
        _frifloEntities = new FrifloEntity[EntityCount];
        _archEntities = new ArchEntity[EntityCount];

        for (int i = 0; i < EntityCount; i++)
        {
            _archEntities[i] = _arch.Create(new Component1(), new Component2(), new Component3());
            _frifloEntities[i] = _friflo.CreateEntity(new Component1(), new Component2(), new Component3());
            _frentities[i] = _frent.Create(new Component1(), new Component2(), new Component3());
        }
    }

    [Benchmark]
    public void GetArch()
    {
        foreach (var item in _archEntities)
        {
            item.Get<Component1>().Num++;
        }
    }

    [Benchmark]
    public void GetFriflo()
    {
        foreach (var entity in _frifloEntities)
        {
            entity.GetComponent<Component1>().Num++;
        }
    }

    [Benchmark]
    public void GetFrent()
    {
        foreach (ref var item in _frentities.AsSpan())
        {
            item.Get<Component1>().Num++;
        }
    }

    [Benchmark]
    public void Get3Arch()
    {
        foreach (var item in _archEntities)
        {
            _arch.Get<Component1>(item).Num++;
            _arch.Get<Component2>(item).Num++;
            _arch.Get<Component3>(item).Num++;
        }
    }

    [Benchmark]
    public void Get3Friflo()
    {
        foreach (var entity in _frifloEntities)
        {
            var data = entity.Data;
            data.Get<Component1>().Num++;
            data.Get<Component2>().Num++;
            data.Get<Component3>().Num++;
        }
    }

    [Benchmark]
    public void Get3Frent()
    {
        foreach (ref var item in _frentities.AsSpan())
        {
            item.Deconstruct<Component1, Component2, Component3>(out var c1, out var c2, out var c3);
            c1.Value.Num++;
            c2.Value.Num++;
            c3.Value.Num++;
        }
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
}