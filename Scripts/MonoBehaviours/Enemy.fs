namespace HalpernRPG.MonoBehaviours

open System.Collections
open UnityEngine
open HalpernRPG.ScriptableObjects

type Enemy() = 
    inherit Character()

    [<DefaultValue>]val mutable hitPoints : float32
    [<DefaultValue>]val mutable damageStrength : float32
    
    let mutable damageCoroutine = Unchecked.defaultof<_>

    override this.ResetCharacter () =
        this.hitPoints <- this.startingHitPoints

    override this.DamageCharacter damage interval = 
        seq {
            let mutable shouldBreak = false
            while not shouldBreak do
                this.StartCoroutine (this.FlickerCharacter ()) |> ignore
                this.hitPoints <- this.hitPoints - damage
                if this.hitPoints <= 0.f then
                    this.KillCharacter ()
                    shouldBreak <- true
                else if interval > 0.f then
                    yield WaitForSeconds interval
                else 
                    shouldBreak <- true
        } :?> IEnumerator

    member this.OnEnable () =
        this.ResetCharacter ()

    member this.OnCollisionEnter2D (collision: Collision2D) =
        if collision.gameObject.CompareTag "Player" && isNull damageCoroutine then
            let player = collision.gameObject.GetComponent<Player> ()
            damageCoroutine <- this.StartCoroutine (player.DamageCharacter this.damageStrength 1.f)

    member this.OnCollisionExit2D (collision: Collision2D) =
        if collision.gameObject.CompareTag "Player" && not (isNull damageCoroutine) then
            this.StopCoroutine damageCoroutine
            damageCoroutine <- null