module Router

open Browser.Dom
open Browser.Types
open Fable.Core
open Fable.Core.JsInterop
open Feliz
open Feliz.UseListener
open System

type IUrlSearchParamters =
    abstract entries : unit -> seq<string array>

/// Determines whether the router will push a new entry to the history of modify the current one.
[<RequireQualifiedAccess>]
type HistoryMode =
    /// A new history will be added to the entries such that if the user clicks the back button,
    /// the previous page will be shown, this is the default bahavior of the router.
    | PushState = 1
    /// Only modifies the current history entry and does not add a new one to the history stack. Clicking the back button will *not* have the effect of retuning to the previous page.
    | ReplaceState = 2

/// Determines whether the router will use path or hash based routes
[<RequireQualifiedAccess>]
type RouteMode =
    | Hash = 1
    | Path = 2

[<RequireQualifiedAccess; System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
module Router =
    [<RequireQualifiedAccess>]
    module String =
        let (|Prefix|) (prefix: string) (str: string) =
            if str.StartsWith prefix then Some str
            else None

        let (|Suffix|) (suffix: string) (str: string) =
            if str.EndsWith suffix then Some str
            else None

        let inline split (sep: char) (str: string) =
            str.Split(sep)

    let [<Literal>] customNavigationEvent = "CUSTOM_NAVIGATION_EVENT"
    let inline hashPrefix str = "#/" + str
    let inline combine xs = String.concat "/" xs

    type RouterProps =
        abstract onUrlChanged: (string list -> unit) option
        abstract application: ReactElement option
        abstract hashMode: RouteMode option

    [<Emit("encodeURIComponent($0)")>]
    let encodeURIComponent (value: string) : string = jsNative
    [<Emit("decodeURIComponent($0)")>]
    let decodeURIComponent (value: string) : string = jsNative

    let encodeQueryString queryStringPairs =
        queryStringPairs
        |> List.map (fun (key, value) ->
            String.concat "=" [ encodeURIComponent key; encodeURIComponent value ])
        |> String.concat "&"
        |> function
            | "" -> ""
            | pairs -> "?" + pairs

    let encodeQueryStringInts queryStringIntPairs =
        queryStringIntPairs
        |> List.map (fun (key, value: int) ->
            String.concat "=" [ encodeURIComponent key; unbox<string> value ])
        |> String.concat "&"
        |> function
            | "" -> ""
            | pairs -> "?" + pairs

    let private normalizeRoute routeMode =
        if routeMode = RouteMode.Hash then
            function
            | String.Prefix "/" (Some path) -> "#" + path
            | String.Prefix "#/" (Some path) -> path
            | String.Prefix "#" (Some path) -> "#/" + path.Substring(1, path.Length - 1)
            | path -> "#/" + path
        else
            function
            | String.Prefix "/" (Some path) -> path
            | path -> "/" + path

    let encodeParts xs routeMode =
        xs
        |> List.map (fun (part: string) ->
            if part.Contains "?" || part.StartsWith "#" || part.StartsWith "/" then part
            else encodeURIComponent part)
        |> combine
        |> normalizeRoute routeMode

    let nav xs (mode: HistoryMode) (routeMode: RouteMode) =
        if mode = HistoryMode.PushState
        then history.pushState ((), "", encodeParts xs routeMode)
        else history.replaceState((), "", encodeParts xs routeMode)

        let ev = document.createEvent("CustomEvent")

        ev.initEvent (customNavigationEvent, true, true)
        window.dispatchEvent ev |> ignore

    /// Parses the URL into multiple path segments
    let urlSegments (path: string) (mode: RouteMode) =
        match path with
        | String.Prefix "#" (Some _) ->
            // remove the hash sign
            path.Substring(1, path.Length - 1)
        | _ when mode = RouteMode.Hash ->
            match path with
            | String.Suffix "#" (Some _)
            | String.Suffix "#/" (Some _) -> ""
            | _ -> path
        | _ -> path
        |> String.split '/'
        |> List.ofArray
        |> List.collect (fun segment ->
            if String.IsNullOrWhiteSpace segment then []
            else
                let segment = segment.TrimEnd '#'

                match segment with
                | "?" -> []
                | String.Prefix "?" (Some _) -> [ segment ]
                | _ ->
                    match segment.Split [| '?' |] with
                    | [| value |] -> [ decodeURIComponent value ]
                    | [| value; "" |] -> [ decodeURIComponent value ]
                    | [| value; query |] -> [ decodeURIComponent value; "?" + query ]
                    | _ -> [])

    [<Emit("new URLSearchParams($0)")>]
    let createUrlSearchParams (queryString: string) : IUrlSearchParamters = jsNative

    [<Emit("window.navigator.userAgent")>]
    let navigatorUserAgent : string = jsNative

    let onUrlChange routeMode urlChanged (ev: _) =
        match routeMode with
        | RouteMode.Path -> window.location.pathname + window.location.search
        | _ -> window.location.hash
        |> fun path -> urlSegments path routeMode
        |> urlChanged

    let router = React.memo(fun (input: RouterProps) ->
        let onChange = React.useCallbackRef(fun (ev: Event) ->
            let urlChanged = Option.defaultValue ignore input.onUrlChanged
            let routeMode = Option.defaultValue RouteMode.Hash input.hashMode

            onUrlChange routeMode urlChanged ev)

        if navigatorUserAgent.Contains "Trident" || navigatorUserAgent.Contains "MSIE"
        then React.useWindowListener.onHashChange(onChange)
        else React.useWindowListener.onPopState(onChange)

        React.useWindowListener.on(customNavigationEvent, onChange)

        match input.application with
        | Some elem -> elem
        | None -> Html.none)

/// Defines a property for the `router` element
type IRouterProperty = interface end

[<AutoOpen>]
module ReactExtension =
    type React with
        /// Initializes the router as an element of the page and starts listening to URL changes.
        static member inline router (props: IRouterProperty list) =
            Router.router (unbox<Router.RouterProps> (createObj !!props))

[<Erase>]
type router =
    /// An event that is triggered when the URL in the address bar changes, whether by hand or programmatically using `Router.navigate(...)`.
    /// The event arguments are the parts of the URL, segmented into strings:
    ///
    /// `segment "#/" => [ ]`
    ///
    /// `segment "#/home" => [ "home" ]`
    ///
    /// `segment "#/home/settings" => [ "home"; "settings" ]`
    ///
    /// `segment "#/users/1" => [ "users"; "1" ]`
    ///
    /// `segment "#/users/1/details" => [ "users"; "1"; "details" ]`
    ///
    /// with query string parameters
    ///
    /// `segment "#/users?id=1" => [ "users"; "?id=1" ]`
    ///
    /// `segment "#/home/users?id=1" => [ "home"; "users"; "?id=1" ]`
    ///
    /// `segment "#/users?id=1&format=json" => [ "users"; "?id=1&format=json" ]`
    static member inline onUrlChanged (eventHandler: string list -> unit) : IRouterProperty = unbox ("onUrlChanged", eventHandler)

    /// The element that is rendered inside where the `router` is placed. Usually this contains the root application but it could also be part of another root element.
    ///
    /// It will keep listening for URL changes as long as the `router` is rendered on screen somewhere.
    static member inline children (element: ReactElement) : IRouterProperty = unbox ("application", element)

    /// The content that is rendered inside where the `router` is placed. Usually this contains the root application but it could also be part of another root element.
    ///
    /// It will keep listening for URL changes as long as the `router` is rendered on screen somewhere.
    static member inline children (elements: ReactElement list) : IRouterProperty = unbox ("application", React.fragment elements)

    /// Use # based routes (default)
    static member inline hashMode : IRouterProperty = unbox ("hashMode", RouteMode.Hash)

    /// Use full (HTML 5) based routes instead of # based.
    /// You have to be careful about which requests you want forwarded to the server and which ones should be handled locally.
    /// To keep the request local, you have to use the 'Router.navigate' function for all the URL transitions.
    static member inline pathMode : IRouterProperty = unbox ("hashMode", RouteMode.Path)
