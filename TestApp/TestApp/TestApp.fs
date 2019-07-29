// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.
namespace TestApp

open System.Diagnostics
open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms
open System.Net
open Types

module App = 
    
    let init () = 
        let loginModel , loginCmd = LoginPage.Controller.init()
        {subModel = LoginPage loginModel;isLogged = false}, Cmd.map (LoginMsg >> SubMsg) loginCmd

    let update (msg : Msg) model = 
        
        let (|ImportantMsg|_|) = 
            function
            | {subModel = LoginPage _},(SubMsg (LoginMsg (LoginPage.Types.Msg.LoginInfo Shared.LoginResponse.Success))) ->
                ({model with isLogged = true},Cmd.none)
                |> Some
            | _ -> None

        let (|SubMsg|_|) = 
            function
            | {subModel = s} , SubMsg msg ->

                let liftToSub (mapper : 'a -> SubModel) (cmd : 'b -> SubMsg) model msg = (mapper model , Cmd.map cmd msg) |> Some 

                match s,msg with
                | LoginPage mo,LoginMsg ms -> LoginPage.Controller.update ms mo ||> liftToSub LoginPage LoginMsg
                | MainPage mo, MainMsg ms -> MainPage.Controller.update ms mo ||> liftToSub MainPage MainMsg
                | _ -> None
            
        match model,msg with
        | ImportantMsg r -> r
        | SubMsg (subM,subMs) -> {model with subModel = subM}, Cmd.map SubMsg subMs
        | _ -> model,Cmd.none
    
    
   
    let renderModelView dispatch model =

        let liftSubMsg = SubMsg >> dispatch

        match model with
        | LoginPage m -> LoginPage.View.view m (LoginMsg >> liftSubMsg)
        | MainPage m -> MainPage.View.view m (MainMsg >> liftSubMsg)

    let view (model: Model) dispatch =
            View.ContentPage(
              content = renderModelView dispatch model.subModel)

    // Note, this declaration is needed if you enable LiveUpdate
    let program = Program.mkProgram init update view

type App () as app = 
    inherit Application ()

    let runner = 
        App.program
#if DEBUG
        |> Program.withConsoleTrace
#endif
        |> XamarinFormsProgram.run app

#if DEBUG
    // Uncomment this line to enable live update in debug mode. 
    // See https://fsprojects.github.io/Fabulous/tools.html for further  instructions.
    //
    //do runner.EnableLiveUpdate()
#endif    

    // Uncomment this code to save the application state to app.Properties using Newtonsoft.Json
    // See https://fsprojects.github.io/Fabulous/models.html for further  instructions.
#if APPSAVE
    let modelId = "model"
    override __.OnSleep() = 

        let json = Newtonsoft.Json.JsonConvert.SerializeObject(runner.CurrentModel)
        Console.WriteLine("OnSleep: saving model into app.Properties, json = {0}", json)

        app.Properties.[modelId] <- json

    override __.OnResume() = 
        Console.WriteLine "OnResume: checking for model in app.Properties"
        try 
            match app.Properties.TryGetValue modelId with
            | true, (:? string as json) -> 

                Console.WriteLine("OnResume: restoring model from app.Properties, json = {0}", json)
                let model = Newtonsoft.Json.JsonConvert.DeserializeObject<App.Model>(json)

                Console.WriteLine("OnResume: restoring model from app.Properties, model = {0}", (sprintf "%0A" model))
                runner.SetCurrentModel (model, Cmd.none)

            | _ -> ()
        with ex -> 
            App.program.onError("Error while restoring model found in app.Properties", ex)

    override this.OnStart() = 
        Console.WriteLine "OnStart: using same logic as OnResume()"
        this.OnResume()
#endif


