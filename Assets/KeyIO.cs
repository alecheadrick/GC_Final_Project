namespace VRTK.Examples
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	[System.Serializable]
	public class KeyIO : VRTK_InteractableObject
	{
		#region Methods
		public override void Ungrabbed(VRTK_InteractGrab previousGrabbingObject)
		{
			if (DropKeyIntoPlace.instance.keyInDropZone == true)
			{
				DropKeyIntoPlace.instance.DropIntoPlace();
			}
			else if (DropKeyIntoPlace.instance.keyInDropZone == false) {
				base.Ungrabbed(previousGrabbingObject);
			}
			
		}

		#endregion
	}
}
