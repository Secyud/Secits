# Secits

---

## Secits

Secits 是一套基于插件化组件的设计。功能组件可以使用各种插件进行自定义而不是传统的组合或者继承。这方便了组件的自定义，但同时可能会带来使用的不便。
Secyud 将对这套设计进行研究，核心在于自定义程度和使用便捷性之间的平衡。
在编写组件时，部分less、js是与项目分离的，便于在Blazor或其他项目中复用。

### Secits React

Secits React 是Secits在React上的实现，目前还未开始。

### Secits Blazor

Secits Blazor 是Secits在Blazor上的实现。目前已经有部分可以使用的组件，例如 Avatar，Button。期间经历过多次重构，以协调我们的设计理念。

#### Project Structure

- `src/Secyud.Secits.Blazor`: 核心的组件和逻辑。
- `src/Secyud.Secits.Blazor.Generator`: 用于代码生成的项目。
- `demo/Secyud.Secits.Blazor.Demo`: 展示页面的代码。
- `demo/Secyud.Secits.Blazor.Server.Demo`: Blazor Server模式的Demo。

#### Build

在编译项目之前，我们推荐先生成必要的样式和字体，`wwwroot`里面的所有less应当正确编译。
如果你修改了部分svg，则需要执行`components/buildSecits.ts`生成字体。
准备好之后，可以编译 `src/Secyud.Secits.Blazor`并执行demo。

