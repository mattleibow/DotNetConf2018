# Stage 3 - Upgrade to SDK-Style Projects

The next step is a bit harder, but usually fairly painless. This is to move
away from the old, verbose and merge-hell project format to the new SDK-Style
project format.

More information can be found on the Microsoft Docs site:

- [Additions to the csproj format for .NET Core](https://docs.microsoft.com/en-us/dotnet/core/tools/csproj)

## Migrate

Migration is usually simple for most projects, and involves taking note of
files or other properties that you have added in addition to the default
template. This is because the new project format is really an empty file
with 5 lines:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
   <TargetFramework>net471</TargetFramework>
  </PropertyGroup>
</Project>
```

### Class Library

In our simple example, we only have 1 source file and we have added 1 NuGet
package. This means that all we have to do is delete everything from the
.csproj and paste in the 5 lines. Then, we have to add back the package
reference. We could use the NuGet Package Manager, but is is also just as
easy to paste in the 3 lines:

```xml
<ItemGroup>
  <PackageReference Include="Newtonsoft.Json" Version="11.0.2" />
</ItemGroup>
```

Another feature of the new SDK-Style project format is that all the attributes
that is usually found in the `AssemblyInfo.cs` file are generated at
compile-time. This means that we can delete the extra file in the
`Properties\AssemblyInfo.cs` sub-folder. If we had additional attributes, we 
don't delete the file, [just the ones found in this list][1].

### Console App

This is the exact same process as the class library, except for 2 extra bits:

#### The Project Reference

Project references in the new project format is very simple. Just add a
single item for each reference:

```xml
<ItemGroup>
  <ProjectReference Include="..\ClassLibrary\ClassLibrary.csproj" />
</ItemGroup>
```

#### Mark as Executable

Because our console app is an executable, we also need to add a property to let
MSBuild know that it must make an .exe instead of a .dll:

```xml
<PropertyGroup>
  <OutputType>Exe</OutputType>
</PropertyGroup>
```


[1]: https://docs.microsoft.com/en-us/dotnet/core/tools/csproj#assemblyinfo-properties