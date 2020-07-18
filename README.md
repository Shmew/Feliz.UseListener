# Feliz.UseListener [![Nuget](https://img.shields.io/nuget/v/Feliz.UseListener.svg?maxAge=0&colorB=brightgreen)](https://www.nuget.org/packages/Feliz.UseListener)

This library exposes React hooks for adding event listeners while ensuring that
the lifecycle and dependcies are all correctly managed.

It has full typed access for all [standard browser events](https://developer.mozilla.org/en-US/docs/Web/Events). 
As well as a more generic `React.useListener.on : string * #Event -> unit` method for custom use-cases.

Has *no* bundle size increase.

Accessible via:
 * `React.useListener` - document
 * `React.useElementListener` - specific element ref
 * `React.useWindowListener` - window

 ### Typical Event Listeners

```fsharp
open Feliz
open Feliz.UseListener

let textInput = React.functionComponent(fun (input: {| callback: string -> unit |}) ->
    let text,setText = React.useState ""

    React.useListener.onKeyDown(fun ev ->
        if ev.key = "Enter" then
            input.callback text
    )

    Html.div [
        Html.input [
            prop.type'.text
            prop.onTextChange setText
        ]
    ])
```

### Inline element state styling

There is also a set of hooks to make applying styles for things such as when an element
is being hovered via the `React.useStyle.on*` hooks. 

```fsharp
let onFocus = React.functionComponent(fun () ->
    let elemRef = React.useInputRef()

    let focused = React.useStyle.onFocus elemRef [
        style.backgroundColor color.blue
        style.color color.white
    ]

    Html.div [
        prop.style focused
        prop.children [
            Html.input [
                prop.ref elemRef
            ]
        ]
    ])
```

Full documentation with live examples can be found [here](https://shmew.github.io/Feliz.UseListener/).
