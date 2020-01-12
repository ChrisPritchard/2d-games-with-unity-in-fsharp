namespace HalpernRPG.MonoBehaviours

open System.Collections
open UnityEngine

[<AllowNullLiteral>]
type Arc() =
    inherit MonoBehaviour()

    member this.TravelArc destination duration =
        seq {
            let startPosition = this.transform.position
            let mutable percentComplete = 0.f

            while percentComplete < 1.f do
                percentComplete <- percentComplete + (Time.deltaTime / duration)
                this.transform.position <- Vector3.Lerp (startPosition, destination, percentComplete)
                yield ()

            this.gameObject.SetActive false
        } :?> IEnumerator
