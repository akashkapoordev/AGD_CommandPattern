using Command.Main;
using Command.Player;
using Command.Actions;
using Commands;
using System;

namespace Command.Input
{
    public class InputService
    {
        private MouseInputHandler mouseInputHandler;

        private InputState currentState;
        private CommandType selectedCommandType;
        private TargetType targetType;

        public InputService()
        {
            mouseInputHandler = new MouseInputHandler(this);
            SetInputState(InputState.INACTIVE);
            SubscribeToEvents();
        }

        public void SetInputState(InputState inputStateToSet) => currentState = inputStateToSet;

        private void SubscribeToEvents() => GameService.Instance.EventService.OnActionSelected.AddListener(OnActionSelected);

        public void UpdateInputService()
        {
            if(currentState == InputState.SELECTING_TARGET)
                mouseInputHandler.HandleTargetSelection(targetType);
        }

        public void OnActionSelected(CommandType selectedCommandType)
        {
            this.selectedCommandType = selectedCommandType;
            SetInputState(InputState.SELECTING_TARGET);
            TargetType targetType = SetTargetType(selectedCommandType);
            ShowTargetSelectionUI(targetType);
        }

        private void ShowTargetSelectionUI(TargetType selectedTargetType)
        {
            int playerID = GameService.Instance.PlayerService.ActivePlayerID;
            GameService.Instance.UIService.ShowTargetOverlay(playerID, selectedTargetType);
        }

        private TargetType SetTargetType(CommandType selectedCommandType) => targetType = GameService.Instance.ActionService.GetTargetTypeForAction(selectedCommandType);

        public void OnTargetSelected(UnitController targetUnit)
        {
            SetInputState(InputState.EXECUTING_INPUT);

            UnitCommand unitCommand = CreateUnitCommand(targetUnit); 
        }

        public CommandData createCommandData(UnitController targetUnit)
        {
            return new CommandData(
                GameService.Instance.PlayerService.ActiveUnitID,
                targetUnit.UnitID,
                GameService.Instance.PlayerService.ActivePlayerID,
                targetUnit.Owner.PlayerID

                );

        }

        private UnitCommand CreateUnitCommand(UnitController targetUnit)
        {
            CommandData commandData = createCommandData(targetUnit);

            switch(selectedCommandType)
            {
                case CommandType.Attack:
                    return new AttackCommand(commandData);
                case CommandType.AttackStance:
                    return new AttackStanceCommand(commandData);
                case CommandType.BerserkAttack:
                    return new BerserkAttackCommand(commandData);
                case CommandType.Heal:
                    return new HealCommand(commandData);
                case CommandType.Cleanse:
                    return new CleanseCommand(commandData);
                case CommandType.Meditate:
                    return new MeditateCommand(commandData);
                case CommandType.ThirdEye:
                    return new ThirdEyeCommand(commandData);
                default:
                    throw new System.Exception($"No Command found of type: {selectedCommandType}");
            }
        }
    }
}