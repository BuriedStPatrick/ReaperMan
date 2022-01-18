using System.CommandLine;

namespace ReaperMan.Cli;

public class CommandBuilder
{
    private readonly RootCommand _rootCommand;
    private readonly Stack<Command> _commandStack;

    public RootCommand Root => _rootCommand;

    public CommandBuilder(string description)
    {
        _rootCommand = new RootCommand(description);
        _commandStack = new Stack<Command>();
    }

    public void AddRootCommand(Command command)
    {
        _commandStack.Push(command);
    }

    public void AddSubCommand(Command command)
    {
        var currentCommand = _commandStack.Peek();
        currentCommand.AddCommand(command);
    }

    public RootCommand Build()
    {
        foreach (var command in _commandStack.Reverse())
        {
            _rootCommand.AddCommand(command);
        }

        return _rootCommand;
    } 
}

public static class CommandFactory
{
    /// <summary>
    /// Add a sub-command on the current root command
    /// </summary>
    /// <param name="commandBuilder"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public static CommandBuilder WithSubCommand(
        this CommandBuilder commandBuilder,
        Command command)
    {
        commandBuilder.AddSubCommand(command);

        return commandBuilder;
    }

    /// <summary>
    /// Add a root command
    /// </summary>
    /// <param name="commandBuilder"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public static CommandBuilder WithRootCommand(
        this CommandBuilder commandBuilder,
        Command command)
    {
        commandBuilder.AddRootCommand(command);

        return commandBuilder;
    }
    
    /// <summary>
    /// Add a root command
    /// </summary>
    /// <param name="commandBuilder"></param>
    /// <param name="name"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public static CommandBuilder WithCommandScope(
        this CommandBuilder commandBuilder,
        string name,
        string description)
    {
        commandBuilder.AddRootCommand(new Command(name, description));

        return commandBuilder;
    }
}