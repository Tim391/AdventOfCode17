module Day8

    open System
    open System.IO

    let private input =
        let path = __SOURCE_DIRECTORY__ + "\Day8Input.txt"
        path 
        |> File.ReadAllLines
        |> Array.map (fun l -> (l.Split(null)))

    type Register = {
        name: string
        value: int
    }

    let registers : (string * int) [] = [||]

    let apply orig cmd value =
        match cmd with
        | "inc" -> orig + value
        | "dec" -> orig - value
        | _ -> orig

    let createInstruction(line: string[]) =
        let reg = line.[0]
        let cmd = line.[1]
        let value = line.[2] |> int
        let condReg = line.[4]
        let cond = line.[5]
        let condVal = line.[6] |> int

        let condRegValue = 
            registers
            |> Array.tryFind (fun (r, _) -> r = condReg)
        
        let modifyReg =
            match cond with
            | ">" -> match condRegValue with | Some (r, v) -> v > condVal | None -> false
            | "<" -> match condRegValue with | Some (r, v) -> v < condVal | None -> false
            | ">=" -> match condRegValue with | Some (r, v) -> v >= condVal | None -> false
            | "==" -> match condRegValue with | Some (r, v) -> v = condVal | None -> false
            | "<=" -> match condRegValue with | Some (r, v) -> v <= condVal | None -> false
            | "!=" -> match condRegValue with | Some (r, v) -> v <> condVal | None -> false
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
            //| None -> (reg, apply 0 cmd value)::registers
        else registers


    let answer =
        let ins = 
            input
            |> Array.map (createInstruction)
        ins