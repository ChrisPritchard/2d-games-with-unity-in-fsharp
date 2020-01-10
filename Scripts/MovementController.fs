namespace HalpernRPG

open UnityEngine

type MovementController() as this = 
    inherit MonoBehaviour()

    [<DefaultValue>]val mutable movementSpeed : float32

    let mutable movement = Vector2()
    let mutable rb2d = Unchecked.defaultof<Rigidbody2D>

    do
        this.movementSpeed <- float32 3.

    member this.Start() = 
        rb2d <- this.GetComponent<Rigidbody2D>()

    member this.FixedUpdate() =

        movement.x <- Input.GetAxisRaw("Horizontal")
        movement.y <- Input.GetAxisRaw("Vertical")
        movement.Normalize ()

        rb2d.velocity <- movement * this.movementSpeed