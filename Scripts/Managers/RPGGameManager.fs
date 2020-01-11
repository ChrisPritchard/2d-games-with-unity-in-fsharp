namespace HalpernRPG.Managers

open UnityEngine

[<AllowNullLiteral>]
type RPGGameManager() =
    inherit MonoBehaviour()

    static let mutable sharedInstance = Unchecked.defaultof<_>

    let setupScene () = ()

    member this.Awake () =
        if sharedInstance <> this && not (isNull sharedInstance) then
            MonoBehaviour.Destroy this.gameObject
        else
            sharedInstance <- this

    member _.Start () =
        setupScene ()