namespace VRTK.Examples
{
	using UnityEngine;
	using UnityEventHelper;

	public class LightSwitchBathroom : MonoBehaviour
	{
		public GameObject wallText;
		public GameObject[] lightBulb;
		public GameObject go;
		public Transform dispenseLocation;
		public bool lightOn;

		private VRTK_Button_UnityEvents buttonEvents;

		private void Start()
		{
			buttonEvents = GetComponent<VRTK_Button_UnityEvents>();
			if (buttonEvents == null)
			{
				buttonEvents = gameObject.AddComponent<VRTK_Button_UnityEvents>();
			}
			buttonEvents.OnPushed.AddListener(handlePush);
			wallText.SetActive(false);
		}
		public void TurnOffLightSwitch()
		{
			for (int i = 0; i < lightBulb.Length; i++) {
				lightBulb[i].SetActive(false);
			}
			wallText.SetActive(true);
			lightOn = false;
		}
		public void TurnOnLightSwitch()
		{
			for (int i = 0; i < lightBulb.Length; i++)
			{
				lightBulb[i].SetActive(true);
			}
			wallText.SetActive(true);
			lightOn = true;
		}

		private void handlePush(object sender, Control3DEventArgs e)
		{
			VRTK_Logger.Info("Pushed");
			if (lightOn)
			{
				TurnOffLightSwitch();
			}
			else {
				TurnOnLightSwitch();
			}
		}
	}
}