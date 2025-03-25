using Command.Actions;
using Command.Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public class ThirdEyeCommand : UnitCommand
    {
        private bool hitTargert;

        public ThirdEyeCommand(CommandData commandData)
        {
            this.commandData = commandData;
            hitTargert = WillHitTarget();

        }

        public override bool WillHitTarget() =>  true;

        public override void Execute()
        {
            GameService.Instance.ActionService.GetActionByType(CommandType.ThirdEye).PerformAction(actorUnit,targetUnit,hitTargert);
        }
    }
}

