using Command.Actions;
using Command.Main;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public class AttackStanceCommand : UnitCommand
    {
        private bool hitTargert;

        public AttackStanceCommand(CommandData commandData)
        {
            this.commandData = commandData;
            hitTargert = WillHitTarget();

        }

        public override bool WillHitTarget() =>  true;

        public override void Execute()
        {
            GameService.Instance.ActionService.GetActionByType(CommandType.AttackStance).PerformAction(actorUnit,targetUnit,hitTargert);
        }

        public override void Undo()
        {
            if (hitTargert)
            {
                targetUnit.CurrentPower -= (int)(targetUnit.CurrentPower * 0.2f);
                actorUnit.Owner.ResetCurrentActiveUnit();
            }
        }
    }
}

