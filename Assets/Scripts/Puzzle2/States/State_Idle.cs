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
            Manager.clock1.GetComponent<ClockScript>().OnPointerPlaced += Clock_OnPointerPlaced;
        }

        public override void OnStateRunning()
        {

        }


        public override void OnStateExit()
        {
            base.OnStateExit();
            Manager.clock1.GetComponent<ClockScript>().OnPointerPlaced -= Clock_OnPointerPlaced;
        }
        #endregion


        private void Clock_OnPointerPlaced()
        {
            Debug.Log("3");
            Manager.ChangeState(Puzzle2States.PointerPlaced);
        }
    }
}
