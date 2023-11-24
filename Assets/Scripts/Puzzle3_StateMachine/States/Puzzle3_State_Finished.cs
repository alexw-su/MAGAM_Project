using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

public partial class Puzzle3_StateMachine
{

    public class Puzzle3_State_Finished : Puzzle3_State_Base
    {

        public Puzzle3_State_Finished(Puzzle3_StateMachine manager, Puzzle3_StateProperties property) : base(manager, property)
        {

        }

        private float _timer = 0f;
        private float _lerp = 0f;
        private int _colorIndex = 0;

        private AnimationCurve _pulseAnimCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        #region OnState...
        public override void OnStateEnter()
        {
            base.OnStateEnter();

            Debug.Log("You have solved the puzzle, Congrats!");
        }


        public override void OnStateRunning()
        {
            _timer += Time.deltaTime;

            _lerp = _timer;

            if (_timer < Manager.solvedPuzzleInterval * 0.5f)
            {
                //Light up
                _lerp = _timer / Manager.solvedPuzzleInterval * 2f;

                _lerp = _pulseAnimCurve.Evaluate(_lerp);

                Manager._cauldronMaterial.SetColor("_MainColor", Color.LerpUnclamped(Color.white, Manager.solvedPuzzleColorSequence[_colorIndex], _lerp));
            }
            else if(_timer >= Manager.solvedPuzzleInterval * 0.5f && _timer < Manager.solvedPuzzleInterval)
            {
                _lerp = Mathf.Repeat(_timer, Manager.solvedPuzzleInterval * 0.5f);

                _lerp = _pulseAnimCurve.Evaluate(_lerp);

                Manager._cauldronMaterial.SetColor("_MainColor", Color.LerpUnclamped( Manager.solvedPuzzleColorSequence[_colorIndex], Color.white, _lerp));
            }
            else
            {
                _timer = 0f;
                _lerp = 0f;

                _colorIndex++;

                if(_colorIndex >= Manager.solvedPuzzleColorSequence.Count)
                {
                    _colorIndex = 0;
                }
            }


            Debug.Log($"Timer: {_timer}, Lerp: {_lerp}, Color Index: {_colorIndex}");
        }


        public override void OnStateExit()
        {
            base.OnStateExit();
        }
        #endregion
    }
}