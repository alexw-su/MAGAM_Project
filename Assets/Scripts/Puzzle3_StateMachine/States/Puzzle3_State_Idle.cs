using UnityEngine;
using System.Collections;


public partial class Puzzle3_StateMachine
{

    public class Puzzle3_State_Idle : Puzzle3_State_Base
    {

        public Puzzle3_State_Idle(Puzzle3_StateMachine manager, Puzzle3_StateProperties property) : base(manager, property)
        {

        }


        #region OnState...
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Manager.ChangeState(Puzzle3_State.Phase1);
        }


        public override void OnStateRunning()
        {

        }


        public override void OnStateExit()
        {
            base.OnStateExit();
        }
        #endregion
    }
}