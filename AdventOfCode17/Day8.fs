module Day8

    open System
    open System.IO

    let private input =
        let path = __SOURCE_DIRECTORY__ + "\Day8Input.txt"
        path 
        |> File.ReadAllLines
        |> Array.map (fun l -> (l.Split(null)))

    let apply orig cmd value =
        match cmd with
        | "inc" -> orig + value
        | "dec" -> orig - value
        | _ -> orig


    let createInstruction(registers: (string * int)[], line: string[]) =
        let reg = line.[0]
        let cmd = line.[1]
        let value = line.[2] |> int
        let condReg = line.[4]
        let cond = line.[5]
        let condVal = line.[6] |> int

        let condRegOption = 
            registers
            |> Array.tryFind (fun (r, _) -> r = condReg)
            
        let condRegValue = 
            match condRegOption with
            | Some (_, v) -> v
            | None -> 0
        
        let modifyReg =
            match cond with
            | ">" -> condRegValue > condVal 
            | "<" -> condRegValue < condVal
            | ">=" -> condRegValue >= condVal
            | "==" -> condRegValue = condVal
            | "<=" -> condRegValue <= condVal
            | "!=" -> condRegValue <> condVal
            | _ -> false      

        if modifyReg then 
            let regToModify = 
                registers
                |> Array.tryFindIndex (fun (r, _) -> r = reg)
            match regToModify with
            | Some v -> 
                let (er, ev) = registers.[v]
                registers.[v] <- (er, apply ev cmd value)
                registers
            | None -> 
                Array.append registers [|(reg, apply 0 cmd value)|]
        else registers


    let answer =
        let max = 
            input
            |> Array.fold (fun acc l -> createInstruction(acc, l)) [||]
            |> Array.maxBy (fun (r, v) -> v)
        max
