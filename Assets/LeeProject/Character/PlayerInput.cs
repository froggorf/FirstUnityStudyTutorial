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

    private Vector3 dirVec;
    private GameObject scanObject;

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

        if (bHorizontalDown)
        {
            bMoveHorizontal = true;
        }
        else if(bVerticalDown)
        {
            bMoveHorizontal = false;
        }
        else if (bHorizontalUp || bVerticalUp)
        {
            bMoveHorizontal = (h != 0);
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

        if (bVerticalDown && v == 1)
        {
            dirVec = Vector3.up;
        }
        else if(bVerticalDown && v==-1)
        {
            dirVec = Vector3.down;
        }
        else if (bHorizontalDown && h == 1)
        {
            dirVec = Vector3.right;
        }
        else if (bHorizontalDown && h == -1)
        {
            dirVec = Vector3.left;
        }
    }


    void FixedUpdate()
    {
        Vector2 moveDir = bMoveHorizontal ? new Vector2(h, 0) : new Vector2(0, v);

        rigid.velocity = moveDir * speed;

        //RayTracing
        Debug.DrawRay(rigid.position , dirVec * 2f, new Color(1.0f,0.0f,0.0f),10.0f);
        RaycastHit2D raycastHit = Physics2D.Raycast(rigid.position, dirVec, 0.75f,LayerMask.GetMask("Object"));

        if (raycastHit)
        {
            scanObject = raycastHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }
}
