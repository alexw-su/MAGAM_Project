using UnityEngine;
using System.Collections;


public partial class Puzzle4_StateMachine
{

    public class Puzzle4_State_Idle : Puzzle4_State_Base
    {

        public Puzzle4_State_Idle(Puzzle4_StateMachine manager) : base(manager)
        {

        }


        #region OnState...
        public override void OnStateEnter()
        {
            base.OnStateEnter();

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