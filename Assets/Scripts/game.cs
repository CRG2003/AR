using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class game : MonoBehaviour
{
    public GameObject tree, apple, par, issac;
    GameObject g;
    uicont UI;
    collector collect;

    Vector3 treePos;
    bool run = false;
    void Start()
    {
        treePos = this.transform.position + new Vector3(0, 0, 10);
        UI = GetComponent<uicont>();
        collect = GetComponent<collector>();
    }
    void Update()
    {
        if (UI.begun() && !run){
            run = true;
            g = Instantiate(tree, treePos, Quaternion.identity);
            g.transform.SetParent(par.transform);
            g = Instantiate(apple, treePos + new Vector3(0, 3.5f, -1.5f), Quaternion.identity);
            g.transform.SetParent(par.transform);
            g = Instantiate(issac, treePos + new Vector3(0, 0, -1.5f), Quaternion.identity);
            g.transform.Rotate(new Vector3(-90, 180, 0));
            g.transform.SetParent(par.transform);
        }
    }
}