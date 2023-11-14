using UnityEngine;
using System.Collections;


public partial class Puzzle3_StateMachine
{

    public class Puzzle3_State_Base : MarcimanState
    {
        protected Puzzle3_StateProperties _stateProperty;

        private Puzzle3_StateMachine _manager;
        public Puzzle3_StateMachine Manager { get => _manager; }

        public Puzzle3_State_Base(Puzzle3_StateMachine manager, Puzzle3_StateProperties property)
        {
            _manager = manager;

            InitPuzzle3State(property.StateGoal, property.Element);
            _stateProperty = property;
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

        //Exit is considered evil if on Exit the amount of times an element was placed on the counter is less than required.
        protected bool IsExitEvil()
        {
            return Manager._currentElementsPlacedCounter < Manager._currentElementsPlacedCounter;
        }


        protected void SpawnCloud(GameObject cloud)
        {
            Instantiate(cloud, Manager.particleEffectLocation);
        }
    }
}