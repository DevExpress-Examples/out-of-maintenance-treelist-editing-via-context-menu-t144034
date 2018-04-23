# TreeList - Editing via Context Menu


This example illustrates how to edit the MVC TreeList via a separate PopupMenu.<br />The main implementation details:<br />- Store the Parent Node Key data within a node by handing the <strong>TreeListSettings.HtmlRowPrepared</strong> event;<br />- Handle the client-side <strong>ASPxClientTreeList.ContextMenu</strong> event, show the context menu via the client-side <strong>ASPxClientPopupMenu.ShowAtPos</strong> method, and store information about a hovered node key/parent key;<br />- Handle the client-side <strong>ASPxClientMenuBase.ItemClick</strong> event and invoke the corresponding CRUD command via ASPxTreeList's client-side API.

<br/>


