namespace HalpernRPG.MonoBehaviours

open UnityEngine

[<AllowNullLiteral>]
type SpawnPoint() =
    inherit MonoBehaviour()

    [<DefaultValue>]val mutable prefabToSpawn : GameObject
    [<DefaultValue>]val mutable repeatInterval : float32

    member this.Start () =
        if this.repeatInterval > 0.f then
            this.InvokeRepeating ("SpawnObject", 0.f, this.repeatInterval)

    member this.SpawnObject () =
        if isNull this.prefabToSpawn then
            null
        else
            MonoBehaviour.Instantiate (this.prefabToSpawn, this.transform.position, Quaternion.identity)