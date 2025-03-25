using Command.Main;
using Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Commands
{
    public class CommandInvoker
    {


        public CommandInvoker()
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameService.Instance.EventService.OnReplayButtonClicked.AddListener(SetReplayStack);
        }

        private Stack<ICommand> commandRegistry = new Stack<ICommand>();

        public void ExecuteCommand(ICommand command) => command.Execute();

        public void RegisterCommand(ICommand command) => commandRegistry.Push(command);

        public void ProcessCommand(ICommand command)
        {
            ExecuteCommand(command);
            RegisterCommand(command);
        }

        public void Undo()
        {
            if (!RegistryEmpty() && CommandBelongsToActivePlayer())
                commandRegistry.Pop().Undo();
        }
        private bool RegistryEmpty() => commandRegistry.Count == 0;

        private bool CommandBelongsToActivePlayer()
        {
            return (commandRegistry.Peek() as UnitCommand).commandData.ActorPlayerID == GameService.Instance.PlayerService.ActivePlayerID;
        }

        private void SetReplayStack()
        {
            GameService.Instance.ReplayService.SetCommandStack(commandRegistry);
            commandRegistry.Clear();
        }
    }
}

