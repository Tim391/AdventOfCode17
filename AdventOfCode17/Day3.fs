module Day3

    let private input = 289326

    let right =
        (1, 0)

    let left = 
        (-1, 0)

    let up = 
        (0, 1)

    let down = 
        (0, -1)

    let previousDir(dir) =
        match dir with
        | (1, 0) -> down //right to down
        | (0, 1) -> right //up to right
        | (-1, 0) -> up //left to up
        | (0, -1) -> left //down to left

    let nextDir(dir)  = //why can't these match on right, left etc?
        match dir with
        | (1, 0) -> up //right to up
        | (0, 1) -> left //up to left
        | (-1, 0) -> down //left to down
        | (0, -1) -> right //down to right

    let rec move(dir, num, loc, arr: (int *bool)[,]) =
        if num = input then loc
        else
            let dx, dy = dir
            let x, y = loc
            let square = arr.[x+dx, y+dy]
            match square with 
            | (_, true) -> move(previousDir(dir), num, loc, arr)
            | (_, false) -> 
                arr.[x+dx, y+dy] <- (num+1, true)
                move(nextDir(dir), num+1, (x+dx, y+dy), arr)
    
    let squareValue(x, y, arr: (int *bool)[,]) =
        try
            let v, _ = arr.[x, y]
            v
        with 
        | :? System.IndexOutOfRangeException -> 0

    let sumAdjacent x y arr =
        let right = squareValue(x+1, y, arr)
        let left = squareValue(x-1, y, arr)
        let up = squareValue(x, y+1, arr)
        let down = squareValue(x, y-1, arr)
        let upDiagLeft = squareValue(x-1, y+1, arr)
        let upDiagRight = squareValue(x+1, y+1, arr)
        let downDiagLeft = squareValue(x-1, y-1, arr)
        let downDiagRight = squareValue(x+1, y-1, arr)
        right + left + up + down + upDiagLeft + upDiagRight + downDiagLeft + downDiagRight
        

    let rec move2(dir, num, loc, arr: (int *bool)[,]) =
        if num > input then num
        else
            let dx, dy = dir
            let x, y = loc
            let square = arr.[x+dx, y+dy]
            match square with 
            | (_, true) -> move2(previousDir(dir), num, loc, arr)
            | (_, false) -> 
                let sum = sumAdjacent (x+dx) (y+dy) arr
                arr.[x+dx, y+dy] <- (sum, true)
                move2(nextDir(dir), sum, (x+dx, y+dy), arr)

    let createArray = 
        let s = input |> float |> sqrt |> ceil |> int
        let sideLength = 
            if s % 2 = 0 then s + 1
            else s
        let arr = Array2D.init sideLength sideLength (fun _ _ -> (0, false))
        let midLength = sideLength / 2 |> float |> floor |> int
        arr.[midLength, midLength] <- (1, true)
        (arr, midLength)

    let answer() =
        let arr, mid = createArray
        let loc = move(right, 1, (mid, mid), arr)
        let fx, fy = loc
        let dx = (mid - fx) |> abs
        let dy = (mid - fy) |> abs
        dx + dy

    let answer2 = 
        let arr, mid = createArray
        move2(right, 1, (mid, mid), arr)
        

