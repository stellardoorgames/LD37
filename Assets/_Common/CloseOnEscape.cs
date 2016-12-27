using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityCommon
{
	public class CloseOnEscape : MonoBehaviour {

		public static FILOStack<CloseOnEscape> objectStack = new FILOStack<CloseOnEscape> ();

		public static bool CloseTop()
		{
			if (objectStack.Count == 0)
				return false;
			else
			{
				CloseOnEscape c = objectStack.Pop();
				c.gameObject.SetActive(false);
				return true;
			}
		}

		void OnAwake()
		{
			//gameObject.SetActive (false);
		}

		void OnEnable()
		{
			objectStack.Push (this);
		}

		void OnDisable()
		{
			//It's been disabled through CloseTop()
			if (objectStack.Count > 0)
			{
				//It was disabled through other means
				if (objectStack.Peek () == this)
					objectStack.Pop ();
				//It was disabled through other means, but it's not the last one on the stack
				//Happens less often, but it can happen when a parent object disables multiple objects at once
				else if (objectStack.Contains(this))
				{
					Debug.Log (objectStack.Count);
					objectStack.Remove (this);
					Debug.Log (objectStack.Count);
				}
			}
		}

	}
}