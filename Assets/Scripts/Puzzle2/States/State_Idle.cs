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
            Manager.clock.OnClockSolved += Clock_OnClockSolved;
        }

        public override void OnStateRunning()
        {

        }


        public override void OnStateExit()
        {
            base.OnStateExit();
            Manager.clock.OnClockSolved -= Clock_OnClockSolved;
        }
        #endregion


        //This is triggered once the ClockScript fires the OnClockSolved event
        private void Clock_OnClockSolved()
        {
            Manager.ChangeState(Puzzle2States.TimeSet);
        }
    }
}