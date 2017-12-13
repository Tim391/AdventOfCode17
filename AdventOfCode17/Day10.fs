module Day10

    open System.IO

    let private input = 
        let path = __SOURCE_DIRECTORY__ + "\Day10Input.txt"
        let str = path |> File.ReadAllText
        str.Split(',')
        |> Array.map int
        |> Array.toList

    type State = { position: int; skip: int; list: int list }

    let newPosition(position, length, listlength) =
        let newPos = position + length
        if newPos > listlength-1 then newPos - listlength
        else newPos

    let reverse current length =
        let splitAt = current.position + length
        if splitAt > current.list.Length then 
            let (h, t) = current.list |> List.splitAt current.position
            let tailSize = t.Length
            let workingList = List.append t h
            let (rh, rt) = workingList |> List.splitAt length
            let reversed = rh |> List.rev
            let b = List.append reversed rt 
            let (bh, bt) = b |> List.splitAt tailSize
            let newList = List.append bt bh
            { position = newPosition(current.position, length+current.skip, current.list.Length); skip = current.skip + 1; list = newList }
        else
            let (h, t) = current.list |> List.splitAt splitAt
            let workingPos = current.position + t.Length
            let workingList = List.append t h
            let (rh, rt) = workingList |> List.splitAt workingPos
            let reversed = rt |> List.rev
            let b = List.append rh reversed
            let (bh, bt) = b |> List.splitAt t.Length
            let newList = List.append bt bh
            { position = newPosition(current.position, length+current.skip, current.list.Length); skip = current.skip + 1; list = newList }

    let answer = 
        let ans =
            input
            |> List.fold reverse { position = 0; skip = 0; list = [ 0..255 ] }
        ans.list
        |> List.take 2
        |> List.fold (fun a e -> e * a) 1

