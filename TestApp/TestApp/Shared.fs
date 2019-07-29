module Shared

type LoginReq = {
    login : string
}


type LoginResponseRaw = {
    status : string
}

type LoginResponse = 
    | Success
    | Fail

let loginRawToNorm = 
    function
    | {status = "success"} -> Success
    | _ -> Fail

