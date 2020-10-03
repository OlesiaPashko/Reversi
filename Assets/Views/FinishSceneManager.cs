using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishSceneManager : MonoBehaviour
{
    public static int FirstPlayerScore;
    public static int SecondPlayerScore;

    public Text WinnerText;
    public Text FirstPlayerScoreText;
    public Text SecondPlayerScoreText;

    private void Start()
    {
        if(FirstPlayerScore>SecondPlayerScore)
            WinnerText.text += " First Player";
        else if (SecondPlayerScore > FirstPlayerScore)
        {
            WinnerText.text += " Second Player";
        }
        else
        {
            WinnerText.text += " Friendship";
        }
        FirstPlayerScoreText.text += " " + FirstPlayerScore;
        SecondPlayerScoreText.text += " " + SecondPlayerScore;
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene("StartScene");
    }
}
