using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOnGround : MonoBehaviour
{
    // public
    public GameObject sphere;
    public float dist_treshold = 0.6f;
    public Material location_marker;
    public Material planting_sign;

    // private
    private GameObject clone;
    private bool attach = false;
    private bool attached = false;
    private Vector3 attachedPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, new Vector3(0,-1,0), out hit))
        {
            if (hit.distance > dist_treshold) { attach = false; attached = false; }

            if (!attached)
            {
                // update sphere position
                if (clone == null) clone = Instantiate(sphere, hit.point, Quaternion.identity, this.transform);
                else clone.transform.position = hit.point;

                // update sphere color
                if (hit.distance > dist_treshold)
                {
                    clone.GetComponent<MeshRenderer>().material = location_marker;
                    attach = false;
                }
                else
                {
                    clone.GetComponent<MeshRenderer>().material = planting_sign;
                    attach = true;
                }
            }
        }
    }
    public float GetLowestPoint(Transform origin)
    {
        return origin.GetComponent<BoxCollider>().bounds.min.y;
    }

    public void attach2Ground()
    {
        Debug.Log("in attach2Ground");
        if (attach || attached)
        {
            attachedPos = transform.root.gameObject.transform.position;
            transform.root.gameObject.transform.position = new Vector3(attachedPos.x, clone.transform.position.y, attachedPos.z);
            transform.root.gameObject.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
            DestroyImmediate(clone);
            attached = true;
        }
    }
}
