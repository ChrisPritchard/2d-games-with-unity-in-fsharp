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
        let setState n = animator.SetInteger ("AnimationState", n)

        if movement.x > 0.f then setState 1
        elif movement.y > 0.f then setState 2
        elif movement.x < 0.f then setState 3
        elif movement.y < 0.f then setState 4
        else setState 0

    let moveCharacter () =
        movement.x <- Input.GetAxisRaw "Horizontal"
        movement.y <- Input.GetAxisRaw "Vertical"
        movement.Normalize ()
        
        rb2d.velocity <- movement * this.movementSpeed

    member __.Start() = 
        rb2d <- __.GetComponent<Rigidbody2D>()
        animator <- __.GetComponent<Animator>()

    member _.Update() =
        updateState ()

    member _.FixedUpdate() =
        moveCharacter ()
        