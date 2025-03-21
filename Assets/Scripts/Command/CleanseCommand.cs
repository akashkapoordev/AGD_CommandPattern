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
        private int previousPower;

        public CleanseCommand(CommandData commandData)
        {
            this.commandData = commandData;
            hitTargert = WillHitTarget();

        }

        public override bool WillHitTarget() =>  true;

        public override void Execute()
        {
            previousPower = targetUnit.CurrentPower;
            GameService.Instance.ActionService.GetActionByType(CommandType.Cleanse).PerformAction(actorUnit,targetUnit,hitTargert);
        }

        public override void Undo()
        {
            if (hitTargert)
                targetUnit.CurrentPower = previousPower;

            actorUnit.Owner.ResetCurrentActiveUnit();
        }
    }
}

