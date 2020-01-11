namespace HalpernRPG.MonoBehaviours

open UnityEngine

type Player() = 
    inherit Character()

    member __.OnTriggerEnter2D (collision: Collider2D) =
        if collision.gameObject.CompareTag "CanBePickedUp" then
            let hitObject = collision.gameObject.GetComponent<Consumable>().item
            if not (isNull hitObject) then
                Debug.Log ("it: " + hitObject.objectName)
                collision.gameObject.SetActive false