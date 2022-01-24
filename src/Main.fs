module Main

open Browser.Dom
open Fable.Core.JsInterop
open Feliz

importSideEffects "./styles/global.scss"

ReactDOM.render (App.Make, document.getElementById "feliz-app")
