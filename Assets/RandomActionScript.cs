using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomActionScript : MonoBehaviour
{
    [SerializeField]Animator anim;
    float wait=5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        wait-=Time.deltaTime;
        TryForRandomAction();
    }
    public void TryForRandomAction()
    {
        if(!anim)
        {
            return;
        }
        if(wait>0)
        {
            return;
        }
        int num = Random.Range(1,4);
        wait = Random.Range(15,20);
        anim.Play("idle"+num.ToString());
    }
}
