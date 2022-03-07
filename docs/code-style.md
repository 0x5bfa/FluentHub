# Style guidlines

* Follow Model-View-ViewModel(MVVM) design path.
* The code must be well-written and expose a clear purpose.
* Your code must be easily maintainable without requiring to rewrite core parts, making sure no code is being duplicated, utilizing abstractions and inheritance is a great way  to sustain high code metrics.
* Follow the CodeFactor configuration.

## Coding style

To make the codebase consistent and easy to understand, we require you to follow [predefined code style rules](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions) and the parts that differ from the guidelines are listed below:

### Naming conventions

* Do not add the `s_` or `t_` prefix to static or thread static fields.

### Layouts

* Adding multiple blank lines and CodeFactor points out as an error.

### Comments

* XML comments can be omitted only if the purpose of the statement is clear or general without writing it.

### Others

* Use new (); whenever possible to instantiate variables.