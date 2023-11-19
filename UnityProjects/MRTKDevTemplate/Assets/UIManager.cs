using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;
using TMPro;
using static MagicLeapInputs;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class UIManager : MonoBehaviour
{
    public GameObject TreeStoreCanvas;
    public GameObject TropicalCanvas;
    public GameObject controllerInput;
    public GameObject CherryTree;
    public GameObject FirTree;
    public GameObject OakTree;
    public GameObject PalmTree;
    public GameObject PoplarTree;

    public void changeMode()  // change is "ChangeMode" button is predded down
    {
        UseRayCasting = !UseRayCasting;
        Debug.Log("Change Mode! use Ray-casting " + UseRayCasting);
    }


    private MagicLeapInputs mlInputs;
    private MagicLeapInputs.ControllerActions controllerActions;

    private bool TriggerDown = false;
    private bool BumperDown = false;
    private bool MenuDown = false;

    private bool oldTriggerDown = false;
    private bool oldBumperDown = false;
    private bool oldMenuDown = false;

    private bool UseRayCasting = true;
    private string AutoPlant_type = "None";
    private bool allowPlant = false;

    // Start is called before the first frame update
    void Start()
    {
        mlInputs = new MagicLeapInputs();
        mlInputs.Enable();
        controllerActions = new MagicLeapInputs.ControllerActions(mlInputs);
        TropicalCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (onePressChecker())
        {
            openMenu(); // open Menu if Menu is pressed down
            TreeStoreManager();
            TropicalManager();
        }
    }


    private void TreeStoreManager()
    {
        if (TriggerDown)
        {
            RaycastHit hit;
            if (Physics.Raycast(controllerInput.transform.position, controllerInput.transform.forward, out hit))
            {
                Debug.Log("TreeStore Hit! " + hit.transform.gameObject.name);
                if (hit.transform.gameObject.name == "TreeStore_tropical")
                {
                    TreeStoreCanvas.SetActive(false);
                    TropicalCanvas.SetActive(true);
                }
                if (hit.transform.gameObject.name == "TreeStore_cherry")
                {
                    TreeStoreCanvas.SetActive(false);
                    Instantiate(CherryTree);
                }
                if (hit.transform.gameObject.name == "CloseButton")
                {
                    closeCanvas();
                }
            }
        }
    }
    private void TropicalManager()
    {
        if (TriggerDown)
        {
            RaycastHit hit;
            if (Physics.Raycast(controllerInput.transform.position, controllerInput.transform.forward, out hit))
            {
                Debug.Log("Tropical Hit! " + hit.transform.gameObject.name);
                string buttonName = hit.transform.gameObject.name;
                if (buttonName == "CloseButton")
                {
                    closeCanvas();
                }
                else
                {
                    bool hitButton = true;
                    if (UseRayCasting)
                    {
                        Debug.Log("Ray Casting Mode");
                        if (allowPlant && buttonName != "ChangeModeButton") AutoPlant_helper(hit);
                        if (buttonName == "FirTreeButton") AutoPlant_type = "FirTree";
                        else if (buttonName == "OakTreeButton") AutoPlant_type = "OakTree";
                        else if (buttonName == "PalmTreeButton") AutoPlant_type = "PalmTree";
                        else if (buttonName == "PoplarTreeButton") AutoPlant_type = "Poplar";
                        else hitButton = false;
                    }
                    else
                    {
                        Debug.Log("Manual Mode");
                        Vector3 pos = controllerInput.transform.position + controllerInput.transform.forward * 0.5f;
                        Quaternion rot = controllerInput.transform.rotation;
                        if (buttonName == "FirTreeButton") Instantiate(FirTree, pos, rot);
                        else if (buttonName == "OakTreeButton") Instantiate(OakTree, pos, rot);
                        else if (buttonName == "PalmTreeButton") Instantiate(PalmTree, pos, rot);
                        else if (buttonName == "PoplarTreeButton") Instantiate(PoplarTree, pos, rot);
                        else hitButton = false;
                    }
                    if (hitButton) closeCanvas();
                }
            }
        }
    }

    private bool onePressChecker()
    {
        TriggerDown = controllerActions.Trigger.IsPressed();
        BumperDown = controllerActions.Bumper.IsPressed();
        MenuDown = controllerActions.Menu.IsPressed();

        bool return_bool = false;
        if (!oldTriggerDown && TriggerDown) return_bool = true;
        if (!oldBumperDown && BumperDown) return_bool = true;
        if (!oldMenuDown && MenuDown) return_bool = true;

        oldTriggerDown = TriggerDown;
        oldBumperDown = BumperDown;
        oldMenuDown = MenuDown;
        return return_bool;
    }

    private void openMenu()
    {
        if (MenuDown)
        {
            TreeStoreCanvas.SetActive(true);
            TropicalCanvas.SetActive(false);
            allowPlant = false;
        }
    }


    private void AutoPlant_helper(RaycastHit hit)
    {
        Vector3 pos = hit.point;
        Quaternion rot = new Quaternion(0f, 0f, 0f, 1f);
        if (AutoPlant_type == "FirTree") Instantiate(FirTree, pos, rot);
        if (AutoPlant_type == "OakTree") Instantiate(OakTree, pos, rot);
        if (AutoPlant_type == "PalmTree") Instantiate(PalmTree, pos, rot);
        if (AutoPlant_type == "Poplar") Instantiate(PoplarTree, pos, rot);
    }

    private void closeCanvas()
    {
        TreeStoreCanvas.SetActive(false);
        TropicalCanvas.SetActive(false);
        allowPlant = true;
    }

    void OnDestroy()
    {
        mlInputs.Dispose();
    }
}
