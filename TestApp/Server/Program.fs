// Learn more about F# at http://fsharp.org

open System
open Suave.Web
open Suave.Successful
open Suave.Filters
open Suave.Operators
open Suave
open Newtonsoft.Json
open Shared


let tryDeser<'a> (str : string) = 
    try 
        JsonConvert.DeserializeObject(str, typeof<'a>) :?> 'a
        |> Some
    with
        | _ -> None


let handleLogin (req : HttpRequest)  = 
    req.rawForm
    |> System.Text.Encoding.UTF8.GetString
    |> tryDeser<'LoginReq>
    |> function | Some _ -> "nice" | _ -> "fail"
    |> OK


[<EntryPoint>]
let main argv =

    let router = choose [
        Suave.Filters.POST >=> choose [
            path "/login" >=> request handleLogin
        ]
    ]

    startWebServer defaultConfig router
    0 // return an integer exit code
