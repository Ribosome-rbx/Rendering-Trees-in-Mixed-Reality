using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.MagicLeap;
using UnityEngine.UI;
using TMPro;
using static MagicLeapInputs;
using UnityEngine.XR.Interaction.Toolkit.AR;
using TreeEditor;

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


    private MagicLeapInputs mlInputs;
    private MagicLeapInputs.ControllerActions controllerActions;
    private bool TriggerDown;
    private bool BumperDown;
    private bool MenuDown;
    private bool UseRayCasting = true;

    // Start is called before the first frame update
    void Start()
    {
        mlInputs = new MagicLeapInputs();
        mlInputs.Enable();
        controllerActions = new MagicLeapInputs.ControllerActions(mlInputs);
        controllerActions.Bumper.performed += HandleOnBumper;
        controllerActions.Trigger.performed += HandleOnTrigger;
        controllerActions.Menu.performed += HandleOnMenu;
        TropicalCanvas.SetActive(false);
    }

    private void HandleOnTrigger(InputAction.CallbackContext obj)
    {
        TriggerDown = obj.ReadValueAsButton();
    }
    private void HandleOnBumper(InputAction.CallbackContext obj)
    {
        BumperDown = obj.ReadValueAsButton();
        Debug.Log("The Bumper is pressed down " + BumperDown);
    }
    private void HandleOnMenu(InputAction.CallbackContext obj)
    {
        MenuDown = obj.ReadValueAsButton();
        Debug.Log("The Bumper is pressed down " + MenuDown);
    }

    // Update is called once per frame
    void Update()
    {
        TreeStoreManager();
        TropicalManager();
    }


    private void TreeStoreManager()
    {
        if (TriggerDown)
        {
            RaycastHit hit;
            if (Physics.Raycast(controllerInput.transform.position, controllerInput.transform.forward, out hit))
            {
                Debug.Log("Hit! " + hit.transform.gameObject.name);
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
                    TreeStoreCanvas.SetActive(false);
                }
            }
        }
        if (MenuDown)
        {
            TreeStoreCanvas.SetActive(true);
        }
    }
    private void TropicalManager()
    {
        if (TriggerDown)
        {
            RaycastHit hit;
            if (Physics.Raycast(controllerInput.transform.position, controllerInput.transform.forward, out hit))
            {
                Debug.Log("Hit! " + hit.transform.gameObject.name);
                string buttonName = hit.transform.gameObject.name;
                TropicalCanvas.SetActive(false);
                if (buttonName != "CloseButton")
                {
                    if (!UseRayCasting)
                    {
                        Vector3 pos = controllerInput.transform.position + controllerInput.transform.forward * 0.5f;
                        Quaternion rot = controllerInput.transform.rotation;
                        if (buttonName == "FirTreeButton") Instantiate(FirTree, pos, rot);
                        if (buttonName == "OakTreeButton") Instantiate(OakTree, pos, rot);
                        if (buttonName == "PalmTreeButton") Instantiate(PalmTree, pos, rot);
                        if (buttonName == "PoplarTreeButton") Instantiate(PoplarTree, pos, rot);
                    }
                    else
                    {
                        Vector3 pos = hit.transform.position;
                        Quaternion rot = new Quaternion(0f, 0f, 0f, 1f);
                        if (buttonName == "FirTreeButton") Instantiate(FirTree, pos, rot);
                        if (buttonName == "OakTreeButton") Instantiate(OakTree, pos, rot);
                        if (buttonName == "PalmTreeButton") Instantiate(PalmTree, pos, rot);
                        if (buttonName == "PoplarTreeButton") Instantiate(PoplarTree, pos, rot);
                    }
                }
            }
        }
    }

    void OnDestroy()
    {
        mlInputs.Dispose();
    }
}
