namespace HalpernRPG.MonoBehaviours

open UnityEngine

type Player() = 
    inherit Character()

    member __.OnTriggerEnter2D (collision: Collider2D) =
        if collision.gameObject.CompareTag "CanBePickedUp" then
            collision.gameObject.SetActive false