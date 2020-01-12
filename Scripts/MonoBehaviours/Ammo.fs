namespace HalpernRPG.MonoBehaviours

open UnityEngine

[<AllowNullLiteral>]
type Ammo() =
    inherit MonoBehaviour()

    [<DefaultValue>]val mutable damageInflicted : float32

    member this.OnTriggerEnter2D (collider: Collider2D) =
        match collider with
        | :? BoxCollider2D ->
            let enemy = collider.gameObject.GetComponent<Enemy>()
            this.StartCoroutine (enemy.DamageCharacter this.damageInflicted 0.f) |> ignore
            this.gameObject.SetActive false
        | _ -> ()
