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

            Manager.button.GetComponent<ButtonPress>().OnTimeSet += Clock_OnTimeSet;
        }

        public override void OnStateRunning()
        {

        }


        public override void OnStateExit()
        {

            base.OnStateExit();
            Manager.button.GetComponent<ButtonPress>().OnTimeSet -= Clock_OnTimeSet;
        }
        #endregion
        void Clock_OnTimeSet()
        {
            Manager.ChangeState(Puzzle2States.TimeSet);
            Manager.clock1.tag = "Untagged";
            Manager.button.tag = "Untagged";
            Manager.crystal.SetActive(true);
            Manager.particles1.gameObject.SetActive(true);
        }
    }
}
