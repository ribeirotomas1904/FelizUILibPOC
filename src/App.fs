module App

open Feliz
open Lib.UI

type Animal =
    | Cat of name: string
    | Dog of name: string

type Person = { name: string }

[<ReactComponent>]
let Make () =
    React.fragment [
        // ERROR: A unique overload for method 'text' could not be determined based on type information prior to this program point. A type annotation may be needed.
        // UI.list' []

        UI.list' [
            list'.items []
        ]

        // ERROR: A unique overload for method 'text' could not be determined based on type information prior to this program point. A type annotation may be needed.
        // UI.list' [
        //     list'.renderItem Html.text
        // ]

        UI.list'<string> [ list'.renderItem Html.text ]

        UI.list' [
            list'.items [
                "ocaml"
                "fsharp"
            ]
        ]

        UI.list' [
            list'.items [
                Cat "oreo"
                Dog "luna"
                Dog "lilica"
                Cat "gata"
            ]
        ]

        UI.list' [
            list'.items [
                { name = "nathan" }
            ]
        ]

        // Error because once the type is established by `list'.items`
        // the `'a` in `renderItem: ('a -> ReactElement) -> IListProperty<'a>`
        // needs to be the same type as the `'a` in
        // `items: list<'a> -> IListProperty<'a>` in order for them to match
        // the type of `UI.list': IListProperty<'a> list -> ReactElement`
        // UI.list' [
        //     list'.items [
        //         { name = "nathan" }
        //     ]

        //     list'.renderItem (fun item -> Html.text item)
        // ]

        UI.list' [
            list'.items [
                { name = "nathan" }
            ]

            list'.renderItem (fun item -> Html.text item.name)
        ]

        UI.list' [
            list'.items [
                "javascript"
                "php"
            ]
            list'.type'.ordered
        ]

        UI.list' [
            list'.items [
                "csharp"
                "java"
            ]
            list'.type'.unordered
            list'.renderItem Html.strong
        ]

        UI.list' [
            UI.list' [
                list'.items [
                    "ml"
                    "lisp"
                ]
                list'.renderItem Html.strong
            ]

            UI.list' [
                list'.items [
                    "csharp"
                    "java"
                ]
            ]

            Html.button [
                prop.text "hello world"
                prop.onClick (fun _ -> Browser.Dom.window.alert "hello world")
            ]
        ]

        UI.list' [
            "react"
            "vue"
            "angular"
        ]
    ]
