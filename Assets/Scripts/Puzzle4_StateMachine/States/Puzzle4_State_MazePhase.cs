using UnityEngine;
using System.Collections;


public partial class Puzzle4_StateMachine
{

    public class Puzzle4_State_MazePhase : Puzzle4_State_Base
    {

        public Puzzle4_State_MazePhase(Puzzle4_StateMachine manager) : base(manager)
        {

        }


        #region OnState...
        public override void OnStateEnter()
        {
            base.OnStateEnter();

            Manager.playerTrigger.OnPlayerEnterTrigger += PlayerTrigger_OnPlayerEnterTrigger;
        }

        public override void OnStateRunning()
        {

        }


        public override void OnStateExit()
        {
            base.OnStateExit();
            Manager.playerTrigger.OnPlayerEnterTrigger -= PlayerTrigger_OnPlayerEnterTrigger;
        }
        #endregion



        private void PlayerTrigger_OnPlayerEnterTrigger()
        {
            Manager.ChangeState(Puzzle4_State.Finished);
        }
    }
}