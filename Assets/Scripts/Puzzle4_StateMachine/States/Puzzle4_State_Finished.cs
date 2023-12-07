using UnityEngine;
using System.Collections;


public partial class Puzzle4_StateMachine
{

    public class Puzzle4_State_Finished : Puzzle4_State_Base
    {

        public Puzzle4_State_Finished(Puzzle4_StateMachine manager) : base(manager)
        {

        }


        #region OnState...
        public override void OnStateEnter()
        {
            base.OnStateEnter();

            Manager.crystalHandler.InitAppearance(2f);
            Manager.DelayOpenTitleScene();
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