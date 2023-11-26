using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

public partial class Puzzle3_StateMachine
{

    public class Puzzle3_State_Finished_Cauldron : Puzzle3_State_Base
    {

        public Puzzle3_State_Finished_Cauldron(Puzzle3_StateMachine manager, Puzzle3_StateProperties property) : base(manager, property)
        {

        }

        #region OnState...
        public override void OnStateEnter()
        {
            base.OnStateEnter();

            Debug.Log("You have solved the puzzle, Congrats!");
        }


        public override void OnStateRunning()
        {
            Manager.cauldronColorHandler.InitColorSequence(Manager.solvedPuzzleColorSequence, Manager.solvedPuzzleInterval);

            Manager.StartCoroutine(Wait_Routine(0.5f));
        }


        public override void OnStateExit()
        {
            base.OnStateExit();
        }
        #endregion


        private IEnumerator Wait_Routine(float waitingTime)
        {
            yield return new WaitForSeconds(waitingTime);
            Manager.UpdatePuzzlePhases(true, Puzzle3_State.Book_Phase1);
        }
    }
}