namespace HalpernRPG.MonoBehaviours

open UnityEngine

type MovementController() as this = 
    inherit MonoBehaviour()

    [<DefaultValue>]val mutable movementSpeed : float32

    do
        this.movementSpeed <- 3.f

    let mutable movement = Vector2()
    let mutable rb2d = Unchecked.defaultof<Rigidbody2D>
    let mutable animator = Unchecked.defaultof<Animator>

    let updateState () =
        let idle = Mathf.Approximately (movement.x, 0.f) && Mathf.Approximately (movement.y, 0.f)
        animator.SetBool ("isWalking", not idle)
        animator.SetFloat ("xDir", movement.x)
        animator.SetFloat ("yDir", movement.y)

    let moveCharacter () =
        movement.x <- Input.GetAxisRaw "Horizontal"
        movement.y <- Input.GetAxisRaw "Vertical"
        movement.Normalize ()
        
        rb2d.velocity <- movement * this.movementSpeed

    member this.Start() = 
        rb2d <- this.GetComponent<Rigidbody2D>()
        animator <- this.GetComponent<Animator>()

    member _.Update() =
        updateState ()

    member _.FixedUpdate() =
        moveCharacter ()
        