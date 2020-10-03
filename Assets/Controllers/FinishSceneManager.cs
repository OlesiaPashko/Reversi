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

    public GameObject winnerText;
    public Text firstPlayerText;
    public Text secondPlayerText;

    public static int FirstPlayerScore;
    public static int SecondPlayerScore;


    public void PlayAgain()
    {
        SceneManager.LoadScene("StartScene");
    }

}
