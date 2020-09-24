
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class CellsController : MonoBehaviour
{
    private Field field;
    public void SetField(Field field)
    {
        this.field = field;
    }
    public void ChangeColor()
    {
        field.ChangeColorOfAllCells();
    }
}
