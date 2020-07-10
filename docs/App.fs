module App

open Browser.Dom
open Elmish
open Feliz
open Feliz.Markdown
open Fable.SimpleHttp
open Feliz.Router
open Feliz.UseElmish
open Fable.Core.JsInterop
open Zanaptak.TypedCssClasses

type Bulma = CssClasses<"https://cdnjs.cloudflare.com/ajax/libs/bulma/0.7.5/css/bulma.min.css", Naming.PascalCase>
type FA = CssClasses<"https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css", Naming.PascalCase>

type Highlight =
    static member inline highlight (properties: IReactProperty list) =
        Interop.reactApi.createElement(importDefault "react-highlight", createObj !!properties)

type State = 
    { CurrentPath : string list
      CurrentTab: string list }

let init () =
    let path = 
        match document.URL.Split('#') with
        | [| _ |] -> []
        | [| _; path |] -> path.Split('/') |> List.ofArray |> List.tail
        | _ -> []
    { CurrentPath = path
      CurrentTab = path }, Cmd.none

type Msg =
    | TabToggled of string list
    | UrlChanged of string list

let update msg state =
    match msg with
    | UrlChanged segments -> 
        { state with CurrentPath = segments }, 
        match state.CurrentTab with
        | [ ] when segments.Length > 2 -> 
            segments
            |> TabToggled
            |> Cmd.ofMsg
        | _ -> Cmd.none
    | TabToggled tabs ->
        match tabs with
        | [ ] -> { state with CurrentTab = [ ] }, Cmd.none
        | _ -> { state with CurrentTab = tabs }, Cmd.none

let centeredSpinner =
    Html.div [
        prop.style [
            style.textAlign.center
            style.marginLeft length.auto
            style.marginRight length.auto
            style.marginTop 50
        ]
        prop.children [
            Html.li [
                prop.className [
                    FA.Fa
                    FA.FaRefresh
                    FA.FaSpin
                    FA.Fa3X
                ]
            ]
        ]
    ]

let samples = 
    [ "listener-basic", Samples.Basic.render()
      "listener-reflistener", Samples.RefListener.render() ]

let githubPath (rawPath: string) =
    let parts = rawPath.Split('/')
    if parts.Length > 5
    then sprintf "http://www.github.com/%s/%s" parts.[3] parts.[4]
    else rawPath

/// Renders a code block from markdown using react-highlight.
/// Injects sample React components when the code block has language of the format <language>:<sample-name>
let codeBlockRenderer' = React.functionComponent(fun (input: {| codeProps: Markdown.ICodeProperties |}) ->
    if input.codeProps.language <> null && input.codeProps.language.Contains ":" then
        let languageParts = input.codeProps.language.Split(':')
        let sampleName = languageParts.[1]
        let sampleApp =
            samples
            |> List.tryFind (fun (name, _) -> name = sampleName)
            |> Option.map snd
            |> Option.defaultValue (Html.h1 [
                prop.style [ style.color.crimson ];
                prop.text (sprintf "Could not find sample app '%s'" sampleName)
            ])
        Html.div [
            sampleApp
            Highlight.highlight [
                prop.className "fsharp"
                prop.text(input.codeProps.value)
            ]
        ]
    else
        Highlight.highlight [
            prop.className "fsharp"
            prop.text(input.codeProps.value)
        ])

let codeBlockRenderer (codeProps: Markdown.ICodeProperties) = codeBlockRenderer' {| codeProps = codeProps |}

let renderMarkdown = React.functionComponent(fun (input: {| path: string; content: string |}) ->
    Html.div [
        prop.className [ Bulma.Content; "scrollbar" ]
        prop.style [ 
            style.width (length.percent 100)
            style.padding (0,20)
        ]
        prop.children [
            if input.path.StartsWith "https://raw.githubusercontent.com" then
                Html.h2 [
                    Html.i [ prop.className [ FA.Fa; FA.FaGithub ] ]
                    Html.anchor [
                        prop.style [ style.marginLeft 10; style.color.lightGreen ]
                        prop.href (githubPath input.path)
                        prop.text "View on Github"
                    ]
                ]

            Markdown.markdown [
                markdown.source input.content
                markdown.escapeHtml false
                markdown.renderers [
                    markdown.renderers.code codeBlockRenderer
                ]
            ]
        ]
    ])

module MarkdownLoader =
    type State =
        | Initial
        | Loading
        | Errored of string
        | LoadedMarkdown of content: string

    type Msg =
        | StartLoading of path: string list
        | Loaded of Result<string, int * string>

    let init (path: string list) = Initial, Cmd.ofMsg (StartLoading path)

    let resolvePath = function
    | [ one: string ] when one.StartsWith "http" -> one
    | segments -> String.concat "/" segments

    let update (msg: Msg) (state: State) =
        match msg with
        | StartLoading path ->
            let loadMarkdownAsync() = async {
                let! (statusCode, responseText) = Http.get (resolvePath path)
                if statusCode = 200
                then return Loaded (Ok responseText)
                else return Loaded (Error (statusCode, responseText))
            }

            Loading, Cmd.OfAsync.perform loadMarkdownAsync () id

        | Loaded (Ok content) ->
            State.LoadedMarkdown content, Cmd.none

        | Loaded (Error (status, _)) ->
            State.LoadedMarkdown (sprintf "Status %d: could not load markdown" status), Cmd.none

    let render = React.functionComponent(fun (input: {| path: string list |}) ->
        let state,dispatch = React.useElmish(init(input.path), update, [| input.path :> obj |])

        match state with
        | Initial -> Html.none
        | Loading -> centeredSpinner
        | LoadedMarkdown content -> renderMarkdown {| path = (resolvePath input.path); content = content |}
        | Errored error ->
            Html.h1 [
                prop.style [ style.color.crimson ]
                prop.text error
            ])

    let inline load (path: string list) = render {| path = path |}

// A collapsable nested menu for the sidebar
// keeps internal state on whether the items should be visible or not based on the collapsed state
let nestedMenuList' = React.functionComponent(fun (input: {| state: State; name: string; basePath: string list; elems: (string list -> Fable.React.ReactElement) list; dispatch: Msg -> unit |}) ->
    let collapsed = 
        match input.state.CurrentTab with
        | [ ] -> false
        | _ -> 
            input.basePath 
            |> List.indexed 
            |> List.forall (fun (i, segment) -> 
                List.tryItem i input.state.CurrentTab 
                |> Option.map ((=) segment) 
                |> Option.defaultValue false) 

    Html.li [
        Html.anchor [
            prop.className Bulma.IsUnselectable
            prop.onClick <| fun _ -> 
                match collapsed with
                | true -> input.dispatch <| TabToggled (input.basePath |> List.rev |> List.tail |> List.rev)
                | false -> input.dispatch <| TabToggled input.basePath
            prop.children [
                Html.i [
                    prop.style [ style.marginRight 10 ]
                    prop.className [
                        FA.Fa
                        if not collapsed then FA.FaAngleDown else FA.FaAngleUp
                    ]
                ]
                Html.span input.name
            ]
        ]

        Html.ul [
            prop.className Bulma.MenuList
            prop.style [ 
                if not collapsed then yield! [ style.display.none ] 
            ]
            prop.children (input.elems |> List.map (fun f -> f input.basePath))
        ]
    ])

// top level label
let menuLabel' = React.functionComponent (fun (input: {| content: string |}) ->
    Html.p [
        prop.className [ Bulma.MenuLabel; Bulma.IsUnselectable ]
        prop.text input.content
    ])

// top level menu
let menuList' = React.functionComponent(fun (input: {| items: Fable.React.ReactElement list |}) ->
    Html.ul [
        prop.className Bulma.MenuList
        prop.style [ style.width (length.percent 95) ]
        prop.children input.items
    ])

let menuItem' = React.functionComponent(fun (input: {| currentPath: string list; name: string; path: string list |}) ->
    Html.li [
        Html.anchor [
            prop.className [
                if input.currentPath = input.path then Bulma.IsActive
                if input.currentPath = input.path then Bulma.HasBackgroundPrimary
            ]
            prop.text input.name
            prop.href (sprintf "#/%s" (String.concat "/" input.path))
        ]
    ])

let menuLabel (content: string) =
    menuLabel' {| content = content |}

let menuList (items: Fable.React.ReactElement list) =
    menuList' {| items = items |}

let allItems = React.functionComponent(fun (input: {| state: State; dispatch: Msg -> unit |} ) ->
    let dispatch = React.useCallback(input.dispatch, [||])

    let menuItem (name: string) (basePath: string list) =
        menuItem' 
            {| currentPath = input.state.CurrentPath
               name = name
               path = basePath |}
    
    let nestedMenuItem (name: string) (extendedPath: string list) (basePath: string list) =
        let path = basePath @ extendedPath
        menuItem' 
            {| currentPath = input.state.CurrentPath
               name = name
               path = path |}

    let nestedMenuList (name: string) (basePath: string list) (items: (string list -> Fable.React.ReactElement) list) =
        nestedMenuList' 
            {| state = input.state
               name = name
               basePath = basePath
               elems = items
               dispatch = dispatch |}
    
    let subNestedMenuList (name: string) (basePath: string list) (items: (string list -> Fable.React.ReactElement) list) (addedBasePath: string list) =
        nestedMenuList' 
            {| state = input.state
               name = name
               basePath = (addedBasePath @ basePath)
               elems = items
               dispatch = dispatch |}

    Html.div [
        prop.className "scrollbar"
        prop.children [
            menuList [
                menuItem "Overview" [ ]
                menuItem "Installation" [ Urls.UseListener; Urls.Installation ]
                menuItem "Release Notes" [ Urls.UseListener; Urls.ReleaseNotes ]
                menuItem "Contributing" [ Urls.UseListener; Urls.Contributing ]
                menuLabel "Examples"
                menuItem "Basic" [ Urls.UseListener; Urls.Examples; Urls.Basic ]
                menuItem "Inline styling" [ Urls.UseListener; Urls.Examples; Urls.RefListener ]
            ]
        ]
    ])

let sidebar = React.functionComponent(fun (input: {| state: State; dispatch: Msg -> unit |}) ->
    let dispatch = React.useCallback(input.dispatch, [||])

    // the actual nav bar
    Html.aside [
        prop.className Bulma.Menu
        prop.style [
            style.width (length.perc 100)
        ]
        prop.children [ 
            menuLabel "Feliz.UseListener"
            allItems {| state = input.state; dispatch = dispatch |} 
        ]
    ])

let readme = sprintf "https://raw.githubusercontent.com/%s/%s/master/README.md"
let contributing = sprintf "https://raw.githubusercontent.com/Zaid-Ajaj/Feliz/master/public/Feliz/Contributing.md"

let (|PathPrefix|) (segments: string list) (path: string list) =
    if path.Length > segments.Length then
        match List.splitAt segments.Length path with
        | start,end' when start = segments -> Some end'
        | _ -> None
    else None

let content = React.functionComponent(fun (input: {| state: State; dispatch: Msg -> unit |}) ->
    match input.state.CurrentPath with
    | [ Urls.UseListener; Urls.Contributing ] -> MarkdownLoader.load [ contributing ]
    | PathPrefix [ Urls.UseListener ] (Some res) ->
        match res with
        | [ Urls.Overview; ] -> [ "README.md" ]
        | [ Urls.Installation ] -> [ "Installation.md" ]
        | [ Urls.ReleaseNotes ] -> [ "RELEASE_NOTES.md" ]
        | PathPrefix [ Urls.Examples ] (Some res) ->
            match res with
            | [ Urls.Basic ] -> [ "Basic.md" ]
            | [ Urls.RefListener ] -> [ "RefListener.md" ]
            | _ -> []
            |> fun path -> [ "Examples" ] @ path
        | _ -> []
        |> fun path -> [ Urls.UseListener ] @ path |> MarkdownLoader.load
    | _ -> MarkdownLoader.load [ "UseListener"; "README.md" ])

let main = React.functionComponent(fun (input: {| state: State; dispatch: Msg -> unit |}) ->
    let dispatch = React.useCallback(input.dispatch, [||])
    
    Html.div [
        prop.className [ Bulma.Tile; Bulma.IsAncestor ]
        prop.children [
            Html.div [
                prop.className [ Bulma.Tile; Bulma.Is2 ]
                prop.children [ sidebar {| state = input.state; dispatch = dispatch |} ]
            ]

            Html.div [
                prop.className Bulma.Tile
                prop.style [ style.paddingTop 30 ]
                prop.children [ content {| state = input.state; dispatch = dispatch |} ]
            ]
        ]
    ])

let render = React.functionComponent(fun () ->
    let state,dispatch = React.useElmish(init(), update, [||])
    
    let application =
        Html.div [
            prop.style [ 
                style.padding 30
            ]
            prop.children [ main {| state = state; dispatch = dispatch |} ]
        ]

    Router.router [
        Router.onUrlChanged (UrlChanged >> dispatch)
        Router.application application
    ])

ReactDOM.render(render(), document.getElementById "root")