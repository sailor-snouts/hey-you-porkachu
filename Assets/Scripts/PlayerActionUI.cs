using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionUI : MonoBehaviour {

    [SerializeField] GameObject playerUICanvasPrefab = null;     GameObject ui;

    private void Start()
    {
        ui = Instantiate(playerUICanvasPrefab, transform.position, Quaternion.identity, transform);
        Hide();
    } 
    public void Show()     {         ui.SetActive(true);
    } 
    public void Hide() 
    {
        ui.SetActive(false);
    } 
}
