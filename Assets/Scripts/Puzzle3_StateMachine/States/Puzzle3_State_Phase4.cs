using UnityEngine;
using System.Collections;


public partial class Puzzle3_StateMachine
{

    public class Puzzle3_State_Phase4 : Puzzle3_State_Base
    {

        public Puzzle3_State_Phase4(Puzzle3_StateMachine manager) : base(manager)
        {

        }


        #region OnState...
        public override void OnStateEnter()
        {
            base.OnStateEnter();

            InitPuzzle3State(1, Puzzle3Elements.Earth);
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