module Lib.UI

open Fable.Core.JsInterop
open Feliz

type IListProperty<'a> =
    interface
    end

type ListType =
    | Unordered
    | Ordered

type ListProps<'a> =
    abstract items : 'a list option
    abstract renderItem : ('a -> ReactElement) option
    abstract type' : ListType option

let mkAttr key value = unbox (key, value)
let unboxProperties props = !!props |> createObj |> unbox

type UI =
    static member list'(children: ReactElement list) : ReactElement = children |> List.map Html.li |> Html.ul

    static member list'<'a>(properties: IListProperty<'a> list) : ReactElement =

        let list' (props: ListProps<'a>) =
            let listType =
                Option.defaultValue Unordered props.type'

            let renderList: ReactElement list -> ReactElement =
                match listType with
                | Unordered -> Html.ul
                | Ordered -> Html.ol

            match props.items with
            | None -> []
            | Some items ->
                let render =
                    match props.renderItem with
                    | None -> string >> Html.text
                    | Some renderItem -> renderItem

                items |> List.map (render >> Html.li)

            |> renderList

        list' (unboxProperties properties)

    static member list'(items: string list) : ReactElement = items |> List.map Html.li |> Html.ul

type list' =
    static member items(xs: 'a list) : IListProperty<'a> = mkAttr "items" xs
    static member renderItem(fn: 'a -> ReactElement) : IListProperty<'a> = mkAttr "renderItem" fn

module list' =
    type type'<'a> =
        static member unordered: IListProperty<'a> = mkAttr "type'" Unordered
        static member ordered: IListProperty<'a> = mkAttr "type'" Ordered
