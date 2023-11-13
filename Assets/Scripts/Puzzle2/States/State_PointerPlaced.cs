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