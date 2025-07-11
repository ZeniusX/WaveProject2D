using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItemButtonSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weaponName;
    [SerializeField] private Transform selected;
    [SerializeField] private WeaponTypeSO weaponType;

    private Button itemButton;

    private void Awake()
    {
        itemButton = GetComponent<Button>();

        Player.Instance.OnCurrentWeaponChange += Player_OnCurrentWeaponChange;

        itemButton.onClick.AddListener(() =>
        {
            SetPlayerWeapon();
        });
    }

    private void Start()
    {
        weaponName.text = weaponType.weaponName;

        SetSelected();
    }

    private void Player_OnCurrentWeaponChange(object sender, EventArgs e)
    {
        SetSelected();
    }

    public void SetPlayerWeapon()
    {
        Player.Instance.SetCurrentPlayerWeapon(weaponType);
    }

    private void SetSelected()
    {
        WeaponTypeSO currentPlayerWeapon = Player.Instance.GetCurrentPlayerWeaponTypeSO();

        if (currentPlayerWeapon == weaponType)
        {
            selected.gameObject.SetActive(true);
        }
        else
        {
            selected.gameObject.SetActive(false);
        }
    }
}
