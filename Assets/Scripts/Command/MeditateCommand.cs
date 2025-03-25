using Command.Actions;
using Command.Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public class MeditateCommand : UnitCommand
    {
        private bool hitTargert;

        public MeditateCommand(CommandData commandData)
        {
            this.commandData = commandData;
            hitTargert = WillHitTarget();

        }

        public override bool WillHitTarget() =>  true;

        public override void Execute()
        {
            GameService.Instance.ActionService.GetActionByType(CommandType.Meditate).PerformAction(actorUnit,targetUnit,hitTargert);
        }
    }
}

