using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Объявление класса UserInputSystem, который наследует от ComponentSystem, предоставляемого Unity.Entities.
/// </summary>
public class UserInputSystem : ComponentSystem
{
    //Объявление переменной inputQuery для запроса сущностей.
    private EntityQuery inputQuery;

    //Объявление переменной moveAction,shootAction,boostAction для действия ввода для движения, стрельбы и ускорения.
    private InputAction moveAction;
    private InputAction shootAction;
    private InputAction boostAction;

    //Объявление переменной moveInput,shootInput,boostInput для хранения данных ввода действий.
    private float2 moveInput;
    private float shootInput;
    private float boostInput;

    /// <summary>
    /// Вызывается при создании системы и устанавливает запрос для получения сущностей с компонентом InputData.
    /// </summary>
    protected override void OnCreate()
    {
        // Запрос для получения всех сущностей с компонентом InputData.
        inputQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>());
    }

    /// <summary>
    /// Вызывается при начале выполнения системы и инициализирует действия ввода.
    /// </summary>
    protected override void OnStartRunning()
    {
        // Инициализация переменной moveAction новым действием ввода "move", привязанным к правому стику геймпада.
        moveAction = new InputAction("move", binding: "<Gamepad>/rightStick");
        // Инициализация переменной shootAction новым действием ввода "shoot", привязанным к пробелу.
        shootAction = new InputAction("shoot", binding: "<Keyboard>/Space");
        // Инициализация переменной boostAction новым действием ввода "boost", привязанным к левому Shift.
        boostAction = new InputAction("boost", binding: "<Keyboard>/LeftShift");

        //Добавление композитной привязки "Dpad" к действию moveAction.
        moveAction.AddCompositeBinding("Dpad")
           .With("Up", "<Keyboard>/w")
           .With("Down", "<Keyboard>/s")
           .With("Left", "<Keyboard>/a")
           .With("Right", "<Keyboard>/d");

        // Состояния устройства ввода  => в любом случае передаем в moveInput и moveRun положение джойстика
        moveAction.performed += context => { moveInput = context.ReadValue<Vector2>(); };
        moveAction.started += context => { moveInput = context.ReadValue<Vector2>(); };
        moveAction.canceled += context => { moveInput = context.ReadValue<Vector2>(); };
        moveAction.Enable(); // включение 

        // Состояния устройства ввода  => в любом случае передаем в shootInput нажатие на кнопку.
        shootAction.performed += context => { shootInput = context.ReadValue<float>(); };
        shootAction.started += context => { shootInput = context.ReadValue<float>(); };
        shootAction.canceled += context => { shootInput = context.ReadValue<float>(); };
        shootAction.Enable(); // включение

        // Состояния устройства ввода  => в любом случае передаем в boostInput нажатие на кнопку.
        boostAction.performed += context => { boostInput = context.ReadValue<float>(); };
        boostAction.started += context => { boostInput = context.ReadValue<float>(); };
        boostAction.canceled += context => { boostInput = context.ReadValue<float>(); };
        boostAction.Enable(); // включение
    }

    /// <summary>
    /// Вызывается при остановке выполнения системы и отключает действия ввода.
    /// </summary>
    protected override void OnStopRunning()
    {
        //Отключение действия ввода moveAction, shootAction, boostAction.
        moveAction.Disable();
        shootAction.Disable();
        boostAction.Disable();
    }

    /// <summary>
    /// Вызывается каждый кадр для обновления данных в компонентах сущностей.
    /// </summary>
    protected override void OnUpdate()
    {
        // Начало цикла для каждой сущности, удовлетворяющей запросу inputQuery.
        Entities.With(inputQuery).ForEach(
          (Entity entity, ref InputData inputData) => 
            {
                //Присвоение данных ввода
                inputData.move = moveInput;
                inputData.shoot = shootInput;
                inputData.boost = boostInput;
            });
    }
}

/* 
### Техническая документация:

#### 1. Назначение:
Этот код определяет систему для обработки пользовательского ввода и передачи данных в компоненты сущностей.

#### 2. Ключевые особенности:
- Система использует InputSystem Unity для обработки пользовательского ввода.
- Действия ввода определяются для различных операций: движение, стрельба и ускорение.
- При каждом обновлении системы данные ввода передаются в компоненты сущностей.

#### 3. Использование:
Эта система может быть применена к сущностям, управляемым пользователем, для обработки их ввода и управления в игре.
*/
