# Stage 1 - Initial

This is the initial code, which consists of 1 solution with 2 projects:

## Class Library

This class library is a simple .csproj:
- uses the old-style projects
- targeting `net471`
- uses packages.config
- references the `Newtonsoft.Json` NuGet package

The code is a simple list-to-string printer that has a special option for
controlling the case. When running on Windows, the case is determined by
first looking at a configuration file (config.json), and if that is missing,
looks at the registry to check if the user has a status bar visible in
Notepad.

## Console App

This console app is also a simple .csproj:
- uses the old-style projects
- targeting `net471`
- uses packages.config
- references the `Newtonsoft.Json` NuGet package

The console app does nothing much, except start the work done in the class
library.

[Stage 2 - PackageReference >>](../Stage%202%20-%20PackageReference)
