namespace HalpernRPG.MonoBehaviours

open UnityEngine
open HalpernRPG

[<AllowNullLiteral>]
[<RequireComponent(typeof<Animator>)>]
type Weapon() as this =
    inherit MonoBehaviour()

    [<DefaultValue>]val mutable ammoPrefab : GameObject
    [<DefaultValue>]val mutable poolSize : int
    [<DefaultValue>]val mutable weaponVelocity : float32

    static let mutable ammoPool = Unchecked.defaultof<GameObject []>

    let mutable isFiring = false
    let mutable animator = Unchecked.defaultof<Animator>
    let mutable localCamera = Unchecked.defaultof<Camera>
    let mutable positiveSlope = 0.f
    let mutable negativeSlope = 0.f

    let spawnAmmo location =
        let candidate = ammoPool |> Array.tryFind (fun ammo -> not ammo.activeSelf)
        match candidate with
        | Some ammo ->
            ammo.transform.position <- location
            ammo.SetActive true
            ammo
        | _ -> null

    let fireAmmo () =
        let mousePos = Camera.main.ScreenToWorldPoint Input.mousePosition
        let ammo = spawnAmmo this.transform.position
        if isNull ammo then ()
        let arc = ammo.GetComponent<Arc> ()
        let duration = 1.f / this.weaponVelocity
        this.StartCoroutine (arc.TravelArc mousePos duration) |> ignore

    let getSlope (pointOne: Vector3) (pointTwo: Vector3) =
        (pointTwo.y - pointOne.y) / (pointTwo.x / pointOne.x)

    let higherThanPositiveSlopeLine (inputPosition: Vector2) =
        let playerPosition = this.gameObject.transform.position
        let mousePosition = localCamera.ScreenToWorldPoint (Vector.from2to3 inputPosition)

        let yIntercept = playerPosition.y - (positiveSlope * playerPosition.x)
        let inputIntercept = mousePosition.y - (positiveSlope * mousePosition.x)
        inputIntercept > yIntercept

    let higherThanNegativeSlopeLine (inputPosition: Vector2) =
        let playerPosition = this.gameObject.transform.position
        let mousePosition = localCamera.ScreenToWorldPoint (Vector.from2to3 inputPosition)

        let yIntercept = playerPosition.y - (negativeSlope * playerPosition.x)
        let inputIntercept = mousePosition.y - (negativeSlope * mousePosition.x)
        inputIntercept > yIntercept

    let getQuadrant () =
        let mousePosition = Vector.from3to2 Input.mousePosition
        match higherThanPositiveSlopeLine mousePosition, higherThanNegativeSlopeLine mousePosition with
        | false, true -> Vector2 (1.f, 0.f)
        | false, false -> Vector2 (0.f, -1.f)
        | true, false -> Vector2 (-1.f, 1.f)
        | true, true -> Vector2 (0.f, 1.f)

    let updateState () =
        if isFiring then
            let quadrantVector = getQuadrant ()
            animator.SetBool ("isFiring", true)
            animator.SetFloat ("fireXDir", quadrantVector.x)
            animator.SetFloat ("fireYDir", quadrantVector.y)
            isFiring <- false 
        else
            animator.SetBool ("isFiring", false)

    member this.Start () =
        animator <- this.GetComponent<Animator> ()
        localCamera <- Camera.main

        let lowerLeft = localCamera.ScreenToWorldPoint (Vector3 (0.f, 0.f, 0.f))
        let upperRight = localCamera.ScreenToWorldPoint (Vector3 (float32 Screen.width, float32 Screen.height, 0.f))
        let upperLeft = localCamera.ScreenToWorldPoint (Vector3 (0.f, float32 Screen.height, 0.f))
        let lowerRight = localCamera.ScreenToWorldPoint (Vector3 (float32 Screen.width, 0.f, 0.f))

        positiveSlope <- getSlope lowerLeft upperRight
        negativeSlope <- getSlope upperLeft lowerRight

    member this.Awake () =
        if isNull ammoPool then
            ammoPool <- Array.init this.poolSize (fun _ ->
                let ammo = MonoBehaviour.Instantiate this.ammoPrefab :?> GameObject
                ammo.SetActive false
                ammo)

    member _.Update () =
        if Input.GetMouseButtonDown 0 then
            isFiring <- true
            fireAmmo ()
        updateState ()
    
    member _.OnDestroy () =
        ammoPool <- null
