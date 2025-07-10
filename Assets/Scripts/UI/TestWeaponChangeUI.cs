using UnityEngine;
using UnityEngine.UI;

public class TestWeaponChangeUI : MonoBehaviour
{
    [SerializeField] private Button handgunButton;
    [SerializeField] private Button doubleBarrelButton;
    [SerializeField] private Button rifleButton;
    [SerializeField] private Button smgButton;

    [Space]
    [SerializeField] private Transform handgun;
    [SerializeField] private Transform doubleBarrel;
    [SerializeField] private Transform rifle;
    [SerializeField] private Transform smg;

    private void Awake()
    {
        handgunButton.onClick.AddListener(() =>
        {
            Player.Instance.SetCurrentPlayerWeapon(handgun);
        });
        doubleBarrelButton.onClick.AddListener(() =>
        {
            Player.Instance.SetCurrentPlayerWeapon(doubleBarrel);
        });
        rifleButton.onClick.AddListener(() =>
        {
            Player.Instance.SetCurrentPlayerWeapon(rifle);
        });
        smgButton.onClick.AddListener(() =>
        {
            Player.Instance.SetCurrentPlayerWeapon(smg);
        });
    }
}
