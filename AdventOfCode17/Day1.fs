module Day1

    open System
    open System.IO

    let private input = 
            let path = __SOURCE_DIRECTORY__ + "\Day1Input.txt"
            let line = File.ReadAllText(path)
            line.ToCharArray()
            |> Array.map (fun c -> Int32.Parse(c.ToString()))

    let rec private sumMatches first total input = 
        match input with
        | [] -> total
        | [x] -> 
            if x = first then total+x
            else total
        | x::xn::xt ->
            if x = xn then sumMatches first (total+x) (xn::xt)
            else sumMatches first total (xn::xt)

    let rec private sumHalfwayMatches(position, total, input: list<int>) =
        if position = input.Length then total
        else
            let halfway = input.[input.Length /2]
            match input with
            | x::xt ->
                if x = halfway then sumHalfwayMatches(position+1, total+x, xt@[x])
                else sumHalfwayMatches(position+1, total, xt@[x])

    let answer = 
        let first = input.[0]
        let sum = sumMatches first 0 (input |> Array.toList)
        sum

    let answer2 =
        let sum = sumHalfwayMatches(0, 0, (input |> Array.toList))
        sum


