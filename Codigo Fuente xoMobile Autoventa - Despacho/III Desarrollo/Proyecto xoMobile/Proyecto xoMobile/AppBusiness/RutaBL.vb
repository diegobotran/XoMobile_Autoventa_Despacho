Imports System.Data
Imports Proyecto_xoMobile_Packs
Imports System.Threading
Imports GpsServiceState


'---ok
Public Class RutaBL
    Dim dtTemporal As New DataTable
    Dim dvRuta As New DataView
    Dim rlayer As New rLayerHandler
    Dim oRuta As New Ruta

    '---<2.0>
    Public Function consultarDiferencias()
        Dim dtDiferencias As New DataTable
        Dim objRuta As New Ruta
        Dim mensaje As New messageCollection
        Dim dtGeneric As New DataTable

        dtGeneric = objRuta.getDiferencias(rlayer)
        rlayer.evaluaTabla(dtGeneric)
        'If rlayer.conError Then
        '    Throw New Exception("No se logro recuperar la informacion de diferencias")
        'End If
        Return dtGeneric
    End Function
    Public Function liquidar(ByVal integracion As IntegracionCO, ByVal lstdiferencias As Windows.Forms.ListView) As Boolean

        '--- Grabar el registro de liquidacion
        Dim objBitacora As New BitacoraBL
        Dim dtGeneric As New DataTable
        Dim mensaje As New messageCollection
        Dim objUtil As New UtilitarioBL
        oRuta.setLiquidacion(integracion, rlayer)
        rlayer.evaluarError()
        If rlayer.conError Then
            Throw New Exception("No se pudo confirmar la liquidacion :(")
        End If

        '--- Registra la operacion como realizada
        objBitacora.registrarOperacion(38, id_glo_cliente)

        '--- Grabar el registro de diferencias
        Dim valor As Decimal
        For i As Integer = 0 To lstdiferencias.Items.Count - 1
            With lstdiferencias.Items.Item(i)
                valor = objUtil.isDecimal(FormatNumber(.SubItems(1).Text, 2))
                oRuta.setDiferenciaLiquidacion(Math.Abs(valor), .SubItems(2).Text, .SubItems(3).Text)
                rlayer.evaluarError()
                If rlayer.conError Then
                    ''--- En estos casos podria colocar un delete para eliminar registros que probablemente se grabaron
                    '' --- alpicar tambien para la preliquidacion
                    Throw New Exception("Error al grabar diferencias de liquidacion:(")
                End If
            End With
        Next
        Return True
    End Function
    '---</2.0>


    Public Function getActiva() As RutaCO
        Dim dtRuta As New DataTable
        Dim objRuta As New Ruta
        Dim ruta As New RutaCO
        Dim npedidos As Integer = 0
        Dim dtNPedidos As New DataTable
        Dim limitecf As Integer = 0
        Dim dtlimite_cf As New DataTable
        dtRuta = objRuta.getRutaActiva()

        Try
            dtNPedidos = objRuta.getNPedidos()
            npedidos = dtNPedidos.Rows(0).Item("npedidos")
        Catch ex As Exception
            npedidos = 100
        End Try

        Try
            dtlimite_cf = objRuta.getLimitecf()
            limitecf = dtlimite_cf.Rows(0).Item("limite")
            id_glo_total_cf = limitecf

        Catch ex As Exception
            limitecf = 2500
            id_glo_total_cf = 2500
        End Try


        If dtRuta.Rows.Count > 0 Then
            ruta.id_ruta = (dtRuta.Rows(0).Item("id_ruta").ToString)
            ruta.codRuta = (dtRuta.Rows(0).Item("codRuta").ToString)
            ruta.tTipo = (dtRuta.Rows(0).Item("tTipo").ToString)
            ruta.nomEmpresa = (dtRuta.Rows(0).Item("nomEmpresa").ToString)
            ruta.direccion = (dtRuta.Rows(0).Item("direccion").ToString)
            ruta.telefono = (dtRuta.Rows(0).Item("telefono").ToString)
            ruta.nit = (dtRuta.Rows(0).Item("nit").ToString)
            ruta.clienteGenerico = (dtRuta.Rows(0).Item("clienteGenerico").ToString)
            ruta.maxVentaCG = (dtRuta.Rows(0).Item("maxVentaCG").ToString)
            ruta.fechaEmision = (dtRuta.Rows(0).Item("fechaEmision").ToString)
            ruta.abreviatura = (dtRuta.Rows(0).Item("abreviatura").ToString)
            ruta.esActual = (dtRuta.Rows(0).Item("esActual"))
            ruta.confirmado = (dtRuta.Rows(0).Item("confirmado"))
            ruta.exportar = (dtRuta.Rows(0).Item("exportar"))
            ruta.importar = (dtRuta.Rows(0).Item("importar"))
            ruta.tipoRuta = (dtRuta.Rows(0).Item("tipoRuta"))
            ruta.tipoRuta = (dtRuta.Rows(0).Item("tipoRuta"))
            ruta.fel = (dtRuta.Rows(0).Item("fel"))
            ruta.sociedad = (dtRuta.Rows(0).Item("sociedad"))
            ruta.cui_hh = (dtRuta.Rows(0).Item("cui_hh"))
            ruta.npedidos = npedidos
            ruta.limitecf = limitecf
            Return ruta
        Else
            Throw New Exception("No exite una ruta activa ")
        End If
    End Function

    Public Function inicializaImportacion() As Boolean
        Dim oRuta As New Ruta
        Dim oImpresion As New ImpresionBL
        oRuta.inicializaImportacion()
        If Not oImpresion.eliminarDirectorio Then
            Throw New Exception("No se pudo eliminar el directorio donde se generan los archivos texto. Proceda a eliminarlo manualmente.")
        End If
        Return True
    End Function

    Public Function actualizar(ByVal ruta As RutaCO) As Boolean
        Dim oRuta As New Ruta
        If oRuta.setActualizar(ruta) = 1 Then
            Return True
        Else
            Throw New Exception("No se pudo actualizar los datos de la ruta.")
            Return False
        End If
    End Function

    Public Function obtenerListadoCargas() As DataTable
        Dim dtRuta As New DataTable
        Dim objRuta As New Ruta
        dtRuta = objRuta.getListadoCargas()
        If dtRuta.Rows.Count > 0 Then
            Return dtRuta
        Else
            Throw New Exception("No hay carga de producto.")
        End If
    End Function

    Public Function obtenerMovimientoLiquidacion() As DataTable
        Dim dtRuta As New DataTable
        Dim objRuta As New Ruta
        dtRuta = objRuta.getMovimientos
        If dtRuta.Rows.Count > 0 Then
            Return dtRuta
        Else
            Throw New Exception("No se pudo cargar datos de movimientos.")
        End If
    End Function

    Public Function ObtenerMovCobros() As DataTable
        Dim dtRuta As New DataTable
        Dim objRuta As New Ruta
        dtRuta = objRuta.getMovimientosCobros
        If dtRuta.Rows.Count > 0 Then
            Return dtRuta
        Else
            Throw New Exception("No se pudo cargar datos de movimientos.")
        End If
    End Function

    Public Function ObtenerEficiencia() As DataTable
        Dim dtRuta As New DataTable
        Dim objRuta As New Ruta
        dtRuta = objRuta.getEficiencia
        If dtRuta.Rows.Count > 0 Then
            Return dtRuta
        Else
            Throw New Exception("No se pudo cargar datos de movimientos.")
        End If
    End Function

    'Public Function obtenerRutaPorIndice(ByVal idRuta As String) As DataTable
    '    Dim dtRuta As New DataTable
    '    Dim objRuta As New Ruta
    '    dtRuta = objRuta.getRutaPorIndice(idRuta)
    '    If dtRuta.Rows.Count > 0 Then
    '        Return dtRuta
    '    Else
    '        Throw New Exception("No hay rutas cargadas")
    '    End If
    'End Function

    Public Function obtenerRutaActiva() As DataTable
        Dim dtRuta As New DataTable
        Dim objRuta As New Ruta
        dtRuta = objRuta.getRutaActiva()
        If dtRuta.Rows.Count > 0 Then
            Return dtRuta
        Else
            Throw New Exception("No hay rutas cargadas")
        End If
        Return Nothing
    End Function

    'Public Function obtenerCargasDelCamion() As DataTable
    '    Dim dtCarga As New DataTable
    '    Dim objRuta As New Ruta
    '    Try
    '        dtCarga = objRuta.getCarga()
    '        If dtCarga.Rows.Count > 0 Then
    '            Return dtCarga
    '        Else
    '            Throw New Exception("No se puede obtener la carga del camion porque no hay informacion almacenada.")
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        Return Nothing
    '    End Try
    'End Function

    Public Function obtenerCargaCamion() As DataTable
        Dim dtCarga As New DataTable
        Dim objRuta As New Ruta
        Try
            dtCarga = objRuta.getCargaCamion()
            If dtCarga.Rows.Count > 0 Then
                Return dtCarga
            Else
                Throw New Exception("No se puede obtener la carga del camion porque no hay informacion almacenada.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function obtenerCargaPorFechaCarga() As DataTable
        Dim dtCarga As New DataTable
        Dim objRuta As New Ruta
        Try
            dtCarga = objRuta.getCargaPorFechaCarga()
            If dtCarga.Rows.Count > 0 Then
                Return dtCarga
            Else
                Throw New Exception("No hay producto en la carga seleccionada.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function existeCargaSinConfirmar() As Boolean
        Dim dtCarga As New DataTable
        Dim objRuta As New Ruta
        Try
            dtCarga = objRuta.getCargaSinConfirmar()
            If dtCarga.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function obtenerDocumentosEmitidos() As DataTable
        Dim dtCarga As New DataTable
        Dim objRuta As New Ruta
        Try
            dtCarga = objRuta.getDocumentosEmitidos()
            If dtCarga.Rows.Count > 0 Then
                Return dtCarga
            Else
                Throw New Exception("No se puede obtener la carga del camion porque no hay informacion almacenada.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function


    Public Function obtenerResumenMarca() As DataTable
        Dim dtCarga As New DataTable
        Dim objRuta As New Ruta
        Try
            dtCarga = objRuta.getResumenMarcas()
            If dtCarga.Rows.Count > 0 Then
                Return dtCarga
            Else
                Throw New Exception("No se puede obtener la carga del camion porque no hay informacion almacenada.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function obtenerSINFEL() As DataTable
        Dim dtCarga As New DataTable
        Dim objRuta As New Ruta
        Try
            dtCarga = objRuta.getResumenSINFEL()
            If dtCarga.Rows.Count > 0 Then
                Return dtCarga
            Else
                Throw New Exception("No se pueden obtener los documentos")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function obtenerResumenPresupuesto() As DataTable
        Dim dtCarga As New DataTable
        Dim objRuta As New Ruta
        Try
            dtCarga = objRuta.getResumenPresupuesto()
            If dtCarga.Rows.Count > 0 Then
                Return dtCarga
            Else
                Throw New Exception("No se puede obtener la carga del camion porque no hay informacion almacenada.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function obtenerResumenPresupuestoD(ByVal diad As String) As DataTable
        Dim dtCarga As New DataTable
        Dim objRuta As New Ruta
        Try
            dtCarga = objRuta.getResumenPresupuestoDias(diad)
            If dtCarga.Rows.Count > 0 Then
                Return dtCarga
            Else
                Throw New Exception("No se puede obtener la carga del camion porque no hay informacion almacenada.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function




    Public Function ObtenerPresupuesto() As DataTable
        Dim dtPresupuesto As New DataTable
        Dim objRuta As New Ruta
        Try
            dtPresupuesto = objRuta.getPresupuesto()

            If dtPresupuesto.Rows.Count > 0 Then

                Return dtPresupuesto
            Else
                Throw New Exception("No se puede obtener datos del presupuesto ya que la ruta no tiene asignado")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        Return dtPresupuesto
    End Function


    Public Function ObtenerPresupuestoDia(ByVal dia As String) As DataTable
        Dim dtPresupuesto As New DataTable
        Dim objRuta As New Ruta
        Try
            dtPresupuesto = objRuta.getPresupuestoDia(dia)
            If dtPresupuesto.Rows.Count > 0 Then
                Return dtPresupuesto
            Else
                Throw New Exception("No se puede obtener datos del presupuesto ya que la ruta no tiene asignado")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)

        End Try
        Return dtPresupuesto
    End Function

    Public Function ObtenerPresupuestoCliente(ByVal idcliente As String) As DataTable
        Dim dtPresupuesto As New DataTable
        Dim objRuta As New Ruta
        Try
            dtPresupuesto = objRuta.getPresupuestoCliente(idcliente)
            If dtPresupuesto.Rows.Count > 0 Then
                Return dtPresupuesto
            Else
                Throw New Exception("No se puede obtener datos del presupuesto ya que la ruta no tiene asignado")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return dtPresupuesto
    End Function

    Public Function obtenerChequesRecibidos() As DataTable
        Dim dtCarga As New DataTable
        Dim objRuta As New Ruta

        dtCarga = objRuta.getChequesRecibidos()
        If dtCarga.Rows.Count > 0 Then
            Return dtCarga
        Else
            Throw New Exception("No se han Recibido cheques.")
        End If
    End Function

    Public Function obtenerIntegracionRep() As DataTable
        Dim dtCarga As New DataTable
        Dim objRuta As New Ruta
        Try
            dtCarga = objRuta.getIntegracionRep()
            If dtCarga.Rows.Count > 0 Then
                Return dtCarga
            Else
                Throw New Exception("No se puede obtener la carga del camion porque no hay informacion almacenada.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function obtenerIntegracion() As DataTable
        Dim dtCarga As New DataTable
        Dim objRuta As New Ruta
        Try
            dtCarga = objRuta.getIntegracion()
            If dtCarga.Rows.Count > 0 Then
                Return dtCarga
            Else
                Throw New Exception("No se puede obtener la carga del camion porque no hay informacion almacenada.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function obtenerCupones() As DataTable
        Dim dtCarga As New DataTable
        Dim objRuta As New Ruta
        Try
            dtCarga = objRuta.getCupones()
            If dtCarga.Rows.Count > 0 Then
                Return dtCarga
            Else
                Throw New Exception("No existen cupones operados")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function obtenerDepositos() As DataTable
        Dim dtCarga As New DataTable
        Dim objRuta As New Ruta
        dtCarga = objRuta.getDepositos()
        Return dtCarga
    End Function

    Public Function obtenerPreguntas(ByVal ttipo As String) As DataTable
        Dim dtCarga As New DataTable
        Dim objRuta As New Ruta
        dtCarga = objRuta.getPreguntas(ttipo)
        If dtCarga.Rows.Count > 0 Then
            Return dtCarga
        Else
            Throw New Exception("No se cargaron preguntas iniciales o finales")
        End If
    End Function

    Public Function responderPregunta(ByVal idPregunta As String, ByVal respuesta As String) As Boolean
        Dim oRuta As New Ruta
        If oRuta.setRespuesta(idPregunta, respuesta) = 1 Then
            Return True
        Else
            Throw New Exception("No se pudo actualizar la respuesta.")
            Return False
        End If
    End Function

    Public Function agregarDeposito(ByVal idBanco As String, ByVal documento As String, ByVal valor As String) As Boolean
        Dim objRuta As New Ruta
        Try
            objRuta.setDeposito(idBanco, documento, valor)
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    Public Function actualizarDeposito(ByVal idDeposito As String, ByVal idBanco As String, ByVal documento As String, ByVal valor As String) As Boolean

        Dim oRuta As New Ruta
        Try
            oRuta.actualizarDeposito(idDeposito, idBanco, documento, valor)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function eliminarDeposito(ByVal idDeposito As String) As Boolean

        Dim oRuta As New Ruta
        Try
            oRuta.eliminarDeposito(idDeposito)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function obtenerEnvaseRecibido() As DataTable
        Dim dtCarga As New DataTable
        Dim objRuta As New Ruta
        dtCarga = objRuta.getEnvaseRecibido()
        If dtCarga.Rows.Count > 0 Then
            Return dtCarga
        Else
            Throw New Exception("No hay informacion sobre Notas de Credito.")
        End If
    End Function

    'Public Function obtenerInventarioVenta() As DataTable
    '    Dim dtCarga As New DataTable
    '    Dim objRuta As New Ruta
    'dtCarga = objRuta.getInventarioVenta()
    '    If dtCarga.Rows.Count > 0 Then
    '        Return dtCarga
    '    Else
    '        Throw New Exception("No hay informacion sobre ventas realizadas.")
    '    End If
    'End Function

    Public Function eliminarRecargaSinConfirmar() As Boolean
        Dim objRuta As New Ruta
        If objRuta.eliminarRecargaSinConfirmar() > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function confirmarCarga(ByVal sd As String) As Boolean
        Dim objRuta As New Ruta
        If objRuta.confirmarRuta(sd) > 0 Then
            Return True
        Else
            Return False
        End If
    End Function


    'Public Function actualizarInventario(ByVal idFactura As String, ByVal tipo As Integer) As Boolean

    '    Dim objDocumentoBL As New DocumentoBL
    '    Dim oRuta As New Ruta
    '    Dim dtFacturaDetalle As New DataTable

    '    '--- Obtener el detalle de la factura
    '    Try
    '        dtFacturaDetalle = objDocumentoBL.getFacturaDetalle(idFactura)
    '        For i As Integer = 0 To dtFacturaDetalle.Rows.Count - 1
    '            oRuta.actualizarInventario(dtFacturaDetalle.Rows(i).Item("idProducto"), dtFacturaDetalle.Rows(i).Item("cantidad") * tipo)
    '        Next
    '        Return True
    '    Catch ex As Exception
    '        Return False
    '    End Try
    'End Function

    Public Function confirmarOperacionComercial() As Boolean
        Dim oRuta As New Ruta
        Try
            oRuta.confirmarOperacionComercial()

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function inicializarXoMobile() As Boolean

        Dim oUtil As New UtilitarioBL
        Dim ruta As New RutaCO
        Dim objBitacora As New BitacoraBL
        Try

            '---- Detener el gps en un proceso separado
            Dim t As New ThreadStart(AddressOf Me.gps_start)
            Dim mt As New Thread(t)
            mt.Start()

            '--- Inicializar variables globales
            ruta = getActiva()



            id_glo_cliente = 0
            id_glo_aplicacion = ""
            id_glo_ruta = ruta.id_ruta
            id_glo_codRuta = ruta.codRuta
            id_glo_sociedad = ruta.sociedad
            id_glo_cui_hh = ruta.cui_hh
            id_glo_npedidos = ruta.npedidos
            id_glo_total_cf = ruta.limitecf
            tipoRuta = ruta.tipoRuta


            co_glo_wfExitoso = False
            co_glo_salir = True
            co_glo_confirma_despacho = False
            co_glo_despacho = False
            id_glo_clienteGenerico = ruta.clienteGenerico

            oUtil.getParametroGeneral("MONEDA", "", co_glo_moneda)

            If (id_glo_sociedad = 4000) Then
                oUtil.getParametroGeneral("IVA", "", co_glo_porcentajeIVA)
            Else
                oUtil.getParametroGeneral2("IVA", "", co_glo_porcentajeIVA)
            End If





            oUtil.getGlobal("co_glo_searchCliente", co_glo_searchCliente)
            oUtil.getGlobal("co_glo_searchProducto", co_glo_searchProducto)
            oUtil.getGlobal("xo_server", id_glo_server)
            oUtil.getGlobal("xo_centro", id_glo_centro)
            oUtil.getGlobal("xo_validaInventario", xo_validaInventario)

            xo_restringeVenta = False
            xo_restringeCobro = False
            xo_restringeAnulacion = False


            If objBitacora.isOperacionRealizada_xo(37) Or objBitacora.isOperacionRealizada_xo(36) Then
                xo_restringeVenta = True
                xo_restringeCobro = True
                xo_restringeAnulacion = True
            End If

            '--- Identifica si el proceso de declaracion de despachos ya fue iniciado
            If objBitacora.isOperacionRealizada_xo(49) Then
                co_glo_despacho = True
            End If

            '--- Identifica si el proceso de confirmacion de despachos ya fue ejecutado
            If objBitacora.isOperacionRealizada_xo(48) Then
                co_glo_confirma_despacho = True
                xo_restringeVenta = True
                xo_restringeCobro = True
                xo_restringeAnulacion = True
            End If

            '--- Aceptar los canales de seguridad SSL
            'System.Net.ServicePointManager.CertificatePolicy = New TrustAllCertificatePolicy()

        Catch ex As Exception
            MsgBox("Ocurrio un error al inicializar xo-mobile " + ex.Message)
        End Try

    End Function

    Public Function obtenerCorrelativos(Optional ByRef cRecibo As CorrelativoCO = Nothing, Optional ByRef cFactura As CorrelativoCO = Nothing, Optional ByRef cNotaCredito As CorrelativoCO = Nothing, Optional ByRef cCambio As CorrelativoCO = Nothing) As DataTable
        Dim oRuta As New Ruta
        Dim dtCorrelativo As New DataTable
        Try
            dtCorrelativo = oRuta.getCorrelativo()
            For i As Integer = 0 To dtCorrelativo.Rows.Count - 1
                Select Case Trim(dtCorrelativo.Rows(i).Item("dtipo"))
                    Case "F"
                        If Not cFactura Is Nothing Then
                            With cFactura
                                .inicial = dtCorrelativo.Rows(i).Item("inicial")
                                .actual = dtCorrelativo.Rows(i).Item("actual") - 1
                                .final = dtCorrelativo.Rows(i).Item("final")
                                .porcentajeConsumido = Math.Abs(((.actual - (.inicial - 1)) * 100) / .final)
                                If .porcentajeConsumido >= 75 Then
                                    xo_LimiteCorrelativoAlcanzado = True
                                End If
                                .quedan = .final - .actual
                                .serie = dtCorrelativo.Rows(i).Item("serie")
                                Select Case .quedan
                                    Case Is <= 0
                                        xo_restringeVenta = True
                                End Select
                            End With
                        End If

                    Case "NC"
                        If Not cNotaCredito Is Nothing Then
                            With cNotaCredito
                                .inicial = dtCorrelativo.Rows(i).Item("inicial")
                                .actual = dtCorrelativo.Rows(i).Item("actual") - 1
                                .final = dtCorrelativo.Rows(i).Item("final")
                                .porcentajeConsumido = Math.Abs(((.actual - (.inicial - 1)) * 100) / .final)
                                If .porcentajeConsumido >= 75 Then
                                    xo_LimiteCorrelativoAlcanzado = True
                                End If
                                .quedan = .final - .actual
                                .serie = dtCorrelativo.Rows(i).Item("serie")
                                Select Case .quedan
                                    Case Is <= 0
                                        xo_restringeVenta = True
                                        xo_restringeCobro = True

                                End Select
                            End With
                        End If
                    Case "R"
                        If Not cRecibo Is Nothing Then
                            With cRecibo
                                .inicial = dtCorrelativo.Rows(i).Item("inicial")
                                .actual = dtCorrelativo.Rows(i).Item("actual") - 1
                                .final = dtCorrelativo.Rows(i).Item("final")
                                .porcentajeConsumido = Math.Abs(((.actual - (.inicial - 1)) * 100) / .final)
                                If .porcentajeConsumido >= 75 Then
                                    xo_LimiteCorrelativoAlcanzado = True
                                End If
                                .quedan = .final - .actual
                                .serie = dtCorrelativo.Rows(i).Item("serie")
                                Select Case .quedan
                                    Case Is <= 0
                                        xo_restringeCobro = True
                                        xo_restringeVenta = True
                                End Select
                            End With
                        End If
                    Case "CD"
                        If Not cCambio Is Nothing Then
                            With cCambio
                                .inicial = dtCorrelativo.Rows(i).Item("inicial")
                                .actual = dtCorrelativo.Rows(i).Item("actual") - 1
                                .final = dtCorrelativo.Rows(i).Item("final")
                                .porcentajeConsumido = Math.Abs(((.actual - (.inicial - 1)) * 100) / .final)
                                If .porcentajeConsumido >= 75 Then
                                    xo_LimiteCorrelativoAlcanzado = True
                                End If
                                .quedan = .final - .actual
                                .serie = dtCorrelativo.Rows(i).Item("serie")
                                Select Case .quedan
                                    Case Is <= 0
                                        xo_restringeCobro = True
                                        xo_restringeVenta = True
                                End Select
                            End With
                        End If
                End Select
            Next
            If Not xo_restringeCobro Then
                If Not xo_restringeVenta Then

                Else
                    MsgBox("No se puede emitir facturas o recibos, sincronize la informacion pendiente")
                End If
            Else
                MsgBox("No se puede emitir facturas o recibos, sincronize la informacion pendiente")
            End If
            Return dtCorrelativo
        Catch ex As Exception
            Return dtCorrelativo
        End Try
    End Function

    Public Function getIndicadoresCorrelativo(ByRef cRecibo As CorrelativoCO, ByRef cFactura As CorrelativoCO, ByRef cNotaCredito As CorrelativoCO, ByRef cCambio As CorrelativoCO) As Boolean
        '--- Obtener correlativos
        Dim dtCorrelativo As New DataTable
        obtenerCorrelativos(cRecibo, cFactura, cNotaCredito, cCambio)
    End Function

    Public Function validarDoctosFELenviado() As Boolean
        Dim dtDoctosFel As New DataTable
        Dim objRuta As New Ruta
        Try
            dtDoctosFel = objRuta.getFElDoctoHH()
            If (dtDoctosFel.Rows.Count > 0) Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return True
        End Try
    End Function

    Private Function gps_start()
        gps.start()
        Return True
    End Function



End Class
