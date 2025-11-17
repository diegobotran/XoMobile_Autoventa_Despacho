Imports System.Data
Imports Proyecto_xoMobile_Packs
'Imports xoMobileGPS



'---ok
Public Class ClienteBL

    'Instancia hacia la capa de datos
    Dim oCliente As New ClienteDT

    'Objetos de Comunicacion entre capas
    Public comCliente As New ClienteCO
    Private oUtilitarioBL As New UtilitarioBL

    Public Function getViasPagoListado(ByVal isContado As Boolean, ByVal soloEfectivo As Boolean) As DataTable
        Dim dtViasPago As New DataTable
        dtViasPago = oCliente.getAllViasPago()
        If dtViasPago.Rows.Count > 0 Then
            Return dtViasPago
        Else
            Return oCliente.getViasPagoGenerico()
            Throw New Exception("No existen formas de pago para este cliente. Utilizara la via generica solo efectivo")
        End If
        Return dtViasPago
    End Function

    Public Function getViasPago(ByRef isContado As Boolean, ByVal soloEfectivo As Boolean) As DataTable
        Dim dtViasPago As New DataTable
        If isContado Then
            If soloEfectivo Then
                dtViasPago = oCliente.getViasPagoGenerico()
            Else
                'Excluir la via de credito
                dtViasPago = oCliente.getViasPagoContado()
            End If
        Else

            ''--- Traer solo credito si es despacho
            'If co_glo_aplicacion = "16" Then
            '    dtViasPago = oCliente.getViaCredito
            'Else
            '    '--- Traer todas las vias si es autoventa
            dtViasPago = oCliente.getAllViasPago
            'MsgBox((dtViasPago.Rows.Count).ToString)

            'End If
        End If

        If dtViasPago.Rows.Count > 0 Then
            Return dtViasPago
        Else
            'MsgBox("Solo se permite el pago en efectivo porque el cliente no tiene la configuracion requerida para el pago.")
            isContado = True
            Return oCliente.getViasPagoGenerico()
        End If
        Return dtViasPago
    End Function

    Public Function aceptaCredito() As Boolean
        Dim dtViasPago As New DataTable
        Dim itemsViaPago() As DataRow
        dtViasPago = getViasPago(False, False)
        itemsViaPago = dtViasPago.Select("id_viaPago = 'CR'")
        If itemsViaPago.Length > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function getDetalleDelCliente(ByVal idCliente As String) As ClienteCO
        Dim dtCliente As New DataTable
        Dim objRlayer As New rLayerHandler

        dtCliente = oCliente.getDetalleCliente(idCliente, objRlayer)
        If Not objRlayer.evaluaTabla(dtCliente) Then
            If objRlayer.codigo = 100 Then MsgBox(objRlayer.texto & " -- " & idCliente)
            Return Nothing
        End If

        comCliente.codigo = (dtCliente.Rows(0).Item("id_cliente").ToString)
        comCliente.nit = dtCliente.Rows(0).Item("nit").ToString
        comCliente.diaVisita = dtCliente.Rows(0).Item("diaVisita").ToString
        comCliente.negocio = RTrim(dtCliente.Rows(0).Item("negocio").ToString)
        comCliente.tipoDi = dtCliente.Rows(0).Item("tipoDi").ToString
        comCliente.numeroDi = dtCliente.Rows(0).Item("numeroDi").ToString
        comCliente.direccion = Trim(dtCliente.Rows(0).Item("direccion").ToString)
        comCliente.telefono = dtCliente.Rows(0).Item("telefono").ToString
        comCliente.patente = dtCliente.Rows(0).Item("patente").ToString
        comCliente.creditoAutorizado = oUtilitarioBL.isDecimal(dtCliente.Rows(0).Item("creditoAutorizado").ToString())

        '--- Obtener el credito Disponible
        Dim objCxc As New DocumentoBL
        Dim saldo As Decimal = 0
        objCxc.getSaldoCliente(comCliente.codigo, saldo)
        Dim CredA As Decimal = oUtilitarioBL.isDecimal2(comCliente.creditoAutorizado)
        comCliente.creditoDisponible = (CredA - saldo).ToString

        'comCliente.creditoDisponible = dtCliente.Rows(0).Item("creditoDisponible").ToString
        comCliente.diasCredito = dtCliente.Rows(0).Item("diasCredito").ToString
        comCliente.diasCreditoPresupuesto = dtCliente.Rows(0).Item("diasCreditoPresupuesto").ToString
        comCliente.categoria = Trim(dtCliente.Rows(0).Item("categoria").ToString)
        'comCliente.ventaConSaldo = dtCliente.Rows(0).Item("ventaConSaldo")
        If (dtCliente.Rows(0).Item("ventaConSaldo").ToString) = "" Then
            comCliente.ventaConSaldo = False
        Else
            comCliente.ventaConSaldo = dtCliente.Rows(0).Item("ventaConSaldo").ToString
        End If

        'comCliente.ventaConSaldov = dtCliente.Rows(0).Item("ventaConSaldov")
        If (dtCliente.Rows(0).Item("ventaConSaldov").ToString) = "" Then
            comCliente.ventaConSaldov = False
        Else
            comCliente.ventaConSaldov = dtCliente.Rows(0).Item("ventaConSaldov").ToString
        End If

        comCliente.volPresupuesto = If(dtCliente.Rows(0).Item("volPresupuesto").ToString = "", 0, dtCliente.Rows(0).Item("volPresupuesto"))
        comCliente.nFaltas = dtCliente.Rows(0).Item("nFaltas").ToString
        comCliente.propietario = Trim(dtCliente.Rows(0).Item("propietario").ToString)
        comCliente.visitado = dtCliente.Rows(0).Item("visitado").ToString
        comCliente.razonNoVisita = dtCliente.Rows(0).Item("razonNoVisita").ToString
        comCliente.condicion = dtCliente.Rows(0).Item("condicion").ToString
        comCliente.razonNoVenta = dtCliente.Rows(0).Item("razonNoVenta").ToString
        comCliente.aceptaCredito = aceptaCredito(comCliente.categoria)
        comCliente.listaPrecio = dtCliente.Rows(0).Item("listaPrecio").ToString
        comCliente.sociedad = dtCliente.Rows(0).Item("sociedad").ToString
        comCliente.oficinaVentas = dtCliente.Rows(0).Item("oficinaVentas").ToString
        comCliente.departamento = dtCliente.Rows(0).Item("departamento").ToString
        comCliente.claseCliente = Trim(dtCliente.Rows(0).Item("claseCliente").ToString)
        comCliente.region_s = Trim(dtCliente.Rows(0).Item("region_s").ToString)
        comCliente.canal = Trim(dtCliente.Rows(0).Item("canal").ToString)
        comCliente.tipoRuta = Trim(dtCliente.Rows(0).Item("tipoRuta").ToString)
        comCliente.n_claseCliente = Trim(dtCliente.Rows(0).Item("n_claseCliente").ToString)
        comCliente.n_region_s = Trim(dtCliente.Rows(0).Item("n_region_s").ToString)
        comCliente.n_canal = Trim(dtCliente.Rows(0).Item("n_canal").ToString)
        comCliente.n_tipoRuta = Trim(dtCliente.Rows(0).Item("n_tipoRuta").ToString)
        comCliente.frecuencia = oUtilitarioBL.isDecimal(dtCliente.Rows(0).Item("frecuencia").ToString)
        comCliente.giro = dtCliente.Rows(0).Item("ramo").ToString()
        comCliente.latitud = dtCliente.Rows(0).Item("latitud").ToString()
        comCliente.longitud = dtCliente.Rows(0).Item("longitud").ToString()
        comCliente.vale_bon = dtCliente.Rows(0).Item("vale_bon").ToString
        comCliente.cambio = dtCliente.Rows(0).Item("reclamo").ToString
        comCliente.devolucion = dtCliente.Rows(0).Item("devolucion").ToString
        'comCliente.georeferencia = Trim(dtCliente.Rows(0).Item("georeferencia").ToString)
        Return comCliente
    End Function

    Public Function getClientesConSaldo() As DataTable
        Dim dtClientesSaldo As New DataTable
        dtClientesSaldo = oCliente.getClientesConSaldo()
        If dtClientesSaldo.Rows.Count() > 0 Then
            Return dtClientesSaldo
        Else
            Throw New Exception("No hay clientes con saldo pendiente en esta ruta")
        End If
    End Function

    Public Function permiteAtencionConSaldo(ByVal v_comCliente As ClienteCO, ByVal tipoAtencion As String) As Boolean
        Dim oRecibo As New Recibo
        Dim message As New messageCollection
        Dim dtDocumentosPorCobrar As New DataTable
        Dim Npedidos As New DataTable
        Dim pedidos As Integer = 0
        dtDocumentosPorCobrar = oRecibo.getDocumentosPorcobrar()

        Try
            Npedidos = oRecibo.getNPedidosCliente(v_comCliente.codigo)
            pedidos = Npedidos.Rows(0).Item("pedidos")

        Catch ex As Exception
            Return False
        End Try

        If pedidos >= id_glo_npedidos Then
            MsgBox("Llego al limite de pedidos que se pueden generar...")
            Return False
        End If

        If dtDocumentosPorCobrar.Rows.Count() > 0 Then
            If Not v_comCliente.ventaConSaldo Then
                message.raiseMensaje(600, tipoAtencion)
                Return False
            End If
        End If

        If dtDocumentosPorCobrar.Select("diasVencidos > 0 ").Length > 0 Then
            If Not v_comCliente.ventaConSaldov Then
                message.raiseMensaje(600, tipoAtencion)
                Return False
            End If
        End If

        'Agregar condicion para validar el limite de crédito
        'If dtDocumentosPorCobrar.Select("

        Return True
    End Function

    Public Function actualizarEstatusVisita(ByVal cliente As ClienteCO, ByVal estatus As String) As Boolean
        Dim oCliente As New ClienteDT
        cliente.visitado = estatus
        If oCliente.editar(cliente) = 1 Then
            Return True
        Else
            Throw New Exception("No se pudo actualizar el estado de visita.")
            Return False
        End If
    End Function

    Public Function actualizaNIT_CUI(ByVal cliente As ClienteCO) As Boolean
        Dim oCliente As New ClienteDT
        If oCliente.editarDatosFEL(cliente) = 1 Then
            Return True
        Else
            Throw New Exception("No se pudo actualizar el estado de visita.")
            Return False
        End If
    End Function

    Public Function actualizarRazonNovisita(ByVal cliente As ClienteCO, ByVal idRazon As String, ByVal tipoRazon As Integer) As Boolean

        Dim oCliente As New ClienteDT
        If tipoRazon = 1 Then

            '---Esta declarando la Razon por la cual no visito
            cliente.razonNoVisita = Trim(idRazon)
        ElseIf tipoRazon = 2 Then
            '---Esta declarando la Razon por la que no vendio
            cliente.razonNoVenta = Trim(idRazon)
        End If

        If oCliente.editar(cliente) = 1 Then
            Return True
        Else
            Throw New Exception("No se pudo actualizar la razon de no visita.")
            Return False
        End If
    End Function

    Public Function actualizarCreditoDisponible(ByRef cliente As ClienteCO, ByVal valor As Decimal) As Boolean
        cliente.creditoDisponible = oUtilitarioBL.isDecimal(cliente.creditoDisponible) + valor
        oCliente.editar(cliente)
    End Function

    Public Function aceptaCredito(ByVal categoria As String) As Boolean
        Dim dtViasPago As New DataTable
        Dim itemsViaPago() As DataRow

        '--- La categoria 8 no tiene credito.
        If Trim(categoria) = "08" Then
            Return False
        End If

        '--- Buscar si el cliente tiene asignado la via de pago CR.
        dtViasPago = getViasPago(False, False)
        itemsViaPago = dtViasPago.Select("id_viaPago = 'CR'")
        If itemsViaPago.Length > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function filtrarSegmento(ByVal dtGeneric As DataTable, ByVal cliente As ClienteCO) As DataTable

        Dim dtMatch, dtCascade As New DataTable
        Dim vMatch, vCascade As New DataView
        Dim filter As String = ""
        Dim filterAplicado As Boolean = False

        Try
            '--- Inicializar tablas


            '--- Obtener todos los registros igual a la segmentacion de cliente
            filter &= "     sociedad        = '" & cliente.sociedad & "'"
            filter &= " and idOficinaVentas = '" & cliente.oficinaVentas & "'"
            filter &= " and idDepartamento  = '" & cliente.departamento & "'"
            filter &= " and idClaseCliente  = '" & cliente.claseCliente & "'"
            filter &= " and idRegion        = '" & cliente.region_s & "'"
            filter &= " and idCanal         = '" & cliente.canal & "'"
            filter &= " and idTipoRuta      = '" & cliente.tipoRuta & "'"

            '--- Los registros que cumplen con el filtro se marcan como mostrar = si
            vMatch = dtGeneric.Copy.DefaultView
            vMatch.RowFilter = filter
            dtMatch = vMatch.ToTable

            '--- Obtener todos los registros que tengan campos nulos
            filter = " sociedad = 0 or idOficinaVentas = '' or idDepartamento = '' or idClaseCliente ='' or  idRegion = '' or idCanal = '' or  idTipoRuta ='' "
            vCascade = dtGeneric.Copy.DefaultView
            vCascade.RowFilter = filter
            dtCascade = vCascade.ToTable

            For i = 0 To dtCascade.Rows.Count - 1

                If Not (dtCascade.Rows(i).Item("sociedad").ToString() = "") And Not dtCascade.Rows(i).Item("sociedad") = cliente.sociedad Then
                    dtCascade.Rows(i)("display") = 0
                    GoTo NextIteration
                End If

                If Not (dtCascade.Rows(i).Item("idOficinaVentas").ToString() = "") And Not dtCascade.Rows(i).Item("idOficinaVentas") = cliente.oficinaVentas Then
                    dtCascade.Rows(i)("display") = 0
                    GoTo NextIteration
                End If

                If Not (dtCascade.Rows(i).Item("idDepartamento").ToString() = "") And Not dtCascade.Rows(i).Item("idDepartamento") = cliente.departamento Then
                    dtCascade.Rows(i)("display") = 0
                    GoTo NextIteration
                End If

                If Not (dtCascade.Rows(i).Item("idClaseCliente").ToString() = "") And Not dtCascade.Rows(i).Item("idClaseCliente") = cliente.claseCliente Then
                    dtCascade.Rows(i)("display") = 0
                    GoTo NextIteration
                End If

                If Not (dtCascade.Rows(i).Item("idRegion").ToString() = "") And Not dtCascade.Rows(i).Item("idRegion") = cliente.region_s Then
                    dtCascade.Rows(i)("display") = 0
                    GoTo NextIteration
                End If

                If Not (dtCascade.Rows(i).Item("idCanal").ToString() = "") And Not dtCascade.Rows(i).Item("idCanal") = cliente.canal Then
                    dtCascade.Rows(i)("display") = 0
                    GoTo NextIteration
                End If

                If Not (dtCascade.Rows(i).Item("idTipoRuta").ToString() = "") And Not dtCascade.Rows(i).Item("idTipoRuta") = cliente.tipoRuta Then
                    dtCascade.Rows(i)("display") = 0
                    GoTo NextIteration
                End If

                '--- Si llego hasta aqui entonces mostrar = si
                dtCascade.Rows(i)("display") = 1
                filterAplicado = True
NextIteration:
            Next
            vCascade = dtCascade.DefaultView
            vCascade.RowFilter = "display = 1"
            dtCascade = vCascade.ToTable
            'If Not filterAplicado Then Return Nothing
        Catch ex As Exception
            Return dtGeneric
        End Try

        '--- Combinar tabla match con tabla cascada
        dtMatch.Merge(dtCascade)
        Return dtMatch

    End Function
 
    Public Function crear(ByVal dtcliente As DataTable) As Boolean
        Dim objCliente As New ClienteDT
        If objCliente.crear(dtcliente) = 1 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function crearOnline(ByVal nombre As String, ByVal negocio As String, ByVal nit As String, ByVal departamento As String, ByVal municipio As String, ByVal direccion As String, ByVal dpi As String, ByVal crealocal As Boolean, ByVal giro As String, Optional ByRef sapID As String = "", Optional ByVal latitud As String = "0", Optional ByVal longitud As String = "0", Optional ByVal raise As Boolean = True) As Boolean

        '--- Crear cliente en linea
        Dim dsxo As New DataSet
        Dim objeto As New xoService.ServiceHollaBack
        Dim objCore As New ceClient
        Dim objService As New xoService.xotest
        Dim objExport As New ceExportLogic
        Dim objCliente As New ClienteBL
        Dim negocio2 As String = ""
        Dim dtCliente As New DataTable
        Dim xoExportLog As New DataSet
        Dim xoRespuesta As String = ""

        '---- Codigo aleatorio
        Dim codigo As Integer = CInt(Int((Len(negocio) * 10000 * Rnd()) + 1))

        'id_glo_server = "192.9.2.86"
        'id_glo_codRuta = 184

        '--- Inicializar URL del objeto de conexion      
        If Not objCore.construyeURL(False, objService, id_glo_server, id_glo_codRuta) Then objCore.configuraParametros()

        '--- Obtener coordenadas GPS
        If (latitud.ToString.Length > 2 And longitud.ToString.Length > 2) Then
            gps.Latitude = latitud
            gps.Longitude = longitud
        Else
            Try
                gps.start()

                '--- Default values if no GPS is captured.
                If gps.Latitude = "" Then gps.Latitude = "0"
                If gps.Longitude = "" Then gps.Longitude = "0"
                If Len(gps.Latitude) > 15 Then gps.Latitude = gps.Latitude.Substring(0, 15)
                If Len(gps.Longitude) > 15 Then gps.Longitude = gps.Longitude.Substring(0, 15)

            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If



        '--- Partir nombre del negocio 
        If Len(negocio) > 40 Then
            negocio2 = negocio.Substring(40)
            negocio = negocio.Substring(0, 40)
        End If

        '--- Partir nombre del propietario
        If Len(nombre) > 40 Then
            nombre = nombre.Substring(0, 40)
        End If



        '--- Crear tabla para enviar datos
        dtCliente.Columns.Add("CLIE_HH", String.Empty.GetType())
        dtCliente.Columns.Add("NOMBRE1", String.Empty.GetType())
        dtCliente.Columns.Add("NOMBRE2", String.Empty.GetType())
        dtCliente.Columns.Add("NOMBRE3", String.Empty.GetType())
        dtCliente.Columns.Add("DIRECCION", String.Empty.GetType())
        dtCliente.Columns.Add("POBLAC", String.Empty.GetType())
        dtCliente.Columns.Add("NIT", String.Empty.GetType())
        dtCliente.Columns.Add("LATITUDE", String.Empty.GetType())
        dtCliente.Columns.Add("LONGITUDE", String.Empty.GetType())
        dtCliente.Columns.Add("DPI", String.Empty.GetType())
        dtCliente.Columns.Add("GIRO", String.Empty.GetType())
        dtCliente.Rows.Add(New String() {codigo, negocio, negocio2, nombre, direccion, municipio & "-" & departamento, nit, gps.Latitude, gps.Longitude, dpi, giro})



        Try
            '--- Comprobar la Conexion
            If objExport.comprobarConexion(objService, False) Then

                '--- Grabar cliente remoto
                dtCliente.TableName = "THH"
                dsxo.Tables.Add(dtCliente)
                objeto = objService.xoFun_ClienteNuevo(dsxo, objeto.mensaje, id_glo_codRuta, id_glo_sociedad)

                '--- Obtener el codigo de SAP
                If objeto.datos.Tables.Contains("THH_R") Then
                    'MsgBox("Se creo el cliente con codigo SAP: " & vbCrLf & objeto.datos.Tables(0).Rows(0).Item(0).ToString)
                    sapID = objeto.datos.Tables(0).Rows(0).Item(0).ToString
                Else
                    If raise Then MsgBox(objeto.datos.Tables(0).Rows(0).Item(0).ToString & vbCrLf & objeto.datos.Tables(0).Rows(0).Item(4).ToString)
                End If
            End If

            '--- Grabar localmente al cliente 
            If crealocal Then
                If objCliente.crear(dtCliente) Then
                    cambiarResponsable(dtCliente.Rows(0).Item(0), sapID)
                End If
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)

            '--- Grabar localmente al cliente
            If crealocal Then
                If objCliente.crear(dtCliente) Then
                    Return True
                End If
            End If
        End Try
    End Function
    Public Function getNuevos(ByRef dtClientes As DataTable) As DataTable
        Dim rlayer As New rLayerHandler
        dtClientes = oCliente.getNuevos(rlayer)
        If Not rlayer.evaluaTabla(dtClientes) Then
            Return Nothing
        End If
        Return dtClientes
    End Function
    Public Function cambiarResponsable(ByVal clienteT As String, ByVal clienteSAP As String) As Integer
        Try
            If clienteSAP = "" Then Return True
            Dim objCliente As New ClienteDT
            objCliente.cambiarResponsable(clienteT, clienteSAP)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

End Class

