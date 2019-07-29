module LoginPage.View

open Types
open Fabulous.XamarinForms
open Fabulous
open Fabulous.XamarinForms
open Xamarin.Forms

let view (model: Model) dispatch =
    View.StackLayout(padding = 20.0, verticalOptions = LayoutOptions.Center,
        children = [
            View.Label(text = "Login Page")
            View.StackLayout(verticalOptions = LayoutOptions.Center,children = [
                View.Label(text = "Login : ", fontSize = 22., verticalTextAlignment = TextAlignment.Center)
                View.Entry(textChanged = (fun e -> e.NewTextValue |> LoginChanged |> dispatch))
            ])
            View.Button(text = "Submit",command = fun () -> Submit |> dispatch)
        ])
    

