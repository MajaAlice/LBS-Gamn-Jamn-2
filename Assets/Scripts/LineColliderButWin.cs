using UnityEngine;

public class LineColliderButWin : MonoBehaviour
{
    GameObject MenuObject;
    MenuShit MenuShit;

    private void Start()
    {
        MenuObject = GameObject.FindGameObjectWithTag("Menu");
        MenuShit = MenuObject.GetComponent<MenuShit>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            MenuShit.ToggleDualUI(MenuShit.Menus.NULL, MenuShit.Menus.Victory);
        }
    }
}
