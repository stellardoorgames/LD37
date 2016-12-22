using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum BarrierStates
{
	Disabled,
	Blue,
	Green,
	Red
}


public class BarrierController : MonoBehaviour {

	static Dictionary<BarrierStates, string> barrierLayers = new Dictionary<BarrierStates, string>
	{
		{BarrierStates.Blue, "BlueBarrier"},
		{BarrierStates.Green, "GreenBarrier"},
		{BarrierStates.Red, "RedBarrier"},
	};

	static Dictionary<string, string> layerNames = new Dictionary<string, string>
	{
		{"BlueBarrier", "BlueTentacle"},
		{"GreenBarrier", "GreenTentacle"},
		{"RedBarrier", "RedTentacle"},
	};

	[SerializeField]
	BarrierStates _state;
	public BarrierStates state{
		get {return _state;}
		set {_state = value; 
			SetState(value);}
	}

	public List<BarrierStates> stateSequence;

	public UnityEvent OnChangeBarrier;

	int sequenceIndex = 0;

	Collider barrierCollider;

	void Awake()
	{
		

		foreach (KeyValuePair<string, string> ln in layerNames)
		{
			int bl = LayerMask.NameToLayer(ln.Key);
			int tl = LayerMask.NameToLayer(ln.Value);

			for (int j = 0; j < 32 ; j++)
			{
				if (j != tl)
					Physics.IgnoreLayerCollision(bl, j);
			}
			
		}

	}

	void Start()
	{
		barrierCollider = GetComponent<Collider>();

		SetState(state);
	}

	public void CycleBarrier()
	{
		sequenceIndex++;
		if (sequenceIndex >= stateSequence.Count)
			sequenceIndex = 0;

		state = stateSequence[sequenceIndex];
	}

	public void ChangeState(BarrierStates newState)
	{
		state = newState;
	}

	BarrierStates SetState(BarrierStates newState)
	{
		if (state == BarrierStates.Disabled)
		{
			barrierCollider.enabled = false;
		}
		else
		{
			barrierCollider.enabled = true;
			gameObject.layer = LayerMask.NameToLayer(barrierLayers[state]);
		}
		//TODO: animations
		//TODO: test if tentacle is in when activated to cut it off?

		OnChangeBarrier.Invoke();

		return newState;
	}
}
