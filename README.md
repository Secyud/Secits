# Secits

[中文](#secits-cn) | [English](#secits-en)

---

<a name="secits-en"></a>
## Secits (English)

Secits is a Blazor component library designed with a plugin-oriented architecture. Unlike most traditional component libraries, Secits focuses on making component development more flexible and reusable through a highly extensible plugin system.

### 🚀 Overview

- **Plugin-based Architecture**: Components are built using a "主体 (Subject) + 插件 (Plugin)" model. You can change a component's functionality and appearance by attaching different plugins.
- **SComponentBase**: A custom base class that provides fine-grained control over the Blazor component lifecycle.
- **Experimental**: This project is currently experimental. Performance and stability have not been strictly verified.

### 🛠 Tech Stack

- **Framework**: .NET 10 (C# 14.0)
- **UI**: Blazor (Server-side & WebAssembly)
- **JS Bundling**: [esbuild](https://esbuild.github.io/)
- **Styling**: CSS Isolation, FontAwesome integration

### 📁 Project Structure

- `src/Secyud.Secits.Blazor`: Core library containing base classes and core logic.
- `src/Secyud.Secits.Blazor.Preset`: Pre-defined component settings and examples.
- `src/Secyud.Secits.Blazor.FontAwesome`: FontAwesome icon support.
- `src/Secyud.Secits.Blazor.SourceGenerator`: Compile-time optimizations for the plugin system.
- `demo/`: Demo applications showing both Server and WASM usage.

### 💻 Getting Started

#### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download)
- An IDE like [JetBrains Rider](https://www.jetbrains.com/rider/) or [Visual Studio](https://visualstudio.microsoft.com/)

#### Build
To build the entire solution:
```powershell
dotnet build -graph
```

#### Running Demos
You can run the demo projects from the `demo/` folder:
- **Server Demo**: `demo/Secyud.Secits.Blazor.Server.Demo`
- **WASM Demo**: `demo/Secyud.Secits.Blazor.WebAssembly.Demo`

#### JS Asset Bundling
The project uses `esbuild` for JavaScript bundling. If you modify JS files in the core library:
```powershell
cd src/Secyud.Secits.Blazor
npm install
npm run build-component
```

### 📝 TODO
- [ ] Strict performance and stability testing.
- [ ] Comprehensive documentation for the plugin system.
- [ ] More pre-defined components in the Preset library.

### ⚖️ License
This project's license is not explicitly defined in the root. (ISC License is mentioned in `package.json`).

---

<a name="secits-cn"></a>
## Secits (中文)

一个 Blazor 组件库。与现有的大部分组件库不同，这个组件库稍显复杂，其目的不是提供现成的可用的组件，而是让组件的开发变得便捷。

采用插件式设计进行组件的开发，提供了一些现有的插件和主体组件，对主体组件使用各种插件可以改变组件的功能，形态。你还可以自己开发各种插件以实现不同程度的复用。

这只是一个实验性的项目，它的效率和功能并没有进行严格的验证，谨慎使用。

### 核心特性

- **SComponentBase**: 实现 `IComponent`, `IHandleEvent`, `IHandleAfterRender` 的自定义基类，提供类似 `ComponentBase` 但更灵活的生命周期管理。
- **可插拔组件**: 基于 `SPluggableBase` 构建，支持动态插件注入。

### Preset

提供了一些预制的主体组件设置，可以作为普通的组件库使用，同时也是一个实例，用于展示组件是如何设置的。
你可以像这样使用面向对象的方式设置组件，也可以使用函数式。

### 后续

插件式开发在运行时的效率和稳定性难以估量，后续考虑使用源码生成器进行进一步优化。