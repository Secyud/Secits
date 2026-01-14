# Secits Development Guidelines

This document provides essential information for developers working on the Secits Blazor component library.

### 1. Build/Configuration Instructions

The project uses standard .NET and follows a modular structure.

#### Prerequisites
- .NET
- IDE (Rider or Visual Studio)

#### Build Steps
To build the entire solution, run the following command from the root directory:
```powershell
dotnet build -graph
```

### 2. Additional Development Information

#### Plugin-based Architecture
Secits is designed with a plugin-oriented approach. Components are often built to be extensible via plugins.
- **SComponentBase**: A custom base class for components that implements `IComponent`, `IHandleEvent`, and `IHandleAfterRender`. It provides a custom lifecycle management similar to `ComponentBase`.
- **Pluggable Components**: Look for `SPluggableBase` to understand how to create components that support plugins.

#### Code Style and Patterns
- **Namespace Convention**: `Secyud.Secits.Blazor.*`
- **File Scoped Namespaces**: Use file-scoped namespaces (e.g., `namespace Secyud.Secits.Blazor;`).
- **Implicit Usings**: Enabled in most projects.
- **Async/Await**: Prefer asynchronous methods for I/O and lifecycle events (e.g., `OnInitializedAsync`).
- **CSS Isolation**: Components use CSS isolation (look for `.razor.css` files).

#### Source Generators
The project includes a `Secyud.Secits.Blazor.SourceGenerator` project, which is intended for future optimizations of the plugin system at compile time.
