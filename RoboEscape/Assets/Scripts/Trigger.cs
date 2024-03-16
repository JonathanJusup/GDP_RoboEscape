using UnityEngine;

public class Trigger : TriggerInterface {
    [SerializeField] private Animator animator;
    
    private void Start() {
        SoundManager = FindObjectOfType<SoundManager>();
        CableController = GetComponent<CableController>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!isActivated)
        {
            isActivated = true;
            CableController.UpdateState(isActivated);
            animator.ResetTrigger("Up");
            animator.SetTrigger("Down");
            //Debug.Log("BUTTON DOWN");
            
            if (SoundManager) {
                SoundManager.PlaySound("Button");
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (!isActivated)
        {
            isActivated = true;
            CableController.UpdateState(isActivated);
            animator.ResetTrigger("Up");
            animator.SetTrigger("Down");
            //Debug.Log("BUTTON STAY");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isActivated)
        {
            isActivated = false;
            CableController.UpdateState(isActivated);
        }
        animator.ResetTrigger("Down");
        animator.SetTrigger("Up");
        //Debug.Log("BUTTON UP");

    }


}
