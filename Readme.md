<!-- default file list -->
*Files to look at*:

* [HomeController.cs](./CS/TreeListEditing/Controllers/HomeController.cs) (VB: [HomeController.vb](./VB/TreeListEditing/Controllers/HomeController.vb))
* [Data.cs](./CS/TreeListEditing/Models/Data.cs) (VB: [Data.vb](./VB/TreeListEditing/Models/Data.vb))
* **[Index.cshtml](./CS/TreeListEditing/Views/Home/Index.cshtml)**
* [TreeListPartial.cshtml](./CS/TreeListEditing/Views/Home/TreeListPartial.cshtml)
<!-- default file list end -->
# TreeList - Editing via Context Menu
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/t144034/)**
<!-- run online end -->


This example illustrates how to edit the MVC TreeList via a separate PopupMenu.<br />The main implementation details:<br />- Store the Parent Node Key data within a node by handing the <strong>TreeListSettings.HtmlRowPrepared</strong> event;<br />- Handle the client-side <strong>ASPxClientTreeList.ContextMenu</strong> event, show the context menu via the client-side <strong>ASPxClientPopupMenu.ShowAtPos</strong> method, and store information about a hovered node key/parent key;<br />- Handle the client-side <strong>ASPxClientMenuBase.ItemClick</strong> event and invoke the corresponding CRUD command via ASPxTreeList's client-side API.

<br/>


