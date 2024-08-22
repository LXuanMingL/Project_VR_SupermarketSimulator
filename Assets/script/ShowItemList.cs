using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowItemkList : MonoBehaviour
{
    public GameObject exitButton;
    public GameObject exitButton2;
    public GameObject canvasMenu;
    public GameObject canvasSoftDrink;
    public GameObject canvasSnack;
    public GameObject canvasMeal;
    public GameObject canvasFrozen;
    public GameObject canvasOutside;
    public List<GameObject> shelves;
    public GameObject shelfMenu;
    public GameObject text;

    public GameObject canvasUI;
    public GameObject vrCamera;
    public GameObject tutorial;

    public GameObject panelTopView;
    public List<GameObject> panelShelf;

    public void showSoftDrink() {
        canvasMenu.SetActive(false);
        exitButton.SetActive(true);

        canvasSoftDrink.SetActive(true);
    }

    public void showSnack()
    {
        canvasMenu.SetActive(false);
        exitButton.SetActive(true);

        canvasSnack.SetActive(true);
    }

    public void showMeal()
    {
        canvasMenu.SetActive(false);
        exitButton.SetActive(true);

        canvasMeal.SetActive(true);
    }

    public void showFrozen()
    {
        canvasMenu.SetActive(false);
        exitButton.SetActive(true);

        canvasFrozen.SetActive(true);
    }

    public void showOutside()
    {
        canvasMenu.SetActive(false);
        exitButton.SetActive(true);

        canvasOutside.SetActive(true);
    }

    public void backToMenu() {
        canvasMenu.SetActive(true);
        exitButton.SetActive(false);

        canvasSoftDrink.SetActive(false);
        canvasSnack.SetActive(false);
        canvasMeal.SetActive(false);
        canvasFrozen.SetActive(false);
        canvasOutside.SetActive(false);
    }

    public void backToShelf() {
        shelfMenu.SetActive(true);
        exitButton2.SetActive(false);
        foreach (GameObject go in shelves) {
            go.SetActive(false);
        }
        panelTopView.SetActive(true);
        foreach (GameObject go in panelShelf) {
            go.SetActive(false);
        }
    }

    public void gotoPreview() {
        canvasUI.SetActive(false);
        text.SetActive(false);
    }

    public void exitTutorial()
    {
        tutorial.SetActive(false);
    }
}
