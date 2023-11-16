using UnityEngine;
using System.Collections;


public partial class Puzzle3_StateMachine
{

    public class Puzzle3_State_Base : MarcimanState
    {
        private Puzzle3_StateMachine _manager;
        public Puzzle3_StateMachine Manager { get => _manager; }

        public Puzzle3_State_Base(Puzzle3_StateMachine manager, Puzzle3_StateProperties property)
        {
            _manager = manager;

            InitPuzzle3State(property);
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


        //Setting ItemType that needs to be placed in the Caludron in order for it to count.
        //Also Setting Element Goal for Amount of Items of this type that need to be placed to advance.
        protected void InitPuzzle3State(Puzzle3_StateProperties property)
        {
            //Resets Counter in Base State everytime a new State is entered
            Manager._currentActiveStateProperty = property;

            Debug.Log("Now in state " + property.Element);
            Manager._currentElementsPlacedCounter = 0;
        }
    }
}