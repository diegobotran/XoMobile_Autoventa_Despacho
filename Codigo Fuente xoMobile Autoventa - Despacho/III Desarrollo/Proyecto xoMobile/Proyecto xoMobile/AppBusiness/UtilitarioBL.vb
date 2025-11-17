Imports System.Data

Public Class UtilitarioBL

    'Objetos de la capa de datos
    Dim oUtil As New UtilitarioDT

    Public Function buscarItem(ByVal dtMaestra As DataTable, ByVal searchTerm As String) As Boolean
        Dim foundRows() As DataRow
        Dim filter As String
        If co_glo_searchProducto = "codigo" Then co_glo_searchProducto = "Codigo"

        filter = co_glo_searchProducto & " not  like '%" & searchTerm & "%'"
        foundRows = dtMaestra.Select(filter)
        For i = 0 To foundRows.GetUpperBound(0)
            foundRows(i)("display") = 0
        Next i
    End Function

    '--- xoMobile 2.0
    Public Function listarSeleccion(ByVal Ambito As String, Optional ByVal orderBy As String = "dataValue") As DataTable
        Dim dtTipo As New DataTable
        Ambito = "'" & Ambito & "'"
        dtTipo = oUtil.getListaSeleccion(Ambito)
        If dtTipo.Rows.Count > 0 Then
        Else
            dtTipo.Rows.Add(New String() {"SIN DATOS MAESTROS", "-0"})
        End If
        Return dtTipo
    End Function
    '--- xoMobile 2.0
    Public Function getParametroGeneral(ByVal tabla As String, ByRef showValue As String, ByRef dataValue As String) As Boolean
        Dim dtTipo As New DataTable
        Dim dvTipo As New DataView
        dvTipo = getListaTipo(tabla).DefaultView
        dtTipo = dvTipo.ToTable
        If dtTipo.Rows.Count <= 0 Or dtTipo.Rows(0).Item("dataValue").ToString() = "-0" Then
            MsgBox("El Parametro General " & tabla & " no se ha cargado en xoMobile.")
            Return False
        Else
            'dataValue = 0.12
            'showValue = 0.12
            If tabla = "IVA" Then
                dataValue = 0.12
                showValue = 0.12
            Else
                dataValue = dtTipo.Rows(0).Item("dataValue").ToString()
                showValue = dtTipo.Rows(0).Item("showValue").ToString()
            End If
            
            Return True

        End If
    End Function

    Public Function getParametroGeneral2(ByVal tabla As String, ByRef showValue As String, ByRef dataValue As String) As Boolean
        Dim dtTipo As New DataTable
        Dim dvTipo As New DataView
        dvTipo = getListaTipo(tabla).DefaultView
        dtTipo = dvTipo.ToTable
        If dtTipo.Rows.Count <= 0 Or dtTipo.Rows(0).Item("dataValue").ToString() = "-0" Then
            MsgBox("El Parametro General " & tabla & " no se ha cargado en xoMobile.")
            Return False

        Else
            If tabla = "IVA" Then
                dataValue = 0.12
                showValue = 0.12
            Else
                dataValue = dtTipo.Rows(0).Item("showValue").ToString()
                showValue = dtTipo.Rows(0).Item("dataValue").ToString()
            End If
            Return True

        End If
    End Function
    '--- </xoMobile 2.0>
    Public Function getGlobal(ByVal variable As String, ByRef valor As String) As Boolean
        Dim oUtil As New UtilitarioDT
        Dim dtTipo As New DataTable

        dtTipo = oUtil.getModGlobal(variable)
        If dtTipo.Rows.Count > 0 Then
            valor = dtTipo.Rows(0).Item("valor")
            Return True
        Else
            MsgBox("La variable Global " & variable & " no se ha cargado en xoMobile.")
            Return False
        End If
    End Function

    '--- </xoMobile 2.0>
    Public Function pDescuento(ByVal nombreDescuento As String) As String
        Dim dtDescuento As New DataTable
        Dim dvDescuento As New DataView
        dvDescuento = oUtil.getListaTipo("PDESCUENTO").DefaultView
        dvDescuento.RowFilter = "showValue = '" + nombreDescuento + "'"

        dtDescuento = dvDescuento.ToTable
        If dtDescuento.Rows.Count > 0 Then
            Return dtDescuento.Rows(0).Item("dataValue").ToString()
        Else
            Throw New Exception("No se ha establecido el porcentaje de descuento. para el tipo descuento: " + nombreDescuento)
            Return Nothing
        End If
    End Function

    Public Function getShowValue(ByVal tabla As String, ByVal datavalue As String) As String
        Dim dtDescuento As New DataTable
        Dim dvDescuento As New DataView
        dvDescuento = oUtil.getListaTipo(tabla).DefaultView
        dvDescuento.RowFilter = "dataValue = '" + datavalue + "'"
        dtDescuento = dvDescuento.ToTable
        If dtDescuento.Rows.Count > 0 Then
            Return dtDescuento.Rows(0).Item("showValue").ToString()
        Else
            Return Nothing
        End If
    End Function

    Public Function getDataValue(ByVal tabla As String, ByVal showValue As String) As String
        Dim dtDescuento As New DataTable
        Dim dvDescuento As New DataView
        dvDescuento = oUtil.getListaTipo(tabla).DefaultView
        dvDescuento.RowFilter = "showValue = '" + showValue + "'"
        dtDescuento = dvDescuento.ToTable
        If dtDescuento.Rows.Count > 0 Then
            Return dtDescuento.Rows(0).Item("dataValue").ToString()
        Else
            Return "0"
        End If
    End Function
   

    Public Function getBancos() As DataTable
        Dim dtBancos As New DataTable
        dtBancos = oUtil.getListaTipo("BANCOS")
        If dtBancos.Rows.Count > 0 Then
            Return dtBancos
        Else
            Throw New Exception("Los bancos no se cargaron correctamente")
        End If
        Return dtBancos
    End Function

    Public Sub paintPannel(ByVal e As System.Windows.Forms.PaintEventArgs, ByVal pan As Windows.Forms.Panel)
        Dim gr As Graphics = e.Graphics
        Dim MyRect As Rectangle = New Rectangle(0, _
        0, pan.Width - 1, pan.Height - 1)
        gr.DrawRectangle(New Pen(Color.Black), MyRect)
        MyRect = Nothing
        gr.Dispose()
    End Sub

    Public Function isDecimal(ByVal texto As String) As Decimal
        Try
            texto = Decimal.Round(Convert.ToDecimal(texto), 2)
            Return texto
        Catch ex As Exception
            Return 0.0
        End Try
    End Function

    Public Function isDecimalFlat(ByVal texto As String) As Decimal
        Try
            texto = Decimal.Round(Convert.ToDecimal(texto), 0)
            Return texto
        Catch ex As Exception
            Return 0.0
        End Try
    End Function

    Public Function isDecimal2(ByVal texto As String) As Decimal
        Try
            texto = Decimal.Round(Convert.ToDecimal(texto), 3)
            Return texto
        Catch ex As Exception
            Return 0.0
        End Try
    End Function

    Public Function isDecimal4(ByVal texto As String) As Decimal
        Try
            texto = Decimal.Round(Convert.ToDecimal(texto), 4)
            Return texto
        Catch ex As Exception
            Return 0.0
        End Try
    End Function

    Public Function isDecimal5(ByVal texto As String) As Decimal
        Try
            texto = Decimal.Round(Convert.ToDecimal(texto), 5)
            Return texto
        Catch ex As Exception
            Return 0.0
        End Try
    End Function

    Public Function isDecimal6(ByVal texto As String) As Decimal
        Try
            texto = Decimal.Round(Convert.ToDecimal(texto), 6)
            Return texto
        Catch ex As Exception
            Return 0.0
        End Try
    End Function

    Public Function isInteger(ByVal texto As String) As Integer
        Try
            texto = Decimal.Round(Convert.ToInt32(texto), 0)
            Return texto
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function isInteger(ByVal texto As String, ByVal zint As Integer) As Integer
        Try
            texto = Decimal.Round(Convert.ToInt32(texto), 0)
            Return texto
        Catch ex As Exception
            Return zint
        End Try
    End Function

    Public Function isDecimalUKID(ByVal texto As String) As Decimal
        Try
            texto = Decimal.Round(Convert.ToDecimal(texto), 2)
            Return texto
        Catch ex As Exception
            Return -78737
        End Try
    End Function

    Public Function xoDia(ByVal dia As Integer) As String
        Select Case dia
            Case 1
                Return "Lunes"
            Case 2
                Return "Martes"
            Case 3
                Return "Miercoles"
            Case 4
                Return "Jueves"
            Case 5
                Return "Viernes"
            Case 6
                Return "Sabado"
            Case 7
                Return "Domingo"
        End Select
        Return Nothing
    End Function

  

    Public Function getListaTipo(ByVal Ambito As String, Optional ByVal orderBy As String = "dataValue") As DataTable
        Dim dtTipo As New DataTable
        Ambito = "'" & Ambito & "'"
        dtTipo = oUtil.getListaConfiguracion(Ambito)
        If dtTipo.Rows.Count > 0 Then
        Else
            dtTipo.Rows.Add(New String() {"SIN DATOS MAESTROS", "-0"})
        End If
        Return dtTipo
    End Function

    Public Function getListaTipoGrupo(ByVal grupo As String, Optional ByVal orderBy As String = "dataValue") As DataTable
        Dim dtTipo As New DataTable
        dtTipo = oUtil.getListaConfiguracionGrupo2(grupo, orderBy)
        If dtTipo.Rows.Count > 0 Then
        Else
            dtTipo.Rows.Add(New String() {"SIN DATOS MAESTROS", "-0"})
        End If
        Return dtTipo
    End Function

    Public Function getListaTipoGrupoDepto() As DataTable
        Dim dtTipo As New DataTable
        dtTipo = oUtil.getListaConfiguracionGrupoDepto()
        If dtTipo.Rows.Count > 0 Then
        Else
            dtTipo.Rows.Add(New String() {"SIN DATOS MAESTROS", "-0"})
        End If
        Return dtTipo
    End Function

    Public Function getListaTipoGrupoMuni(Optional ByVal Depto As String = "") As DataTable
        Dim dtTipo As New DataTable
        dtTipo = oUtil.getListaConfiguracionGrupoMuni(Depto)
        If dtTipo.Rows.Count > 0 Then
        Else
            dtTipo.Rows.Add(New String() {"SIN DATOS MAESTROS", "-0"})
        End If
        Return dtTipo
    End Function

    Public Function crearModGlobal(ByVal variable, ByVal valor) As Boolean
        Dim oUtil As New UtilitarioDT
        If valor = "" Then
            Throw New Exception("Escriba una palabra valida.")
        End If
        If oUtil.setModGlobal(variable, valor) >= 1 Then
            Return True
        Else
            Throw New Exception("No fue posible agregar el parametro Global. " + variable + " con el valor " + valor)
        End If
    End Function

    Public Function eliminarModGlobal(ByVal variable As String, ByVal idmod As String, ByVal killAll As Boolean) As Boolean
        Dim oUtil As New UtilitarioDT
        If oUtil.deleteModGlobal(variable, idmod, killAll) >= 1 Then
            Return True
        Else
            Throw New Exception("No fue posible eliminar el parametro Global. " + variable)
        End If
    End Function

    Public Function actualizarModGlobal(ByVal variable, ByVal valor) As Boolean
        Dim oUtil As New UtilitarioDT
        If oUtil.updateModGlobal(variable, valor) >= 1 Then
            Return True
        Else
            Throw New Exception("No fue posible modificar el parametro Global. " + variable + " con el valor " + valor)
        End If
    End Function

    'Public Function obtenerModGlobal() As DataTable
    '    Dim dtGlobal As New DataTable
    '    dtGlobal = oUtil.getModGlobalByVariable("id_glo_codRuta")
    '    If dtGlobal.Rows.Count > 0 Then
    '        Return dtGlobal
    '    Else
    '    End If
    'End Function

    Public Function obtenerModGlobalPorVariable(ByVal variable As String) As DataTable
        Dim dtGlobal As New DataTable
        dtGlobal = oUtil.getModGlobalByVariable(variable)
        Return dtGlobal
    End Function

    Public Function obtenerPorcentaje() As String
        Dim porcentajeNC As String
        porcentajeNC = oUtil.getPorcentaje()
        Return porcentajeNC
    End Function



    
End Class
