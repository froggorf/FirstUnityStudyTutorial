using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float h;
    private float v;

    public float speed;
    //
    private bool bIsKeyDowned;
    private bool bMoveHorizontal;

    private Rigidbody2D rigid;
    private Animator anim;


    void Start()
    {
        
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        bool bHorizontalDown = Input.GetButtonDown("Horizontal");
        bool bVerticalDown = Input.GetButtonDown("Vertical");
        bool bHorizontalUp = Input.GetButtonUp("Horizontal");
        bool bVerticalUp = Input.GetButtonUp("Vertical");

        if (bHorizontalDown || bVerticalUp)
        {
            bMoveHorizontal = true;
        }
        else if(bVerticalDown || bHorizontalUp)
        {
            bMoveHorizontal = false;
        }

        if (anim.GetInteger("hAxisRaw") != h)
        {
            anim.SetBool("bIsChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if(anim.GetInteger("vAxisRaw") != v)
        {
            anim.SetBool("bIsChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            anim.SetBool("bIsChange", false);
        }
        
        
        
    }


    void FixedUpdate()
    {
        Vector2 moveDir = bMoveHorizontal ? new Vector2(h, 0) : new Vector2(0, v);

        rigid.velocity = moveDir * speed;
    }
}
