using UnityEngine;
using System.Collections;


public partial class Puzzle3_StateMachine
{

    public class Puzzle3_State_Phase1 : Puzzle3_State_Base
    {

        public Puzzle3_State_Phase1(Puzzle3_StateMachine manager, Puzzle3_StateProperties property) : base(manager, property)
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