namespace HalpernRPG.Managers

open UnityEngine
open HalpernRPG.MonoBehaviours

[<AllowNullLiteral>]
type RPGGameManager() as this =
    inherit MonoBehaviour()

    [<DefaultValue>]val mutable cameraManager : RPGCameraManager
    [<DefaultValue>]val mutable playerSpawnPoint : SpawnPoint

    static let mutable sharedInstance = Unchecked.defaultof<_>

    let spawnPlayer () =
        if not (isNull this.playerSpawnPoint) then
            let player = this.playerSpawnPoint.SpawnObject ()
            this.cameraManager.virtualCamera.Follow <- player.transform

    let setupScene () =
        spawnPlayer ()

    member this.Awake () =
        if sharedInstance <> this && not (isNull sharedInstance) then
            MonoBehaviour.Destroy this.gameObject
        else
            sharedInstance <- this

    member _.Start () =
        setupScene ()