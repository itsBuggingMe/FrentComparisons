# Frent Comparisons

A small collection of microbenchmarks used to measure Frent's preformance. 

### Create.cs
```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4830/23H2/2023Update/SunValley3)
AMD Ryzen 3 5300G with Radeon Graphics, 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.102
  [Host]     : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2
  Job-ZEOTZC : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2

InvocationCount=1  UnrollFactor=1  

```
| Method       | EntityCount | Mean        | Error       | StdDev      | Median      | Code Size | Gen0       | Gen1      | Gen2      | Allocated   |
|------------- |------------ |------------:|------------:|------------:|------------:|----------:|-----------:|----------:|----------:|------------:|
| **CreateArch**   | **1000**        |    **274.9 μs** |     **4.27 μs** |     **3.56 μs** |    **275.9 μs** |     **762 B** |          **-** |         **-** |         **-** |    **71.63 KB** |
| CreateFriflo | 1000        |    277.7 μs |     5.43 μs |     5.58 μs |    275.5 μs |     582 B |          - |         - |         - |    69.26 KB |
| CreateFrent  | 1000        |    170.6 μs |     3.33 μs |     3.96 μs |    170.3 μs |   1,168 B |          - |         - |         - |    49.77 KB |
| **CreateArch**   | **100000**      |  **4,945.2 μs** |   **452.39 μs** | **1,297.99 μs** |  **5,544.9 μs** |     **972 B** |  **1000.0000** |         **-** |         **-** |  **4544.39 KB** |
| CreateFriflo | 100000      |  7,156.5 μs |   140.69 μs |   257.26 μs |  7,089.5 μs |     498 B |  2000.0000 | 2000.0000 | 2000.0000 | 10234.58 KB |
| CreateFrent  | 100000      |  2,769.9 μs |    55.30 μs |   119.03 μs |  2,748.8 μs |   1,168 B |  1000.0000 | 1000.0000 | 1000.0000 |  5553.52 KB |
| **CreateArch**   | **1000000**     | **73,919.5 μs** | **1,461.28 μs** | **3,745.81 μs** | **72,651.9 μs** |     **862 B** | **12000.0000** | **7000.0000** | **2000.0000** | **62024.16 KB** |
| CreateFriflo | 1000000     | 55,323.0 μs | 1,368.46 μs | 4,034.93 μs | 55,747.2 μs |   4,894 B |  4000.0000 | 4000.0000 | 4000.0000 | 81919.41 KB |
| CreateFrent  | 1000000     | 24,535.5 μs |   279.89 μs |   248.12 μs | 24,530.4 μs |     967 B |  2000.0000 | 2000.0000 | 2000.0000 | 40118.16 KB |


### Delete.cs
```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4830/23H2/2023Update/SunValley3)
AMD Ryzen 3 5300G with Radeon Graphics, 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.102
  [Host]     : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2
  Job-ZEOTZC : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2

InvocationCount=1  UnrollFactor=1  

```
| Method       | Mean       | Error    | StdDev   | Median     | Code Size | Allocated |
|------------- |-----------:|---------:|---------:|-----------:|----------:|----------:|
| DeleteArch   |   376.7 μs | 16.00 μs | 42.15 μs |   363.7 μs |     517 B |   2.69 KB |
| DeleteFriflo | 1,269.0 μs | 17.96 μs | 14.99 μs | 1,265.8 μs |     504 B | 130.98 KB |
| DeleteFrent  |   246.6 μs |  4.64 μs | 11.57 μs |   243.6 μs |   2,664 B | 194.85 KB |


### GetComponents.cs
```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4830/23H2/2023Update/SunValley3)
AMD Ryzen 3 5300G with Radeon Graphics, 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.102
  [Host]     : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2


```
| Method     | Mean      | Error    | StdDev   | Code Size | Allocated |
|----------- |----------:|---------:|---------:|----------:|----------:|
| GetArch    |  60.61 μs | 0.046 μs | 0.043 μs |     219 B |         - |
| GetFriflo  |  33.30 μs | 0.035 μs | 0.029 μs |     302 B |         - |
| GetFrent   |  80.79 μs | 0.099 μs | 0.083 μs |     991 B |         - |
| Get3Arch   | 151.53 μs | 0.215 μs | 0.191 μs |     493 B |         - |
| Get3Friflo |  45.73 μs | 0.057 μs | 0.047 μs |     432 B |         - |
| Get3Frent  |  91.67 μs | 0.117 μs | 0.104 μs |   1,213 B |         - |


### Systems.cs
```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4830/23H2/2023Update/SunValley3)
AMD Ryzen 3 5300G with Radeon Graphics, 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.102
  [Host]     : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.12 (8.0.1224.60305), X64 RyuJIT AVX2


```
| Method       | Mean     | Error     | StdDev    | Code Size | Allocated |
|------------- |---------:|----------:|----------:|----------:|----------:|
| SystemArch   | 4.963 μs | 0.0063 μs | 0.0059 μs |     495 B |         - |
| SystemFriflo | 4.853 μs | 0.0060 μs | 0.0053 μs |     586 B |         - |
| SystemFrent  | 5.123 μs | 0.0127 μs | 0.0112 μs |     916 B |         - |
