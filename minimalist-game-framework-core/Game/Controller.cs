///* CONTROLLER CLASS:
// * Implementation for Game Controllers:
// * Works with any controller that supports PC Gaming
// * Author: David Gershony
// * LAST EDITED: Nov. 21
// * */


using System;
using System.Collections;


	public class Controller
	{
		public Controller()
		{
			// MOVEMENTS: Rotate, Move forward, Fire
			if (Engine.GetGamepadConnected(0) == false)
			{
				// There is NO controller connected
				return;
			}
		}
		public String[] controllerMovement()
		{
			ArrayList movements = new ArrayList();
			// If "A" is released, then SHOOT
			if (Engine.GetGamepadButtonHeld(0, GamepadButton.A) == true)
			{
				movements.Add("SHOOT");

			}

			// Rotate Sprite (output LEFT or RIGHT)
			if (Engine.GetGamepadButtonHeld(0, GamepadButton.LeftShoulder) == true)
			{
				movements.Add("LEFT");
			}
			if (Engine.GetGamepadButtonHeld(0, GamepadButton.RightShoulder) == true)
			{
				movements.Add("RIGHT");
			}

			// DPad-Up to move forward
			if (Engine.GetGamepadButtonHeld(0, GamepadButton.Up))
			{
				movements.Add("MOVE FORWARD");
			}
			// return a STRING array of all the movements that are done (Since multiple can be pressed at once)
			string[] movementsArr = movements.ToArray(typeof(string)) as string[];
			return movementsArr;

		}

	}


