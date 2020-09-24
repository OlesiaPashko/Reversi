using UnityEngine;

public class Linker : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        CellsDrawer view = GetComponent<CellsDrawer>();
        Field gameField = new Field(view.ChangeColor);
        CellsController cellsController = GetComponent<CellsController>();
        cellsController.SetField(gameField);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
