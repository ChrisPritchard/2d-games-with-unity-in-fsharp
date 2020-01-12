namespace HalpernRPG

open UnityEngine

module Vector =

    let from2to3 (v: Vector2) =
        Vector3 (v.x, v.y, 0.f)

    let from3to2 (v: Vector3) =
        Vector2 (v.x, v.y)