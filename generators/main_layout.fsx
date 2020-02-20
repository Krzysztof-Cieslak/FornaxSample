#r "../_lib/Fornax.Core.dll"
#if !FORNAX
#load "../loaders/postloader.fsx"
#endif

open Html

let injectWebsocketCode (webpage:string) =
    let websocketScript =
        """
        <script type="text/javascript">
          var wsUri = "ws://localhost:8080/websocket";
      function init()
      {
        websocket = new WebSocket(wsUri);
        websocket.onclose = function(evt) { onClose(evt) };
      }
      function onClose(evt)
      {
        console.log('closing');
        websocket.close();
        document.location.reload();
      }
      window.addEventListener("load", init, false);
      </script>
        """
    let head = "<head>"
    let index = webpage.IndexOf head
    webpage.Insert ( (index + head.Length + 1),websocketScript)

let layout (ctx : SiteContents) bodyCnt =
    html [] [
        head [] [
            meta [CharSet "utf-8"]
            meta [Name "viewport"; Content "width=device-width, initial-scale=1"]
            title [] [!! "Fornax sample blog"]
        ]
        body [] [
            bodyCnt
        ]
    ]