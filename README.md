# Secits

---

## Secits (English)

Secits is a Blazor component library designed with a plugin-oriented architecture. Unlike most traditional component
libraries, Secits focuses on making component development more flexible and reusable through a highly extensible plugin
system.

### 🚀 Overview

- **Plugin-based Architecture**: Components are built using a "主体 (Subject) + 插件 (Plugin)" model. You can change a
  component's functionality and appearance by attaching different plugins.
- **SComponentBase**: A custom base class that provides fine-grained control over the Blazor component lifecycle.
- **Experimental**: This project is currently experimental. Performance and stability have not been strictly verified.

### 🛠 Tech Stack

- **Framework**: .NET 10 (C# 14.0)
- **UI**: Blazor (Server-side & WebAssembly)
- **JS Bundling**: [esbuild](https://esbuild.github.io/)
- **Styling**: CSS Isolation, FontAwesome integration

### 📁 Project Structure

- `src/Secyud.Secits.Blazor`: Core library containing base classes and core logic.
- `demo/`: Demo applications showing both Server and WASM usage.

### 💻 Getting Started

#### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- An IDE like [JetBrains Rider](https://www.jetbrains.com/rider/)
  or [Visual Studio](https://visualstudio.microsoft.com/)

#### Build

To build the entire solution:

#### Running Demos

You can run the demo projects from the `demo/` folder:

- **Server Demo**: `demo/Secyud.Secits.Blazor.Server.Demo`
- **WASM Demo**: `demo/Secyud.Secits.Blazor.WebAssembly.Demo`

#### JS Asset Bundling

The project uses `esbuild` for JavaScript bundling. If you modify JS files in the core library:

### 📝 TODO

- [ ] Strict performance and stability testing.
- [ ] Comprehensive documentation for the plugin system.
- [ ] More pre-defined components in the Preset library.

### ⚖️ License

This project's license is not explicitly defined in the root. (ISC License is mentioned in `package.json`).

---
