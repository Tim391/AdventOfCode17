module Day12

    open System
    open System.IO

    let private input =
        let path = __SOURCE_DIRECTORY__ + "\Day12Input.txt"
        path 
        |> File.ReadAllLines
        |> Array.map (fun l -> 
            let parts = l.Split([| "<->" |], StringSplitOptions.None)
            let num = parts.[0] |> int
            let pipes = parts.[1].Split(',') |> Array.map int |> set
            (num, pipes)
        ) |> Map.ofArray

    let rec getPipes(key: int, map, (curr: Set<int>)) =
        let p = map |> Map.find key
        let uniques = Set.difference p curr
        let newSet = Set.union curr p
        if uniques.Count = 0 then newSet
        else
            uniques 
            |> Set.fold (fun n s -> getPipes(s, map, n)) newSet

    let answer =
        getPipes(0, input, Set.empty)
        |> Set.count

    let answer2 =
        input
        |> Map.toList
        |> List.map (fun (i, s) -> getPipes(i, input, Set.empty))
        |> List.distinct
        |> List.length

