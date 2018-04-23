@Code
    Dim treeList As TreeListExtension = Html.DevExpress().TreeList( _
        Sub(settings)
                settings.Name = "TreeList"
                settings.CallbackRouteValues = New With {.Controller = "Home", .Action = "TreeListPartial"}

                settings.KeyFieldName = "EmployeeID"
                settings.ParentFieldName = "SupervisorID"

                settings.Columns.Add("FirstName")
                settings.Columns.Add("MiddleName")
                settings.Columns.Add("LastName")
                settings.Columns.Add("Title")

                settings.SettingsEditing.AddNewNodeRouteValues = New With {.Controller = "Home", .Action = "TreeListPartialAddNew"}
                settings.SettingsEditing.UpdateNodeRouteValues = New With {.Controller = "Home", .Action = "TreeListPartialUpdate"}
                settings.SettingsEditing.NodeDragDropRouteValues = New With {.Controller = "Home", .Action = "TreeListPartialMove"}
                settings.SettingsEditing.DeleteNodeRouteValues = New With {.Controller = "Home", .Action = "TreeListPartialDelete"}

                settings.CommandColumn.Visible = True

                settings.HtmlRowPrepared = _
                    Sub(s, e)
                            Dim tree As MVCxTreeList = CType(s, MVCxTreeList)
                            If (e.RowKind = TreeListRowKind.Data) Then
                                Dim node As TreeListNode = tree.FindNodeByKeyValue(e.NodeKey)
                                If (node IsNot Nothing And node.ParentNode IsNot Nothing) Then
                                    e.Row.Attributes("ParentNodeKey") = node.ParentNode.Key
                                End If
                            End If
                    End Sub
                
                settings.ClientSideEvents.ContextMenu = "OnContextMenu"
        End Sub)
    If (ViewData("EditNodeError") IsNot Nothing) Then
        treeList.SetEditErrorText(CType(ViewData("EditNodeError"), String))
    End If
End Code

@treeList.Bind(Model).GetHtml()
