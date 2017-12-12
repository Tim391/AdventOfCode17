module Day10

    open System.IO

    let private input = 
        let path = __SOURCE_DIRECTORY__ + "\Day10Input.txt"
        let str = path |> File.ReadAllText
        str.Split(',')
        |> Array.map int
        |> Array.toList


    let answer = 
        let initialList = [ 0..255 ]
        input

