using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Puzzle2
{

    public class State_PointerPlaced : State_Base
    {

        public State_PointerPlaced(Puzzle2 manager) : base(manager)
        {

        }


        #region OnState...
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Manager.clock1.SetActive(false);
            Manager.clock2.SetActive(true);
            Manager.crystal.SetActive(false);
            Manager.particles1.gameObject.SetActive(false);
            Manager.particles2.gameObject.SetActive(false);
        }

        public override void OnStateRunning()
        {

        }


        public override void OnStateExit()
        {

            base.OnStateExit();
        }
        #endregion


        //This is triggered once the ClockScript fires the OnClockSolved event
        private void Clock_OnClockSolved()
        {
            Debug.Log("Clock_OnClockSolved");
            Manager.ChangeState(Puzzle2States.TimeSet);
        }
    }
}
