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
        private int previousMaxHealth;

        public MeditateCommand(CommandData commandData)
        {
            this.commandData = commandData;
            hitTargert = WillHitTarget();

        }

        public override bool WillHitTarget() =>  true;

        public override void Execute()
        {
            previousMaxHealth = targetUnit.CurrentMaxHealth;
            GameService.Instance.ActionService.GetActionByType(CommandType.Meditate).PerformAction(actorUnit,targetUnit,hitTargert);
        }

        public override void Undo()
        {
            if (hitTargert)
            {
                var healthToReduce = targetUnit.CurrentMaxHealth - previousMaxHealth;
                targetUnit.CurrentMaxHealth = previousMaxHealth;
                targetUnit.TakeDamage(healthToReduce);
            }
            actorUnit.Owner.ResetCurrentActiveUnit();
        }
    }
}

