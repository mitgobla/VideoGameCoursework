using System;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : Singleton<CommandManager>
{
    private Stack<ICommand> _commands = new Stack<ICommand>();

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();
        _commands.Push(command);
    }

    public void UndoCommand()
    {
        if (_commands.Count <= 0) return;
        var command = _commands.Pop();
        command.Undo();
    }
}
