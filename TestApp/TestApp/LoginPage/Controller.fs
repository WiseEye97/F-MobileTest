module LoginPage.Controller

open Types
open View
open Fabulous

let init() = 
    {
        login = ""
        loginState = Idle
    }, Cmd.none


let handleLogin = Util.tryDeser >> function  | Some r -> Shared.loginRawToNorm r | _ -> Shared.LoginResponse.Fail 

let update (msg : Msg) model = 
    match msg with
    | LoginChanged newLogin -> {model with Model.login = newLogin}, Cmd.none
    | Submit ->

        let http = Http.sendPost (handleLogin >> LoginInfo) "http://5e416a15.ngrok.io/login" { Shared.LoginReq.login = model.login }
        
        {model with loginState = Logging},Cmd.ofAsyncMsg http

    | _ -> model,Cmd.none

