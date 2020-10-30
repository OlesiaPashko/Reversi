using UnityEngine;
using Assets.Models;
using UnityEngine.SceneManagement;

public class Linker : MonoBehaviour
{
    public void Start()
    {
        //link controllers, models and views
        CellsDrawer view = GetComponent<CellsDrawer>();
        CellsController cellsController = GetComponent<CellsController>();
        GameManager gameManager = new GameManager();
        cellsController.GameManager = gameManager;
        view.ListenTo(gameManager);

        //start game
        cellsController.StartGame();
    }
}
