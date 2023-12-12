using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;
using TMPro;
using static MagicLeapInputs;
using UnityEngine.XR.Interaction.Toolkit.AR;
using MixedReality.Toolkit.SpatialManipulation;

public class UIManager : MonoBehaviour
{
    [Header("Canvas Settings")]
    public GameObject TreeStoreCanvas;
    public GameObject TropicalCanvas;
    public GameObject billboardRegularCanvas;
    public GameObject billboardStyledCanvas;
    [Header("Controller Settings")]
    public GameObject controllerInput;
    [Header("Topic 1")]
    public GameObject CherryTree;
    [Header("Topic 2")]
    public GameObject FirTree;
    public GameObject OakTree;
    public GameObject PalmTree;
    public GameObject PoplarTree;
    [Header("Topic 3")]
    public GameObject BillAutumnTree;
    public GameObject BillPineTree;
    public GameObject BillRegularTree;
    [Header("Topic 4")]
    public GameObject StyleTree1;
    public GameObject StyleTree2;
    public GameObject StyleTree3;
    public GameObject StyleTree4;

    public void changeMode()  // "ChangeMode" button is pressed down
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
        billboardRegularCanvas.SetActive(false);
        billboardStyledCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (onePressChecker())
        {
            if (TriggerDown)
            {
                RaycastHit hit;
                if (Physics.Raycast(controllerInput.transform.position, controllerInput.transform.forward, out hit))
                {
                    Debug.Log("Hit! " + hit.transform.gameObject.name);
                    TreeStoreManager(hit);
                    TopicManager(hit);
                }
            }
            if (BumperDown)
            {
                RaycastHit hit;
                if (Physics.Raycast(controllerInput.transform.position, controllerInput.transform.forward, out hit))
                {
                    if (UseRayCasting) AutoDelete_helper(hit);
                }
            }
            if (MenuDown)
            {
                openMenu();
            }
        }
    }


    private void TreeStoreManager(RaycastHit hit)
    {
        if (hit.transform.gameObject.name == "TreeStore_tropical")
        {
            TreeStoreCanvas.SetActive(false);
            TropicalCanvas.SetActive(true);
        }
        if (hit.transform.gameObject.name == "TreeStore_billboardRegular")
        {
            TreeStoreCanvas.SetActive(false);
            billboardRegularCanvas.SetActive(true);
        }
        if (hit.transform.gameObject.name == "TreeStore_billboardStyle")
        {
            TreeStoreCanvas.SetActive(false);
            billboardStyledCanvas.SetActive(true);
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
    private void TopicManager(RaycastHit hit)
    {
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
                else if (buttonName == "PoplarTreeButton") AutoPlant_type = "PoplarTree";
                else if (buttonName == "BillAutumnTreeButton") AutoPlant_type = "AutumnTree";
                else if (buttonName == "BillPineTreeButton") AutoPlant_type = "PineTree";
                else if (buttonName == "BillRegularTreeButton") AutoPlant_type = "RegularTree";
                else if (buttonName == "StyleTree1Button") AutoPlant_type = "StyleTree1";
                else if (buttonName == "StyleTree2Button") AutoPlant_type = "StyleTree2";
                else if (buttonName == "StyleTree3Button") AutoPlant_type = "StyleTree3";
                else if (buttonName == "StyleTree4Button") AutoPlant_type = "StyleTree4";
                else hitButton = false;
            }
            else
            {
                Debug.Log("Manual Mode");
                Vector3 pos = controllerInput.transform.position + controllerInput.transform.forward * 1.2f;
                Quaternion rot = controllerInput.transform.rotation;
                if (buttonName == "FirTreeButton")
                {
                    Instantiate(FirTree, pos, rot);
                    AutoPlant_type = "FirTree";
                }
                else if (buttonName == "OakTreeButton")
                {
                    Instantiate(OakTree, pos, rot);
                    AutoPlant_type = "OakTree";
                }
                else if (buttonName == "PalmTreeButton")
                {
                    Instantiate(PalmTree, pos, rot);
                    AutoPlant_type = "PalmTree";
                }
                else if (buttonName == "PoplarTreeButton")
                {
                    Instantiate(PoplarTree, pos, rot);
                    AutoPlant_type = "PoplarTree";
                }
                else if (buttonName == "BillAutumnTreeButton")
                {
                    Instantiate(BillAutumnTree, pos, rot);
                    AutoPlant_type = "AutumnTree";
                }
                else if (buttonName == "BillPineTreeButton")
                {
                    Instantiate(BillPineTree, pos, rot);
                    AutoPlant_type = "PineTree";
                }
                else if (buttonName == "BillRegularTreeButton")
                {
                    Instantiate(BillRegularTree, pos, rot);
                    AutoPlant_type = "RegularTree";
                }
                else if (buttonName == "StyleTree1Button")
                {
                    Instantiate(StyleTree1, pos, rot);
                    AutoPlant_type = "StyleTree1";
                }
                else if (buttonName == "StyleTree2Button")
                {
                    Instantiate(StyleTree2, pos, rot);
                    AutoPlant_type = "StyleTree2";
                }
                else if (buttonName == "StyleTree3Button")
                {
                    Instantiate(StyleTree3, pos, rot);
                    AutoPlant_type = "StyleTree3";
                }
                else if (buttonName == "StyleTree4Button")
                {
                    Instantiate(StyleTree4, pos, rot);
                    AutoPlant_type = "StyleTree4";
                }
                else hitButton = false;
            }
            if (hitButton) closeCanvas();
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
        TreeStoreCanvas.SetActive(true);
        TropicalCanvas.SetActive(false);
        billboardRegularCanvas.SetActive(false);
        billboardStyledCanvas.SetActive(false);
        allowPlant = false;
    }

    private bool hitTree(RaycastHit hit)
    {
        // check if hit tree objects
        if (hit.transform.gameObject.name == "Manipulator") return true;
        if (hit.transform.gameObject.name == "FirTree") return true;
        if (hit.transform.gameObject.name == "OakTree") return true;
        if (hit.transform.gameObject.name == "PalmTree") return true;
        if (hit.transform.gameObject.name == "PoplarTree") return true;
        if (hit.transform.gameObject.name == "AutumnTree") return true;
        if (hit.transform.gameObject.name == "PineTree") return true;
        if (hit.transform.gameObject.name == "RegularTree") return true;
        if (hit.transform.gameObject.name == "StyleTree1") return true;
        if (hit.transform.gameObject.name == "StyleTree2") return true;
        if (hit.transform.gameObject.name == "StyleTree3") return true;
        if (hit.transform.gameObject.name == "StyleTree4") return true;
        return false;
    }

    private void AutoPlant_helper(RaycastHit hit)
    {
        Vector3 pos = hit.point;
        Quaternion rot = new Quaternion(0f, 0f, 0f, 1f);
        if (!hitTree(hit))
        {
            if (AutoPlant_type == "FirTree") Instantiate(FirTree, pos, rot);
            if (AutoPlant_type == "OakTree") Instantiate(OakTree, pos, rot);
            if (AutoPlant_type == "PalmTree") Instantiate(PalmTree, pos, rot);
            if (AutoPlant_type == "PoplarTree") Instantiate(PoplarTree, pos, rot);
            if (AutoPlant_type == "AutumnTree") Instantiate(BillAutumnTree, pos, rot);
            if (AutoPlant_type == "PineTree") Instantiate(BillPineTree, pos, rot);
            if (AutoPlant_type == "RegularTree") Instantiate(BillRegularTree, pos, rot);
            if (AutoPlant_type == "StyleTree1") Instantiate(StyleTree1, pos, rot);
            if (AutoPlant_type == "StyleTree2") Instantiate(StyleTree2, pos, rot);
            if (AutoPlant_type == "StyleTree3") Instantiate(StyleTree3, pos, rot);
            if (AutoPlant_type == "StyleTree4") Instantiate(StyleTree4, pos, rot);
        }
    }

    private void AutoDelete_helper(RaycastHit hit)
    {
        if (hitTree(hit)) DestroyImmediate(hit.transform.root.gameObject);
    }

    private void closeCanvas()
    {
        TreeStoreCanvas.SetActive(false);
        TropicalCanvas.SetActive(false);
        billboardRegularCanvas.SetActive(false);
        billboardStyledCanvas.SetActive(false);
        allowPlant = true;
    }

    void OnDestroy()
    {
        mlInputs.Dispose();
    }
}
