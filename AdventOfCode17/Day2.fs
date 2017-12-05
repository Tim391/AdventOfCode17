module Day2

    open System
    open System.IO

    let private input =
        let path = __SOURCE_DIRECTORY__ + "\Day2Input.txt"
        path 
        |> File.ReadAllLines
        |> Array.toList
        |> List.map (fun l -> (l.Split(null) |> Array.map Int32.Parse))

    let private difference(acc, arr) =
        let min = arr |> Array.min
        let max = arr |> Array.max
        let dif = max - min
        acc + dif

    let private findDivisible curr rest = 
        rest 
        |> Array.map (fun n ->
            match curr with
            | curr when curr = n -> 0
            | curr when curr % n = 0 -> curr / n
            | _ -> 0
            )
        |> Array.sum
            

    let private divisors(acc, arr) = 
        let s = 
            arr
            |> Array.map (fun e -> findDivisible e arr)
            |> Array.sum
        acc + s

    let answer =
        let ans = 
            input
            |> List.fold (fun acc arr -> difference(acc, arr)) 0
        ans

    let answer2 = 
        let ans = 
            input
            |> List.fold (fun acc arr -> divisors(acc, arr)) 0
        ans




