using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBlocksPage : MonoBehaviour
{
    public GameObject ShelvesButton;
    public GameObject Blocks;
    public GameObject exitButton;

    public GameObject panelTopView;
    public GameObject panelShelf;

    public void OnClick() {
        ShelvesButton.SetActive(false);
        Blocks.SetActive(true);
        exitButton.SetActive(true);

        panelTopView.SetActive(false);
        panelShelf.SetActive(true);
    }
}
