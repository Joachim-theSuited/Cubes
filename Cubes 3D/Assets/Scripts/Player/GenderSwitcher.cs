using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenderSwitcher : MonoBehaviour {

    public RuntimeAnimatorController animationController;

    public GameObject male;
    public GameObject female;

    bool isMale = false;
    GameObject last;

    void Start() {
        ResetModel();
    }

    void Update() {
        if(Input.GetKeyUp(KeyCode.G)) {
            isMale = !isMale;
            ResetModel();
        }
    }

    void ResetModel() {
        Destroy(last);
        if(isMale)
            last = Instantiate(male, transform);
        else
            last = Instantiate(female, transform);

        var anim = last.GetComponent<Animator>();
        anim.runtimeAnimatorController = animationController;
    }

}
