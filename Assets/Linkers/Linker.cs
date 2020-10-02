using UnityEngine;
using Assets.Models;
using UnityEngine.SceneManagement;

public class Linker : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        CellsDrawer view = GetComponent<CellsDrawer>();
        CellsController cellsController = GetComponent<CellsController>();
        GameManager gameManager = new GameManager();
        cellsController.GameManager = gameManager;
        view.ListenTo(gameManager);
        cellsController.StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
