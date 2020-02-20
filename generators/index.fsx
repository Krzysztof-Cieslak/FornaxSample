#r "../_lib/Fornax.Core.dll"
#load "layout.fsx"

open Html

let generate' (ctx : SiteContents) (_: string) =
  let posts = ctx.TryGetValues<Postloader.Post> () |> Option.defaultValue Seq.empty
  let siteInfo = ctx.TryGetValue<Globalloader.SiteInfo> ()
  let desc =
    siteInfo
    |> Option.map (fun si -> si.description)
    |> Option.defaultValue ""


  let psts =
    posts
    |> Seq.toList
    |> List.map (fun post ->
      div [Class "card article"] [
        div [Class "card-content"] [
          if post.avatar.IsSome then
            div [Class "media"] [
              img [Src post.avatar.Value]
            ]
          div [Class "media-content has-text-centered"] [
            p [Class "title article-title"; ] [ a [Href post.link] [!! post.title]]
            p [Class "subtitle is-6 article-subtitle"] [
              a [Href "#"] [!! (defaultArg post.author "")]
              !! (sprintf "on %A" post.published)
            ]
          ]
        ]
        div [Class "content article-body"] [
          !! post.content
        ]
      ])

  Layout.layout ctx "Index" [
    section [Class "hero is-info is-medium is-bold"] [
      div [Class "hero-body"] [
        div [Class "container has-text-centered"] [
          h1 [Class "title"] [!!desc]
        ]
      ]
    ]
    div [Class "container"] [
      section [Class "articles"] [
        div [Class "column is-8 is-offset-2"] psts
      ]
    ]]

let generate (ctx : SiteContents) (projectRoot: string) (page: string) =
  generate' ctx page
  |> Layout.render ctx