module Day15

    let generateNextValue preValue factor =
        (preValue * factor) % 2147483647L

    let generatePair (a, b) =
        let nextA = generateNextValue a 16807L
        let nextB = generateNextValue b 48271L
        (nextA, nextB)

    let compare (a: int64, b: int64) =
        let aBits = a &&& 0xffffL 
        let bBits = b &&& 0xffffL
        aBits = bBits

    let rec findPairs iter count (a, b) =
        if iter = 40000000 then count
        else
            let (nextA, nextB) = generatePair(a, b)
            if compare (nextA, nextB) then findPairs (iter+1) (count+1) (nextA, nextB)
            else findPairs (iter+1) (count) (nextA, nextB)
     
    let answer =
        findPairs 1 0 (289L, 629L)