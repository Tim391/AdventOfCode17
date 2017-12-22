module Day13

    open System.IO

    let private input = 
        let path = __SOURCE_DIRECTORY__ + "\Day13Input.txt"
        path 
        |> File.ReadAllLines
        |> Array.map (fun l -> l.Split(':') |> Array.map int)
        |> Array.map (fun l -> (l.[0], l.[1]))
        |> Array.toList
    
    let passesFirewall delay inputList =
        let rec step inputList =
            match inputList with
            | (location, depth)::xs ->
                if (location+delay) % (2 * (depth-1)) = 0 then
                    false
                else 
                    step xs
            | [] -> true
        step inputList

    let rec minDelay delay inputList = 
        if passesFirewall delay inputList then
            delay
        else
            minDelay (delay + 1) inputList

    let answer = 
        input
        |> List.fold (fun s (l, d) -> 
            if l % (2 *(d-1)) = 0 then
                s + l * d
            else
                s 
        ) 0

    let answer2 = minDelay 0 input

