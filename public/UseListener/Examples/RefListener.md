# Feliz.UseListener - Inline element state styling

The `React.useStyle.on*` hooks allow you to define a React ref
and use the state of it for styling components.

```fsharp:listener-reflistener
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

let inline actionStyle count = [
    style.backgroundColor (if count % 2 = 0 then color.red else color.blue)
    style.color color.white
]

let onClick = React.functionComponent(fun (input: {| count: int |}) ->
    let elemRef = React.useElementRef()

    let clicked = React.useStyle.onClick elemRef (actionStyle input.count)

    Html.div [
        prop.classes [ Bulma.Box ]
        prop.style [
            style.userSelect.none
            yield! clicked
        ]
        prop.ref elemRef
        prop.text "onClick"
    ])
    
let onFocus = React.functionComponent(fun (input: {| count: int |}) ->
    let elemRef = React.useElementRef()

    let focused = React.useStyle.onFocus elemRef (actionStyle input.count)

    Html.div [
        prop.classes [ Bulma.Box ]
        prop.style [
            style.userSelect.none
            yield! focused
        ]
        prop.children [
            label {| name = "onFocus" |}
            Html.input [
                prop.ref elemRef
            ]
        ]
    ])

let onHover = React.functionComponent(fun (input: {| count: int |}) ->
    let elemRef = React.useElementRef()

    let hovered = React.useStyle.onHover elemRef (actionStyle input.count)

    Html.div [
        prop.classes [ Bulma.Box ]
        prop.style [
            style.userSelect.none
            yield! hovered
        ]
        prop.ref elemRef
        prop.text "onHover"
    ])


let onValidInvalid = React.functionComponent(fun (input: {| count: int |}) ->
    let elemRef = React.useInputRef()
    let text,setText = React.useState ""

    let onInvalid = React.useStyle.onValid elemRef (actionStyle (input.count + 1))
    let onValid = React.useStyle.onValid elemRef (actionStyle input.count)

    Html.div [
        prop.classes [ Bulma.Box ]
        prop.style [
            style.userSelect.none

            yield! 
                match text with
                | "hello" -> onValid
                | _ -> onInvalid
        ]
        prop.children [
            label {| name = "onValidInvalid, enter \"hello\" for valid. You need to hit enter after typing for it to start checking validity" |}
            Html.input [
                prop.ref elemRef
                prop.onTextChange setText
                prop.defaultValue ""
            ]
        ]
    ])

let render = React.functionComponent(fun () ->
    let count,setCount = React.useState 0
    
    Html.div [
        prop.style [
            style.marginTop (length.em 1)
            maxWidth
        ]
        prop.classes [ Bulma.Box ]
        prop.children [
            onClick {| count = count |}
            onFocus {| count = count |}
            onHover {| count = count |}
            onValidInvalid {| count = count |}
            Html.button [
                prop.classes [ 
                    Bulma.Button
                    Bulma.HasBackgroundPrimary
                    Bulma.HasTextWhite 
                ]
                prop.text "Change State"
                prop.onClick <| fun _ -> setCount (count + 1)
            ]
        ]
    ])
```
