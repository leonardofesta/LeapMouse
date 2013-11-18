// Learn more about F# at http://fsharp.net
// See the 'F# Tutorial' project for more help.

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    System.Console.WriteLine("Bla")
    System.Threading.Thread.Sleep(1000)
    0 // return an integer exit code
