module MainPage.Controller

open MainPage.Types
open Fabulous


let update (msg : Msg) (model : Model) =
    match msg with
    | _ -> model , Cmd.none
    
