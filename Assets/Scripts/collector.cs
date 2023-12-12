using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class collector : MonoBehaviour
{
    private List<ARRaycastHit> rayhits = new List<ARRaycastHit>();
    public ARRaycastManager rayman;
    public GameObject apple, par, tree, issac;
    GameObject g, tre;
    List<GameObject> apples = new List<GameObject>();
    Transform plane;

    public uicont script;

    bool s = false;
    int total;

    void Start(){
        total = 3;
    }

    
    void Update()
    {
        if (Input.touchCount > 0){
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended){
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) {
                    if (s){
                        if (hit.collider.tag == "apple")
                        {
                            if (hit.distance < 1.5f) {
                                Destroy(hit.collider.gameObject);
                                script.incriment();
                            }
                            else {
                                script.showMessage("Apple too far away?");
                            }
                        }
                        else if (hit.collider.tag == "tree" && script.collected()) {
                            script.reset();
                            total++;
                            spawn();
                        }
                    }
                    else{
                        s = true;
                        script.star();
                        Vector3 pos = ray.direction * hit.distance + new Vector3(0, 2, 0);
                        tre = Instantiate(tree, pos, Quaternion.identity);
                        tre.transform.SetParent(par.transform);
                        g = Instantiate(issac, pos * 0.5f, Quaternion.identity);
                        spawn();
                    }
                }
            }
        }   
    }

    void spawn(){
        float xmin = plane.position.x - .5f * plane.localScale.x;
        float xmax = plane.position.x + .5f * plane.localScale.x;
        float zmin = plane.position.z - .5f * plane.localScale.z;
        float zmax = plane.position.z + .5f * plane.localScale.z;

        for (int i = 0; i < total; i++) {
            g = Instantiate(apple, new Vector3(Random.Range(xmin, xmax), 1, Random.Range(zmin, zmax)), Quaternion.identity);
            g.transform.SetParent(par.transform);
            apples.Add(g);
        }
    }
}
