Imports System.Data
Imports System.IO

Public Class ceExportLogic
    Dim objExport As New ceExportData
    Dim outil As New UtilitarioBL

    Public Function comprobarConexion(ByVal objTest As xoservice.xotest, ByVal ip As String, ByRef zText As String, ByVal secureText As String) As Boolean
        Try
            objTest.Url = secureText & "://" & ip & "/xoservice/xoTest.asmx"
            zText = objTest.xoTestConn
            id_glo_server = ip
            Return True
        Catch ex As Exception
            zText = "Ocurrio un Error al establecer comunicacion con el servidor: " + ex.Message
            Return False
        End Try
    End Function

    Public Function comprobarConexion(ByVal service As xoservice.xotest, Optional ByVal rise As Boolean = True) As Boolean
        Cursor.Current = Cursors.WaitCursor
        'Try
        ' MessageBox.Show(service.Url)
        ' If rise Then MsgBox(service.xoTestConn())
        Cursor.Current = Cursors.Default
        ' Return True
        ' Catch ex As Exception
        'MsgBox(ex.Message)
        'Dim canal As String = " Dentro de licorera, canal http://"
        'If id_glo_estoy_fuera Then
        ' canal = " Fuera de licorera, canal seguro https://"
        ' End If
        'MsgBox("Ocurrio un Error al establecer comunicacion con el servidor.")
        'MsgBox("Se esta tratando de conectar al servidor " & id_glo_server & canal)
        'MsgBox("Asegurese que la IP sea la correcta y que esta usando la opcion fuera de licorera si esta transmitiendo por chip o WiFi privada.")
        'MsgBox(ex.Message)
        'Cursor.Current = Cursors.Default
        'Return False
        'End Try
        Dim ServiceResponce As String

        '--- Llamar la operacion WebService
        Try
            ServiceResponce = service.xoTestConn()
        Catch ex As Exception
            Throw New Exception("No hay conectividad con el servicio, favor verifique los parametros de conexion.")
            Return False
        End Try

        '--- El servicio responde de forma esperada
        If Not ServiceResponce.StartsWith("Prueba") Then
            Throw New Exception("No hay conectividad con el servicio, favor verifique los parametros de conexion.")
            Return False
        Else
            MsgBox(ServiceResponce)
        End If
        Return True
    End Function

#Region " EXPORTAR PREGUNTAS INICIALES / FINALES "
    Public Function xo_dataSet_AgregarPreguntasIF(ByRef dsxo As DataSet, ByVal nombreTabla As String, ByRef dtImportLog As DataTable) As Boolean
        Try
            If dsxo.Tables.Contains(nombreTabla) Then
                dsxo.Tables.Remove(nombreTabla)
            End If
            Dim dt As New DataTable
            Dim oceExport As New ceExportData
            dt = oceExport.agregarPreguntasIF()
            If dt.Rows.Count > 0 Then
                dt.TableName = nombreTabla
                dsxo.Tables.Add(dt)
            Else
                dtImportLog.Rows.Add(New String() {"Advertencia", "Preguntas iniciales/finales", "", "xo_dataSet_AgregarPreguntasIF", "No hay informacion para exportar", "xoMobile"})
            End If
            Return True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Correlativos SAT", "", "getCorrelativoSAP", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function
#End Region

#Region " EXPORTAR INVENTARIO FISICO DEL CLIENTE "
    Public Function xo_dataSet_AgregarInventarioFisico(ByRef dsxo As DataSet, ByVal nombreTabla As String, ByRef dtImportLog As DataTable) As Boolean
        Try
            If dsxo.Tables.Contains(nombreTabla) Then
                dsxo.Tables.Remove(nombreTabla)
            End If
            Dim dt As New DataTable
            Dim oceExport As New ceExportData
            dt = oceExport.agregarInventarioFisico()
            If dt.Rows.Count > 0 Then
                dt.TableName = nombreTabla
                dsxo.Tables.Add(dt)
            Else
                dtImportLog.Rows.Add(New String() {"Advertencia", "Inventario fisico de cliente", "", "xo_dataSet_AgregarInventarioFisico", "No hay informacion para exportar", "xoMobile"})
            End If
            Return True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Correlativos SAT", "", "getCorrelativoSAP", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function
#End Region

#Region " EXPORTAR CORRELATIVO SAP "
    Public Function xo_dataSet_AgregarCorrelativo(ByRef dsxo As DataSet, ByVal nombreTabla As String, ByRef dtImportLog As DataTable) As Boolean
        Try
            If dsxo.Tables.Contains(nombreTabla) Then
                dsxo.Tables.Remove(nombreTabla)
            End If
            Dim dt As New DataTable
            Dim oceExport As New ceExportData
            dt = oceExport.agregarCorrelativoSAP()
            If dt.Rows.Count > 0 Then
                dt.TableName = nombreTabla
                dsxo.Tables.Add(dt)
            Else
                dtImportLog.Rows.Add(New String() {"Advertencia", "Correlativos SAT", "", "xo_dataSet_AgregarCorrelativo", "No hay informacion para exportar", "xoMobile"})
            End If
            Return True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Correlativos SAT", "", "getCorrelativoSAP", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function
#End Region

#Region " ACTUALIZACION CLIENTES "
    Public Function xo_dataSet_actualizacliente(ByRef dsxo As DataSet, ByVal nombreTabla As String, ByRef dtImportLog As DataTable) As Boolean
        Try
            If dsxo.Tables.Contains(nombreTabla) Then
                dsxo.Tables.Remove(nombreTabla)
            End If
            Dim dt As New DataTable
            Dim oceExport As New ceExportData
            dt = oceExport.actualizarclienteSAP()
            If dt.Rows.Count > 0 Then
                dt.TableName = nombreTabla
                dsxo.Tables.Add(dt)
            Else
                dtImportLog.Rows.Add(New String() {"Advertencia", "Actualizar Cliente", "", "xo_dataSet_actualizacliente", "No hay informacion para exportar", "xoMobile"})
            End If
            Return True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Correlativos SAT", "", "Actualiza-Clientes", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function
#End Region


#Region " ACTUALIZACION CLIENTES NOMBRES FEL"
    Public Function xo_dataSet_actualizaNombre(ByRef dsxo As DataSet, ByVal nombreTabla As String, ByRef dtImportLog As DataTable) As Boolean
        Try
            If dsxo.Tables.Contains(nombreTabla) Then
                dsxo.Tables.Remove(nombreTabla)
            End If
            Dim dt As New DataTable
            Dim oceExport As New ceExportData
            dt = oceExport.actualizaNombreFEL()
            If dt.Rows.Count > 0 Then
                dt.TableName = nombreTabla
                dsxo.Tables.Add(dt)
            Else
                dtImportLog.Rows.Add(New String() {"Advertencia", "Actualizar Nombre FEL", "", "xo_dataSet_actualizaNombre", "No hay informacion para exportar", "xoMobile"})
            End If
            Return True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Actualiza Nombre FEL", "", "Actualiza-Clientes", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function
#End Region

#Region " ACTUALIZACION CLIENTES NOMBRES FEL"
    Public Function xo_dataSet_libroventas(ByRef dsxo As DataSet, ByVal nombreTabla As String, ByRef dtImportLog As DataTable) As Boolean
        Try
            If dsxo.Tables.Contains(nombreTabla) Then
                dsxo.Tables.Remove(nombreTabla)
            End If
            Dim dt As New DataTable
            Dim oceExport As New ceExportData
            dt = oceExport.actualizalibroventas()
            If dt.Rows.Count > 0 Then
                dt.TableName = nombreTabla
                dsxo.Tables.Add(dt)
            Else
                dtImportLog.Rows.Add(New String() {"Advertencia", "Actualizar Libro de Ventas", "", "xo_dataSet_libroventas", "No hay informacion para exportar", "xoMobile"})
            End If
            Return True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Actualiza Libro de Ventas", "", "Actualiza_Libro_ventas", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function
#End Region

#Region " EXPORTAR CORRELATIVO CONTINGENCIA "
    Public Function xo_dataSet_AgregarCorrelativoContingencia(ByRef dsxo As DataSet, ByVal nombreTabla As String, ByRef dtImportLog As DataTable) As Boolean
        Try
            If dsxo.Tables.Contains(nombreTabla) Then
                dsxo.Tables.Remove(nombreTabla)
            End If
            Dim dt As New DataTable
            Dim oceExport As New ceExportData
            dt = oceExport.agregarCorrelativoSAPContigencia()
            If dt.Rows.Count > 0 Then
                dt.TableName = nombreTabla
                dsxo.Tables.Add(dt)
            Else
                dtImportLog.Rows.Add(New String() {"Advertencia", "Correlativos CONTINGENCIA", "", "xo_dataSet_AgregarCorrelativoContingencia", "No hay informacion para exportar", "xoMobile"})
            End If
            Return True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Correlativos Contingencia", "", "getCorrelativoSAPContingencia", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function
#End Region

#Region " EXPORTAR ENCUESTAS "
    Public Function xo_dataSet_AgregarEncuestaRespuestas(ByRef dsxo As DataSet, ByVal nombreTabla As String, ByRef dtImportLog As DataTable) As Boolean
        Try
            Dim dt As New DataTable
            Dim oceExport As New ceExportData
            If dsxo.Tables.Contains(nombreTabla) Then
                dsxo.Tables.Remove(nombreTabla)
            End If
            dt = oceExport.agregarEncuestaRespuestas()
            If dt.Rows.Count > 0 Then
                dt.TableName = nombreTabla
                dsxo.Tables.Add(dt)
            Else
                dtImportLog.Rows.Add(New String() {"Advertencia", "Encuestas", "", "xo_dataSet_AgregarEncuestaRespuestas", "No hay informacion para exportar", "xoMobile"})
            End If
            Return True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Encuestas ", "", "xo_dataSet_AgregarEncuestaRespuestas", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function
#End Region


#Region " EXPORTAR BITACORA "
    Public Function xo_dataSet_AgregarBitacora(ByRef dsxo As DataSet, ByVal nombreTabla As String, ByRef dtImportLog As DataTable) As Boolean
        Try
            If dsxo.Tables.Contains(nombreTabla) Then
                dsxo.Tables.Remove(nombreTabla)
            End If
            Dim dt As New DataTable
            Dim oceExport As New ceExportData
            dt = oceExport.agregarBitacora()
            If dt.Rows.Count > 0 Then
                dt.TableName = nombreTabla
                dsxo.Tables.Add(dt)
            Else
                dtImportLog.Rows.Add(New String() {"Advertencia", "Bitacora", "", "xo_dataSet_AgregarBitacora", "No hay informacion para exportar", "xoMobile"})
            End If
            Return True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Bitacora", "", "xo_dataSet_AgregarBitacora", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function
#End Region

#Region " EXPORTAR EFECTIVIDAD "
    Public Function xo_dataSet_AgregarEfectividad(ByRef dsxo As DataSet, ByVal nombreTabla As String, ByRef dtImportLog As DataTable) As Boolean
        Try
            If dsxo.Tables.Contains(nombreTabla) Then
                dsxo.Tables.Remove(nombreTabla)
            End If
            Dim dt As New DataTable
            Dim oceExport As New ceExportData
            dt = oceExport.agregarEfectividad(id_glo_codRuta)
            If dt.Rows.Count > 0 Then
                dt.TableName = nombreTabla
                dsxo.Tables.Add(dt)
            Else
                dtImportLog.Rows.Add(New String() {"Advertencia", "Efectividad", "", "xo_dataSet_AgregarEfectividad", "No hay informacion para exportar", "xoMobile"})
            End If
            Return True
        Catch ex As Exception
            'dtImportLog.Rows.Add(New String() {"Error", "Efectividad ", "", "xo_dataSet_AgregarEfectividad", ex.Message, "xoMobile"})
            Return True
        End Try
    End Function
#End Region

#Region " LIQUIDACION INTEGRACION "
    Public Function xo_dataSet_AgregarIntegracion(ByRef dsxo As DataSet, ByVal nombreTabla As String, ByRef dtImportLog As DataTable) As Boolean
        Try
            If dsxo.Tables.Contains(nombreTabla) Then
                dsxo.Tables.Remove(nombreTabla)
            End If
            Dim dt As New DataTable
            Dim oceExport As New ceExportData
            dt = oceExport.agregarIntegracion()
            If dt.Rows.Count > 0 Then
                dt.TableName = nombreTabla
                dsxo.Tables.Add(dt)
            Else
                dtImportLog.Rows.Add(New String() {"Advertencia", "Encabezado de liquidacion", "", "xo_dataSet_AgregarIntegracion", "No hay informacion para exportar", "xoMobile"})
            End If
            Return True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Encabezado de liquidacion", "", "xo_dataSet_AgregarIntegracion", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function
#End Region

#Region " LIQUIDACION MOVIMIENTO "
    Public Function xo_dataSet_AgregarMovimiento(ByRef dsxo As DataSet, ByVal nombreTabla As String, ByRef dtImportLog As DataTable) As Boolean
        Try
            If dsxo.Tables.Contains(nombreTabla) Then
                dsxo.Tables.Remove(nombreTabla)
            End If
            Dim dt As New DataTable
            Dim objRuta As New InventarioBL
            Dim oceExport As New ceExportData
            dt = objRuta.consultarMovimientoLiquidacion
            If dt.Rows.Count > 0 Then
                dt.TableName = nombreTabla
                dsxo.Tables.Add(dt)
            Else
                dtImportLog.Rows.Add(New String() {"Advertencia", "Liquidacion movimientos", "", "xo_dataSet_AgregarMovimiento", "No hay informacion para exportar", "xoMobile"})
            End If
            Return True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Liquidacion movimientos", "", "xo_dataSet_AgregarMovimiento", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function
#End Region

#Region " LIQUIDACION DIFERENCIA "
    Public Function xo_dataSet_AgregarDiferencia(ByRef dsxo As DataSet, ByVal nombreTabla As String, ByRef dtImportLog As DataTable) As Boolean
        Try
            If dsxo.Tables.Contains(nombreTabla) Then
                dsxo.Tables.Remove(nombreTabla)
            End If
            Dim dt As New DataTable
            Dim oceExport As New ceExportData
            dt = oceExport.agregarDiferencia()
            If dt.Rows.Count > 0 Then
                dt.TableName = nombreTabla
                dsxo.Tables.Add(dt)
            Else
                dtImportLog.Rows.Add(New String() {"Advertencia", "Liquidacion diferencia", "", "xo_dataSet_AgregarEfectividad", "No hay informacion para exportar", "xoMobile"})
            End If
            Return True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Liquidacion diferencia", "", "xo_dataSet_AgregarEfectividad", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function
#End Region

#Region " EXPORTAR A BAPI MAESTRA "

    Public Function xo_dataSet_AgregarMaestro(ByRef dsxo As DataSet, ByVal nombreTabla As String, ByRef dtImportLog As DataTable) As Boolean
        Try
            If dsxo.Tables.Contains(nombreTabla) Then
                dsxo.Tables.Remove(nombreTabla)
            End If
            Dim dt As New DataTable
            If (id_glo_fel = "X") Then
                dt = xo_exp_maestroComercial()
            Else
                dt = xo_exp_maestroComercialSINFEL()
            End If

            If dt.Rows.Count > 0 Then
                dt.TableName = nombreTabla
                dsxo.Tables.Add(dt)
            Else
                dtImportLog.Rows.Add(New String() {"Advertencia", "Documentos comerciales", "", "xo_dataSet_AgregarMaestro", "No hay informacion para exportar", "xoMobile"})
            End If
            Return True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Documentos comerciales", "", "xo_dataSet_AgregarMaestro", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function

    Public Function xo_exp_maestroComercial() As DataTable
        Dim dtMaestroComercial As New DataTable
        Dim dtMaestroComercialSend As New DataTable

        '--- Columna
        dtMaestroComercial.Columns.Add("xo", String.Empty.GetType())
        dtMaestroComercialSend.Columns.Add("xo", String.Empty.GetType())
        xo_exp_notaCreditoDE(dtMaestroComercial)
        xo_exp_notaCreditoABONO(dtMaestroComercial)
        xo_exp_factura(dtMaestroComercial)
        xo_exp_despacho_no_facturado(dtMaestroComercial)
        xo_exp_Recibo(dtMaestroComercial)
        xo_exp_notaCreditoDP(dtMaestroComercial)

        'Agregados para los cambios de producto que se envian como devolucion de inventario
        Try
            'Cambios de producto
            xo_exp_Cambios(dtMaestroComercial)
            'xo_exp_LiquidacionBodega(dtMaestroComercial, "0")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        Try
            xo_exp_LiquidacionBodega(dtMaestroComercial, "0")
            'xo_exp_LiquidacionBodega(dtMaestroComercial, "0")
        Catch ex As Exception
        End Try
        Try
            xo_exp_LiquidacionBodega(dtMaestroComercial, "1")
        Catch ex As Exception
        End Try

        xo_exp_factura(dtMaestroComercial, True)
        Dim ruta As String = cerosIzq(id_glo_codRuta, 4)
        Dim año As String = Date.Today.Year.ToString
        Dim mes As String = cerosIzq(Date.Today.Month.ToString, 2)
        Dim dia As String = cerosIzq(Date.Today.Day.ToString, 2)
        Dim registros As String = (dtMaestroComercial.Rows.Count + 1).ToString
        Dim encabezado As String = ruta & año & mes & dia & registros
        dtMaestroComercialSend.Rows.Add(New String() {encabezado})
        For i As Integer = 0 To dtMaestroComercial.Rows.Count - 1
            dtMaestroComercialSend.Rows.Add(New String() {dtMaestroComercial.Rows(i).Item(0).ToString})
        Next
        Return dtMaestroComercialSend
    End Function

    Public Function xo_exp_maestroComercialSINFEL() As DataTable
        Dim dtMaestroComercial As New DataTable
        Dim dtMaestroComercialSend As New DataTable

        '--- Columna
        dtMaestroComercial.Columns.Add("xo", String.Empty.GetType())
        dtMaestroComercialSend.Columns.Add("xo", String.Empty.GetType())
        xo_exp_notaCreditoDESFEL(dtMaestroComercial)
        xo_exp_notaCreditoNASFEL(dtMaestroComercial)
        xo_exp_facturaSFEL(dtMaestroComercial)
        xo_exp_despacho_no_facturado(dtMaestroComercial)
        xo_exp_Recibo(dtMaestroComercial)
        xo_exp_notaCreditoDPSFEL(dtMaestroComercial)

        'Agregados para los cambios de producto que se envian como devolucion de inventario
        Try
            'Cambios de producto
            xo_exp_Cambios(dtMaestroComercial)
            'xo_exp_LiquidacionBodega(dtMaestroComercial, "0")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


        Try
            xo_exp_LiquidacionBodega(dtMaestroComercial, "0")
            'xo_exp_LiquidacionBodega(dtMaestroComercial, "0")
        Catch ex As Exception
        End Try
        Try
            xo_exp_LiquidacionBodega(dtMaestroComercial, "1")
        Catch ex As Exception
        End Try

        xo_exp_factura(dtMaestroComercial, True)
        Dim ruta As String = cerosIzq(id_glo_codRuta, 4)
        Dim año As String = Date.Today.Year.ToString
        Dim mes As String = cerosIzq(Date.Today.Month.ToString, 2)
        Dim dia As String = cerosIzq(Date.Today.Day.ToString, 2)
        Dim registros As String = (dtMaestroComercial.Rows.Count + 1).ToString
        Dim encabezado As String = ruta & año & mes & dia & registros
        dtMaestroComercialSend.Rows.Add(New String() {encabezado})
        For i As Integer = 0 To dtMaestroComercial.Rows.Count - 1
            dtMaestroComercialSend.Rows.Add(New String() {dtMaestroComercial.Rows(i).Item(0).ToString})
        Next
        Return dtMaestroComercialSend
    End Function

    Public Function xo_exp_factura(ByRef dtCustom As DataTable) As Boolean

        Dim xResp As String = ""
        Dim cliente As New ClienteBL
        Dim dtFacturaEncabezado, dtFacturaArticulo, dtFacturaDetalleArticulo, dtFacturaEncabezadoFel As New DataTable

        '--- Nivel 1 ---'
        dtFacturaEncabezado = objExport.xo_get_factura_Encabezado()
        For i As Integer = 0 To dtFacturaEncabezado.Rows.Count - 1
            Dim item As Integer = 0
            With dtFacturaEncabezado.Rows(i)
                If .Item("estado").ToString = 1 Then
                    '---Documento emitido 
                    cliente.getDetalleDelCliente(.Item("idCliente").ToString)
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString & "|" & .Item("TipoDocumento").ToString & "|" & .Item("idCliente").ToString & "|" & .Item("fechaEmision").ToString & "%" + .Item("tipo_receptor").ToString & "%" & .Item("idreceptor").ToString & "|" & .Item("NumeroFactura").ToString & "|" & id_glo_usuario.ToString & "|" & .Item("codigoPago").ToString & "|" & .Item("CentroL").ToString & "|" & .Item("id_pedido").ToString & "|" & .Item("importe").ToString & "|" & .Item("des_estado").ToString})
                Else
                    '--- Documento anulado
                    cliente.getDetalleDelCliente(.Item("idCliente").ToString)
                    'dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString & "|" & .Item("TipoDocumento").ToString & "|" & .Item("idCliente").ToString & "|" & .Item("fechaEmision").ToString & "|" & .Item("NumeroFactura").ToString & "|" & id_glo_usuario.ToString + "|" & "|" & id_glo_sociedad})
                    'dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString & "|" & .Item("TipoDocumento").ToString & "|" & .Item("idCliente").ToString & "|" & .Item("fechaEmision").ToString & "|" & .Item("NumeroFactura").ToString & "|" & id_glo_usuario.ToString & "|" & "|" & .Item("CentroL").ToString & "|" & .Item("id_pedido").ToString & "|" & .Item("importe").ToString & "|" & .Item("des_estado").ToString})
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString & "|" & .Item("TipoDocumento").ToString & "|" & .Item("idCliente").ToString & "|" & .Item("fechaEmision").ToString & "|" & .Item("NumeroFactura").ToString & "|" & id_glo_usuario.ToString & "|" & .Item("serie").ToString & "|" & .Item("preimpreso").ToString & "|" & .Item("numeroautorizacion").ToString & "|" & .Item("CentroL").ToString & "|" & .Item("importe").ToString & "|" & .Item("des_estado").ToString})
                End If
            End With

            '--- Continuar si el documento no esta anulado
            If dtFacturaEncabezado.Rows(i).Item("estado").ToString = 1 Then
                dtFacturaArticulo = objExport.xo_get_factura_Articulo(dtFacturaEncabezado.Rows(i).Item("fnumero"))
                '--- Nivel 2 ---'
                For j As Integer = 0 To dtFacturaArticulo.Rows.Count - 1
                    item = item + 1
                    With dtFacturaArticulo.Rows(j)
                        dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString & "|" + .Item("NumeroFactura").ToString & "|" & cerosIzq((item * 10).ToString, 4) & "|" & .Item("idproducto").ToString & "|" & .Item("cantidad").ToString & "|" & .Item("um").ToString})
                    End With
                    dtFacturaDetalleArticulo = objExport.xo_get_factura_DetalleArticulo(dtFacturaEncabezado.Rows(i).Item("fnumero"), dtFacturaArticulo.Rows(j).Item("idProducto"))

                    '--- Nivel 3 ---'
                    For k As Integer = 0 To dtFacturaDetalleArticulo.Rows.Count - 1
                        With dtFacturaDetalleArticulo.Rows(k)
                            Dim importe As Decimal = .Item("importe")
                            dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString & "|" + .Item("NumeroFactura").ToString & "|" & cerosIzq((item * 10).ToString, 4) & "|" & .Item("ClaseCondicion").ToString & "|" & Math.Abs(outil.isDecimal(importe)).ToString()})
                        End With
                    Next
                Next

                '--- Nivel 8 ---'
                dtFacturaEncabezadoFel = objExport.xo_get_factura_EncabezadoFel(dtFacturaEncabezado.Rows(i).Item("fnumero"))
                With dtFacturaEncabezadoFel.Rows(0)
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("NumeroFactura").ToString + "|" + .Item("serie").ToString + "|" + .Item("preimpreso").ToString + "|" + .Item("numeroautorizacion").ToString})
                End With

            End If
        Next

    End Function


    Public Function xo_exp_facturaSFEL(ByRef dtCustom As DataTable) As Boolean

        Dim xResp As String = ""
        Dim cliente As New ClienteBL
        Dim dtFacturaEncabezado, dtFacturaArticulo, dtFacturaDetalleArticulo As New DataTable

        '--- Nivel 1 ---'
        dtFacturaEncabezado = objExport.xo_get_factura_Encabezado()
        For i As Integer = 0 To dtFacturaEncabezado.Rows.Count - 1
            Dim item As Integer = 0
            With dtFacturaEncabezado.Rows(i)

                If .Item("estado").ToString = 1 Then
                    '---Documento emitido 
                    cliente.getDetalleDelCliente(.Item("idCliente").ToString)
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString & "|" & .Item("TipoDocumento").ToString & "|" & .Item("idCliente").ToString & "|" & .Item("fechaEmision").ToString & "|" & .Item("NumeroFactura").ToString & "|" & id_glo_usuario.ToString & "|" & .Item("codigoPago").ToString & "|" & .Item("CentroL").ToString & "|" & .Item("id_pedido").ToString & "|" & .Item("importe").ToString & "|" & .Item("des_estado").ToString})
                Else
                    '--- Documento anulado
                    cliente.getDetalleDelCliente(.Item("idCliente").ToString)
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString & "|" & .Item("TipoDocumento").ToString & "|" & .Item("idCliente").ToString & "|" & .Item("fechaEmision").ToString & "|" & .Item("NumeroFactura").ToString & "|" & id_glo_usuario.ToString & "|" & id_glo_sociedad})
                End If
            End With

            '--- Continuar si el documento no esta anulado
            If dtFacturaEncabezado.Rows(i).Item("estado").ToString = 1 Then
                dtFacturaArticulo = objExport.xo_get_factura_Articulo(dtFacturaEncabezado.Rows(i).Item("fnumero"))
                '--- Nivel 2 ---'
                For j As Integer = 0 To dtFacturaArticulo.Rows.Count - 1
                    item = item + 1
                    With dtFacturaArticulo.Rows(j)
                        dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString & "|" + .Item("NumeroFactura").ToString & "|" & cerosIzq((item * 10).ToString, 4) & "|" & .Item("idproducto").ToString & "|" & .Item("cantidad").ToString & "|" & .Item("um").ToString})
                    End With
                    dtFacturaDetalleArticulo = objExport.xo_get_factura_DetalleArticulo(dtFacturaEncabezado.Rows(i).Item("fnumero"), dtFacturaArticulo.Rows(j).Item("idProducto"))

                    '--- Nivel 3 ---'
                    For k As Integer = 0 To dtFacturaDetalleArticulo.Rows.Count - 1
                        With dtFacturaDetalleArticulo.Rows(k)
                            Dim importe As Decimal = .Item("importe")
                            dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString & "|" + .Item("NumeroFactura").ToString & "|" & cerosIzq((item * 10).ToString, 4) & "|" & .Item("ClaseCondicion").ToString & "|" & Math.Abs(outil.isDecimal(importe)).ToString()})
                        End With
                    Next
                Next
            End If
        Next

    End Function

    Public Function xo_exp_Cambios(ByRef dtCustom As DataTable) As Boolean

        Dim xResp As String = ""
        Dim cliente As New ClienteBL
        Dim dtCambioEncabezado, dtCambioArticulo, dtCambioDetalleArticulo As New DataTable

        '--- Nivel 1 ---'
        dtCambioEncabezado = objExport.xo_get_cambio_Encabezado()
        For i As Integer = 0 To dtCambioEncabezado.Rows.Count - 1
            Dim item As Integer = 0
            With dtCambioEncabezado.Rows(i)
                dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("codRuta").ToString + "|" + .Item("fechaEmision").ToString + "|" + .Item("fnumero").ToString + "|" + .Item("idvendedor").ToString + "|" + .Item("codigoPago").ToString + "|" + .Item("centroL").ToString + "|" + .Item("motivo").ToString})
            End With

            '--- Continuar si el documento no esta anulado

            dtCambioArticulo = objExport.xo_get_cambio_Articulo(dtCambioEncabezado.Rows(i).Item("documento"))
            '--- Nivel 2 ---'
            'For j As Integer = 0 To dtCambioArticulo.Rows.Count - 1
            'item = item + 1
            'With dtCambioArticulo.Rows(j)
            ' dtCustom.Rows.Add(New String() {.Item("cabecera").ToString + "|" + dtCambioEncabezado.Rows(0).Item("fNumero").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("idProducto").ToString + "|" + .Item("unidadesFisico").ToString + "|" + .Item("um").ToString})
            'End With
            'Next

            '--- Nivel 2 ---'
            For k As Integer = 0 To dtCambioArticulo.Rows.Count - 1
                item = item + 1
                With dtCambioArticulo.Rows(k)
                    'MsgBox(.Item("cabecera").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("idProducto").ToString + "|" + .Item("cantidad").ToString + "|" + .Item("um").ToString)
                    dtCustom.Rows.Add(New String() {.Item("cabecera").ToString + "|" + dtCambioEncabezado.Rows(i).Item("fNumero").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("idProducto").ToString + "|" + .Item("cantidad").ToString + "|" + .Item("um").ToString})
                End With
            Next

        Next

    End Function

    Public Function xo_exp_despacho_no_facturado(ByRef dtCustom As DataTable) As Boolean

        Dim xResp As String = ""
        Dim cliente As New ClienteBL
        Dim dtFacturaEncabezado, dtFacturaArticulo, dtFacturaDetalleArticulo As New DataTable

        '--- Nivel 1 ---'
        dtFacturaEncabezado = objExport.xo_get_despacho_no_Facturado()
        For i As Integer = 0 To dtFacturaEncabezado.Rows.Count - 1
            Dim item As Integer = 0
            With dtFacturaEncabezado.Rows(i)
                dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("accion").ToString + "|" + .Item("noEntrega").ToString})
            End With
        Next
    End Function

    Public Function xo_exp_factura(ByRef dtCustom As DataTable, ByVal faltantes As Boolean) As Boolean

        Dim xResp As String = ""
        Dim cliente As New ClienteBL
        Dim dtFacturaEncabezado, dtFacturaArticulo, dtFacturaDetalleArticulo, dtNotaCreditoFel As New DataTable

        '--- Nivel 1 ---'
        dtFacturaEncabezado = objExport.xo_get_factura_Encabezado(True)
        For i As Integer = 0 To dtFacturaEncabezado.Rows.Count - 1
            Dim item As Integer = 0
            With dtFacturaEncabezado.Rows(i)
                cliente.getDetalleDelCliente(.Item("idCliente").ToString)
                '+ .Item("codigoPago").ToString + "|"
                dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "%" + .Item("tipo_receptor").ToString + "%" + .Item("idreceptor").ToString + "|" + .Item("NumeroFactura").ToString + "|" + id_glo_usuario.ToString + "|" + "|" + .Item("CentroL").ToString + "|" + .Item("importe").ToString + "|" + .Item("des_estado").ToString})
            End With

            '--- Continuar si el documento no esta anulado
            If dtFacturaEncabezado.Rows(i).Item("estado").ToString = 1 Then
                dtFacturaArticulo = objExport.xo_get_factura_Articulo(dtFacturaEncabezado.Rows(i).Item("fnumero"))
                '--- Nivel 2 ---'
                For j As Integer = 0 To dtFacturaArticulo.Rows.Count - 1
                    item = item + 1
                    With dtFacturaArticulo.Rows(j)
                        dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("NumeroFactura").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("idproducto").ToString + "|" + .Item("cantidad").ToString + "|" + .Item("um").ToString})
                    End With
                    dtFacturaDetalleArticulo = objExport.xo_get_factura_DetalleArticulo(dtFacturaEncabezado.Rows(i).Item("fnumero"), dtFacturaArticulo.Rows(j).Item("idProducto"))

                    '--- Nivel 3 ---'
                    For k As Integer = 0 To dtFacturaDetalleArticulo.Rows.Count - 1
                        With dtFacturaDetalleArticulo.Rows(k)
                            Dim importe As Decimal = .Item("importe")
                            dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("NumeroFactura").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("ClaseCondicion").ToString + "|" + Math.Abs(outil.isDecimal(importe)).ToString()})
                        End With
                    Next
                Next

                '--- Nivel 8 ---'
                dtNotaCreditoFel = objExport.xo_get_factura_EncabezadoFel(dtFacturaEncabezado.Rows(i).Item("fnumero"))
                With dtNotaCreditoFel.Rows(0)
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("NumeroFactura").ToString + "|" + .Item("serie").ToString + "|" + .Item("preimpreso").ToString + "|" + .Item("numeroautorizacion").ToString})
                End With

            End If
        Next

    End Function

    Public Function xo_exp_Recibo(ByRef dtCustom As DataTable) As Boolean

        Dim xResp As String = ""
        Dim cliente As New ClienteBL
        Dim dtRecibo, dtReciboPagos As New DataTable

        dtRecibo = objExport.xo_get_Recibo
        '--- Nivel 1 ---'
        For i As Integer = 0 To dtRecibo.Rows.Count - 1
            Dim item As Integer = 0
            With dtRecibo.Rows(i)
                dtCustom.Rows.Add(New String() {.Item("cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "|" + .Item("numeroRecibo").ToString + "|" + .Item("documentoAfecta").ToString + "|" + id_glo_usuario.ToString() + "|" + .Item("centroL").ToString + "|" + .Item("cuentaC").ToString + "|" + .Item("importe").ToString + "|" + .Item("importe").ToString + "|" + .Item("des_estado").ToString})
            End With
            If dtRecibo.Rows(i).Item("estado").ToString = 1 Then

                '--- Nivel 2 ---'
                dtReciboPagos = objExport.xo_get_recibo_pagos(dtRecibo.Rows(i).Item("rnumero"))

                '--- Cabecera del pago
                'With dtRecibo.Rows(i)
                'dtCustom.Rows.Add(New String() {"5|" + .Item("numeroRecibo").ToString + "|0010|" + .Item("cuentaC").ToString + "|@|" + "|" + .Item("importe").ToString})
                'End With

                '--- Detalle del pago
                For j As Integer = 0 To dtReciboPagos.Rows.Count - 1
                    item = item + 1
                    With dtReciboPagos.Rows(j)
                        dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString & "|" & .Item("numeroRecibo").ToString & "|" & cerosIzq((item * 10).ToString, 4) & "|" & .Item("idCliente").ToString & "|" & .Item("idViaPago").ToString + "||" + .Item("importe").ToString})
                    End With
                Next
            End If
        Next
    End Function

    Public Function xo_exp_notaCreditoDP(ByRef dtCustom As DataTable) As Boolean

        Dim xResp As String = ""
        Dim dtnotaCreditoEncabezado, dtnotaCreditoArticulo, dtnotaCreditoDetalleArticulo, dtNotaCreditoFel As New DataTable

        '--- Nivel 1 ---'
        dtnotaCreditoEncabezado = objExport.xo_get_NotaCreditoDP_Encabezado
        For i As Integer = 0 To dtnotaCreditoEncabezado.Rows.Count - 1
            Dim item As Integer = 0
            With dtnotaCreditoEncabezado.Rows(i)

                If .Item("estado").ToString = 1 Then
                    '---Documento emitido 
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "|" + .Item("numeronNc").ToString + "|" + .Item("documentoAfecta").ToString + "| |" + .Item("centroL").ToString + "|" + .Item("importe").ToString + "|" + .Item("des_estado").ToString})
                Else
                    '--- Documento anulado
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "|" + .Item("numeronNc").ToString + id_glo_usuario.ToString + "|" + .Item("serie").ToString + "|" + .Item("preimpreso").ToString + "|" + .Item("numeroautorizacion").ToString + "|" + .Item("importe").ToString + "|" + .Item("des_estado").ToString})
                End If
            End With
            dtnotaCreditoArticulo = objExport.xo_get_NotaCreditoDP_Articulo(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"))

            '--- Continuar si el documento no esta anulado
            If dtnotaCreditoEncabezado.Rows(i).Item("estado").ToString = 1 Then
                '--- Nivel 2 ---'
                For j As Integer = 0 To dtnotaCreditoArticulo.Rows.Count - 1
                    item = item + 1
                    With dtnotaCreditoArticulo.Rows(j)
                        dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("numeronNc").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("idproducto").ToString + "|" + .Item("cantidad").ToString + "|" + .Item("UM").ToString})
                    End With
                    dtnotaCreditoDetalleArticulo = objExport.xo_get_NotaCreditoDP_DetalleArticulo(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"), dtnotaCreditoArticulo.Rows(j).Item("idProducto"))

                    '--- Nivel 3 ---'
                    For k As Integer = 0 To dtnotaCreditoDetalleArticulo.Rows.Count - 1
                        With dtnotaCreditoDetalleArticulo.Rows(k)
                            dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("NumeroNc").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("claseCondicion").ToString + "|" + .Item("importe").ToString})
                        End With
                    Next
                Next

                '--- Nivel 8 ---'
                dtNotaCreditoFel = objExport.xo_get_notacredito_EncabezadoFel(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"))
                With dtNotaCreditoFel.Rows(0)
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("NumeroFactura").ToString + "|" + .Item("serie").ToString + "|" + .Item("preimpreso").ToString + "|" + .Item("numeroautorizacion").ToString})
                End With

            End If
        Next
    End Function



    Public Function xo_exp_notaCreditoDPSFEL(ByRef dtCustom As DataTable) As Boolean

        Dim xResp As String = ""
        Dim dtnotaCreditoEncabezado, dtnotaCreditoArticulo, dtnotaCreditoDetalleArticulo As New DataTable

        '--- Nivel 1 ---'
        dtnotaCreditoEncabezado = objExport.xo_get_NotaCreditoDP_Encabezado
        For i As Integer = 0 To dtnotaCreditoEncabezado.Rows.Count - 1
            Dim item As Integer = 0
            With dtnotaCreditoEncabezado.Rows(i)

                If .Item("estado").ToString = 1 Then
                    '---Documento emitido 
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "|" + .Item("numeronNc").ToString + "|" + .Item("documentoAfecta").ToString + "| |" + .Item("centroL").ToString + "|" + .Item("importe").ToString + "|" + .Item("des_estado").ToString})
                Else
                    '--- Documento anulado
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "|" + .Item("numeronNc").ToString + id_glo_usuario.ToString + "|" + id_glo_sociedad})
                End If


            End With
            dtnotaCreditoArticulo = objExport.xo_get_NotaCreditoDP_Articulo(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"))

            '--- Continuar si el documento no esta anulado
            If dtnotaCreditoEncabezado.Rows(i).Item("estado").ToString = 1 Then
                '--- Nivel 2 ---'
                For j As Integer = 0 To dtnotaCreditoArticulo.Rows.Count - 1
                    item = item + 1
                    With dtnotaCreditoArticulo.Rows(j)
                        dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("numeronNc").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("idproducto").ToString + "|" + .Item("cantidad").ToString + "|" + .Item("UM").ToString})
                    End With
                    dtnotaCreditoDetalleArticulo = objExport.xo_get_NotaCreditoDP_DetalleArticulo(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"), dtnotaCreditoArticulo.Rows(j).Item("idProducto"))

                    '--- Nivel 3 ---'
                    For k As Integer = 0 To dtnotaCreditoDetalleArticulo.Rows.Count - 1
                        With dtnotaCreditoDetalleArticulo.Rows(k)
                            dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("NumeroNc").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("claseCondicion").ToString + "|" + .Item("importe").ToString})
                        End With
                    Next
                Next
            End If
        Next
    End Function

    Public Function xo_exp_notaCreditoDE(ByRef dtCustom As DataTable) As Boolean

        Dim xResp As String = ""
        Dim dtnotaCreditoEncabezado, dtnotaCreditoArticulo, dtnotaCreditoDetalleArticulo, dtNotaCreditoFel As New DataTable

        '--- Nivel 1 ---'
        dtnotaCreditoEncabezado = objExport.xo_get_NotaCreditoDE_Encabezado
        For i As Integer = 0 To dtnotaCreditoEncabezado.Rows.Count - 1
            Dim item As Integer = 0
            With dtnotaCreditoEncabezado.Rows(i)
                If .Item("estado").ToString = 1 Then
                    '---Documento emitido 
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "%" + .Item("tipo_receptor").ToString + "%" + .Item("idreceptor").ToString + "|" + .Item("numeronNc").ToString + "|" + .Item("documentoAfecta").ToString + "|" + .Item("codigoPago").ToString + "|" + .Item("centroL").ToString + "|" + .Item("importe").ToString + "|" + .Item("des_estado").ToString})
                Else
                    '--- Documento anulado
                    'dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "|" + .Item("numeronNc").ToString + "|" + id_glo_usuario.ToString + "|" + id_glo_sociedad})
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "%" + .Item("tipo_receptor").ToString + "%" + .Item("idreceptor").ToString + "|" + .Item("numeronNc").ToString + id_glo_usuario.ToString + "|" + .Item("serie").ToString + "|" + .Item("preimpreso").ToString + "|" + .Item("numeroautorizacion").ToString + "|" + .Item("importe").ToString + "|" + .Item("des_estado").ToString})
                End If
            End With
            dtnotaCreditoArticulo = objExport.xo_get_NotaCreditoDE_Articulo(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"))

            '--- Nivel 2 ---'
            '--- Continuar si el documento no esta anulado
            If dtnotaCreditoEncabezado.Rows(i).Item("estado").ToString = 1 Then
                For j As Integer = 0 To dtnotaCreditoArticulo.Rows.Count - 1
                    item = item + 1
                    With dtnotaCreditoArticulo.Rows(j)
                        dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("numeronNc").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("idproducto").ToString + "|" + .Item("cantidad").ToString + "|" + .Item("UM").ToString})
                    End With
                    dtnotaCreditoDetalleArticulo = objExport.xo_get_NotaCreditoDE_DetalleArticulo(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"), dtnotaCreditoArticulo.Rows(j).Item("idProducto"))

                    '--- Nivel 3 ---'
                    For k As Integer = 0 To dtnotaCreditoDetalleArticulo.Rows.Count - 1
                        With dtnotaCreditoDetalleArticulo.Rows(k)
                            dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("numeroNc").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("claseCondicion").ToString + "|" + .Item("importe").ToString})
                        End With
                    Next
                Next

                '--- Nivel 8 ---'
                dtNotaCreditoFel = objExport.xo_get_notacredito_EncabezadoFel(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"))
                With dtNotaCreditoFel.Rows(0)
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("NumeroFactura").ToString + "|" + .Item("serie").ToString + "|" + .Item("preimpreso").ToString + "|" + .Item("numeroautorizacion").ToString})
                End With

            End If
        Next
    End Function

    Public Function xo_exp_notaCreditoABONO(ByRef dtCustom As DataTable) As Boolean

        Dim xResp As String = ""
        Dim dtnotaCreditoEncabezado, dtnotaCreditoArticulo, dtnotaCreditoDetalleArticulo, dtNotaCreditoFel As New DataTable

        '--- Nivel 1 ---'
        dtnotaCreditoEncabezado = objExport.xo_get_NotaAbono_Encabezado
        For i As Integer = 0 To dtnotaCreditoEncabezado.Rows.Count - 1
            Dim item As Integer = 0
            With dtnotaCreditoEncabezado.Rows(i)
                If .Item("estado").ToString = 1 Then
                    '---Documento emitido 
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "%" + .Item("tipo_receptor").ToString + "%" + .Item("idreceptor").ToString + "|" + .Item("numeronNc").ToString + "|" + .Item("documentoAfecta").ToString + "|" + .Item("codigoPago").ToString + "|" + .Item("centroL").ToString + "|" + .Item("importe").ToString + "|" + .Item("des_estado").ToString})
                Else
                    '--- Documento anulado
                    'dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "|" + .Item("numeronNc").ToString + "|" + id_glo_usuario.ToString + "|" + id_glo_sociedad})
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "%" + .Item("tipo_receptor").ToString + "%" + .Item("idreceptor").ToString + "|" + .Item("numeronNc").ToString + id_glo_usuario.ToString + "|" + .Item("serie").ToString + "|" + .Item("preimpreso").ToString + "|" + .Item("numeroautorizacion").ToString + "|" + .Item("importe").ToString + "|" + .Item("des_estado").ToString})
                End If
            End With
            dtnotaCreditoArticulo = objExport.xo_get_NotaCreditoDE_Articulo(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"))

            '--- Nivel 2 ---'
            '--- Continuar si el documento no esta anulado
            If dtnotaCreditoEncabezado.Rows(i).Item("estado").ToString = 1 Then
                For j As Integer = 0 To dtnotaCreditoArticulo.Rows.Count - 1
                    item = item + 1
                    With dtnotaCreditoArticulo.Rows(j)
                        dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("numeronNc").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("idproducto").ToString + "|" + .Item("cantidad").ToString + "|" + .Item("UM").ToString})
                    End With
                    dtnotaCreditoDetalleArticulo = objExport.xo_get_NotaCreditoDE_DetalleArticulo(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"), dtnotaCreditoArticulo.Rows(j).Item("idProducto"))
                    'dtnotaCreditoDetalleArticulo = objExport.xo_get_NotaCreditoNA_DetalleArticulo(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"), dtnotaCreditoArticulo.Rows(j).Item("idProducto"))

                    '--- Nivel 3 ---'
                    For k As Integer = 0 To dtnotaCreditoDetalleArticulo.Rows.Count - 1
                        With dtnotaCreditoDetalleArticulo.Rows(k)
                            dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("numeroNc").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("claseCondicion").ToString + "|" + .Item("importe").ToString})
                        End With
                    Next
                Next

                '--- Nivel 8 ---'
                dtNotaCreditoFel = objExport.xo_get_notacredito_EncabezadoFel(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"))
                With dtNotaCreditoFel.Rows(0)
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("NumeroFactura").ToString + "|" + .Item("serie").ToString + "|" + .Item("preimpreso").ToString + "|" + .Item("numeroautorizacion").ToString})
                End With

            End If
        Next
    End Function

    Public Function xo_exp_notaCreditoDESFEL(ByRef dtCustom As DataTable) As Boolean

        Dim xResp As String = ""
        Dim dtnotaCreditoEncabezado, dtnotaCreditoArticulo, dtnotaCreditoDetalleArticulo As New DataTable

        '--- Nivel 1 ---'
        dtnotaCreditoEncabezado = objExport.xo_get_NotaCreditoDE_Encabezado
        For i As Integer = 0 To dtnotaCreditoEncabezado.Rows.Count - 1
            Dim item As Integer = 0
            With dtnotaCreditoEncabezado.Rows(i)
                If .Item("estado").ToString = 1 Then
                    '---Documento emitido 
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "|" + .Item("numeronNc").ToString + "|" + .Item("documentoAfecta").ToString + "|" + .Item("codigoPago").ToString + "|" + .Item("centroL").ToString + "|" + .Item("importe").ToString + "|" + .Item("des_estado").ToString})
                Else
                    '--- Documento anulado
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "|" + .Item("numeronNc").ToString + "|" + id_glo_usuario.ToString + "|" + id_glo_sociedad})
                End If
            End With
            dtnotaCreditoArticulo = objExport.xo_get_NotaCreditoDE_Articulo(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"))

            '--- Nivel 2 ---'
            '--- Continuar si el documento no esta anulado
            If dtnotaCreditoEncabezado.Rows(i).Item("estado").ToString = 1 Then
                For j As Integer = 0 To dtnotaCreditoArticulo.Rows.Count - 1
                    item = item + 1
                    With dtnotaCreditoArticulo.Rows(j)
                        dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("numeronNc").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("idproducto").ToString + "|" + .Item("cantidad").ToString + "|" + .Item("UM").ToString})
                    End With
                    dtnotaCreditoDetalleArticulo = objExport.xo_get_NotaCreditoDE_DetalleArticulo(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"), dtnotaCreditoArticulo.Rows(j).Item("idProducto"))

                    '--- Nivel 3 ---'
                    For k As Integer = 0 To dtnotaCreditoDetalleArticulo.Rows.Count - 1
                        With dtnotaCreditoDetalleArticulo.Rows(k)
                            dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("numeroNc").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("claseCondicion").ToString + "|" + .Item("importe").ToString})
                        End With
                    Next
                Next
            End If
        Next
    End Function

    Public Function xo_exp_notaCreditoNA(ByRef dtCustom As DataTable) As Boolean

        Dim xResp As String = ""
        Dim dtnotaCreditoEncabezado, dtnotaCreditoArticulo, dtnotaCreditoDetalleArticulo, dtNotaCreditoFel As New DataTable

        '--- Nivel 1 ---'
        dtnotaCreditoEncabezado = objExport.xo_get_NotaAbono_Encabezado
        For i As Integer = 0 To dtnotaCreditoEncabezado.Rows.Count - 1
            Dim item As Integer = 0
            With dtnotaCreditoEncabezado.Rows(i)
                If .Item("estado").ToString = 1 Then
                    '---Documento emitido 
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "|" + .Item("numeronNc").ToString + "|" + .Item("documentoAfecta").ToString + "|" + .Item("codigoPago").ToString + "|" + .Item("centroL").ToString + "|" + .Item("importe").ToString + "|" + .Item("des_estado").ToString})
                Else
                    '--- Documento anulado
                    'dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "|" + .Item("numeronNc").ToString + "|" + id_glo_usuario.ToString + "|" + id_glo_sociedad})
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "|" + .Item("numeronNc").ToString + id_glo_usuario.ToString + "|" + .Item("serie").ToString + "|" + .Item("preimpreso").ToString + "|" + .Item("numeroautorizacion").ToString + "|" + .Item("importe").ToString + "|" + .Item("des_estado").ToString})
                End If
            End With
            dtnotaCreditoArticulo = objExport.xo_get_NotaCreditoDE_Articulo(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"))

            '--- Nivel 2 ---'
            '--- Continuar si el documento no esta anulado
            If dtnotaCreditoEncabezado.Rows(i).Item("estado").ToString = 1 Then
                For j As Integer = 0 To dtnotaCreditoArticulo.Rows.Count - 1
                    item = item + 1
                    With dtnotaCreditoArticulo.Rows(j)
                        dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("numeronNc").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("idproducto").ToString + "|" + .Item("cantidad").ToString + "|" + .Item("UM").ToString})
                    End With
                    dtnotaCreditoDetalleArticulo = objExport.xo_get_NotaCreditoDE_DetalleArticulo(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"), dtnotaCreditoArticulo.Rows(j).Item("idProducto"))

                    '--- Nivel 3 ---'
                    For k As Integer = 0 To dtnotaCreditoDetalleArticulo.Rows.Count - 1
                        With dtnotaCreditoDetalleArticulo.Rows(k)
                            dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("numeroNc").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("claseCondicion").ToString + "|" + .Item("importe").ToString})
                        End With
                    Next
                Next

                '--- Nivel 8 ---'
                dtNotaCreditoFel = objExport.xo_get_notacredito_EncabezadoFel(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"))
                With dtNotaCreditoFel.Rows(0)
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("NumeroFactura").ToString + "|" + .Item("serie").ToString + "|" + .Item("preimpreso").ToString + "|" + .Item("numeroautorizacion").ToString})
                End With

            End If
        Next

        
    End Function


    Public Function xo_exp_notaCreditoNASFEL(ByRef dtCustom As DataTable) As Boolean
        Dim xResp As String = ""
        Dim dtnotaCreditoEncabezado, dtnotaCreditoArticulo, dtnotaCreditoDetalleArticulo, dtNotaCreditoFel As New DataTable

        '--- Nivel 1 ---'
        dtnotaCreditoEncabezado = objExport.xo_get_NotaAbono_Encabezado
        For i As Integer = 0 To dtnotaCreditoEncabezado.Rows.Count - 1
            Dim item As Integer = 0
            With dtnotaCreditoEncabezado.Rows(i)
                If .Item("estado").ToString = 1 Then
                    '---Documento emitido 
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "|" + .Item("numeronNc").ToString + "|" + .Item("documentoAfecta").ToString + "|" + .Item("codigoPago").ToString + "|" + .Item("centroL").ToString + "|" + .Item("importe").ToString + "|" + .Item("des_estado").ToString})
                Else
                    '--- Documento anulado
                    'dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "|" + .Item("numeronNc").ToString + "|" + id_glo_usuario.ToString + "|" + id_glo_sociedad})
                    dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("idCliente").ToString + "|" + .Item("fechaEmision").ToString + "|" + .Item("numeronNc").ToString + "|" + id_glo_usuario.ToString + "|" + id_glo_sociedad})
                End If
            End With
            dtnotaCreditoArticulo = objExport.xo_get_NotaCreditoDE_Articulo(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"))

            '--- Nivel 2 ---'
            '--- Continuar si el documento no esta anulado
            If dtnotaCreditoEncabezado.Rows(i).Item("estado").ToString = 1 Then
                For j As Integer = 0 To dtnotaCreditoArticulo.Rows.Count - 1
                    item = item + 1
                    With dtnotaCreditoArticulo.Rows(j)
                        dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("numeronNc").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("idproducto").ToString + "|" + .Item("cantidad").ToString + "|" + .Item("UM").ToString})
                    End With
                    dtnotaCreditoDetalleArticulo = objExport.xo_get_NotaCreditoDE_DetalleArticulo(dtnotaCreditoEncabezado.Rows(i).Item("ncnumero"), dtnotaCreditoArticulo.Rows(j).Item("idProducto"))

                    '--- Nivel 3 ---'
                    For k As Integer = 0 To dtnotaCreditoDetalleArticulo.Rows.Count - 1
                        With dtnotaCreditoDetalleArticulo.Rows(k)
                            dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("numeroNc").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("claseCondicion").ToString + "|" + .Item("importe").ToString})
                        End With
                    Next
                Next
            End If
        Next

    End Function

    Public Function xo_exp_LiquidacionBodega(ByRef dtCustom As DataTable, ByVal ttipo As String) As Boolean

        Dim xResp As String = ""
        Dim cliente As New ClienteBL
        Dim dtBodega, dtBodegaDetalle As New DataTable
        Dim item As Integer

        dtBodega = objExport.xo_get_Devolucion_Encabezado(ttipo)

        '--- Nivel 1 ---'
        For i As Integer = 0 To 0
            With dtBodega.Rows(i)
                dtCustom.Rows.Add(New String() {.Item("Cabecera").ToString + "|" + .Item("TipoDocumento").ToString + "|" + .Item("codRuta").ToString + "|" + .Item("fechaEmision").ToString + "|" + .Item("fnumero").ToString + "|" + .Item("idvendedor").ToString + "|" + .Item("codigoPago").ToString + "|" + .Item("centroL").ToString})
            End With
        Next

        dtBodegaDetalle = objExport.xo_get_Devolucion_Detalle(ttipo)

        '--- Nivel 2 ---'
        For i As Integer = 0 To dtBodegaDetalle.Rows.Count - 1
            item = item + 1
            With dtBodegaDetalle.Rows(i)
                dtCustom.Rows.Add(New String() {.Item("cabecera").ToString + "|" + dtBodega.Rows(0).Item("fNumero").ToString + "|" + cerosIzq((item * 10).ToString, 4) + "|" + .Item("idProducto").ToString + "|" + .Item("unidadesFisico").ToString + "|" + .Item("um").ToString})
            End With
        Next
    End Function

    

#End Region

#Region " EXPORTAR DETALLE DESPACHO "
    Public Function xo_dataSet_AgregarDetalleDespacho(ByRef dsxo As DataSet, ByVal nombreTabla As String, ByRef dtImportLog As DataTable) As Boolean
        Try
            Dim dt As New DataTable
            Dim oceExport As New ceExportData
            If dsxo.Tables.Contains(nombreTabla) Then
                dsxo.Tables.Remove(nombreTabla)
            End If
            dt = oceExport.agregarDetalleDespacho()
            If dt.Rows.Count > 0 Then
                dt.TableName = nombreTabla
                dsxo.Tables.Add(dt)
            Else
                dtImportLog.Rows.Add(New String() {"Advertencia", "Despacho", "", "xo_dataSet_AgregarDetalleDespacho", "No hay informacion para exportar", "xoMobile"})
            End If
            Return True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Despacho ", "", "xo_dataSet_AgregarDetalleDespacho", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function
#End Region

#Region " EXPORTAR EFECTIVIDAD DESPACHO "
    Public Function xo_dataSet_AgregarEfectividadDespacho(ByRef dsxo As DataSet, ByVal nombreTabla As String, ByRef dtImportLog As DataTable) As Boolean
        Try
            Dim dt As New DataTable
            Dim oceExport As New ceExportData
            If dsxo.Tables.Contains(nombreTabla) Then
                dsxo.Tables.Remove(nombreTabla)
            End If
            dt = oceExport.agregarEfectividadDespacho()
            If dt.Rows.Count > 0 Then
                dt.TableName = nombreTabla
                dsxo.Tables.Add(dt)
            Else
                dtImportLog.Rows.Add(New String() {"Advertencia", "Despacho", "", "xo_dataSet_AgregarEfectividadDespacho", "No hay informacion para exportar", "xoMobile"})
            End If
            Return True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Despacho ", "", "xo_dataSet_AgregarEfectividadDespacho", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function
#End Region

#Region " EXPORTAR EFECTIVIDAD COBRO "
    Public Function xo_dataSet_AgregarEfectividadCobro(ByRef dsxo As DataSet, ByVal nombreTabla As String, ByRef dtImportLog As DataTable) As Boolean
        Try
            Dim dt As New DataTable
            Dim oceExport As New ceExportData
            If dsxo.Tables.Contains(nombreTabla) Then
                dsxo.Tables.Remove(nombreTabla)
            End If
            dt = oceExport.agregarEfectividadCobro()
            If dt.Rows.Count > 0 Then
                dt.TableName = nombreTabla
                dsxo.Tables.Add(dt)
            Else
                dtImportLog.Rows.Add(New String() {"Advertencia", "Efectividad Cobro", "", "xo_dataSet_AgregarEfectividadCobro", "No hay informacion para exportar", "xoMobile"})
            End If
            Return True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Efectividad Cobro ", "", "xo_dataSet_AgregarEfectividadCobro", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function
#End Region

#Region " DOCUMENTOS COMPROMISO DE PAGO "
    Public Function xo_dataSet_AgregarCompromiso(ByRef dsxo As DataSet, ByVal nombreTabla As String, ByRef dtImportLog As DataTable) As Boolean
        Try
            If dsxo.Tables.Contains(nombreTabla) Then
                dsxo.Tables.Remove(nombreTabla)
            End If
            Dim dt As New DataTable
            Dim oceExport As New ceExportData
            dt = oceExport.agregarCopromisoPago()
            If dt.Rows.Count > 0 Then
                dt.TableName = nombreTabla
                dsxo.Tables.Add(dt)
            Else
                dtImportLog.Rows.Add(New String() {"Advertencia", "Compromiso de pago", "", "xo_dataSet_AgregarCompromiso", "No hay informacion para exportar", "xoMobile"})
            End If
            Return True
        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Compromiso de pago", "", "xo_dataSet_AgregarCompromiso", ex.Message, "xoMobile"})
            Return False
        End Try
    End Function
#End Region

    Public Function enviarArchivoTexto(ByRef dtImportLog As DataTable) As Boolean
        Dim x As New xoService.xotest
        Dim filename As String
        Dim fileCount As Integer = 0
        Try

            x.Url = "http://" + id_glo_server + "/xoservice/xoTest.asmx"
            For Each entry As String In Directory.GetFiles("\TransientStorage")
                filename = DisplayFileSystemInfoAttributes(New FileInfo(entry))
                Dim fInfo As New FileInfo(filename)
                Dim numBytes As Long = fInfo.Length
                Dim fStream As New FileStream(filename, FileMode.Open, FileAccess.Read)
                Dim br As New BinaryReader(fStream)
                Dim data As Byte() = br.ReadBytes(CInt(numBytes))
                Dim res As String
                res = x.escribirArchivo(data, filename, id_glo_codRuta)
                fileCount = fileCount + 1
                br.Close()
                fStream.Close()
            Next
            dtImportLog.Rows.Add(New String() {"Exito", "Archivos de texto", "", "enviarArchivoTexto", "Se exportaron " + fileCount.ToString() + " archivos,", "xoMobile"})
            Return True

        Catch ex As Exception
            dtImportLog.Rows.Add(New String() {"Error", "Archivos de texto", "", "enviarArchivoTexto", ex.ToString, "xoMobile"})
            Return False
        End Try
    End Function

    Private Function DisplayFileSystemInfoAttributes(ByVal fsi As IO.FileSystemInfo) As String
        ' Assume that this entry is a file.
        Dim entryType As String = "File"

        ' Determine if this entry is really a directory.
        If (fsi.Attributes And FileAttributes.Directory) <> 0 Then
            entryType = "Directory"
        End If

        ' Show this entry's type, name, and creation date.
        Return fsi.FullName
    End Function

#Region " METODOS UTILITARIOS "
    Public Function cerosIzq(ByVal cadena As String, ByVal CantidadZero As Integer) As String
        Dim l As Long
        Dim espacios As Long
        Dim nueva As String
        Dim i As Integer
        l = Len(cadena)                           'longitud real de la cadena
        If l < CantidadZero And l > 0 Then        'completo ancho con espacios
            nueva = Trim(RTrim(cadena))
            espacios = CantidadZero - l           'espacios que necesito para completar el ancho
            For i = 1 To espacios
                nueva = "0" & nueva
            Next i
        Else                                'ajusto ancho al ancho deseado
            nueva = Left(cadena, CantidadZero)
        End If
        cadena = nueva
        Return nueva
    End Function
#End Region

End Class
