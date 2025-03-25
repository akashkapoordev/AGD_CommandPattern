using Command.Actions;
using Command.Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public class CleanseCommand : UnitCommand
    {
        private bool hitTargert;

        public CleanseCommand(CommandData commandData)
        {
            this.commandData = commandData;
            hitTargert = WillHitTarget();

        }

        public override bool WillHitTarget() =>  true;

        public override void Execute()
        {
            GameService.Instance.ActionService.GetActionByType(CommandType.Cleanse).PerformAction(actorUnit,targetUnit,hitTargert);
        }
    }
}

