# Stage 4(b) - Add .NET Core / Multi-Targeting

Adding additional targets to a .csproj is very simple and is just a matter
of a few extra characters.

## Class Library

To add additional target frameworks, such as .NET Standard or .NET Core, all we
have to do is change the `<TargetFramework>` to the plural,
`<TargetFrameworks>`. and then semicolon-separate all the desired platforms:

```xml
<TargetFrameworks>net471;netstandard2.0</TargetFrameworks>
```

Now, when we build, the compiler will spit out 2 assemblies, one for each
target framework that we specified. Because we have effectively said that we
want a .NET Standard assembly, we have to follow the steps in the previous
stage: [Stage 4(a) - Switch to .NET Core](../Stage%204a%20-%20Add%20.NET%20Core).

The thing that we will be doing different is that we can also use `#if`
instead of just the normal `if` and `RuntimeInformation`:

```csharp
#if NETFRAMEWORK
    // access APIs for the full .NET Framework
#elif NETSTANDARD
    // access APIS for .NET Standard
#else
    // do something different, or, throw a PlatformNotSupportedException
#endif
```

## Console App

This is the exact same process as the class library, except for 1 extra bit:
instead of using `netstandard2.0` as the target framework, use
`netcoreapp2.1`. This lets the compiler know that we want a .NET Core app and
not a library.
