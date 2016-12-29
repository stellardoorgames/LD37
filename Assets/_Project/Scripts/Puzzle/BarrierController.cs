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

	public MeshRenderer barrierRenderer;

	public Color redColor = new Color(255f, 0f, 0f, 180f);
	public Color greenColor = new Color(0f, 255f, 0f, 180f);
	public Color blueColor = new Color(0f, 0f, 255f, 180f);
	public Color disabledColor = new Color(255f, 255f, 255f, 100f);

	Dictionary<BarrierStates, Color> barrierColors;

	int sequenceIndex = 0;

	Collider barrierCollider;

	static bool hasBeenInitialized = false;

	void Awake()
	{
		if (!hasBeenInitialized)
		{
			List<int> barrierLayers = new List<int>();
			List<int> tentacleLayers = new List<int>();
			
			foreach (KeyValuePair<string, string> ln in layerNames)
			{
				barrierLayers.Add(LayerMask.NameToLayer(ln.Key));
				tentacleLayers.Add(LayerMask.NameToLayer(ln.Value));
			}
			
			//Ignore all collisions except with tentacles, except with the matching color
			for(int i = 0; i < barrierLayers.Count; i++)
			{
				for (int j = 0; j < 32 ; j++)
				{
					if (!tentacleLayers.Contains(j) || j == tentacleLayers[i])
						Physics.IgnoreLayerCollision(barrierLayers[i], j);
				}
			}
		}

	}

	void Start()
	{
		barrierCollider = GetComponent<Collider>();

		barrierColors = new Dictionary<BarrierStates, Color>();
		barrierColors.Add(BarrierStates.Red, redColor);
		barrierColors.Add(BarrierStates.Green, greenColor);
		barrierColors.Add(BarrierStates.Blue, blueColor);
		barrierColors.Add(BarrierStates.Disabled, disabledColor);

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

		barrierRenderer.material.color = barrierColors[newState];
		//TODO: animations
		//TODO: test if tentacle is in when activated to cut it off?

		OnChangeBarrier.Invoke();

		return newState;
	}

	void OnDisable()
	{
		hasBeenInitialized = false;
	}
}
