# Frent Comparisons

A small collection of microbenchmarks used to measure Frent's preformance. 

### Create.cs
```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4890/23H2/2023Update/SunValley3)
AMD Ryzen 3 5300G with Radeon Graphics, 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.102
  [Host]     : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2
  Job-ZGEAXZ : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2

InvocationCount=1  UnrollFactor=1  

```
| Method       | EntityCount | Mean          | Error         | StdDev        | Median        | Gen0       | Code Size | Gen1      | Gen2      | Allocated   |
|------------- |------------ |--------------:|--------------:|--------------:|--------------:|-----------:|----------:|----------:|----------:|------------:|
| CreateArch   | 1           |     17.698 μs |     1.8655 μs |     5.4711 μs |     14.300 μs |          - |     762 B |         - |         - |    31.02 KB |
| CreateFriflo | 1           |      6.262 μs |     0.9046 μs |     2.6529 μs |      4.700 μs |          - |     582 B |         - |         - |    10.77 KB |
| CreateFrent  | 1           |      2.726 μs |     0.0997 μs |     0.2843 μs |      2.700 μs |          - |   1,468 B |         - |         - |     2.66 KB |
| CreateArch   | 10          |     19.776 μs |     1.9036 μs |     5.4922 μs |     16.850 μs |          - |     762 B |         - |         - |    31.63 KB |
| CreateFriflo | 10          |      9.261 μs |     0.8023 μs |     2.3657 μs |      9.100 μs |          - |     582 B |         - |         - |    11.09 KB |
| CreateFrent  | 10          |      9.223 μs |     0.9172 μs |     2.6316 μs |      8.900 μs |          - |   1,468 B |         - |         - |     3.56 KB |
| CreateArch   | 100         |     37.300 μs |     0.5779 μs |     0.6183 μs |     37.250 μs |          - |     762 B |         - |         - |    31.35 KB |
| CreateFriflo | 100         |     29.431 μs |     0.4159 μs |     0.3473 μs |     29.500 μs |          - |     582 B |         - |         - |    11.09 KB |
| CreateFrent  | 100         |     18.120 μs |     0.2834 μs |     0.2651 μs |     18.100 μs |          - |   1,468 B |         - |         - |     7.78 KB |
| CreateArch   | 1000        |    287.587 μs |     5.7303 μs |    15.3940 μs |    290.250 μs |          - |     762 B |         - |         - |    72.24 KB |
| CreateFriflo | 1000        |    283.689 μs |     5.5716 μs |     5.9615 μs |    284.150 μs |          - |     582 B |         - |         - |    69.26 KB |
| CreateFrent  | 1000        |    147.462 μs |     1.9565 μs |     1.6338 μs |    147.600 μs |          - |   1,468 B |         - |         - |    75.61 KB |
| CreateArch   | 10000       |  1,905.609 μs |   463.9250 μs | 1,367.8935 μs |  2,438.400 μs |          - |     972 B |         - |         - |    456.8 KB |
| CreateFriflo | 10000       |  2,294.384 μs |    45.5784 μs |    69.6030 μs |  2,285.100 μs |          - |     582 B |         - |         - |  1269.73 KB |
| CreateFrent  | 10000       |    546.217 μs |    19.9300 μs |    56.5381 μs |    520.600 μs |          - |   1,526 B |         - |         - |  1335.75 KB |
| CreateArch   | 100000      |  4,424.210 μs |   394.3586 μs | 1,162.7753 μs |  3,602.800 μs |          - |     972 B |         - |         - |  4543.78 KB |
| CreateFriflo | 100000      |  6,832.073 μs |   135.7483 μs |   332.9928 μs |  6,871.400 μs |  2000.0000 |     611 B | 2000.0000 | 2000.0000 | 10234.58 KB |
| CreateFrent  | 100000      |  4,910.061 μs |    98.3614 μs |   279.0349 μs |  4,914.000 μs |  1000.0000 |     685 B | 1000.0000 | 1000.0000 | 10746.73 KB |
| CreateArch   | 1000000     | 73,469.571 μs | 1,468.6944 μs | 3,033.1088 μs | 72,729.850 μs | 12000.0000 |     862 B | 7000.0000 | 2000.0000 | 62024.16 KB |
| CreateFriflo | 1000000     | 55,920.398 μs | 1,859.0149 μs | 5,452.1694 μs | 56,985.200 μs |  3000.0000 |   4,894 B | 3000.0000 | 3000.0000 | 81917.23 KB |
| CreateFrent  | 1000000     | 42,081.847 μs | 1,482.6953 μs | 4,371.7608 μs | 40,393.400 μs |  2000.0000 |   1,588 B | 2000.0000 | 2000.0000 |  86013.3 KB |

### Delete.cs
```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4890/23H2/2023Update/SunValley3)
AMD Ryzen 3 5300G with Radeon Graphics, 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.102
  [Host]     : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2
  Job-ZGEAXZ : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2

InvocationCount=1  UnrollFactor=1  

```
| Method       | Mean       | Error     | StdDev    | Median     | Code Size | Allocated |
|------------- |-----------:|----------:|----------:|-----------:|----------:|----------:|
| DeleteArch   |   829.6 μs |   9.38 μs |   7.83 μs |   826.2 μs |     484 B |   2.69 KB |
| DeleteFriflo | 1,299.0 μs |  11.11 μs |   9.28 μs | 1,299.5 μs |     504 B | 130.98 KB |
| DeleteFrent  |   522.5 μs | 183.18 μs | 540.11 μs |   195.7 μs |     631 B |   2.69 KB |

### GetComponents.cs
```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4890/23H2/2023Update/SunValley3)
AMD Ryzen 3 5300G with Radeon Graphics, 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.102
  [Host]     : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2


```
| Method     | Mean      | Error    | StdDev   | Code Size | Allocated |
|----------- |----------:|---------:|---------:|----------:|----------:|
| GetArch    |  60.08 μs | 0.204 μs | 0.170 μs |     219 B |         - |
| GetFriflo  |  33.85 μs | 0.122 μs | 0.114 μs |     302 B |         - |
| GetFrent   |  55.13 μs | 0.260 μs | 0.243 μs |     194 B |         - |
| Get3Arch   | 151.91 μs | 0.671 μs | 0.628 μs |     493 B |         - |
| Get3Friflo |  45.83 μs | 0.151 μs | 0.134 μs |     432 B |         - |
| Get3Frent  |  81.38 μs | 0.366 μs | 0.324 μs |     325 B |         - |



### Systems.cs
```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4890/23H2/2023Update/SunValley3)
AMD Ryzen 3 5300G with Radeon Graphics, 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.102
  [Host]     : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2


```
| Method       | Mean     | Error     | StdDev    | Code Size | Allocated |
|------------- |---------:|----------:|----------:|----------:|----------:|
| SystemArch   | 5.037 μs | 0.0233 μs | 0.0182 μs |     495 B |         - |
| SystemFriflo | 4.949 μs | 0.0351 μs | 0.0329 μs |     586 B |         - |
| SystemFrent  | 4.607 μs | 0.0088 μs | 0.0078 μs |     227 B |         - |
