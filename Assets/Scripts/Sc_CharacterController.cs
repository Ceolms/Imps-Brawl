using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_CharacterController : MonoBehaviour
{
    public enum JumpState
    {
        CanJump,
        CanDoubleJump,
        Fall
    }


    [SerializeField]
    public float F_MOVESPEED = 5f;
    public float F_JUMPFORCE = 8.5f;
    public float RAY_GROUND_START = 0.1f;
    public float RAY_GROUND_LENGHT = 0.3f;
    public int playerID = 0;
    [Header("Components")]
    private Player player;
    private Rigidbody rb;
    private new CapsuleCollider collider;
    private Sc_AnimatorController animatorController;
    [Header("Variables")]
    [SerializeField]
    private Vector2 v2_moveAxis;
    [SerializeField]
    private bool b_IsGrounded;
    private bool b_JumpButtonDown;
    [SerializeField]
    private JumpState jumpState = JumpState.CanJump;

    private void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        player = ReInput.players.GetPlayer(playerID);
        rb = GetComponent<Rigidbody>();
        animatorController = GetComponentInChildren<Sc_AnimatorController>();
    }

    // Update is called once per frame
    private void Update()
    {
        b_IsGrounded = Physics.Raycast(transform.position + (Vector3.up * RAY_GROUND_START), -Vector3.up * RAY_GROUND_LENGHT, RAY_GROUND_LENGHT, 1 << 3, QueryTriggerInteraction.UseGlobal) && rb.velocity.y <= 0;
        if (b_IsGrounded)
        {
            jumpState = JumpState.CanJump;
        }

        if (player.GetButtonDown("Jump")) b_JumpButtonDown = true;

        if (rb.velocity.y <= 0) this.gameObject.layer = LayerMask.NameToLayer("Default");
        else this.gameObject.layer = LayerMask.NameToLayer("Flying");


        Debug.DrawRay(transform.position + (Vector3.up * RAY_GROUND_START), -Vector3.up * RAY_GROUND_LENGHT);
        GetInputs();
        Move();

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            F_MOVESPEED += 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            F_MOVESPEED -= 0.1f;
        }
    }

    private void FixedUpdate()
    {
        if (jumpState != JumpState.Fall && b_JumpButtonDown)
        {
            Debug.Log("AddForce");
            b_JumpButtonDown = false;
            rb.AddForce(new Vector3(0, F_JUMPFORCE, 0), ForceMode.Impulse);
            if (jumpState == JumpState.CanJump) jumpState = JumpState.CanDoubleJump;
            else if (jumpState == JumpState.CanDoubleJump) jumpState = JumpState.Fall;
        }
    }

    private void GetInputs()
    {
        if (!ReInput.isReady) return;
        v2_moveAxis.x = player.GetAxis("Move Horizontal");
        //    v2_moveAxis.y = player.GetAxis("Move Vertical");
    }

    private void Move()
    {
        Vector3 tempVect = new Vector3(v2_moveAxis.x, 0, 0);
        float finalSpeed = F_MOVESPEED;
        if (!b_IsGrounded) finalSpeed /= 3;
        tempVect = tempVect.normalized * finalSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + tempVect);
        if (v2_moveAxis.x > 0) this.transform.eulerAngles = new Vector3(0, 145, 0);
        else if (v2_moveAxis.x < 0) this.transform.eulerAngles = new Vector3(0, -145, 0);

        if (v2_moveAxis.x != 0 && b_IsGrounded) animatorController.SetWalking(true);
        else animatorController.SetWalking(false);
    }

    private bool CollideWithPlateforms()
    {
        foreach (Collider col in Sc_LevelManager.Instance.list_collidersPlatforms)
        {
            if (col.bounds.Intersects(this.collider.bounds)) return true;
        }
        return false;
    }
}
