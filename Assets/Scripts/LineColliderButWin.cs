using UnityEngine;
using UnityEngine.SceneManagement;

public class LineColliderButWin : MonoBehaviour
{
    GameObject MenuObject;
    MenuShit MenuShit;
    GameObject BossObject;
    Boss BossScript;

    private void Start()
    {
        MenuObject = GameObject.FindGameObjectWithTag("Menu");
        MenuShit = MenuObject.GetComponent<MenuShit>();
        BossObject = GameObject.FindGameObjectWithTag("Boss");
        BossScript = BossObject.GetComponent<Boss>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            BossScript.isChasing = false;
            MenuShit.ToggleDualUI(MenuShit.Menus.NULL, MenuShit.Menus.Victory);
        }
    }
}
