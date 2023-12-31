using UnityEngine;
using System.Collections;


public partial class Puzzle2
{

    public class State_TimeSet : State_Base
    {

        public State_TimeSet(Puzzle2 manager) : base(manager)
        {

        }


        #region OnState...
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            RenderSettings.skybox = Manager.skyBoxMaterialNight;
            Instantiate(Manager.paintingPart, Manager.paintingPartSpawnPoint);
            Manager.puzzlePostProcessingObject.SetActive(false);
            Manager.realityPostProcessingObject.SetActive(true);
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
