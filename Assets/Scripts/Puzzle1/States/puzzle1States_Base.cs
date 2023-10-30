using UnityEngine;
using System.Collections;


public partial class Puzzle1
{

    public class puzzle1States_Base : MarcimanState
    {
        private Puzzle1 _manager;
        public Puzzle1 Manager { get => _manager; }

        public puzzle1States_Base(Puzzle1 manager)
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