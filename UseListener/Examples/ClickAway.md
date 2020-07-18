# Feliz.UseListener - Click Away

```fsharp:listener-clickaway
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

let render = React.functionComponent(fun () ->
    let clickedAway,setClickedAway = React.useState 0
    let elemRef = React.useElementRef()

    React.useListener.onClickAway(elemRef, fun _ -> setClickedAway(clickedAway + 1))

    Html.div [
        prop.classes [ Bulma.Box ]
        prop.ref elemRef
        prop.style [
            style.userSelect.none
            maxWidth
        ]

        prop.children [
            label {| name = "Click outside this element to trigger the click away" |}
            Html.text clickedAway
        ]
    ])
```
