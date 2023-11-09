using UnityEngine;
using System.Collections;


public partial class Puzzle3_StateMachine
{

    public class Puzzle3_State_Base : MarcimanState
    {
        private Puzzle3_StateMachine _manager;
        public Puzzle3_StateMachine Manager { get => _manager; }

        public Puzzle3_State_Base(Puzzle3_StateMachine manager)
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


        //Setting ItemType that needs to be placed in the Caludron in order for it to count.
        //Also Setting Element Goal for Amount of Items of this type that need to be placed to advance.
        protected void InitPuzzle3State(int elementGoal, Puzzle3Elements elementType)
        {
            //Resets Counter in Base State everytime a new State is entered
            Manager._currentElementsPlacedCounter = 0;

            Manager._currentElementsPlacedGoal = elementGoal;
            Manager._currentElementToBePlaced = elementType;
        }
    }
}