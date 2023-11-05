using UnityEngine;
using System.Collections;


public partial class Puzzle2
{

    public class State_Base : MarcimanState
    {
        private Puzzle2 _manager;
        public Puzzle2 Manager { get => _manager; }

        public State_Base(Puzzle2 manager)
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