using Arch.Core;
using BenchmarkDotNet.Attributes;
using Frent;
using Frent.Updating.Runners;
using Friflo.Engine.ECS;
using System.Runtime.CompilerServices;
using ArchWorld = Arch.Core.World;
using World = Frent.World;

namespace FrentComparisons;

[MemoryDiagnoser]
public class CacheSample
{
    private ArchWorld _arch;
    private QueryDescription _rDescription;
    private QueryDescription _pDescription;

    private UpdatePosition _system;
    private UpdateReadComponent _readSystem;

    private EntityStore _friflo;
    private ArchetypeQuery<Position, Velocity> _posSystem;
    private ArchetypeQuery<Position, Velocity, ReadComponent> _rSystem;

    private World _frent;

    [GlobalSetup]
    public void Setup()
    {
        _arch = ArchWorld.Create();
        _friflo = new EntityStore();
        _frent = new World();

        _rDescription = new QueryDescription().WithAll<Position, Velocity, ReadComponent>();
        _pDescription = new QueryDescription().WithAll<Position, Velocity>();

        _posSystem = _friflo.Query<Position, Velocity>();
        _rSystem = _friflo.Query<Position, Velocity, ReadComponent>();

        for (int i = 0; i < 100_000; i++)
        {
            if(i % 2 == 0)
            {
                _arch.Create<Position, Velocity, ReadComponent>();
                _friflo.CreateEntity<Position, Velocity, ReadComponent>(default, default, default);
                _frent.Create<Position, Velocity, ReadComponent>(default, default, default);
            }
            else
            {
                _arch.Create<Position, Velocity, ReadComponent>();
                _friflo.CreateEntity<Position, Velocity, ReadComponent>(default, default, default);
                _frent.Create<Position, Velocity, ReadComponent>(default, default, default);
            }
        }
    }

    [Benchmark]
    [BenchmarkCategory("Cache")]
    public void SystemArch()
    {
        _arch.InlineQuery<UpdatePosition, Position, Velocity>(in _pDescription, ref _system);
        _arch.InlineQuery<UpdateReadComponent, Position, Velocity, ReadComponent>(in _pDescription, ref _readSystem);
    }

    [Benchmark]
    [BenchmarkCategory("Cache")]
    public void SystemFriflo()
    {
        _posSystem.Each(_system);
        _rSystem.Each(_readSystem);
    }

    [Benchmark]
    [BenchmarkCategory("Cache")]
    public void SystemFrent()
    {
        _frent.Update();
    }

    internal struct UpdatePosition : IForEach<Position, Velocity>, IEach<Position, Velocity>
    {
        public void Execute(ref Position p, ref Velocity v) => p.X += v.DX;
        public void Update(ref Position p, ref Velocity v) => p.X += v.DX;
    }

    internal struct UpdateReadComponent : IForEach<Position, Velocity, ReadComponent>, IEach<Position, Velocity, ReadComponent>
    {
        public void Execute(ref Position c1, ref Velocity c2, ref ReadComponent c3) => c3.Sum += c1.X + c2.DX;
        public void Update(ref Position t0, ref Velocity t1, ref ReadComponent t2) => t2.Sum += t0.X + t1.DX;
    }
}

internal record struct Position(float X) : IComponent;
internal record struct Velocity(float DX) : IComponent, Frent.Components.IUpdateComponent<Position>
{
    public void Update(ref Position pos) => pos.X += DX;
}

internal record struct ReadComponent(float Sum, long A1, long A2, long A3, long A4, long A5, long A6, long A7, long A8) : Frent.Components.IUpdateComponent<Position, Velocity>, IComponent
{
    public void Update(ref Position arg, ref Velocity velocity)
    {
        Sum += arg.X + velocity.DX;
    }
}

internal class Init
{
    [ModuleInitializer]
    public static void InitRead() => Frent.Updating.GenerationServices.RegisterType(typeof(ReadComponent), new Update<ReadComponent, Position, Velocity>());
    [ModuleInitializer]
    public static void InitVelocity() => Frent.Updating.GenerationServices.RegisterType(typeof(Velocity), new Update<Velocity, Position>());
}