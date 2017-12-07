module Day6

    let testInput = [|0; 2; 7; 0|]
    let private input = [|14; 0; 15; 12; 11; 11; 3; 5; 1; 6; 8; 4; 9; 1; 8; 4|]

    let rec private nextList(allLists, curr, count) =
        let newList = curr |> Array.toList
        if allLists |> List.contains newList then (count, newList)
        else 
            let newAllLists = newList::allLists
            let max = curr |> Array.max
            let mutable pos = curr |> Array.findIndex (fun e -> e = max)
            curr.[pos] <- 0
            for _ in 1..max do
                pos <- pos+1
                if pos > curr.Length-1 then pos <- 0
                curr.[pos] <- curr.[pos] + 1
            nextList(newAllLists, curr, count+1)


    let answer =
        let ansC, ansL = nextList([], input, 0)
        ansL

    let answer2 = 
        let ansC, ansL = nextList([], answer |> List.toArray, 0)
        ansC