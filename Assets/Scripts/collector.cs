using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class collector : MonoBehaviour
{
    private List<ARRaycastHit> rayhits = new List<ARRaycastHit>();
    public ARRaycastManager rayman;
    public GameObject apple, par, tree, issac, pointer;
    GameObject g, tre;
    List<GameObject> apples = new List<GameObject>(), pointers = new List<GameObject>();
    Transform plane;

    public uicont script;

    bool s = false, ran = false, helped = false;
    int total;

    void Start(){
        total = 3;
    }

    
    void Update()
    {
        if (!ran && script.collected()) {
            script.showMessage("Now put the apples back on the tree");
            ran = true;
        }
        if (Input.touchCount > 0){
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Ended){
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) {
                    if (s){
                        // hit apple
                        if (hit.collider.tag == "apple") {
                            if (hit.distance < 1.5f) { 
                                Destroy(hit.collider.gameObject);
                                script.incriment();
                            }
                            else {  
                                script.showMessage("Apple too far away");
                            }
                        }
                        // hit tree with all apples
                        else if (hit.collider.tag == "tree" && script.collected()) {
                            if (total == 3) {
                                script.showMessage("More apples fell off, go collect them");
                                ran = false;
                                script.res();
                                total++;
                                spawn();
                            }
                            else{
                                script.showMessage("Congrats! you helped discover gravity");
                                script.final();
                            }
                        }
                    }
                    // first tap starts game
                    else {
                        script.showMessage("Collect the apples to help Issac Newton discover gravity");
                        s = true;
                        script.star();
                        plane = hit.collider.transform;
                        Vector3 pos = ray.direction * hit.distance + new Vector3(0, 2, 0);

                        tre = Instantiate(tree, pos, Quaternion.identity);
                        g = Instantiate(issac, pos + new Vector3(0, -0.6f, 0), Quaternion.identity);
                        g.transform.LookAt(GameObject.Find("Main Camera").transform.position);
                        g.transform.position += g.transform.forward;
                        g.transform.eulerAngles = new Vector3(-90, g.transform.eulerAngles.y, g.transform.eulerAngles.z);
                        spawn();
                    }
                }
            }
        }   
    }

    void spawn(){
        float xmin = plane.position.x - plane.localScale.x;
        float xmax = plane.position.x + plane.localScale.x;
        float zmin = plane.position.z - plane.localScale.z;
        float zmax = plane.position.z + plane.localScale.z;

        for (int i = 0; i < total; i++) {
            g = Instantiate(apple, new Vector3(Random.Range(xmin, xmax), 2f, Random.Range(zmin, zmax)), Quaternion.identity);
            g.transform.SetParent(par.transform);
            apples.Add(g);
        }
    }

    public void help() {
        if (!helped) {
            helped = true;
            foreach (GameObject i in apples) {
                g = Instantiate(pointer, i.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                g.transform.SetParent(i.transform);
                pointers.Add(g);
                g.transform.eulerAngles = new Vector3(180, g.transform.eulerAngles.y, g.transform.eulerAngles.z);
            }
        }
        else {
            foreach (GameObject i in pointers) {
                Destroy(i.gameObject);
            }
            pointers.Clear();
        }
        
    }
}
