using Arch.Core.Extensions;
using BenchmarkDotNet.Attributes;
using Friflo.Engine.ECS;

namespace FrentComparisons;

[MemoryDiagnoser]
[DisassemblyDiagnoser]
public class Delete
{
    const int EntityCount = 10_000;

    private FrentWorld _frent;
    private FrifloWorld _friflo;
    private ArchWorld _arch;

    private Frentity[] _frentities;
    private FrifloEntity[] _frifloEntities;
    private ArchEntity[] _archEntities;


    [IterationSetup]
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
    public void DeleteArch()
    {
        foreach(var item in _archEntities)
            _arch.Destroy(item);
    }

    [Benchmark]
    public void DeleteFriflo()
    {
        foreach(var item in _frifloEntities)
            item.DeleteEntity();
    }

    [Benchmark]
    public void DeleteFrent()
    {
        foreach (var item in _frentities)
            item.Delete();
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
