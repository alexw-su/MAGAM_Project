using UnityEngine;
using System.Collections;


public partial class Puzzle2
{

    public class State_Idle : State_Base
    {

        public State_Idle(Puzzle2 manager) : base(manager)
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