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

            Manager.crystal.SetActive(true);
            Manager.doorBlock.GetComponent<Collider>().enabled = false;
            Manager.particles1.gameObject.SetActive(true);
            Manager.particles2.gameObject.SetActive(true);

            RenderSettings.skybox = Manager.skyBoxMaterialNight;
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