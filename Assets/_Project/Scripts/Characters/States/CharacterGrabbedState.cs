using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;

public class CharacterGrabbedState : SKState<Character> {
	
	public override void begin ()
	{
		
	}

	public override void update (float deltaTime)
	{
		_context.transform.position = Vector3.Lerp (_context.transform.position, _context.grabber.position, 0.1f);
	}

	public override void end ()
	{

	}
}