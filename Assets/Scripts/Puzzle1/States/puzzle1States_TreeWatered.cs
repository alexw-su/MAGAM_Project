using UnityEngine;
using System.Collections;


public partial class Puzzle1
{

    public class puzzle1States_TreeWatered : puzzle1States_Base
    {

        public puzzle1States_TreeWatered(Puzzle1 manager) : base(manager)
        {
            
        }

        #region OnState...
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("Entering Watered Tree State");
            Manager.SetActiveTree(true);
            Manager.ScaleUpPaintingTree(Manager.scaleSize);
        }


        public override void OnStateRunning()
        {

        }


        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("Exiting Watered Tree State");
        }
        #endregion
    }
}