# Feliz.UseListener [![Nuget](https://img.shields.io/nuget/v/Feliz.UseListener.svg?maxAge=0&colorB=brightgreen)](https://www.nuget.org/packages/Feliz.UseListener)

This library exposes React hooks for adding document event listeners while ensuring that
the lifecycle and dependcies are all correctly managed.

It has full typed access for all [standard browser events](https://developer.mozilla.org/en-US/docs/Web/Events). 
As well as a more generic `React.useListener.on : string * #Event -> unit` method for custom use-cases.

Has *no* bundle size increase.

A quick look:

```fs
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

Full documentation with live examples can be found [here](https://shmew.github.io/Feliz.UseListener/).
