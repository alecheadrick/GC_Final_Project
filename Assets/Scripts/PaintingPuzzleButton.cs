namespace VRTK.Examples
{
	using UnityEngine;
	using UnityEventHelper;
	using UnityEngine.UI;

	public class PaintingPuzzleButton : MonoBehaviour
	{
		public Text numberText;
		public  int currentNumber = 0;
		public int correctNumber;
		private VRTK_Button_UnityEvents buttonEvents;

		private void Start()
		{
			buttonEvents = GetComponent<VRTK_Button_UnityEvents>();
			if (buttonEvents == null)
			{
				buttonEvents = gameObject.AddComponent<VRTK_Button_UnityEvents>();
			}
			buttonEvents.OnPushed.AddListener(handlePush);
		}
		public void ChangeNumberByOne()
		{
			currentNumber++;
			if (currentNumber > 9) {
				currentNumber = 0;
			}
			numberText.text = ("" + currentNumber);
			if (currentNumber == correctNumber)
			{
				SafeManager.instance.PaintingPuzzleComplete();
			}
			else if (currentNumber != correctNumber) {
				SafeManager.instance.PaintingReset();
			}
		}
		private void handlePush(object sender, Control3DEventArgs e)
		{
			VRTK_Logger.Info("Pushed");
			ChangeNumberByOne();
		}
	}
}