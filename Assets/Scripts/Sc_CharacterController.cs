using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_CharacterController : MonoBehaviour
{
    private const float F_MOVESPEED = 10f;
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
        b_IsGrounded = Physics.Raycast(transform.position + (Vector3.up * 0.1f), -Vector3.up * 0.3f, 0.3f, 1 << 3, QueryTriggerInteraction.UseGlobal);
        //   Debug.DrawRay(transform.position + (Vector3.up * 0.1f), -Vector3.up * 0.3f);
        GetInputs();
        Move();
    }

    private void GetInputs()
    {
        v2_moveAxis.x = player.GetAxis("Move Horizontal");
        v2_moveAxis.y = player.GetAxis("Move Vertical");
    }

    private void Move()
    {
        Vector3 tempVect = new Vector3(v2_moveAxis.x, 0, 0);
        tempVect = tempVect.normalized * F_MOVESPEED * Time.deltaTime;
        rb.MovePosition(transform.position + tempVect);
        if (v2_moveAxis.x > 0) this.transform.eulerAngles = new Vector3(0, 145, 0);
        else if (v2_moveAxis.x < 0) this.transform.eulerAngles = new Vector3(0, 325, 0);

        if (v2_moveAxis.x != 0 && b_IsGrounded) animatorController.SetWalking(true);
        else animatorController.SetWalking(false);
    }
}
