# Stage 4(a) - Switch to .NET Core

This step is more optional since there is no reason to switch to .NET Core and
.NET Standard just because they exist. If your app is running fine, then you
now have a nice .csproj and all your old code works.

More information can be found on the Microsoft Docs site:

- [Porting to .NET Core from .NET Framework](https://docs.microsoft.com/en-us/dotnet/core/porting)
- [Using the Windows Compatibility Pack](https://docs.microsoft.com/en-us/dotnet/core/porting/windows-compat-pack)
- [.NET API analyzer](https://docs.microsoft.com/en-us/dotnet/standard/analyzers/api-analyzer)

However, if you want to .NET Core all in and use .NET Standard, this is very
easy to do.

## Class Library

To switch to .NET Standard for a class library, this is very easy. Just change
the target framework to `netstandard2.0` instead of the old `net471`:

```xml
<TargetFramework>netstandard2.0</TargetFramework>
```

**You now have a .NET Standard 2.0 library** _It may not yet compile because
you might be using an API that does not exist in the .NET Standard. If this is
the case, read on._

### Missing Types

Because the .NET Standard is just a subset of all the APIs of .NET Core and
the full .NET Framework, not everything is available. For example: the Windows
Registry. This is a very Windows-specific API, and will never go into the
standard.

There is still hope for your library, just install the
[Microsoft.Windows.Compatibility][1] NuGet package and thousands of APIs will be
available again.

Keep in mind, many of these APIs will only ever work on Windows, so you will
have to write some logic to handle cases when the library is used on Linux or
macOS. If you don't want to support other platforms, then you can just let the
compatibility pack throw an exception.

### Other Platforms

If we do want to support other platforms, such as Linux and macOS, we have to
handle the areas in which the code uses Windows-specific features. In our
example, we are using the registry.

We could read all of the docs, or keep trying our app by trial and error, but
there is an easier way: use the
[Microsoft.DotNet.Analyzers.Compatibility][2] NuGet package.

This package will provide compiler warnings for all types and members that
will not work on some platforms. It will also let you know about any types
that you are using which are deprecated.

In some cases, you can either change the code to make it work on all
platforms, but sometimes this this is not possible. In a few cases, the
platform-specific feature only makes sense on that platform.

For example: in our example, we are looking for a configuration point in the
registry. There is no registry on macOS or Linux, so this is meaningless. But,
because we want to support multiple platforms, we have written logic to also
use configuration files. This means that on Linux or mac, we want to just
check the configuration file, but on Windows, we want to check the registry.

This is actually easy to do. If we have the compatibility pack, we have all
the APIs and the app compiles - they just won't work. To keep our app from
crashing, we can wrap the code in logic to first check the platform:

```csharp
if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    // access Windows APIs
}
else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
{
    // access Linux APIs
}
else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
{
    // access macOS APIs
}
```

If we need more control, or cannot access certain APIs, we can make use of
multi-targeting, see [Stage 4(b) - Add .NET Core / Multi-Targeting](../Stage%204b%20-%20Add%20.NET%20Core)

## Console App

This is the exact same process as the class library, except for 1 extra bit:
instead of using `netstandard2.0` as the target framework, use
`netcoreapp2.1`. This lets the compiler know that we want a .NET Core app and
not a library.


[1]: https://www.nuget.org/packages/Microsoft.Windows.Compatibility
[2]: https://www.nuget.org/packages/Microsoft.DotNet.Analyzers.Compatibility
