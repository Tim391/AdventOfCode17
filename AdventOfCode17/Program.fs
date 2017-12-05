open System

// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

[<EntryPoint>]
let main argv = 
    let answer = Day4.answer2
    printfn "%A" answer
    Console.ReadLine() |> ignore
    0 // return an integer exit code
