namespace VRTK.Examples
{
	using UnityEngine;
	using UnityEventHelper;

	public class Dials : MonoBehaviour
	{
		public TextMesh go;

		private VRTK_Control_UnityEvents controlEvents;

		private void Start()
		{
			controlEvents = GetComponent<VRTK_Control_UnityEvents>();
			if (controlEvents == null)
			{
				controlEvents = gameObject.AddComponent<VRTK_Control_UnityEvents>();
			}

			controlEvents.OnValueChanged.AddListener(HandleChange);
		}

		private void HandleChange(object sender, Control3DEventArgs e)
		{
			//Debug.Log("" + e.normalizedValue);
			if (go != null) {
				go.text = e.value.ToString() + "(" + e.normalizedValue.ToString() + "%)";
			}
			/*if (e.normalizedValue == 75)
			{
				//play sound for puzzle
			}
			else {
				//play static
			}*/
		}
	}
}