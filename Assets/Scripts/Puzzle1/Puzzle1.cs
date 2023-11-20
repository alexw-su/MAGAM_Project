using UnityEngine;
using System.Collections;

public enum puzzle1States
{
	Idle,
	PushLadder,
	TreeWatered,
}

public partial class Puzzle1 : MarcimanStateMachine {

    private puzzle1States _currentState;
    public puzzle1States CurrentState { get => _currentState; }
	public float scaleSize;
	public GameObject tree;
	public ScaleUpTree paintingTree;


	void Start() {
		ChangeState(puzzle1States.Idle);
	}

    private void ChangeState(puzzle1States newState)
    {
        _currentState = newState;

        switch (newState)
        {
		case puzzle1States.Idle:
			SetState(new puzzle1States_Idle(this));
			break;
		case puzzle1States.PushLadder:
			SetState(new puzzle1States_PushLadder(this));
			break;
		case puzzle1States.TreeWatered:
			SetState(new puzzle1States_TreeWatered(this));
			break;
        }
    }


	public void Change(puzzle1States newState) 
	{
		ChangeState(newState);
	}


	private void SetActiveTree(bool enable)
	{
		tree.SetActive(enable);
	}

	private void ScaleUpPaintingTree(float size)
	{
		paintingTree.StartScaleToSize(size);
	}
}