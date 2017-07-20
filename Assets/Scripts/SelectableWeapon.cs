using UnityEngine;
using UnityEngine.UI;

public class SelectableWeapon : MonoBehaviour
{
    public string WeaponName;
    public Weapon weapon;

    public Text text;
    private bool isActive;

    void Start()
    {
    }


    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player" && !isActive && Input.GetKeyDown(KeyCode.E))
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            var controller = player.GetComponent<PlayerController>();
            controller.CurrentWeapon.SetActive(false);
            controller.CurrentWeapon = gameObject;
            var hand = GameObject.FindGameObjectWithTag("PlayerWeaponHand");
            transform.parent = hand.transform;
            text.text = "";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isActive)
            text.text = new System.Text.StringBuilder("Naciśnij 'E', aby zamienić bieżącą broń na ")
            .Append(WeaponName)
            .ToString();
    }

    private void OnTriggerExit(Collider other)
    {
        text.text = "";
    }
}
