# Stage 5 - Add Mobile

Once we know how to add one target, we can now add as many as we want to. All
we have to do is add the mobile target frameworks.

## Class Library

Because the SDK-Style projects can't understand target frameworks other than
the .NET Core, .NET Standard and the full .NET Framework, we make use of an
extra NuGet package: [MSBuild.Sdk.Extras](https://www.nuget.org/packages/MSBuild.Sdk.Extras).

This package adds support for many more platforms, including Android, iOS
and UWP.

This NuGet package is not like traditional packages where you add a reference,
instead, you let the project system know that you are going to use a different
SDK-Style projec. In this case, instead of specifying `Microsoft.NET.Sdk` in
the `Sdk` attribute of the root `<Project>` element of the .csproj, we use
`MSBuild.Sdk.Extras` with a version number. Our new .csproj will be:

```xml
<Project Sdk="MSBuild.Sdk.Extras/1.6.55">

  <PropertyGroup>
   <TargetFrameworks>net471;netstandard2.0;uap10.0.16299;xamarinios1.0;monoandroid8.1</TargetFrameworks>
  </PropertyGroup>

  <!-- the rest of the file -->

</Project>
```

## Console App

This is not useful since a .NET Core app cannot run on mobile devices, but we
can now add a new Xamarin.iOS or Xamarin.Android project to our solution and
just reference the same class library.

[<< Stage 4b - Add .NET Core](../Stage%204b%20-%20Add%20.NET%20Core)
