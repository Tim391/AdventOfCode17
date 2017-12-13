module Day11

    open System.IO
    open System

    let private input = 
        let path = __SOURCE_DIRECTORY__ + "\Day11Input.txt"
        let str = path |> File.ReadAllText
        str.Split(',')
        |> Array.toList

    let nextCoord coord dir =
        let (x, y, z) = coord
        match dir with
        | "n" -> (x, y+1, z-1)
        | "nw" -> (x-1, y+1, z)
        | "ne" -> (x+1, y, z-1)
        | "s" -> (x, y-1, z+1)
        | "sw" -> (x-1, y, z+1)
        | "se" -> (x+1, y-1, z)

    let rec furthest (list: string list) coord distance =
        match list with
        | [] -> distance
        | x::xt -> 
            let (x, y , z) = nextCoord coord x
            let dis = (Math.Abs x + Math.Abs y + Math.Abs z) / 2
            furthest xt (x, y, z) (if dis > distance then dis else distance)

    let answer =
        let (x, y, z) = 
            input
            |> List.fold nextCoord (0, 0, 0)
        (Math.Abs x + Math.Abs y + Math.Abs z) / 2

    let answer2 =
        furthest input (0, 0, 0) 0


