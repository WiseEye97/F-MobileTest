module Util

open Fabulous.XamarinForms
open Xamarin.Forms
open Newtonsoft.Json

let renderEntry text onChanged = 
    View.StackLayout(verticalOptions = LayoutOptions.Center,children = [
        View.Label(text = text, fontSize = 22., verticalTextAlignment = TextAlignment.Center)
        View.Entry(textChanged = onChanged)
    ])

let tryDeser<'a> (str : string) = 
    try 
        JsonConvert.DeserializeObject(str, typeof<'a>) :?> 'a
        |> Some
    with
        | _ -> None