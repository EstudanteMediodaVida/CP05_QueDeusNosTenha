using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    private PlayerControls pCon;

    void Awake()
    {
        anim = GetComponent<Animator>();
        pCon = GetComponent<PlayerControls>();
    }

    void Update()
    {
        anim.SetInteger("pMove", pCon.MoveValue());
        anim.SetInteger("pJump", pCon.Jumping());
        anim.SetInteger("pFall", pCon.Falling());
    }
}
