module Http

open System.Net
open Newtonsoft.Json
open System.Text
open System

let sendPost callback (url:string) (content : obj)  = 
    let req = WebRequest.Create(url)
    let serialized = JsonConvert.SerializeObject(content) |> Encoding.ASCII.GetBytes
    req.Method <- "POST"
    req.ContentType <- "application/json"

    async {
        use! reqS = req.GetRequestStreamAsync() |> Async.AwaitTask
        do! reqS.WriteAsync(serialized,0,serialized.Length) |> Async.AwaitTask
        use! resp = req.GetResponseAsync() |> Async.AwaitTask
        use stream = resp.GetResponseStream() 
        use reader = new IO.StreamReader(stream) 
        let! txt = reader.ReadToEndAsync() |> Async.AwaitTask
        return callback txt
    }
