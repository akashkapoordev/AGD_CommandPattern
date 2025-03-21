using Command.Actions;
using Command.Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public class BerserkAttackCommand : UnitCommand
    {
        private bool hitTargert;

        public BerserkAttackCommand(CommandData commandData)
        {
            this.commandData = commandData;
            hitTargert = WillHitTarget();

        }

        public override bool WillHitTarget() =>  true;

        public override void Execute()
        {
            GameService.Instance.ActionService.GetActionByType(CommandType.BerserkAttack).PerformAction(actorUnit,targetUnit,hitTargert);
        }

        public override void Undo()
        {
            if (hitTargert)
            {
                if (!targetUnit.IsAlive())
                    targetUnit.Revive();

                targetUnit.RestoreHealth(actorUnit.CurrentPower * 2);
            }
            else
            {
                if (!actorUnit.IsAlive())
                    actorUnit.Revive();

                actorUnit.RestoreHealth(actorUnit.CurrentPower * 2);
            }
            actorUnit.Owner.ResetCurrentActiveUnit();
        }
    }
}

