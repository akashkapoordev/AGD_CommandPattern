using Command.Actions;
using Command.Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public class AttackCommand : UnitCommand
    {
        private bool hitTargert;

        public AttackCommand(CommandData commandData)
        {
            this.commandData = commandData;
            hitTargert = WillHitTarget();

        }

        public override bool WillHitTarget() =>  true;

        public override void Execute()
        {
            GameService.Instance.ActionService.GetActionByType(CommandType.Attack).PerformAction(actorUnit,targetUnit,hitTargert);
        }

        public override void Undo()
        {
            if (hitTargert)
            {
                if (!targetUnit.IsAlive())
                    targetUnit.Revive();

                targetUnit.RestoreHealth(actorUnit.CurrentPower);
                actorUnit.Owner.ResetCurrentActiveUnit();
            }
        }
    }
}

