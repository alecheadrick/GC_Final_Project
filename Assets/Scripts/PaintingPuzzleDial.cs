namespace VRTK.Examples
{
	using UnityEngine;
	using UnityEventHelper;

	public class PaintingPuzzleDial : MonoBehaviour
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
			go.text = e.value.ToString() + "(" + e.normalizedValue.ToString() + "%)";
			/*if (e.normalizedValue == 75)
			{
				SafeManager.instance.PaintingComplete();
			}
			else {
				SafeManager.instance.PaintingReset();
			}*/
		}
	}
}