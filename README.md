# ScaffoldR

ScaffoldR is a [SOLID](https://en.wikipedia.org/wiki/SOLID_%28object-oriented_design%29) platform for structuring or scaffolding .NET applications. ScaffoldR supports commands, queries, events, repositories and validation with intelligent dispatching via C# generic variance.

It is recommended to have some basic knowledge of CQRS (Command Query Responsibility Segregation). A good starting point is these articles:

* [Meanwhile... on the command side of my architecture](https://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=91)
* [Meanwhile... on the query side of my architecture](https://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=92)

### Installing ScaffoldR

You should install [ScaffoldR with NuGet](https://www.nuget.org/packages/ScaffoldR):

    Install-Package ScaffoldR

This command from Package Manager Console will download and install ScaffoldR and all required dependencies.

You should register ScaffoldR in the Simple Injector container during startup:

```cs
var container = new Container();
container.Options.DefaultScopedLifestyle = Lifestyle.CreateHybrid(() =>
	container.GetCurrentLifetimeScope() != null,
	new LifetimeScopeLifestyle(),
	new WebRequestLifestyle() // This example is a MVC application
);

container.RegisterScaffoldR(settings =>
{
	settings.EventAssemblies = new[] { Assembly.GetExecutingAssembly() };
	settings.FluentValidationAssemblies = new[] { Assembly.GetExecutingAssembly() };
	settings.TransactionAssemblies = new[] { Assembly.GetExecutingAssembly() };
	settings.ViewModelAssemblies = new[] { Assembly.GetExecutingAssembly() };
});
```


### Dependencies 
**[Simple Injector](https://simpleinjector.org)**<br />
Simple Injector is an easy, flexible and fast dependency injection library which ScaffoldR integrates heavily into.

**[Fluent Validation](https://fluentvalidation.codeplex.com)**<br />
Fluent Validation (FV) is a small validation library for .NET that uses a fluent interface and lambda expressions for building validation rules for your business objects. Nerve Framework uses FV in its validation processor.

### Examples
