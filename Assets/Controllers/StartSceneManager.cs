using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    public Dropdown dropdownGameMode;
    public Dropdown dropdownColor;

    private List<string> gameModes = new List<string>() { "2 players", "With Computer" };
    private List<string> colors = new List<string>() { "Black", "White" };

    private bool isTwoPlayers;
    private  CellState colorSelected;
    // Start is called before the first frame update
    void Start()
    {
        dropdownColor.AddOptions(colors);
        dropdownGameMode.AddOptions(gameModes);
        colorSelected = CellState.Black;
        isTwoPlayers = true;
    }

    public void DropdownGameModeIndexChanged(int index)
    {
        if (index == 0)
            isTwoPlayers = true;
        else
            isTwoPlayers = false;
    }

    public void DropdownColorIndexChanged(int index)
    {
        if (index == 0)
            colorSelected = CellState.Black;
        else
            colorSelected = CellState.White;
    }
    public void StartGame()
    {
        CellsController.IsTwoPlayers = isTwoPlayers;
        CellsController.firstPlayerColor = colorSelected;
        SceneManager.LoadScene("MainScene");
    }
}
