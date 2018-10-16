using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour
{

    private CharacterController characterController;
    private Vector3 velocity;
    [SerializeField] private float walkSpeed;
    private Animator animator;

    [SerializeField] private PlayerFollowCamera refCamera;  // カメラの水平回転を参照する用
    [SerializeField] private float applySpeed = 0.2f;       // 振り向きの適用速度

    // Use this for initialization
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            if (velocity.magnitude > 0.1f)
            {
                animator.SetBool("walkFlag", true);
                transform.LookAt(transform.position + velocity);
                // プレイヤーの回転(transform.rotation)の更新
                // 無回転状態のプレイヤーのZ+方向(後頭部)を、
                // カメラの水平回転(refCamera.hRotation)で回した移動の反対方向(-velocity)に回す回転に段々近づけます
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(refCamera.hRotation * velocity),
                                                      applySpeed);

                // プレイヤーの位置(transform.position)の更新
                // カメラの水平回転(refCamera.hRotation)で回した移動方向(velocity)を足し込みます
                transform.position += refCamera.hRotation * velocity;
            }
            else
            {
                animator.SetBool("walkFlag", false);
            }
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * walkSpeed * Time.deltaTime);
    }
}
