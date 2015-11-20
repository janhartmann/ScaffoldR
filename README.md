# ScaffoldR [![Build status](https://ci.appveyor.com/api/projects/status/tei7i18ousu5nfjx/branch/master?svg=true)](https://ci.appveyor.com/project/janhartmann/scaffoldr/branch/master)

ScaffoldR is a [SOLID](https://en.wikipedia.org/wiki/SOLID_%28object-oriented_design%29) platform for structuring or scaffolding .NET applications. ScaffoldR supports commands, queries, events, repositories and validation with intelligent dispatching via C# generic variance.

It is recommended to have some basic knowledge of CQRS (Command Query Responsibility Segregation) and Dependency Injection. A good starting point is these articles:

* [Meanwhile... on the command side of my architecture](https://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=91)
* [Meanwhile... on the query side of my architecture](https://www.cuttingedge.it/blogs/steven/pivot/entry.php?id=92)

### Dependencies 
**[Simple Injector](https://simpleinjector.org)**<br />
Simple Injector is an easy, flexible and fast dependency injection library which ScaffoldR integrates heavily into.

**[Fluent Validation](https://fluentvalidation.codeplex.com)**<br />
Fluent Validation (FV) is a small validation library for .NET that uses a fluent interface and lambda expressions for building validation rules for your business objects. ScaffoldR uses FV in its validation processor.

### Installing ScaffoldR

You should install [ScaffoldR with NuGet](https://www.nuget.org/packages/ScaffoldR):

    Install-Package ScaffoldR

This command from Package Manager Console will download and install ScaffoldR and all required dependencies.

### Getting Started

You should register ScaffoldR in the Simple Injector container during startup. In the registration below, we configure the default scope of the container and register the ScaffoldR into it. 

We also make Fluent Validation use Simple Injector for finding the validation classes, using the `SimpleInjectorValidatorFactory`:

```cs
var container = new Container();
container.Options.DefaultScopedLifestyle = Lifestyle.CreateHybrid(() =>
	container.GetCurrentLifetimeScope() != null,
	new LifetimeScopeLifestyle(),
	new WebRequestLifestyle() // Example is for .NET MVC application
);

container.RegisterScaffoldR(settings =>
{
	settings.EventAssemblies = new[] { Assembly.GetExecutingAssembly() };
	settings.FluentValidationAssemblies = new[] { Assembly.GetExecutingAssembly() };
	settings.TransactionAssemblies = new[] { Assembly.GetExecutingAssembly() };
	settings.ViewModelAssemblies = new[] { Assembly.GetExecutingAssembly() };
});

FluentValidationModelValidatorProvider.Configure(provider => {
	provider.ValidatorFactory = new SimpleInjectorValidatorFactory(container);
	provider.AddImplicitRequiredValidator = false;
});
```

### Examples

In this example, we create a command with the nessecery properties to create a cup of coffee. A validator is attached to the command, which validates the command can be executed before the actual execution. 

Finally, we have the handler which does the business and creates the entity (Coffee) in the database.

```cs
/// <summary>
/// Create a cup of coffee.
/// </summary>
public class MakeCoffee : ICommand
{
	public int Strength { get; set; }
	public bool WithMilk { get; set; }
}

/// <summary>
/// Validates the coffee command, before executing it.
/// </summary>
public class ValidateMakeCoffee : AbstractValidator<MakeCoffee>
{
	public ValidateMakeCoffee()
	{
		RuleFor(coffee => coffee.Strength)
                    .NotEmpty()
                    .GreaterThan(0)
                    .LessThanOrEqualTo(10);
	}
}

/// <summary>
/// Create the cup of coffee and save it in the database.
/// </summary>
public class HandleMakeCoffee : IHandleCommand<MakeCoffee>
{
	private readonly IEntityWriter<Coffee> _entityWriter;

	public HandleMakeCoffee(IEntityWriter<Coffee> entityWriter)
	{
		_entityWriter = entityWriter;
	}

	public void Handle(MakeCoffee command)
	{
		var coffee = new Coffee
		{
			Strength = command.Strength,
			Milk = command.WithMilk
		};

		_entityWriter.Save(coffee);
	}
}
```

Now that we have our command, its validator and its handler - we can now execute the command from our controllers or other classes by simple depending on the `IHandleCommand<TCommand>` or using the mediators for executing:

```cs
public class CoffeeController : Controller
{
	private readonly IProcessCommands _commands;

	public CoffeeController(IProcessCommands commands)
	{
		_commands = commands;
	}

	[HttpPost]
	public ActionResult Create()
	{
		var cup = new MakeCoffee
		{
			Strength = 10,
			WithMilk = false
		};
		
		// Use the mediator to execute the command
		_commands.Execute(cup);

		return View();
	}
}
```

