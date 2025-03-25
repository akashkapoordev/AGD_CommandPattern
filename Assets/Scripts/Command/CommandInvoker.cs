using Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Commands
{
    public class CommandInvoker
    {
        private Stack<ICommand> commandRegistry = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command) => command.Execute();

        public void RegisterCommand(ICommand command) => commandRegistry.Push(command);

        public void ProcessCommand(ICommand command)
        {
            ExecuteCommand(command);
            RegisterCommand(command);
        }
    }
}

