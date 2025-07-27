using System;
using UnityEngine;

public class WaitForPressUI : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGameWaitingToStart())
        {
            Hide();
        }
    }

    private void Hide() => gameObject.SetActive(false);
}
