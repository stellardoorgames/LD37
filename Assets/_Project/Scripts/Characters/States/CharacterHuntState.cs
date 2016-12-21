using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.StateKit;

public class CharacterHuntState : SKState<Character> {

	float retargetTime = 3f;
	float lastRetarget;

	public override void begin ()
	{
		if (_context.stealTag != null && _context.stealTag != "")
			_context.Retarget(_context.stealTag);
		else
			_context.Retarget(_context.attackTag);
		lastRetarget = Time.time;
	}

	public override void update (float deltaTime)
	{
		if (Time.time > lastRetarget + retargetTime)
		{
			_context.Retarget();
			lastRetarget = Time.time;
		}
	}

	public override void end ()
	{
		
	}
}
