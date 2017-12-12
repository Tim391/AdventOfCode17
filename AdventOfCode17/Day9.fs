module Day9

    open System.IO

    let private input = 
        let path = __SOURCE_DIRECTORY__ + "\Day9Input.txt"
        let line = File.ReadAllText(path)
        line.ToCharArray()

    type GarbageState = NotGarbage | Garbage | Ignored
    type State = { level: int; state: GarbageState; score: int; garbage: int }

    let cmd current nextChar =
        match (current.state, nextChar) with
        | (Garbage, '!') -> { current with state = Ignored }
        | (Garbage, '>') -> { current with state = NotGarbage } 
        | (Garbage, _)   -> { current with garbage = current.garbage + 1 }
        | (Ignored, _) | (NotGarbage, '<') -> { current with state = Garbage }
        | (NotGarbage, '{') -> { current with level = current.level + 1 }
        | (NotGarbage, '}') -> { current with level = current.level - 1; score = current.score + current.level }
        | _ -> current;

    let answer =
        let ans = 
            input
            |> Seq.fold cmd { level=0; state=NotGarbage; score=0; garbage=0 }
        (ans.score, ans.garbage)
        
