using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LeaderBoardSample : MonoBehaviour {
    public Text firstPlaceText;
    public Text secondPlaceText;
    public Text thirdPlaceText;

    float startTime = 10.0f;
	float timeLeft = 0.0f;
	
	int totalScore = 0;
	string playerName = "";
	string code = "";

	enum gameState {
		waiting,
		running,
		enterscore,
		leaderboard
	};
	
	gameState gs;
	
	
	// Reference to the dreamloLeaderboard prefab in the scene
	
	dreamloLeaderBoard dl;

	void Start () 
	{
		// get the reference here...
		this.dl = dreamloLeaderBoard.GetSceneDreamloLeaderboard();

		this.timeLeft = startTime;
		this.gs = gameState.leaderboard;

        this.dl.LoadScores();

        //dl.AddScore("Anja", 1);
    }

	void Update () 
	{
		if (this.gs == gameState.running)
		{
			timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0, this.startTime);
			if (timeLeft == 0)
			{
				this.gs = gameState.enterscore;
			}
		}
	}
	
	void OnGUI()
	{
        if (this.gs == gameState.leaderboard)
        {
            List<dreamloLeaderBoard.Score> scoreList = dl.ToListHighToLow();

            if (scoreList == null)
            {
                firstPlaceText.text = "Loading...";
                secondPlaceText.text = "";
                thirdPlaceText.text = "";
            }
            else
            {
                int maxToDisplay = 3;
                int count = 0;
                foreach (dreamloLeaderBoard.Score currentScore in scoreList)
                {
                    count++;

                    var duration = GetDurationFromSeconds(currentScore.score);
                    var boardEntry = currentScore.playerName + " (" + duration + ")";

                    if (count == 1)
                    {
                        firstPlaceText.text = boardEntry;
                    }
                    else if (count == 2)
                        secondPlaceText.text = boardEntry;
                    else if (count == 3)
                        thirdPlaceText.text = boardEntry;

                    if (count >= maxToDisplay) break;
                }
                if (count == 0)
                    firstPlaceText.text = "";
                if (count > 0 && count <= 1)
                    secondPlaceText.text = "";
                if (count > 1 && count < maxToDisplay)
                    thirdPlaceText.text = "";
            }
        }
        else {
            /*

            GUILayoutOption[] width200 = new GUILayoutOption[] {GUILayout.Width(200)};

            float width = 400;  // Make this wider to add more columns
            float height = 200;

            Rect r = new Rect((Screen.width / 2) - (width / 2), (Screen.height / 2) - (height), width, height);
            GUILayout.BeginArea(r, new GUIStyle("box"));

            GUILayout.BeginVertical();
            GUILayout.Label("Time Left:" + this.timeLeft.ToString("0.000"));
            if (this.gs == gameState.waiting || this.gs == gameState.running)
            {
                if (GUILayout.Button("Click me as much as you can in " + this.startTime.ToString("0") + " seconds!"))
                {
                    this.totalScore++;
                    this.gs = gameState.running;
                }

                GUILayout.Label("Total Score: " + this.totalScore.ToString());
            }



            if (this.gs == gameState.enterscore)
            {
                GUILayout.Label("Total Score: " + this.totalScore.ToString());
                GUILayout.BeginHorizontal();
                GUILayout.Label("Your Name: ");
                this.playerName = GUILayout.TextField(this.playerName, width200);

                if (GUILayout.Button("Save Score"))
                {
                    // add the score...
                    if (dl.publicCode == "") Debug.LogError("You forgot to set the publicCode variable");
                    if (dl.privateCode == "") Debug.LogError("You forgot to set the privateCode variable");

                    dl.AddScore(this.playerName, totalScore);

                    this.gs = gameState.leaderboard;
                }
                GUILayout.EndHorizontal();
            }

            if (this.gs == gameState.leaderboard)
            {
                GUILayout.Label("High Scores:");
                List<dreamloLeaderBoard.Score> scoreList = dl.ToListHighToLow();

                if (scoreList == null) 
                {
                    GUILayout.Label("(loading...)");
                } 
                else 
                {
                    int maxToDisplay = 20;
                    int count = 0;
                    foreach (dreamloLeaderBoard.Score currentScore in scoreList)
                    {
                        count++;
                        GUILayout.BeginHorizontal();
                        GUILayout.Label(currentScore.playerName, width200);
                        GUILayout.Label(currentScore.score.ToString(), width200);
                        GUILayout.EndHorizontal();

                        if (count >= maxToDisplay) break;
                    }
                }
            }
            GUILayout.EndArea();

            r.y = r.y + r.height + 20;

            GUILayout.BeginArea(r, new GUIStyle("box"));
            GUILayout.BeginHorizontal();
            GUILayout.Label("Redeem Code: ");
            this.code = GUILayout.TextField(this.code, width200);

            if (GUILayout.Button("Redeem"))
            {
                this.pc.RedeemCode(this.code);
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(50);
            if (this.pc != null)
            {
                GUILayout.Label("State: " + this.pc.state.ToString());
                GUILayout.Label("Error: " + this.pc.error);
                GUILayout.Label("Value: " + this.pc.value);
            }

            GUILayout.EndVertical();
            GUILayout.EndArea();
            */
        }
	}

    string GetDurationFromSeconds(int passedSeconds)
    {
        int hours = passedSeconds / 3600;
        int remainingSeconds = passedSeconds - 3600 * hours;
        int minutes = remainingSeconds / 60;
        remainingSeconds -= minutes * 60;

        if (hours == 0)
        {
            return minutes + ":" + remainingSeconds;
        }
        else
        {
            return hours + ":" + minutes + ":" + remainingSeconds;
        }
    }
	
	
}
