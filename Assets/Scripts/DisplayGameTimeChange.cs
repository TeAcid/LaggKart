using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DisplayGameTimeChange : MonoBehaviour {

	public Text gameTimeText;
	public Text placeText;
	public Image pickupImage;
	int passedSeconds = 0;

	void FixedUpdate() {
		passedSeconds += 1;
		int hours = passedSeconds / 3600;
		int remainingSeconds = passedSeconds - 3600 * hours;
		int minutes = remainingSeconds / 60;
		remainingSeconds -= minutes * 60;

		if (hours == 0) {
			gameTimeText.text = minutes + ":" + remainingSeconds;
		}
		else {
			gameTimeText.text = hours + ":" + minutes + ":" + remainingSeconds;
		}
	}
}
