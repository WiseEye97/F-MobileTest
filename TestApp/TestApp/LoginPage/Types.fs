module LoginPage.Types

type LoginState =
    | Idle
    | Logging
    | LoginFailed of string

type Model = {
    login : string
    loginState : LoginState
}

type Msg = 
    | LoginChanged of string
    | LoginInfo of Shared.LoginResponse
    | Logged
    | NLogged
    | Submit

