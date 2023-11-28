using UnityEngine;
using System.Collections;


public partial class Puzzle4_StateMachine
{

    public class Puzzle4_State_Base : MarcimanState
    {
        private Puzzle4_StateMachine _manager;
        public Puzzle4_StateMachine Manager { get => _manager; }

        public Puzzle4_State_Base(Puzzle4_StateMachine manager)
        {
            _manager = manager;
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