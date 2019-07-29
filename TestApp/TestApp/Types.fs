module Types

type SubModel = 
    | MainPage of MainPage.Types.Model
    | LoginPage of LoginPage.Types.Model


type Model = 
    {
      subModel : SubModel  
      isLogged : bool
    }

type SubMsg = 
    | MainMsg of MainPage.Types.Msg
    | LoginMsg of LoginPage.Types.Msg


type Msg =
     | SubMsg of SubMsg
     