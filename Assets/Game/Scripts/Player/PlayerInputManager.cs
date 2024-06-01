using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
	PlayerInput playerInput;

	[Header("Character Input Values")]
	Vector2 move;
	public Vector2 Move => move;

	void Awake()
	{
		playerInput = new PlayerInput();
	}

    private void OnEnable()
    {
		playerInput.Enable();
    }

    private void OnDisable()
    {
		playerInput.Disable();
    }

    private void Update()
    {
		move = playerInput.Movement.Move.ReadValue<Vector2>();
	}
}

