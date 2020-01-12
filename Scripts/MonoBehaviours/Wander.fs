namespace HalpernRPG.MonoBehaviours

open UnityEngine
open System.Collections

[<RequireComponent(typeof<Rigidbody2D>)>]
[<RequireComponent(typeof<CircleCollider2D>)>]
[<RequireComponent(typeof<Animator>)>]
type Wander() = 
    inherit MonoBehaviour()

    [<DefaultValue>]val mutable pursuitSpeed : float32
    [<DefaultValue>]val mutable wanderSpeed : float32
    let mutable currentSpeed = 0.f

    [<DefaultValue>]val mutable directionChangeInterval : float32
    [<DefaultValue>]val mutable followPlayer : bool
    let mutable moveCoroutine = Unchecked.defaultof<Coroutine>

    let mutable rb2d = Unchecked.defaultof<Rigidbody2D>
    let mutable animator = Unchecked.defaultof<Animator>

    let mutable targetTransform = Unchecked.defaultof<Transform>
    let mutable endPosition = Unchecked.defaultof<Vector3>
    let mutable currentAngle = 0.f