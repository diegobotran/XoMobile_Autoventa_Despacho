Imports System.Data
Imports System.Data.SqlServerCe
'---ok
Public Class UtilitarioDT
    Dim SQL_QUERY As String
    Dim SQL_RESULT As Integer
    Dim SQL_DT As DataTable
    Dim Conexion As SqlCeConnection

    Dim objCe As New ceClient

    ''' <summary>
    ''' Valida que el texto ingresado sea entero
    ''' </summary>
    ''' <param name="texto">Valor ingresado por el usuario que se va a validar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 

    '--- <xomobile 2.0>
    Public Function getListaSeleccion(ByVal Tabla As String, Optional ByVal orderby As String = "dataValue") As DataTable
        SQL_QUERY = _
        "   select 	showValue,dataValue,tabla " _
        + " from	ttipo  " _
        + " where   tabla   in(" & Tabla & ") and datavalue <> '-1'" _
        + " union all    " _
        + " select '--- Seleccione ---','-1','-1' " _
        + " order by " & orderby

        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function
    '--- </xoMobile 2.0>
    Public Function isEntero(ByVal texto) As Boolean
        Try
            Convert.ToInt32(texto)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function isDecimal(ByVal texto) As Decimal
        Try
            Convert.ToDecimal(texto)
            Return texto
        Catch ex As Exception
            Return 0.0
        End Try
    End Function

    Public Sub paintPannel(ByVal e As System.Windows.Forms.PaintEventArgs, ByVal pan As Windows.Forms.Panel)
        Dim gr As Graphics = e.Graphics
        Dim MyRect As Rectangle = New Rectangle(0, _
        0, pan.Width - 1, pan.Height - 1)
        gr.DrawRectangle(New Pen(Color.Black), MyRect)
        MyRect = Nothing
        gr.Dispose()

    End Sub

    Public Function getListaTipo(ByVal Tabla As String) As DataTable
        SQL_QUERY = _
        "  select 	showValue,dataValue " _
        + " from	ttipo  " _
        + " where   tabla   =  '" + Tabla + "' " _
        + " union all    " _
        + " select '--- Seleccione ---','-1' " _
        + " order by showValue "

        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function invertirTipo() As DataTable
        SQL_QUERY = _
         "  UPDATE TTIPO SET dataValue = ShowValue, ShowValue = DataValue WHERE TABLA IN ('PDESCUENTO','IVA')"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function updateModGlobal(ByVal variable, ByVal valor) As Integer
        SQL_QUERY = _
         "  UPDATE modGlobal SET valor = '" + valor + "' WHERE variable = '" + variable + "'"
        Return objCe.SetExecute(SQL_QUERY)
    End Function

    Public Function getListaConfiguracion(ByVal Tabla As String, Optional ByVal orderby As String = "dataValue") As DataTable

        SQL_QUERY = _
        "    select 	showValue,dataValue,tabla " _
        + "  from	ttipo " _
        + "  where   tabla   in(" & Tabla & ") and datavalue <> '-1'" _
        + "  order by " & orderby
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getListaConfiguracionGrupo(ByVal Tabla As String, Optional ByVal orderby As String = "dataValue") As DataTable

        SQL_QUERY = _
        "    select 	showValue, tabla as datavalue " _
        + "  from	ttipo " _
        + "  where   grupo   = '" & Tabla & "'" _
        + "  order by " & orderby
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getListaConfiguracionGrupo2(ByVal Tabla As String, Optional ByVal orderby As String = "dataValue") As DataTable

        SQL_QUERY = _
        "     select 	'' as showValue, ''  datavalue from rruta where 1=1 UNION ALL " _
        + "    select 	showValue, tabla as datavalue " _
        + "  from	ttipo " _
        + "  where   grupo   = '" & Tabla & "'" _
        + "  order by " & orderby
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getListaConfiguracionGrupoDepto() As DataTable
        SQL_QUERY = _
        "     select DISTINCT(DEPTO) AS DEPTO, NOM_DEPTO FROM (" _
        + "     select 	'' as depto, ''  nom_depto from rruta where 1=1 UNION ALL " _
        + "    select 	NOM_DEPTO DEPTO, NOM_DEPTO " _
        + "  from	RDEPTO_MUNI)DP " _
        + "  order by DP.DEPTO ASC "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function


    Public Function getListaConfiguracionGrupoMuni(Optional ByVal DEPTO As String = "") As DataTable
        SQL_QUERY = _
        "    select 	NOM_MUNI AS MUNI, NOM_MUNI " _
        + "  from	RDEPTO_MUNI  WHERE NOM_DEPTO = '" + DEPTO + "'"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    'Public Function getListaConfiguracionCG(ByVal Tabla) As DataTable
    '    SQL_QUERY = _
    '   "    select 	showValue,dataValue " _
    '   + "  from	ttipo " _
    '   + "  where   tabla   =  '" + Tabla + "' " _
    '   + "  AND showValue not in ('Encuesta','Cobro','Inventario') order by dataValue"
    '    SQL_DT = objCe.GetDataSet(SQL_QUERY)
    '    Return SQL_DT
    'End Function

    Public Function getModGlobal(ByVal variable) As DataTable
        SQL_QUERY = _
       "    select 	valor " _
       + "  from	modGlobal " _
       + "  where   variable   =  '" + variable + "' "
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getModGlobalByVariable(ByVal variable As String) As DataTable
        SQL_QUERY = _
        " select * " _
        + " from	modGlobal where variable like '%" + variable + "%'"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    'Public Function getListadoModGlobal() As DataTable
    '    SQL_QUERY = _
    '   "    select 	valor " _
    '   + "  from	modGlobal "
    '    SQL_DT = objCe.GetDataSet(SQL_QUERY)
    '    Return SQL_DT
    'End Function

    Public Function setModGlobal(ByVal variable As String, ByVal valor As String) As Integer
        SQL_QUERY = _
        " INSERT INTO [modGlobal]([variable],[valor]) VALUES ('" + Variable + "','" + Valor + "') "
        Return objCe.SetExecute(SQL_QUERY)
    End Function

    Public Function deleteModGlobal(ByVal variable As String, ByVal idMod As String, ByVal killAll As Boolean) As Integer
        If killAll Then
            SQL_QUERY = "DELETE FROM [modGlobal] WHERE [variable] = '" + variable + "'"
        Else
            SQL_QUERY = "DELETE FROM [modGlobal] WHERE [idmod] = " + idMod
        End If
        Return objCe.SetExecute(SQL_QUERY)

    End Function

    'Public Function buscarItem(ByRef listado As Windows.Forms.ListView, ByVal dtMaestra As DataTable, ByVal searchKey As String) As Boolean
    '    Dim dtTemporal As New DataTable
    '    '--- Crear el filtro mediante la llave de busqueda
    '    Dim rows() As DataRow
    '    If co_glo_searchProducto = "codigo" Then
    '        rows = dtMaestra.Select(co_glo_searchProducto + " like '%" + searchKey + "%'", " orden ASC")
    '    Else
    '        rows = dtMaestra.Select(co_glo_searchProducto + " like '%" + searchKey + "%'", " orden ASC")
    '    End If

    '    listado.Items.Clear()
    '    '-- Carga los datos filtrados en la tabla de busqueda
    '    Dim row As DataRow
    '    Dim i As Integer = 0
    '    For Each row In rows
    '        dtTemporal.ImportRow(row)
    '        Dim lvi As New ListViewItem(row(0).ToString())
    '        lvi.SubItems.Add(row(1).ToString())
    '        lvi.SubItems.Add(row(4).ToString())
    '        lvi.SubItems.Add(row(14).ToString())
    '        listado.Items.Add(lvi)
    '        Select Case row(4).ToString()
    '            Case 1 ' completo
    '                listado.Items(i).ImageIndex = 0
    '                listado.Items(i).BackColor = xoInfomat
    '            Case 2 ' parcial
    '                listado.Items(i).ImageIndex = 1
    '            Case 3 ' no atencion
    '                listado.Items(i).ImageIndex = 2
    '                listado.Items(i).BackColor = xoWarning
    '        End Select
    '        i += 1
    '    Next
    'End Function

    Public Function buscarCliente(ByRef listado As Windows.Forms.ListView, ByVal dtMaestra As DataTable, ByVal searchKey As String) As Boolean
        Dim dtTemporal As New DataTable
        '--- Crear el filtro mediante la llave de busqueda
        Dim rows() As DataRow

        If co_glo_searchCliente = "codigo" Then
            rows = dtMaestra.Select(co_glo_searchCliente + " like '%" + searchKey + "%'", " orden ASC")
        Else
            rows = dtMaestra.Select(co_glo_searchCliente + " like '%" + searchKey + "%'", " orden ASC")
        End If

        listado.Items.Clear()
        '-- Carga los datos filtrados en la tabla de busqueda
        Dim row As DataRow
        Dim i As Integer = 0
        For Each row In rows
            dtTemporal.ImportRow(row)
            Dim lvi As New ListViewItem(row(0).ToString())
            lvi.SubItems.Add(row(1).ToString())
            lvi.SubItems.Add(row(4).ToString())
            listado.Items.Add(lvi)
            Select Case row(4).ToString()
                Case 1 ' completo
                    listado.Items(i).ImageIndex = 0
                    listado.Items(i).BackColor = xoInfomat
                Case 2 ' parcial
                    listado.Items(i).ImageIndex = 1
                Case 3 ' no atencion
                    listado.Items(i).ImageIndex = 2
                    listado.Items(i).BackColor = xoWarning
            End Select
            i += 1
        Next
    End Function

    Public Function toEntero(ByVal texto As String) As Integer
        Try
            Dim ep As Int32 = Convert.ToInt32(texto)
            Return ep
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function getListaConfiguracionCG(ByVal Tabla) As DataTable
        SQL_QUERY = "SELECT * FROM TTIPO WHERE TABLA = 'WORKFLOW' and SHOWVALUE = 'Venta'"
        '"    select 	showValue,dataValue " _
        '+ "  from	ttipo " _
        '+ "  where   tabla   =  '" + Tabla + "' " _
        '+ "  AND showValue not in ('Encuesta','Cobro','Inventario') order by dataValue"
        SQL_DT = objCe.GetDataSet(SQL_QUERY)
        Return SQL_DT
    End Function

    Public Function getPorcentaje() As String
        Conexion = objCe.dbConnect()
        Try
            Conexion.Open()

            '--- Query 2: Obtener el id del encabezado
            SQL_QUERY = "SELECT showValue FROM TTIPO WHERE TABLA = 'LIMITE_NC'"
            SQL_DT = objCe.ExecuteIdentity(SQL_QUERY, Conexion)
            '--- Cerrar la conexion
            Conexion.Close()

            '--- Devolver el id del encabezado
            Return SQL_DT.Rows(0).Item(0).ToString

        Catch ex As Exception
            Return 0
        End Try

    End Function

End Class
