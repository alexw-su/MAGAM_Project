using UnityEngine;
using System.Collections;


public partial class Puzzle1
{

    public class puzzle1States_Idle : puzzle1States_Base
    {

        public puzzle1States_Idle(Puzzle1 manager) : base(manager)
        {

        }


        #region OnState...
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            //Debug.Log("Entered Idle State");
            Manager.SetActiveTree(false);
        }


        public override void OnStateRunning()
        {

        }


        public override void OnStateExit()
        {
            base.OnStateExit();
            //Debug.Log("Exiting Idle State");
        }
        #endregion
    }
}