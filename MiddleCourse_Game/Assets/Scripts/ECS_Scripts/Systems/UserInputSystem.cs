using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ���������� ������ UserInputSystem, ������� ��������� �� ComponentSystem, ���������������� Unity.Entities.
/// </summary>
public class UserInputSystem : ComponentSystem
{
    //���������� ���������� inputQuery ��� ������� ���������.
    private EntityQuery inputQuery;

    //���������� ���������� moveAction,shootAction,boostAction ��� �������� ����� ��� ��������, �������� � ���������.
    private InputAction moveAction;
    private InputAction shootAction;
    private InputAction boostAction;

    //���������� ���������� moveInput,shootInput,boostInput ��� �������� ������ ����� ��������.
    private float2 moveInput;
    private float shootInput;
    private float boostInput;

    /// <summary>
    /// ���������� ��� �������� ������� � ������������� ������ ��� ��������� ��������� � ����������� InputData.
    /// </summary>
    protected override void OnCreate()
    {
        // ������ ��� ��������� ���� ��������� � ����������� InputData.
        inputQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>());
    }

    /// <summary>
    /// ���������� ��� ������ ���������� ������� � �������������� �������� �����.
    /// </summary>
    protected override void OnStartRunning()
    {
        // ������������� ���������� moveAction ����� ��������� ����� "move", ����������� � ������� ����� ��������.
        moveAction = new InputAction("move", binding: "<Gamepad>/rightStick");
        // ������������� ���������� shootAction ����� ��������� ����� "shoot", ����������� � �������.
        shootAction = new InputAction("shoot", binding: "<Keyboard>/Space");
        // ������������� ���������� boostAction ����� ��������� ����� "boost", ����������� � ������ Shift.
        boostAction = new InputAction("boost", binding: "<Keyboard>/LeftShift");

        //���������� ����������� �������� "Dpad" � �������� moveAction.
        moveAction.AddCompositeBinding("Dpad")
           .With("Up", "<Keyboard>/w")
           .With("Down", "<Keyboard>/s")
           .With("Left", "<Keyboard>/a")
           .With("Right", "<Keyboard>/d");

        // ��������� ���������� �����  => � ����� ������ �������� � moveInput � moveRun ��������� ���������
        moveAction.performed += context => { moveInput = context.ReadValue<Vector2>(); };
        moveAction.started += context => { moveInput = context.ReadValue<Vector2>(); };
        moveAction.canceled += context => { moveInput = context.ReadValue<Vector2>(); };
        moveAction.Enable(); // ��������� 

        // ��������� ���������� �����  => � ����� ������ �������� � shootInput ������� �� ������.
        shootAction.performed += context => { shootInput = context.ReadValue<float>(); };
        shootAction.started += context => { shootInput = context.ReadValue<float>(); };
        shootAction.canceled += context => { shootInput = context.ReadValue<float>(); };
        shootAction.Enable(); // ���������

        // ��������� ���������� �����  => � ����� ������ �������� � boostInput ������� �� ������.
        boostAction.performed += context => { boostInput = context.ReadValue<float>(); };
        boostAction.started += context => { boostInput = context.ReadValue<float>(); };
        boostAction.canceled += context => { boostInput = context.ReadValue<float>(); };
        boostAction.Enable(); // ���������
    }

    /// <summary>
    /// ���������� ��� ��������� ���������� ������� � ��������� �������� �����.
    /// </summary>
    protected override void OnStopRunning()
    {
        //���������� �������� ����� moveAction, shootAction, boostAction.
        moveAction.Disable();
        shootAction.Disable();
        boostAction.Disable();
    }

    /// <summary>
    /// ���������� ������ ���� ��� ���������� ������ � ����������� ���������.
    /// </summary>
    protected override void OnUpdate()
    {
        // ������ ����� ��� ������ ��������, ��������������� ������� inputQuery.
        Entities.With(inputQuery).ForEach(
          (Entity entity, ref InputData inputData) => 
            {
                //���������� ������ �����
                inputData.move = moveInput;
                inputData.shoot = shootInput;
                inputData.boost = boostInput;
            });
    }
}

/* 
### ����������� ������������:

#### 1. ����������:
���� ��� ���������� ������� ��� ��������� ����������������� ����� � �������� ������ � ���������� ���������.

#### 2. �������� �����������:
- ������� ���������� InputSystem Unity ��� ��������� ����������������� �����.
- �������� ����� ������������ ��� ��������� ��������: ��������, �������� � ���������.
- ��� ������ ���������� ������� ������ ����� ���������� � ���������� ���������.

#### 3. �������������:
��� ������� ����� ���� ��������� � ���������, ����������� �������������, ��� ��������� �� ����� � ���������� � ����.
*/
