using System.Collections;
using System.Collections.Generic;
using Command.Main;
using Commands;
using UnityEngine;

namespace Replay
{
    public class ReplayService
    {
        private Stack<ICommand> replayCommandsStack;

        public ReplayMode replayState {  get; private set; }

        public ReplayService()
        {
            SetReplayMode(ReplayMode.DEACTIVATE);
        }
        private void SetReplayMode(ReplayMode replayMode) => replayState = replayMode; 

        public void SetCommandStack(Stack<ICommand> commandToSet)
        {
            replayCommandsStack = new Stack<ICommand>(commandToSet);
        }

        public void ExceuteNext()
        {
            if(replayCommandsStack.Count > 0)
            {
                GameService.Instance.ProcessUnitCommand(replayCommandsStack.Pop());
            }
        }
    }

}
