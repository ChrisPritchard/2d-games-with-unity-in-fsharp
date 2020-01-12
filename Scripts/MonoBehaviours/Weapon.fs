namespace HalpernRPG.MonoBehaviours

open UnityEngine

[<AllowNullLiteral>]
type Weapon() as this =
    inherit MonoBehaviour()

    [<DefaultValue>]val mutable ammoPrefab : GameObject
    [<DefaultValue>]val mutable poolSize : int
    [<DefaultValue>]val mutable weaponVelocity : float32

    static let mutable ammoPool = Unchecked.defaultof<GameObject []>

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

    member this.Awake () =
        if isNull ammoPool then
            ammoPool <- Array.init this.poolSize (fun _ ->
                let ammo = MonoBehaviour.Instantiate this.ammoPrefab :?> GameObject
                ammo.SetActive false
                ammo)

    member _.Update () =
        if Input.GetMouseButtonDown 0 then
            fireAmmo ()
    
    member _.OnDestroy () =
        ammoPool <- null
