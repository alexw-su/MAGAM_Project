using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

public partial class Puzzle3_StateMachine
{

    public class Puzzle3_State_Finished_Book : Puzzle3_State_Base
    {

        public Puzzle3_State_Finished_Book(Puzzle3_StateMachine manager, Puzzle3_StateProperties property) : base(manager, property)
        {

        }

        #region OnState...
        public override void OnStateEnter()
        {
            base.OnStateEnter();

            Debug.Log("And Now you get the final piece of the puzzle");
        }


        public override void OnStateRunning()
        {
            return;
        }


        public override void OnStateExit()
        {
            base.OnStateExit();
        }
        #endregion
    }
}