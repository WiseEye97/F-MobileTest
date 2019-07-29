module MainPage.Types


type Msg = 
    | Nothing
    | SugarInputChanged of string

type SugarValue = private SugarValue of int

type Model = {
    sugarInp : SugarValue option
}
    with
        static member Init() = {sugarInp = None}




