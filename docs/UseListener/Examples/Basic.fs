[<RequireQualifiedAccess>]
module Samples.Basic

open Css
open Feliz
open Feliz.UseListener
open Zanaptak.TypedCssClasses

let maxWidth = style.maxWidth (length.em 30)

let label = React.functionComponent(fun (input: {| name: string |}) ->
    Html.div [
        prop.style [
            style.paddingBottom (length.em 1)
            style.paddingTop (length.em 1)
        ]
        prop.text input.name
    ])

let textInput = React.functionComponent(fun (input: {| callback: string -> unit |}) ->
    let text,setText = React.useState ""

    React.useListener.onKeyDown(fun ev ->
        if ev.key = "Enter" then
            input.callback text
    )

    Html.div [
        Html.input [
            prop.style [ maxWidth ]
            prop.classes [ Bulma.Input ]
            prop.type'.text
            prop.onTextChange setText
        ]
    ])

let render = React.functionComponent(fun () ->
    let text,setText = React.useState "None"

    let setText = React.useCallback(setText)

    Html.div [
        prop.style [
            style.marginTop (length.em 1)
            maxWidth
        ]
        prop.classes [ Bulma.Box ]
        prop.children [
            label {| name = "Listener output:" |}
            Html.text text
            label {| name = "Input:" |}
            textInput {| callback = setText |}
        ]
    ])
