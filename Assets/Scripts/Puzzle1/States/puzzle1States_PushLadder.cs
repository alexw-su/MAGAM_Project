using UnityEngine;
using System.Collections;


public partial class Puzzle1
{

    public class puzzle1States_PushLadder : puzzle1States_Base
    {

        public puzzle1States_PushLadder(Puzzle1 manager) : base(manager)
        {

        }


        #region OnState...
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("Entered Pushing the Ladder State");
        }


        public override void OnStateRunning()
        {

        }


        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Ladder Pushed, Exiting Pushing the Ladder State");
        }
        #endregion
    }
}