using UnityEngine;
using System.Collections;


public partial class Puzzle3_StateMachine
{

    public class Puzzle3_State_Phase1 : Puzzle3_State_Base
    {

        public Puzzle3_State_Phase1(Puzzle3_StateMachine manager) : base(manager)
        {

        }


        #region OnState...
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            
            InitPuzzle3State(2, Puzzle3Elements.Fire);
        }


        public override void OnStateRunning()
        {

        }


        public override void OnStateExit()
        {
            base.OnStateExit();

            if (IsExitEvil())
            {
                //Trigger some sort of indication that you failed :D
                Debug.Log("You failed lol!!!!");
            }
        }
        #endregion
    }
}