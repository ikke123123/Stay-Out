using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private KeyCode up = KeyCode.None;
    [SerializeField] private KeyCode down = KeyCode.None;
    [SerializeField] private KeyCode left = KeyCode.None;
    [SerializeField] private KeyCode right = KeyCode.None;

    [Header("Speed")]
    [SerializeField] private float speed = 10;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        characterController.SimpleMove(new Vector3(((Input.GetKey(up) ? -1 : 0) + (Input.GetKey(down) ? 1 : 0)), 0, ((Input.GetKey(left) ? -1 : 0) + (Input.GetKey(right) ? 1 : 0))).normalized * speed);
    }
}
