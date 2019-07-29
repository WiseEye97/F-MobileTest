module MainPage.View

open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms
open Types

let view (model: Model) dispatch = 
    View.StackLayout(padding = 20.0, verticalOptions = LayoutOptions.Center,
        children = [
            Util.renderEntry "Pomiar:" (fun e -> e.NewTextValue |> SugarInputChanged |> dispatch)
        ])

