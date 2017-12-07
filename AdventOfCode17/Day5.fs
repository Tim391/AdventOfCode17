module Day5

    open System
    open System.IO

    let private input =
        let path = __SOURCE_DIRECTORY__ + "\Day5Input.txt"
        path 
        |> File.ReadAllLines
        |> Array.map (Int32.Parse)

    let rec private jump(count, loc, arr: int []) =
        if loc >= arr.Length then count+1
        else
            let cmd = arr.[loc]
            let next = loc + cmd
            arr.[loc] <- cmd + 1
            jump(count+1, next, arr)

    let rec private jumpOffset(count, loc, arr: int []) =
        if loc >= arr.Length then count+1
        else
            let cmd = arr.[loc]
            let next = loc + cmd
            if cmd >=3 then arr.[loc] <- cmd - 1
            else arr.[loc] <- cmd + 1
            jumpOffset(count+1, next, arr)

    let answer() =
        let ans = jump(0, input.[0], input)
        ans

    let answer2 =
        let ans = jumpOffset(0, input.[0], input)
        ans
