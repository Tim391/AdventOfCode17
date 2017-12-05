module Day4

    open System
    open System.IO

    let private input = 
        let path = __SOURCE_DIRECTORY__ + "\Day4Input.txt"
        path 
        |> File.ReadAllLines
        |> Array.toList
        |> List.map (fun l -> (l.Split(null)))

    let valid(acc, arr) =
        let d = arr |> Array.distinct |> Array.length
        if d = arr.Length then acc + 1
        else acc

    let answer =
        let ans = 
            input 
            |> List.fold (fun acc x -> valid(acc, x)) 0
        ans