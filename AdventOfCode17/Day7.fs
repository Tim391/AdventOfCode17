module Day7

    open System
    open System.IO
    open System.Text.RegularExpressions

    let private input =
        let path = __SOURCE_DIRECTORY__ + "\Day7Input.txt"
        path 
        |> File.ReadAllLines

    let private (|Regex|_|) pattern input =
            let m = Regex.Match(input, pattern)
            if m.Success then Some(List.tail [ for g in m.Groups -> g.Value ])
            else None

    let private createNode line =
        match line with
        | Regex @"([a-z]+) \((\d+)\)(?: -> )?(.*)" [name; weight; childList] ->
                name,
                int weight,
                childList.Split([|','|]) 
                |> Array.choose(fun s -> match s.Trim() with "" -> None | trimmed -> Some trimmed)
                |> Array.toList
        | _ -> failwith "Didn't match"

    let private isBase(curr,  nodes: (string * int * string list) list) =
        let (name, _, _) = curr
        let b = 
            nodes
            |> List.exists (fun (_, _, children) -> children |> List.contains name)
        not b

    let private getNode name nodes =
        nodes
        |> List.find (fun (n, _, _) -> n = name)

    let rec private calculateWeight(weight: int, node: (string * int * string list), nodes: (string * int * string list) list) =
        let (_, w, children) = node
        let childrensWeight =
            children 
            |> List.fold (fun acc c -> 
                let n = getNode c nodes
                calculateWeight(acc, n, nodes)) 0
        weight + w + childrensWeight

    let answer =
        let nodes = 
            input
            |> Array.map createNode
            |> Array.toList
        let findBase = 
            nodes
            |> List.find (fun n -> isBase(n, nodes))
        let (name, _, _) = findBase
        name

    let answer2 =
        let nodes = 
            input
            |> Array.map createNode
            |> Array.toList
        nodes

