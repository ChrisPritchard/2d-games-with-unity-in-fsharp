namespace HalpernRPG.Managers

open UnityEngine
open HalpernRPG.MonoBehaviours

[<AllowNullLiteral>]
type RPGGameManager() as this =
    inherit MonoBehaviour()

    [<DefaultValue>]val mutable playerSpawnPoint : SpawnPoint

    static let mutable sharedInstance = Unchecked.defaultof<_>

    let spawnPlayer () =
        if not (isNull this.playerSpawnPoint) then
            this.playerSpawnPoint.SpawnObject () |> ignore

    let setupScene () =
        spawnPlayer ()

    member this.Awake () =
        if sharedInstance <> this && not (isNull sharedInstance) then
            MonoBehaviour.Destroy this.gameObject
        else
            sharedInstance <- this

    member _.Start () =
        setupScene ()