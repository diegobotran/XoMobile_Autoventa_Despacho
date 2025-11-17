' ***** Importe de Librerías y utilitarios del sistema *****
Imports PW.JSON
Imports System
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO
Imports System.Net
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlServerCe
Imports System.Globalization
Imports Proyecto_xoMobile_Packs

' **** Fin Declaración de Librerias *****

Public Class Generador
    Dim objDocumentoBL As New DocumentoBL
    Dim objCliente As New ClienteBL
    Dim Documento As New felCabecera
    Dim objFactura As New Factura
    Dim Detalle As New FelDetalle
    Dim Respuesta As New felRespuesta
    Dim request As HttpWebRequest
    Dim response As HttpWebResponse = Nothing
    Dim reader As StreamReader
    Dim address As Uri
    Dim appId As String
    Dim context As String
    Dim query As String
    Dim data As StringBuilder
    Dim byteData() As Byte
    Dim postStream As Stream = Nothing
    Dim objNotaCreditoDT As New NotaCreditoDT
    Public comNotaCreditoEnv As New documentoCO
    '--- Objetos de la capa de negocios
    Dim objUtilBL As New UtilitarioBL

    Public Function test() As Boolean
        Dim PostData As String = ""
        Try
            Dim request As WebRequest = WebRequest.Create("HTTP" & "://" + "192.9.2.56" + "/NAUTILUSQA/v1/test")
            PostData = [String].Format("test={0}", "TEST")
            request.Credentials = CredentialCache.DefaultCredentials
            CType(request, HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
            request.Method = "POST"
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(PostData)
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()
            Dim response As WebResponse = request.GetResponse()
            'dataStream = response.GetResponseStream()
            Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            dataStream.Close()
            response.Close()
            MessageBox.Show(responseFromServer)
        Catch ex As Exception
            Throw New Exception("No hay conectividad para facturación electrónica, favor verifique los parametros de conexion.")
            Return False
        End Try
        '--- El servicio responde de forma esperada

        Return True
    End Function

    Public Function anulacion(ByVal serie As String, ByVal preimpreso As String, ByVal nit As String, ByVal fechaAnulacion As String, ByVal motivo As String) As Boolean
        Try
            Dim dtCredenciales As DataTable
            Dim servidorFEL As String = ""
            Dim servicioFEL As String = ""
            Dim protocoloFEL As String = ""

            dtCredenciales = objDocumentoBL.credencialesServidor()
            servidorFEL = dtCredenciales.Rows(0).Item("SERVIDOR")
            servicioFEL = dtCredenciales.Rows(0).Item("SERVICIO")
            protocoloFEL = dtCredenciales.Rows(0).Item("PROTOCOLO")
            '*** Declaración de Variables de la función ***
            Dim PostData As String = ""
            If id_glo_protocolo = "https" Then
                servidorFEL = ""
                protocoloFEL = id_glo_protocolo
            End If
            'Envio de la Ubicación de donde se esta Generando la Factura Electronica
            Try
                gps.start()
                '--- Default values if no GPS is captured.
                If gps.Latitude = "" Then gps.Latitude = "0"
                If gps.Longitude = "" Then gps.Longitude = "0"
                If Len(gps.Latitude) > 15 Then gps.Latitude = gps.Latitude.Substring(0, 15)
                If Len(gps.Longitude) > 15 Then gps.Longitude = gps.Longitude.Substring(0, 15)
            Catch ex As Exception
                gps.stop()
            End Try

            id_glo_api = "7e3e0ab330fffd33f244089444ac5db7"

            Dim request As WebRequest = WebRequest.Create(protocoloFEL & "://" + servidorFEL + "/" + servicioFEL + "/v1/anuladocumento")
            '****** Conectividad al servicio Nautilus ******
            PostData = [String].Format("sociedad={0}&serie={1}&preimpreso={2}&nit={3}&fechaAnulacion={4}&motivo={5}&latitud={6}&longitud={7}", id_glo_sociedad, serie, preimpreso, nit, Format(CDate(fechaAnulacion), "dd/MM/yyyy"), motivo, gps.Latitude, gps.Longitude)
            request.Headers.Add("Authorization", id_glo_api)
            request.Credentials = CredentialCache.DefaultCredentials
            CType(request, HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
            request.Method = "POST"
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(PostData)
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()
            Dim response As WebResponse = request.GetResponse()
            'dataStream = response.GetResponseStream()
            Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            dataStream.Close()
            response.Close()
            'MessageBox.Show(responseFromServer)
            displayAnulacion(responseFromServer)
            Return True
        Catch ex As Exception
            MessageBox.Show("Error al anulador documento " + ex.Message())
            Return False
        End Try

    End Function


    Public Function generador(ByVal OrdenVenta As documentoCO, ByVal Cliente As ClienteCO) As Boolean
        '*** Declaración de Variables de la función ***
        Dim vBruto As Double = 0
        Dim vDescuento As Double = 0
        Dim vExento As Double = 0
        Dim vOtros As Double = 0
        Dim vNeto As Double = 0
        Dim vIsr As Double = 0
        Dim vIva As Double = 0
        Dim vTotal As Double = 0
        Dim dBruto, dDescuento, dDescuentoDPP, dTotal, dNeto, dIva, dPorcentaje, dOtros, dprecioUnidad As Double
        Dim item As ItemCO
        Dim factura As New documentoCO      'Variable del tipo data Table
        Dim dtFactura As DataTable
        Dim objEncabezado As String = ""
        Dim objDetalle As String = ""
        Dim PostData As String = ""
        Dim dtCredenciales As DataTable
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL
        Dim comCliente As New ClienteCO
        Dim servidorFEL As String = ""
        Dim servicioFEL As String = ""
        Dim protocoloFEL As String = ""
        dtCredenciales = objDocumentoBL.credencialesServidor()
        servidorFEL = Trim(dtCredenciales.Rows(0).Item("SERVIDOR"))
        servicioFEL = Trim(dtCredenciales.Rows(0).Item("SERVICIO"))
        protocoloFEL = Trim(dtCredenciales.Rows(0).Item("PROTOCOLO"))

        If id_glo_protocolo = "https" Then
            servidorFEL = ""
            protocoloFEL = id_glo_protocolo
        End If
        'Envio de la Ubicación de donde se esta Generando la Factura Electronica
        Try
            gps.start()
            '--- Default values if no GPS is captured.
            If gps.Latitude = "" Then gps.Latitude = "0"
            If gps.Longitude = "" Then gps.Longitude = "0"
            If Len(gps.Latitude) > 15 Then gps.Latitude = gps.Latitude.Substring(0, 15)
            If Len(gps.Longitude) > 15 Then gps.Longitude = gps.Longitude.Substring(0, 15)
        Catch ex As Exception
            gps.stop()
        End Try

        Dim servicios As String = "/v1/generadocumentodlsa"


        'Dim request As WebRequest = WebRequest.Create("http://192.9.0.36:8080/nautilusp/v1/generadocumento")
        If id_glo_sociedad = 7000 Then
            servicios = "/v1/generadocumentodlsa2"
        Else
            servicios = "/v1/generadocumentodlsa"
        End If

        Dim request As WebRequest = WebRequest.Create("http" & "://" + servidorFEL + "/" + servicioFEL + servicios)

        'If (id_glo_internet = False) Then
        'Else
        'Dim request As WebRequest = WebRequest.Create("http" & "://" + "3.23.160.55:41062/www" + "/" + servicioFEL + "/v1/generadocumentodlsa2")
        'End If

        Try
            Cursor.Current = Cursors.Default
            'Lectura de datos Detalle Facturación
            dtFactura = objDocumentoBL.getFacturaDetalle(OrdenVenta.idEncabezado)
            factura = objDocumentoBL.getFactura(OrdenVenta.idEncabezado)
            comCliente = objCliente.getDetalleDelCliente(id_glo_cliente)
            Cliente.nit = comCliente.nit
            Cliente.numeroDi = comCliente.numeroDi
            '*************************************************************************



            Dim registros = 0
            Dim Pagocontado As Integer = 0
            Dim PagoCredito As Integer = 0
            Dim PagoCheque As Integer = 0

            registros = dtFactura.Rows.Count

            If (registros = 1) Then
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0

                    item = objDocumentoBL.getFacturaByItem(dtFactura.Rows(i).Item("item").ToString, dtFactura.Rows(i))
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())
                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************
                    'dprecioUnidad = objUtilBL.isDecimal4((item.precio) / (comProducto.unidadesCaja))
                    dprecioUnidad = objUtilBL.isDecimal6((item.importe) / (item.cantidad))
                    dBruto = objUtilBL.isDecimal(item.cantidad * dprecioUnidad)

                    If (item.porcentajeDesto < 0) Then
                        dPorcentaje = objUtilBL.isDecimal4(((item.importeDestoFEL + item.valorIvaDesto) * 100) / dBruto) * (-1)
                    Else
                        dPorcentaje = objUtilBL.isDecimal4(((item.importeDestoFEL + item.valorIvaDesto) * 100) / dBruto)
                    End If

                    objDocumentoBL.actualizarDetalleFact(dtFactura.Rows(i).Item("id_detFactura").ToString, OrdenVenta.idEncabezado, dtFactura.Rows(i).Item("idProducto").ToString, objUtilBL.isDecimal(dBruto), objUtilBL.isDecimal(dBruto / 1.12))

                    dDescuento = objUtilBL.isDecimal4(item.importeDesto * (-1) + item.valorIvaDesto * (-1))
                    dPorcentaje = objUtilBL.isDecimal4(dDescuento * 100 / dBruto)
                    dNeto = objUtilBL.isDecimal((dBruto - dDescuento) / 1.12)
                    dIva = objUtilBL.isDecimal4(dBruto - dNeto - dDescuento)
                    dTotal = objUtilBL.isDecimal(dNeto + dIva)
                    'Un Ajuston 
                    'dNeto = dBruto - dDescuento - dIva
                    'dBruto = dDescuento + dNeto + dIva - dOtros
                    '*************************** FIN CALCULO GUATEFACTURAS  ***************************

                    vBruto = vBruto + dBruto
                    vDescuento = vDescuento + dDescuento
                    vExento = vExento + 0
                    vOtros = vOtros + dOtros
                    vNeto = vNeto + dNeto
                    vIsr = vIsr + 0
                    vIva = vIva + dIva
                    vTotal = vTotal + dTotal

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.CANTIDADGRAVABLE = 0
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = objUtilBL.isDecimal(item.cantidad.ToString())
                    Detalle.PRECIO = objUtilBL.isDecimal(item.precio) / objUtilBL.isDecimal(comProducto.unidadesCaja)
                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = 0
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                Next
            Else
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0

                    item = objDocumentoBL.getFacturaByItem(dtFactura.Rows(i).Item("item").ToString, dtFactura.Rows(i))
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())
                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************
                    'dprecioUnidad = objUtilBL.isDecimal4((item.precio) / (comProducto.unidadesCaja))
                    dprecioUnidad = objUtilBL.isDecimal6((item.importe) / (item.cantidad))
                    dBruto = objUtilBL.isDecimal(item.cantidad * dprecioUnidad)

                    'objDocumentoBL.actualizarDetalleFact(dtFactura.Rows(i).Item("id_detFactura").ToString, OrdenVenta.idEncabezado, dtFactura.Rows(i).Item("idProducto").ToString, objUtilBL.isDecimal(dBruto), objUtilBL.isDecimal(dBruto / 1.12))

                    If (item.importeDesto = 0) Then
                        dDescuento = 0
                        dPorcentaje = 0
                    Else
                        'dPorcentaje = objUtilBL.isDecimal4(((item.importeDesto + item.valorIvaDesto) * 100) / dBruto) * (-1)

                        dDescuento = objUtilBL.isDecimal(item.importeDesto * (-1) + item.valorIvaDesto * (-1))
                        dPorcentaje = objUtilBL.isDecimal4(dDescuento * 100 / dBruto)
                    End If



                    dNeto = objUtilBL.isDecimal((dBruto - dDescuento) / 1.12)
                    dIva = objUtilBL.isDecimal(dBruto - dNeto - dDescuento)
                    dTotal = objUtilBL.isDecimal(dNeto + dIva)
                    'Un Ajuston 
                    'dNeto = dBruto - dDescuento - dIva
                    'dBruto = dDescuento + dNeto + dIva - dOtros
                    '*************************** FIN CALCULO GUATEFACTURAS  ***************************

                    vBruto = vBruto + dBruto
                    vDescuento = vDescuento + dDescuento
                    vExento = vExento + 0
                    vOtros = vOtros + dOtros
                    vNeto = vNeto + dNeto
                    vIsr = vIsr + 0
                    vIva = vIva + dIva
                    vTotal = vTotal + dTotal

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.CANTIDADGRAVABLE = 0
                    Detalle.DESCRIPCION = Trim(item.descripcion.ToString())
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = objUtilBL.isDecimal(item.cantidad.ToString())
                    Detalle.PRECIO = objUtilBL.isDecimal(item.precio) / objUtilBL.isDecimal(comProducto.unidadesCaja)
                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = 0
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    If (i = 0) Then
                        objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                    Else
                        If (i = dtFactura.Rows.Count - 1) Then
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                        Else
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                        End If
                    End If
                Next
            End If



            ' ******* Asignación de Datos de Facturación Documento Encabezado *******
            Documento.NITRECEPTOR = Cliente.nit
            Documento.CUIRECEPTOR = Cliente.numeroDi

            Try
                Dim texto As String = ""
                texto = Replace(Cliente.nit, "-", "")
                Cliente.nit = Replace(texto, "/", "")
            Catch ex As Exception
                Cliente.nit = "CF"
            End Try



            'El cliente tiene NIT
            If Cliente.nit <> "CF" Then
                Documento.TIPORECEPTOR = 4
                Documento.IDRECEPTOR = Cliente.nit
            Else
                'el cliente tiene CF en su dato maestro
                If vTotal <= id_glo_total_cf Then
                    Documento.TIPORECEPTOR = 4
                    Documento.IDRECEPTOR = Cliente.nit
                Else
                    If Len(Cliente.numeroDi) > 0 Then
                        If Cliente.numeroDi.Substring(0, 1) <> "P" Then
                            Documento.TIPORECEPTOR = 2
                            Documento.IDRECEPTOR = Cliente.numeroDi
                        Else
                            Try
                                Dim quitarP As String = ""
                                quitarP = Replace(Cliente.numeroDi, "P", "")
                                Documento.TIPORECEPTOR = 3
                                Documento.IDRECEPTOR = quitarP
                            Catch ex As Exception
                                Documento.TIPORECEPTOR = 4
                                Documento.IDRECEPTOR = Cliente.nit
                            End Try
                        End If
                    End If
                End If
            End If
            'Else
            'Esta modificacion se agrego de ultimo momento.
            'Documento.TIPORECEPTOR = 4
            'Documento.IDRECEPTOR = Cliente.nit
            'End If

            Documento.NOMBRE = Cliente.negocio
            Documento.DIRECCION = Cliente.direccion
            Documento.TIPOVENTA = "B"
            Documento.DESTINOVENTA = 1
            Documento.FECHA = Format(CDate(factura.fechaEmision), "dd/MM/yyyy")
            Documento.MONEDA = 1
            Documento.TASA = 1.0
            Documento.REFERENCIA = factura.numero

            If (Len(factura.numeroacceso) > 0) Then
                Documento.NUMEROACCESO = factura.numeroacceso
            Else
                Documento.NUMEROACCESO = ""
            End If

            Documento.SERIEADMIN = factura.serie
            Documento.NUMEROADMIN = factura.numero
            Documento.REVERSION = "N"
            Documento.BRUTO = vBruto
            Documento.DESCUENTO = vDescuento
            Documento.EXENTO = vExento
            Documento.OTROS = vOtros
            Documento.NETO = vNeto
            Documento.ISR = vIsr
            Documento.IVA = vIva
            Documento.TOTAL = vTotal
            Documento.NUMEROABONO = 0
            If (factura.fechaVence Is DBNull.Value) Then
                Documento.FECHAVENCIMIENTO = Format(CDate(factura.fechaVence), "yyyyMMdd")
            Else
                Documento.FECHAVENCIMIENTO = Format(CDate(factura.fechaEmision), "yyyyMMdd")
            End If
            Documento.MONTOABONO = 0

            ' ******************************************************************************

            id_glo_api = "7e3e0ab330fffd33f244089444ac5db7"

            ' ********************************************************* Convertir Objeto a JSON *********************************************************
            objEncabezado = "{""ENCABEZADO"":" & PW.JSON.JSONHelper.ObjectToString(Documento) & ",""DETALLE"":["
            objDetalle = objDetalle & "]}"
            '****** Conectividad al servicio Nautilus ******
            'objEncabezado = QuitarCaracteres(objEncabezado)
            'objDetalle = QuitarCaracteres(objDetalle)
            objEncabezado = objEncabezado.Replace("&", " ")
            objDetalle = objDetalle.Replace("&", " ")
            'gps.Latitude
            'gps.Longitude
            PostData = [String].Format("encabezado={0}&detalle={1}&sociedad={2}&tipo={3}&ruta={4}&serie={5}&correlativo={6}&latitud={7}&longitud={8}", objEncabezado, objDetalle, id_glo_sociedad, 2, id_glo_codRuta, factura.serie, factura.numero, 0, 0)
            'PostData = [String].Format("encabezado={0}&detalle={1}" + objEncabezado
            request.Headers.Add("Authorization", id_glo_api)
            request.Credentials = CredentialCache.DefaultCredentials
            CType(request, HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
            request.Method = "POST"
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(PostData)
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()
            Dim response As WebResponse = request.GetResponse()
            'dataStream = response.GetResponseStream()
            Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            dataStream.Close()
            response.Close()


            If display(responseFromServer, OrdenVenta.idEncabezado) Then
                '********* INICIO LOGICA DE AJUSTE ****** 
                objDocumentoBL.UpdateTipoReceptor(Documento.TIPORECEPTOR, Documento.IDRECEPTOR, OrdenVenta.idEncabezado)
                Return True
            End If
            ' *************************************************** Fin de la rutina de comunicacion ****************************************************
        Catch ex As Exception
            'generador3(OrdenVenta.idEncabezado, Cliente)
            MsgBox(ex.Message.ToString)
            MsgBox("No se pudo generar FACTURA ELECTRONICA")
            Return False
        End Try
        'TextBox1.Text = PW.JSON.JSONHelper.ObjectToString(objprova)
    End Function




    Public Function generadorNC(ByVal NotaCredito As documentoCO, ByRef Cliente As ClienteCO) As Boolean
        '*** Declaración de Variables de la función ***
        Dim vBruto As Double = 0
        Dim vDescuento As Double = 0
        Dim vExento As Double = 0
        Dim vOtros As Double = 0
        Dim vNeto As Double = 0
        Dim vIsr As Double = 0
        Dim vIva As Double = 0
        Dim vTotal As Double = 0
        Dim dBruto, dDescuento, dTotal, dNeto, dIva, dPorcentaje, dOtros, dprecioUnidad As Double
        Dim item As ItemCO
        Dim factura As New documentoCO      'Variable del tipo data Table
        Dim origen As New documentoCO
        Dim dtFactura As DataTable
        Dim objEncabezado As String = ""
        Dim objDetalle As String = ""
        Dim PostData As String = ""
        Dim dtCredenciales As DataTable

        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL
        Dim UnidadMedidas As String = ""
        Dim servidorFEL As String = ""
        Dim servicioFEL As String = ""
        Dim protocoloFEL As String = ""
        dtCredenciales = objDocumentoBL.credencialesServidor()
        servidorFEL = dtCredenciales.Rows(0).Item("SERVIDOR")
        servicioFEL = dtCredenciales.Rows(0).Item("SERVICIO")
        protocoloFEL = dtCredenciales.Rows(0).Item("PROTOCOLO")

        If id_glo_protocolo = "https" Then
            servidorFEL = ""
            protocoloFEL = id_glo_protocolo
        End If
        'Envio de la Ubicación de donde se esta Generando la Factura Electronica
        Try
            gps.start()
            '--- Default values if no GPS is captured.
            If gps.Latitude = "" Then gps.Latitude = "0"
            If gps.Longitude = "" Then gps.Longitude = "0"
            If Len(gps.Latitude) > 15 Then gps.Latitude = gps.Latitude.Substring(0, 15)
            If Len(gps.Longitude) > 15 Then gps.Longitude = gps.Longitude.Substring(0, 15)
        Catch ex As Exception
            gps.stop()
        End Try


        'Dim request As WebRequest = WebRequest.Create("http://192.9.2.56/NAUTILUSQA/v1/generadocumentonc")
        'Dim request As WebRequest = WebRequest.Create("HTTP" & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentonc")

        Dim request As WebRequest = WebRequest.Create("HTTP" & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentonc")


        Try
            Cursor.Current = Cursors.Default
            'Lectura de datos Detalle Facturación
            Dim Pagocontado As Integer = 0
            Dim PagoCredito As Integer = 0
            Dim PagoCheque As Integer = 0
            Dim Diferencias As Decimal = 0
            Dim IDRECIBO As Integer = 0
            dtFactura = objDocumentoBL.getNotaCreditoDetalle2(NotaCredito.idEncabezado)
            factura = objDocumentoBL.getNotaCredito(NotaCredito.idEncabezado)

            '*************************************************************************
            Dim registros = 0
            registros = dtFactura.Rows.Count

            If (registros = 1) Then
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0

                    item = objDocumentoBL.getNotaCreditoByItem(dtFactura.Rows(i))

                    'Traer el precio por caja del material
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())

                    'dtClienteListaPrecio = objDocumentoBL.ListaPrecioCliente(factura.idCliente)

                    'dtListaPrecio = objDocumentoBL.ListaPrecioProducto(item.idProducto.ToString(), dtClienteListaPrecio.Rows(0).Item("LISTAPRECIO"))

                    'UnidadMedidas = dtListaPrecio.Rows(0).Item("UM").ToString()

                    dprecioUnidad = objUtilBL.isDecimal6((item.importe) / (item.cantidad))
                    dBruto = objUtilBL.isDecimal(item.cantidad * dprecioUnidad)

                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************
                    If (item.importeDesto = 0) Then
                        dDescuento = 0
                        dPorcentaje = 0
                    Else
                        dPorcentaje = item.porcentajeDesto
                        dDescuento = (objUtilBL.isDecimal(dBruto * (dPorcentaje / 100)))
                    End If
                    dTotal = dBruto - dDescuento
                    dNeto = objUtilBL.isDecimal(dTotal / 1.12)
                    dIva = objUtilBL.isDecimal(dNeto * 0.12)
                    'Un Ajuston 
                    dNeto = dBruto - dDescuento - dIva
                    dBruto = dDescuento + dNeto + dIva - dOtros
                    '*************************** FIN CALCULO GUATEFACTURAS  ***************************

                    vBruto = objUtilBL.isDecimal2(vBruto) + dBruto
                    vDescuento = objUtilBL.isDecimal2(vDescuento) + dDescuento
                    vExento = objUtilBL.isDecimal2(vExento) + 0
                    vOtros = objUtilBL.isDecimal2(vOtros) + dOtros
                    vNeto = objUtilBL.isDecimal2(vNeto) + dNeto
                    vIsr = vIsr + 0
                    vIva = objUtilBL.isDecimal2(vIva) + dIva
                    vTotal = objUtilBL.isDecimal2(vTotal) + objUtilBL.isDecimal2(dBruto - dDescuento)

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = objUtilBL.isDecimal(item.cantidad.ToString())

                    Detalle.PRECIO = dprecioUnidad

                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = 0
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                Next


            Else
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0

                    item = objDocumentoBL.getNotaCreditoByItem(dtFactura.Rows(i))

                    'Traer el precio por caja del material
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())

                    dprecioUnidad = objUtilBL.isDecimal6((item.importe) / (item.cantidad))
                    dBruto = objUtilBL.isDecimal(item.cantidad * dprecioUnidad)


                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************
                    If (item.importeDesto = 0) Then
                        dDescuento = 0
                        dPorcentaje = 0
                    Else
                        dPorcentaje = item.porcentajeDesto
                        dDescuento = (objUtilBL.isDecimal(dBruto * (dPorcentaje / 100)))
                    End If


                    dTotal = dBruto - dDescuento
                    dNeto = objUtilBL.isDecimal(dTotal / 1.12)
                    dIva = objUtilBL.isDecimal(dNeto * 0.12)
                    'Un Ajuston 
                    dNeto = dBruto - dDescuento - dIva
                    dBruto = dDescuento + dNeto + dIva - dOtros
                    '*************************** FIN CALCULO GUATEFACTURAS  ***************************

                    vBruto = objUtilBL.isDecimal2(vBruto) + dBruto
                    vDescuento = objUtilBL.isDecimal2(vDescuento) + dDescuento
                    vExento = objUtilBL.isDecimal2(vExento) + 0
                    vOtros = objUtilBL.isDecimal2(vOtros) + dOtros
                    vNeto = objUtilBL.isDecimal2(vNeto) + dNeto
                    vIsr = vIsr + 0
                    vIva = objUtilBL.isDecimal2(vIva) + dIva
                    vTotal = objUtilBL.isDecimal2(vTotal) + objUtilBL.isDecimal2(dBruto - dDescuento)

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = objUtilBL.isDecimal(item.cantidad.ToString())

                    'If (UnidadMedidas = "UN") Then
                    ' Detalle.PRECIO = objUtilBL.isDecimal(item.precio * 1.12)
                    ' Else
                    ' Detalle.PRECIO = objUtilBL.isDecimal(item.precio * 1.12) / objUtilBL.isDecimal(comProducto.unidadesCaja)
                    'End If
                    Detalle.PRECIO = dprecioUnidad

                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = 0
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    If (i = 0) Then
                        objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                    Else
                        If (i = dtFactura.Rows.Count - 1) Then
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                        Else
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                        End If
                    End If
                Next


            End If



            ' ******* Asignación de Datos de Facturación Documento Encabezado *******
            Documento.NITRECEPTOR = Cliente.nit
            Try
                origen = objDocumentoBL.getFactura(factura.idEncFacturaRelacionada)
                Documento.TIPORECEPTOR = origen.tipoReceptor
                Documento.IDRECEPTOR = origen.idreceptor
            Catch ex As Exception
                Documento.TIPORECEPTOR = "4"
                Documento.IDRECEPTOR = Cliente.nit
            End Try
            

            Documento.NOMBRE = Cliente.negocio
            Documento.DIRECCION = Cliente.direccion
            Documento.TIPOVENTA = "B"
            Documento.DESTINOVENTA = 1
            Documento.FECHA = Format(CDate(factura.fechaEmision), "dd/MM/yyyy")
            Documento.MONEDA = 1
            Documento.TASA = 1.0
            Documento.REFERENCIA = factura.numero
            Documento.NUMEROACCESO = ""
            Documento.SERIEADMIN = factura.serie
            Documento.NUMEROADMIN = factura.numero
            Documento.REVERSION = "N"
            Documento.BRUTO = vBruto
            Documento.DESCUENTO = vDescuento
            Documento.EXENTO = vExento
            Documento.OTROS = vOtros
            Documento.NETO = vNeto
            Documento.ISR = vIsr
            Documento.IVA = vIva
            Documento.TOTAL = vTotal
            Documento.NUMEROABONO = 0
            Documento.FECHAVENCIMIENTO = ""
            Documento.MONTOABONO = 0
            Documento.SERIEFEL = NotaCredito.serieFEL.Substring(0, 8)
            Documento.PREIMPRESO = NotaCredito.preimpreso

            'Actualizar el ajuste de la nota de crédito en recibo_pagos

            ' ******************************************************************************

            id_glo_api = "7e3e0ab330fffd33f244089444ac5db7"

            ' ********************************************************* Convertir Objeto a JSON *********************************************************
            objEncabezado = "{""ENCABEZADO"":" & PW.JSON.JSONHelper.ObjectToString(Documento) & ",""DETALLE"":["
            objDetalle = objDetalle & "]}"
            '****** Conectividad al servicio Nautilus ******
            PostData = [String].Format("encabezado={0}&detalle={1}&sociedad={2}&tipo={3}&ruta={4}&serie={5}&correlativo={6}&latitud={7}&longitud={8}", objEncabezado, objDetalle, id_glo_sociedad, 2, id_glo_codRuta, factura.serie, factura.numero, gps.Latitude, gps.Longitude)
            request.Headers.Add("Authorization", id_glo_api)
            request.Credentials = CredentialCache.DefaultCredentials
            CType(request, HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
            request.Method = "POST"
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(PostData)
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()
            Dim response As WebResponse = request.GetResponse()
            'dataStream = response.GetResponseStream()
            Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            dataStream.Close()
            response.Close()
            'MessageBox.Show(responseFromServer)

            If displayNC(responseFromServer, NotaCredito.idEncabezado) Then
                objDocumentoBL.UpdateTipoReceptorNC(Documento.TIPORECEPTOR, Documento.IDRECEPTOR, NotaCredito.idEncabezado)
                Return True
            End If
            Return True
            ' *************************************************** Fin de la rutina de comunicacion ****************************************************
        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
            MsgBox("No se pudo generar NOTA DE CREDITO")
            Return False
        End Try
        'TextBox1.Text = PW.JSON.JSONHelper.ObjectToString(objprova)
    End Function

    Public Function generadorNCCXC(ByVal NotaCredito As documentoCO, ByRef Cliente As ClienteCO) As Boolean
        '*** Declaración de Variables de la función ***
        Dim vBruto As Double = 0
        Dim vDescuento As Double = 0
        Dim vExento As Double = 0
        Dim vOtros As Double = 0
        Dim vNeto As Double = 0
        Dim vIsr As Double = 0
        Dim vIva As Double = 0
        Dim vTotal As Double = 0
        Dim dBruto, dDescuento, dTotal, dNeto, dIva, dPorcentaje, dOtros, dprecioUnidad As Double
        Dim item As ItemCO
        Dim factura As New documentoCO      'Variable del tipo data Table
        Dim cxc As New documentoCO
        Dim dtFactura As DataTable
        Dim objEncabezado As String = ""
        Dim objDetalle As String = ""
        Dim PostData As String = ""
        Dim dtCredenciales As DataTable
        Dim dtFEL As DataTable
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL


        Dim UnidadMedidas As String = ""

        Dim servidorFEL As String = ""
        Dim servicioFEL As String = ""
        Dim protocoloFEL As String = ""
        dtCredenciales = objDocumentoBL.credencialesServidor()
        servidorFEL = dtCredenciales.Rows(0).Item("SERVIDOR")
        servicioFEL = dtCredenciales.Rows(0).Item("SERVICIO")
        protocoloFEL = dtCredenciales.Rows(0).Item("PROTOCOLO")

        If id_glo_protocolo = "https" Then
            servidorFEL = ""
            protocoloFEL = id_glo_protocolo
        End If
        'Envio de la Ubicación de donde se esta Generando la Factura Electronica
        Try
            gps.start()
            '--- Default values if no GPS is captured.
            If gps.Latitude = "" Then gps.Latitude = "0"
            If gps.Longitude = "" Then gps.Longitude = "0"
            If Len(gps.Latitude) > 15 Then gps.Latitude = gps.Latitude.Substring(0, 15)
            If Len(gps.Longitude) > 15 Then gps.Longitude = gps.Longitude.Substring(0, 15)
        Catch ex As Exception
            gps.stop()
        End Try


        'Dim request As WebRequest = WebRequest.Create("http://192.9.2.56/NAUTILUSQA/v1/generadocumentonc")
        'Dim request As WebRequest = WebRequest.Create("HTTP" & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentonc")

        Dim request As WebRequest = WebRequest.Create("HTTP" & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentonc")

        Try
            Cursor.Current = Cursors.Default
            'Lectura de datos Detalle Facturación

            dtFactura = objDocumentoBL.getNotaCreditoDetalle(NotaCredito.idEncabezado)
            factura = objDocumentoBL.getNotaCredito(NotaCredito.idEncabezado)


            '*************************************************************************
            Dim registros = 0
            registros = dtFactura.Rows.Count

            If (registros = 1) Then
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0
                    item = objDocumentoBL.getNotaCreditoByItem(dtFactura.Rows(i))
                    'Traer el precio por caja del material
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())
                    dprecioUnidad = objUtilBL.isDecimal4((item.importe) / (item.cantidad))
                    dBruto = objUtilBL.isDecimal4(item.cantidad * dprecioUnidad)
                    If (item.porcentajeDesto < 0) Then
                        dPorcentaje = objUtilBL.isDecimal4(((item.importeDesto + item.valorIvaDesto) * 100) / dBruto) * (-1)
                    Else
                        dPorcentaje = objUtilBL.isDecimal4(((item.importeDesto + item.valorIvaDesto) * 100) / dBruto)
                    End If
                    dDescuento = objUtilBL.isDecimal4(item.importeDesto + item.valorIvaDesto) * (-1)
                    dPorcentaje = objUtilBL.isDecimal4(dDescuento * 100 / dBruto)
                    dNeto = objUtilBL.isDecimal((dBruto - dDescuento) / 1.12)
                    dIva = objUtilBL.isDecimal4(dBruto - dNeto - dDescuento)
                    dTotal = objUtilBL.isDecimal(dNeto + dIva)
                    vBruto = vBruto + dBruto
                    vDescuento = vDescuento + dDescuento
                    vExento = vExento + 0
                    vOtros = vOtros + dOtros
                    vNeto = vNeto + dNeto
                    vIsr = vIsr + 0
                    vIva = vIva + dIva
                    vTotal = vTotal + dTotal

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = objUtilBL.isDecimal(item.cantidad.ToString())

                    If (UnidadMedidas = "UN") Then
                        Detalle.PRECIO = objUtilBL.isDecimal(item.precio * 1.12)
                    Else
                        Detalle.PRECIO = objUtilBL.isDecimal(item.precio * 1.12) / objUtilBL.isDecimal(comProducto.unidadesCaja)
                    End If

                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = 0
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                Next
            Else
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0

                    item = objDocumentoBL.getNotaCreditoByItem(dtFactura.Rows(i))

                    'Traer el precio por caja del material
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())

                    dprecioUnidad = objUtilBL.isDecimal4((item.importe) / (item.cantidad))
                    dBruto = objUtilBL.isDecimal4(item.cantidad * dprecioUnidad)

                    If (item.porcentajeDesto < 0) Then
                        dPorcentaje = objUtilBL.isDecimal4(((item.importeDesto + item.valorIvaDesto) * 100) / dBruto) * (-1)
                    Else
                        dPorcentaje = objUtilBL.isDecimal4(((item.importeDesto + item.valorIvaDesto) * 100) / dBruto)
                    End If

                    dDescuento = objUtilBL.isDecimal4(item.importeDesto + item.valorIvaDesto) * (-1)
                    dPorcentaje = objUtilBL.isDecimal4(dDescuento * 100 / dBruto)
                    dNeto = objUtilBL.isDecimal((dBruto - dDescuento) / 1.12)
                    dIva = objUtilBL.isDecimal4(dBruto - dNeto - dDescuento)
                    dTotal = objUtilBL.isDecimal(dNeto + dIva)


                    vBruto = vBruto + dBruto
                    vDescuento = vDescuento + dDescuento
                    vExento = vExento + 0
                    vOtros = vOtros + dOtros
                    vNeto = vNeto + dNeto
                    vIsr = vIsr + 0
                    vIva = vIva + dIva
                    vTotal = vTotal + dTotal

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = objUtilBL.isDecimal(item.cantidad.ToString())

                    If (UnidadMedidas = "UN") Then
                        Detalle.PRECIO = objUtilBL.isDecimal(item.precio * 1.12)
                    Else
                        Detalle.PRECIO = objUtilBL.isDecimal(item.precio * 1.12) / objUtilBL.isDecimal(comProducto.unidadesCaja)
                    End If

                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = 0
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    If (i = 0) Then
                        objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                    Else
                        If (i = dtFactura.Rows.Count - 1) Then
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                        Else
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                        End If
                    End If
                Next
            End If



            ' ******* Asignación de Datos de Facturación Documento Encabezado *******
            Documento.NITRECEPTOR = Cliente.nit
            Documento.CUIRECEPTOR = ""
            Try
                dtFEL = objDocumentoBL.getCXCFEL(factura.idReciboRelacionado)
                Documento.TIPORECEPTOR = dtFEL.Rows(0).Item("tipoidrecep")
                Documento.IDRECEPTOR = dtFEL.Rows(0).Item("idrecep")
            Catch ex As Exception
                Documento.TIPORECEPTOR = "4"
                Documento.IDRECEPTOR = Cliente.nit
            End Try

            Documento.NOMBRE = Cliente.negocio
            Documento.DIRECCION = Cliente.direccion
            Documento.TIPOVENTA = "B"
            Documento.DESTINOVENTA = 1
            Documento.FECHA = Format(CDate(factura.fechaEmision), "dd/MM/yyyy")
            Documento.MONEDA = 1
            Documento.TASA = 1.0
            Documento.REFERENCIA = factura.numero
            Documento.NUMEROACCESO = ""
            Documento.SERIEADMIN = factura.serie
            Documento.NUMEROADMIN = factura.numero
            Documento.REVERSION = "N"
            Documento.BRUTO = vBruto
            Documento.DESCUENTO = vDescuento
            Documento.EXENTO = vExento
            Documento.OTROS = vOtros
            Documento.NETO = vNeto
            Documento.ISR = vIsr
            Documento.IVA = vIva
            Documento.TOTAL = vTotal
            Documento.NUMEROABONO = 0
            Documento.FECHAVENCIMIENTO = ""
            Documento.MONTOABONO = 0
            Documento.SERIEFEL = NotaCredito.serieFEL
            Documento.PREIMPRESO = NotaCredito.preimpreso

            ' ******************************************************************************

            id_glo_api = "7e3e0ab330fffd33f244089444ac5db7"

            ' ********************************************************* Convertir Objeto a JSON *********************************************************
            objEncabezado = "{""ENCABEZADO"":" & PW.JSON.JSONHelper.ObjectToString(Documento) & ",""DETALLE"":["
            objDetalle = objDetalle & "]}"
            '****** Conectividad al servicio Nautilus ******
            PostData = [String].Format("encabezado={0}&detalle={1}&sociedad={2}&tipo={3}&ruta={4}&serie={5}&correlativo={6}&latitud={7}&longitud={8}", objEncabezado, objDetalle, id_glo_sociedad, 2, id_glo_codRuta, factura.serie, factura.numero, gps.Latitude, gps.Longitude)
            request.Headers.Add("Authorization", id_glo_api)
            request.Credentials = CredentialCache.DefaultCredentials
            CType(request, HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
            request.Method = "POST"
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(PostData)
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()
            Dim response As WebResponse = request.GetResponse()
            'dataStream = response.GetResponseStream()
            Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            dataStream.Close()
            response.Close()
            If displayNC(responseFromServer, NotaCredito.idEncabezado) Then
                objDocumentoBL.UpdateTipoReceptorNC(Documento.TIPORECEPTOR, Documento.IDRECEPTOR, NotaCredito.idEncabezado)
                Return True
            End If

            Return True
            ' *************************************************** Fin de la rutina de comunicacion ****************************************************
        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
            MsgBox("No se pudo generar NOTA DE CREDITO")
            Return False
        End Try
        'TextBox1.Text = PW.JSON.JSONHelper.ObjectToString(objprova)
    End Function

    Public Function generadorNCCXCDPP(ByVal NotaCredito As documentoCO, ByRef Cliente As ClienteCO) As Boolean
        '*** Declaración de Variables de la función ***
        Dim vBruto As Double = 0
        Dim vDescuento As Double = 0
        Dim vExento As Double = 0
        Dim vOtros As Double = 0
        Dim vNeto As Double = 0
        Dim vIsr As Double = 0
        Dim vIva As Double = 0
        Dim vTotal As Double = 0
        Dim dBruto, dDescuento, dTotal, dNeto, dIva, dPorcentaje, dOtros As Double
        Dim item As ItemCO
        Dim factura As New documentoCO      'Variable del tipo data Table
        Dim dtFactura As DataTable
        Dim dtFEL As DataTable
        Dim objEncabezado As String = ""
        Dim objDetalle As String = ""
        Dim PostData As String = ""
        Dim dtCredenciales As DataTable
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL

        
        Dim servidorFEL As String = ""
        Dim servicioFEL As String = ""
        Dim protocoloFEL As String = ""
        dtCredenciales = objDocumentoBL.credencialesServidor()
        servidorFEL = dtCredenciales.Rows(0).Item("SERVIDOR")
        servicioFEL = dtCredenciales.Rows(0).Item("SERVICIO")
        protocoloFEL = dtCredenciales.Rows(0).Item("PROTOCOLO")

        If id_glo_protocolo = "https" Then
            servidorFEL = ""
            protocoloFEL = id_glo_protocolo
        End If
        'Envio de la Ubicación de donde se esta Generando la Factura Electronica
        Try
            gps.start()
            '--- Default values if no GPS is captured.
            If gps.Latitude = "" Then gps.Latitude = "0"
            If gps.Longitude = "" Then gps.Longitude = "0"
            If Len(gps.Latitude) > 15 Then gps.Latitude = gps.Latitude.Substring(0, 15)
            If Len(gps.Longitude) > 15 Then gps.Longitude = gps.Longitude.Substring(0, 15)
        Catch ex As Exception
            gps.stop()
        End Try


        'Dim request As WebRequest = WebRequest.Create("http://192.9.2.56/NAUTILUSQA/v1/generadocumentonc")
        'Dim request As WebRequest = WebRequest.Create("HTTP" & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentonc")


        Dim request As WebRequest = WebRequest.Create("HTTP" & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentonc")
        

        Try
            Cursor.Current = Cursors.Default
            'Lectura de datos Detalle Facturación

            dtFactura = objDocumentoBL.getNotaCreditoDetalle(NotaCredito.idEncabezado)
            factura = objDocumentoBL.getNotaCredito(NotaCredito.idEncabezado)
            '*************************************************************************
            Dim registros = 0
            registros = dtFactura.Rows.Count

            If (registros = 1) Then
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0

                    item = objDocumentoBL.getNotaCreditoByItem(dtFactura.Rows(i))

                    'Traer el precio por caja del material
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())



                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************
                    dBruto = item.importeDesto
                    dPorcentaje = 0
                    dDescuento = 0
                    dTotal = dBruto - dDescuento
                    dNeto = objUtilBL.isDecimal(dTotal / 1.12)
                    dIva = objUtilBL.isDecimal(dNeto * 0.12)
                    'Un Ajuston 
                    dNeto = dBruto - dDescuento - dIva
                    dBruto = dDescuento + dNeto + dIva - dOtros
                    '*************************** FIN CALCULO GUATEFACTURAS  ***************************

                    vBruto = objUtilBL.isDecimal2(vBruto) + dBruto
                    vDescuento = objUtilBL.isDecimal2(vDescuento) + dDescuento
                    vExento = objUtilBL.isDecimal2(vExento) + 0
                    vOtros = objUtilBL.isDecimal2(vOtros) + dOtros
                    vNeto = objUtilBL.isDecimal2(vNeto) + dNeto
                    vIsr = vIsr + 0
                    vIva = objUtilBL.isDecimal2(vIva) + dIva
                    vTotal = objUtilBL.isDecimal2(vTotal) + objUtilBL.isDecimal2(dBruto - dDescuento)

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = 1
                    Detalle.PRECIO = item.importeDesto
                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = 0
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                Next
            Else
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0

                    item = objDocumentoBL.getNotaCreditoByItem(dtFactura.Rows(i))

                    'Traer el precio por caja del material
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())


                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************
                    dBruto = item.importeDesto
                    dPorcentaje = 0
                    dDescuento = 0
                    dTotal = dBruto - dDescuento
                    dNeto = objUtilBL.isDecimal(dTotal / 1.12)
                    dIva = objUtilBL.isDecimal(dNeto * 0.12)
                    'Un Ajuston 
                    dNeto = dBruto - dDescuento - dIva
                    dBruto = dDescuento + dNeto + dIva - dOtros
                    '*************************** FIN CALCULO GUATEFACTURAS  ***************************

                    vBruto = objUtilBL.isDecimal2(vBruto) + dBruto
                    vDescuento = objUtilBL.isDecimal2(vDescuento) + dDescuento
                    vExento = objUtilBL.isDecimal2(vExento) + 0
                    vOtros = objUtilBL.isDecimal2(vOtros) + dOtros
                    vNeto = objUtilBL.isDecimal2(vNeto) + dNeto
                    vIsr = vIsr + 0
                    vIva = objUtilBL.isDecimal2(vIva) + dIva
                    vTotal = objUtilBL.isDecimal2(vTotal) + objUtilBL.isDecimal2(dBruto - dDescuento)

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = 1

                    Detalle.PRECIO = item.importeDesto
                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = 0
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    If (i = 0) Then
                        objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                    Else
                        If (i = dtFactura.Rows.Count - 1) Then
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                        Else
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                        End If
                    End If
                Next
            End If



            ' ******* Asignación de Datos de Facturación Documento Encabezado *******
            Documento.NITRECEPTOR = Cliente.nit

            Try
                dtFEL = objDocumentoBL.getCXCFEL(factura.idReciboRelacionado)
                Documento.TIPORECEPTOR = dtFEL.Rows(0).Item("tipoidrecep")
                Documento.IDRECEPTOR = dtFEL.Rows(0).Item("idrecep")
            Catch ex As Exception
                Documento.TIPORECEPTOR = 4
                Documento.IDRECEPTOR = Cliente.nit
            End Try

            Documento.NOMBRE = Cliente.negocio
            Documento.DIRECCION = Cliente.direccion
            Documento.TIPOVENTA = "B"
            Documento.DESTINOVENTA = 1
            Documento.FECHA = Format(CDate(factura.fechaEmision), "dd/MM/yyyy")
            Documento.MONEDA = 1
            Documento.TASA = 1.0
            Documento.REFERENCIA = factura.numero
            Documento.NUMEROACCESO = ""
            Documento.SERIEADMIN = factura.serie
            Documento.NUMEROADMIN = factura.numero
            Documento.REVERSION = "N"
            Documento.BRUTO = vBruto
            Documento.DESCUENTO = vDescuento
            Documento.EXENTO = vExento
            Documento.OTROS = vOtros
            Documento.NETO = vNeto
            Documento.ISR = vIsr
            Documento.IVA = vIva
            Documento.TOTAL = vTotal
            Documento.NUMEROABONO = 0
            Documento.FECHAVENCIMIENTO = ""
            Documento.MONTOABONO = 0
            Documento.SERIEFEL = NotaCredito.serieFEL
            Documento.PREIMPRESO = NotaCredito.preimpreso



            ' ******************************************************************************
            If (vTotal > 0) Then
                id_glo_api = "7e3e0ab330fffd33f244089444ac5db7"

                ' ********************************************************* Convertir Objeto a JSON *********************************************************
                objEncabezado = "{""ENCABEZADO"":" & PW.JSON.JSONHelper.ObjectToString(Documento) & ",""DETALLE"":["
                objDetalle = objDetalle & "]}"
                objEncabezado = objEncabezado.Replace("&", " ")
                objDetalle = objDetalle.Replace("&", " ")
                '****** Conectividad al servicio Nautilus ******
                PostData = [String].Format("encabezado={0}&detalle={1}&sociedad={2}&tipo={3}&ruta={4}&serie={5}&correlativo={6}&latitud={7}&longitud={8}", objEncabezado, objDetalle, id_glo_sociedad, 2, id_glo_codRuta, factura.serie, factura.numero, gps.Latitude, gps.Longitude)
                request.Headers.Add("Authorization", id_glo_api)
                request.Credentials = CredentialCache.DefaultCredentials
                CType(request, HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
                request.Method = "POST"
                Dim byteArray As Byte() = Encoding.ASCII.GetBytes(PostData)
                request.ContentType = "application/x-www-form-urlencoded"
                request.ContentLength = byteArray.Length
                Dim dataStream As Stream = request.GetRequestStream()
                dataStream.Write(byteArray, 0, byteArray.Length)
                dataStream.Close()
                Dim response As WebResponse = request.GetResponse()
                'dataStream = response.GetResponseStream()
                Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
                dataStream = response.GetResponseStream()
                Dim reader As New StreamReader(dataStream)
                Dim responseFromServer As String = reader.ReadToEnd()
                reader.Close()
                dataStream.Close()
                response.Close()
                'MessageBox.Show(responseFromServer)

                If displayNC(responseFromServer, NotaCredito.idEncabezado) Then
                    objDocumentoBL.UpdateTipoReceptorNC(Documento.TIPORECEPTOR, Documento.IDRECEPTOR, NotaCredito.idEncabezado)
                    Return True
                End If
                Return True
                ' *************************************************** Fin de la rutina de comunicacion ****************************************************
            Else
                MessageBox.Show("ESTA NOTA DE CREDITO TIENE 0 EN FEL")
                Return False
            End If

        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
            MsgBox("No se pudo generar NOTA DE CREDITO")
            Return False
        End Try
        'TextBox1.Text = PW.JSON.JSONHelper.ObjectToString(objprova)
    End Function

    Public Function generadorNCCXCDPPREF(ByVal idEncabezado As Integer, ByRef Cliente As ClienteCO) As Boolean
        '*** Declaración de Variables de la función ***
        Dim vBruto As Double = 0
        Dim vDescuento As Double = 0
        Dim vExento As Double = 0
        Dim vOtros As Double = 0
        Dim vNeto As Double = 0
        Dim vIsr As Double = 0
        Dim vIva As Double = 0
        Dim vTotal As Double = 0
        Dim dBruto, dDescuento, dTotal, dNeto, dIva, dPorcentaje, dOtros As Double
        Dim item As ItemCO
        Dim factura As New documentoCO      'Variable del tipo data Table
        Dim dtFEL As DataTable
        Dim dtFactura As DataTable
        Dim objEncabezado As String = ""
        Dim objDetalle As String = ""
        Dim PostData As String = ""
        Dim dtCredenciales As DataTable
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL


        Dim servidorFEL As String = ""
        Dim servicioFEL As String = ""
        Dim protocoloFEL As String = ""
        dtCredenciales = objDocumentoBL.credencialesServidor()
        servidorFEL = dtCredenciales.Rows(0).Item("SERVIDOR")
        servicioFEL = dtCredenciales.Rows(0).Item("SERVICIO")
        protocoloFEL = dtCredenciales.Rows(0).Item("PROTOCOLO")

        If id_glo_protocolo = "https" Then
            servidorFEL = ""
            protocoloFEL = id_glo_protocolo
        End If
        'Envio de la Ubicación de donde se esta Generando la Factura Electronica
        Try
            gps.start()
            '--- Default values if no GPS is captured.
            If gps.Latitude = "" Then gps.Latitude = "0"
            If gps.Longitude = "" Then gps.Longitude = "0"
            If Len(gps.Latitude) > 15 Then gps.Latitude = gps.Latitude.Substring(0, 15)
            If Len(gps.Longitude) > 15 Then gps.Longitude = gps.Longitude.Substring(0, 15)
        Catch ex As Exception
            gps.stop()
        End Try


        'Dim request As WebRequest = WebRequest.Create("http://192.9.2.56/NAUTILUSQA/v1/generadocumentonc")
        'Dim request As WebRequest = WebRequest.Create("HTTP" & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentonc")


        Dim request As WebRequest = WebRequest.Create("HTTP" & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentonc")
        

        Try
            Cursor.Current = Cursors.Default
            'Lectura de datos Detalle Facturación

            dtFactura = objDocumentoBL.getNotaCreditoDetalle(idEncabezado)
            factura = objDocumentoBL.getNotaCredito(idEncabezado)

            '*************************************************************************
            Dim registros = 0
            registros = dtFactura.Rows.Count

            If (registros = 1) Then
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0

                    item = objDocumentoBL.getNotaCreditoByItem(dtFactura.Rows(i))

                    'Traer el precio por caja del material
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())



                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************
                    dBruto = item.importeDesto
                    dPorcentaje = 0
                    dDescuento = 0
                    dTotal = dBruto - dDescuento
                    dNeto = objUtilBL.isDecimal(dTotal / 1.12)
                    dIva = objUtilBL.isDecimal(dNeto * 0.12)
                    'Un Ajuston 
                    dNeto = dBruto - dDescuento - dIva
                    dBruto = dDescuento + dNeto + dIva - dOtros
                    '*************************** FIN CALCULO GUATEFACTURAS  ***************************

                    vBruto = objUtilBL.isDecimal2(vBruto) + dBruto
                    vDescuento = objUtilBL.isDecimal2(vDescuento) + dDescuento
                    vExento = objUtilBL.isDecimal2(vExento) + 0
                    vOtros = objUtilBL.isDecimal2(vOtros) + dOtros
                    vNeto = objUtilBL.isDecimal2(vNeto) + dNeto
                    vIsr = vIsr + 0
                    vIva = objUtilBL.isDecimal2(vIva) + dIva
                    vTotal = objUtilBL.isDecimal2(vTotal) + objUtilBL.isDecimal2(dBruto - dDescuento)

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = 1
                    Detalle.PRECIO = item.importeDesto
                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = 0
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                Next
            Else
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0

                    item = objDocumentoBL.getNotaCreditoByItem(dtFactura.Rows(i))

                    'Traer el precio por caja del material
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())


                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************
                    dBruto = item.importeDesto
                    dPorcentaje = 0
                    dDescuento = 0
                    dTotal = dBruto - dDescuento
                    dNeto = objUtilBL.isDecimal(dTotal / 1.12)
                    dIva = objUtilBL.isDecimal(dNeto * 0.12)
                    'Un Ajuston 
                    dNeto = dBruto - dDescuento - dIva
                    dBruto = dDescuento + dNeto + dIva - dOtros
                    '*************************** FIN CALCULO GUATEFACTURAS  ***************************

                    vBruto = objUtilBL.isDecimal2(vBruto) + dBruto
                    vDescuento = objUtilBL.isDecimal2(vDescuento) + dDescuento
                    vExento = objUtilBL.isDecimal2(vExento) + 0
                    vOtros = objUtilBL.isDecimal2(vOtros) + dOtros
                    vNeto = objUtilBL.isDecimal2(vNeto) + dNeto
                    vIsr = vIsr + 0
                    vIva = objUtilBL.isDecimal2(vIva) + dIva
                    vTotal = objUtilBL.isDecimal2(vTotal) + objUtilBL.isDecimal2(dBruto - dDescuento)

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = 1

                    Detalle.PRECIO = item.importeDesto
                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = 0
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    If (i = 0) Then
                        objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                    Else
                        If (i = dtFactura.Rows.Count - 1) Then
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                        Else
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                        End If
                    End If
                Next
            End If



            ' ******* Asignación de Datos de Facturación Documento Encabezado *******
            Documento.NITRECEPTOR = Cliente.nit
            Try
                dtFEL = objDocumentoBL.getCXCFEL(factura.idReciboRelacionado)
                Documento.TIPORECEPTOR = dtFEL.Rows(0).Item("tipoidrecep")
                Documento.IDRECEPTOR = dtFEL.Rows(0).Item("idrecep")
            Catch ex As Exception
                Documento.TIPORECEPTOR = 4
                Documento.IDRECEPTOR = Cliente.nit
            End Try
            Documento.NOMBRE = Cliente.negocio
            Documento.DIRECCION = Cliente.direccion
            Documento.TIPOVENTA = "B"
            Documento.DESTINOVENTA = 1
            Documento.FECHA = Format(CDate(factura.fechaEmision), "dd/MM/yyyy")
            Documento.MONEDA = 1
            Documento.TASA = 1.0
            Documento.REFERENCIA = factura.numero
            Documento.NUMEROACCESO = ""
            Documento.SERIEADMIN = factura.serie
            Documento.NUMEROADMIN = factura.numero
            Documento.REVERSION = "N"
            Documento.BRUTO = vBruto
            Documento.DESCUENTO = vDescuento
            Documento.EXENTO = vExento
            Documento.OTROS = vOtros
            Documento.NETO = vNeto
            Documento.ISR = vIsr
            Documento.IVA = vIva
            Documento.TOTAL = vTotal
            Documento.NUMEROABONO = 0
            Documento.FECHAVENCIMIENTO = ""
            Documento.MONTOABONO = 0

            Dim dtElectronico As DataTable
            If (factura.idEncFacturaRelacionada = 0) Then
                dtElectronico = objDocumentoBL.datosElectronicosCXC(idEncabezado)
            Else
                dtElectronico = objDocumentoBL.datosElectronicos(factura.idEncFacturaRelacionada)
            End If


            Documento.SERIEFEL = dtElectronico.Rows(0).Item("PREIMPRESO")
            Documento.PREIMPRESO = dtElectronico.Rows(0).Item("NUMEROAUTORIZACION")

            'Documento.SERIEFEL = factura.serieFEL
            'Documento.PREIMPRESO = factura.preimpreso



            ' ******************************************************************************
            If (vTotal > 0) Then
                id_glo_api = "7e3e0ab330fffd33f244089444ac5db7"

                ' ********************************************************* Convertir Objeto a JSON *********************************************************
                objEncabezado = "{""ENCABEZADO"":" & PW.JSON.JSONHelper.ObjectToString(Documento) & ",""DETALLE"":["
                objDetalle = objDetalle & "]}"
                objEncabezado = objEncabezado.Replace("&", " ")
                objDetalle = objDetalle.Replace("&", " ")
                '****** Conectividad al servicio Nautilus ******
                PostData = [String].Format("encabezado={0}&detalle={1}&sociedad={2}&tipo={3}&ruta={4}&serie={5}&correlativo={6}&latitud={7}&longitud={8}", objEncabezado, objDetalle, id_glo_sociedad, 2, id_glo_codRuta, factura.serie, factura.numero, gps.Latitude, gps.Longitude)
                request.Headers.Add("Authorization", id_glo_api)
                request.Credentials = CredentialCache.DefaultCredentials
                CType(request, HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
                request.Method = "POST"
                Dim byteArray As Byte() = Encoding.ASCII.GetBytes(PostData)
                request.ContentType = "application/x-www-form-urlencoded"
                request.ContentLength = byteArray.Length
                Dim dataStream As Stream = request.GetRequestStream()
                dataStream.Write(byteArray, 0, byteArray.Length)
                dataStream.Close()
                Dim response As WebResponse = request.GetResponse()
                'dataStream = response.GetResponseStream()
                Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
                dataStream = response.GetResponseStream()
                Dim reader As New StreamReader(dataStream)
                Dim responseFromServer As String = reader.ReadToEnd()
                reader.Close()
                dataStream.Close()
                response.Close()
                'MessageBox.Show(responseFromServer)

                If displayNC(responseFromServer, idEncabezado) Then
                    objDocumentoBL.UpdateTipoReceptorNC(Documento.TIPORECEPTOR, Documento.IDRECEPTOR, idEncabezado)
                    Return True
                End If
                Return True
                ' *************************************************** Fin de la rutina de comunicacion ****************************************************
            Else
                MessageBox.Show("ESTA NOTA DE CREDITO TIENE 0 EN FEL")
                Return False
            End If

        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
            MsgBox("No se pudo generar NOTA DE CREDITO")
            Return False
        End Try
        'TextBox1.Text = PW.JSON.JSONHelper.ObjectToString(objprova)
    End Function

    Public Function generadorNCRef(ByVal idEncabezado As Integer, ByRef Cliente As ClienteCO) As Boolean
        '*** Declaración de Variables de la función ***
        Dim vBruto As Double = 0
        Dim vDescuento As Double = 0
        Dim vExento As Double = 0
        Dim vOtros As Double = 0
        Dim vNeto As Double = 0
        Dim vIsr As Double = 0
        Dim vIva As Double = 0
        Dim vTotal As Double = 0
        Dim dprecioUnidad As Double = 0
        Dim dBruto, dDescuento, dTotal, dNeto, dIva, dPorcentaje, dOtros As Double
        Dim item As ItemCO
        Dim factura As New documentoCO      'Variable del tipo data Table
        Dim origen As New documentoCO      'Origen Factura
        Dim dtFactura As DataTable
        Dim dtFEL As DataTable
        Dim objEncabezado As String = ""
        Dim objDetalle As String = ""
        Dim PostData As String = ""
        Dim dtCredenciales As DataTable
        'Dim dtClienteListaPrecio As DataTable
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL
        'Dim dtListaPrecio As DataTable
        Dim servidorFEL As String = ""
        Dim servicioFEL As String = ""
        Dim protocoloFEL As String = ""
        Dim UnidadMedidas As String = ""
        dtCredenciales = objDocumentoBL.credencialesServidor()
        servidorFEL = dtCredenciales.Rows(0).Item("SERVIDOR")
        servicioFEL = dtCredenciales.Rows(0).Item("SERVICIO")
        protocoloFEL = dtCredenciales.Rows(0).Item("PROTOCOLO")


        If id_glo_protocolo = "https" Then
            servidorFEL = ""
            protocoloFEL = id_glo_protocolo
        End If
        'Envio de la Ubicación de donde se esta Generando la Factura Electronica
        Try
            gps.start()
            '--- Default values if no GPS is captured.
            If gps.Latitude = "" Then gps.Latitude = "0"
            If gps.Longitude = "" Then gps.Longitude = "0"
            If Len(gps.Latitude) > 15 Then gps.Latitude = gps.Latitude.Substring(0, 15)
            If Len(gps.Longitude) > 15 Then gps.Longitude = gps.Longitude.Substring(0, 15)
        Catch ex As Exception
            gps.stop()
        End Try




        'Dim request As WebRequest = WebRequest.Create("http://192.9.2.56/NAUTILUSQA/v1/generadocumentonc")
        'Dim request As WebRequest = WebRequest.Create("HTTP" & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentonc")


        Dim request As WebRequest = WebRequest.Create("HTTP" & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentonc")
        

        Try
            Cursor.Current = Cursors.Default
            'Lectura de datos Detalle Facturación

            dtFactura = objDocumentoBL.getNotaCreditoDetalle2(idEncabezado)
            factura = objDocumentoBL.getNotaCredito(idEncabezado)

            If factura.idEncFacturaRelacionada <> 0 Then
                Try
                    origen = objDocumentoBL.getFactura(factura.idEncFacturaRelacionada)
                    Documento.TIPORECEPTOR = origen.tipoReceptor
                    Documento.IDRECEPTOR = origen.idreceptor
                Catch ex As Exception
                    Documento.TIPORECEPTOR = 4
                    Documento.IDRECEPTOR = Cliente.nit
                End Try
            Else
                Try
                    dtFEL = objDocumentoBL.getCXCFEL(factura.idReciboRelacionado)
                    Documento.TIPORECEPTOR = dtFEL.Rows(0).Item("tipoidrecep")
                    Documento.IDRECEPTOR = dtFEL.Rows(0).Item("idrecep")
                Catch ex As Exception
                    Documento.TIPORECEPTOR = 4
                    Documento.IDRECEPTOR = Cliente.nit
                End Try
            End If




            '*************************************************************************
            Dim registros = 0
            registros = dtFactura.Rows.Count

            If (registros = 1) Then
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0

                    item = objDocumentoBL.getNotaCreditoByItem(dtFactura.Rows(i))

                    'Traer el precio por caja del material
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())

                    'dtClienteListaPrecio = objDocumentoBL.ListaPrecioCliente(factura.idCliente)

                    'dtListaPrecio = objDocumentoBL.ListaPrecioProducto(item.idProducto.ToString(), dtClienteListaPrecio.Rows(0).Item("LISTAPRECIO"))

                    'UnidadMedidas = dtListaPrecio.Rows(0).Item("UM").ToString()

                    dprecioUnidad = objUtilBL.isDecimal6((item.importe) / (item.cantidad))
                    dBruto = objUtilBL.isDecimal(item.cantidad * dprecioUnidad)

                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************
                    'If (UnidadMedidas = "UN") Then
                    'dBruto = (objUtilBL.isDecimal(item.cantidad) * (objUtilBL.isDecimal(item.precio * 1.12)))
                    'Else
                    'dBruto = (objUtilBL.isDecimal(item.cantidad) * (objUtilBL.isDecimal(item.precio * 1.12) / objUtilBL.isDecimal(comProducto.unidadesCaja)))
                    'End If


                    'Actualizar el registro del detalle de nota de credito detalle con los campos 

                    'objDocumentoBL.actualizarDetalleNC(dtFactura.Rows(i).Item("id_detNC").ToString, dtFactura.Rows(i).Item("idEncNC").ToString, dtFactura.Rows(i).Item("idProducto").ToString, objUtilBL.isDecimal(dBruto), objUtilBL.isDecimal(dBruto / 1.12))

                    'Importe = cantidad * precio  update detalle
                    'importe sin iva = importe / 1.12

                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************
                    If (item.importeDesto = 0) Then
                        dDescuento = 0
                        dPorcentaje = 0
                    Else
                        dPorcentaje = item.porcentajeDesto
                        dDescuento = (objUtilBL.isDecimal(dBruto * (dPorcentaje / 100)))
                    End If
                    dTotal = dBruto - dDescuento
                    dNeto = objUtilBL.isDecimal(dTotal / 1.12)
                    dIva = objUtilBL.isDecimal(dNeto * 0.12)
                    'Un Ajuston 
                    dNeto = dBruto - dDescuento - dIva
                    dBruto = dDescuento + dNeto + dIva - dOtros
                    '*************************** FIN CALCULO GUATEFACTURAS  ***************************

                    vBruto = objUtilBL.isDecimal2(vBruto) + dBruto
                    vDescuento = objUtilBL.isDecimal2(vDescuento) + dDescuento
                    vExento = objUtilBL.isDecimal2(vExento) + 0
                    vOtros = objUtilBL.isDecimal2(vOtros) + dOtros
                    vNeto = objUtilBL.isDecimal2(vNeto) + dNeto
                    vIsr = vIsr + 0
                    vIva = objUtilBL.isDecimal2(vIva) + dIva
                    vTotal = objUtilBL.isDecimal2(vTotal) + objUtilBL.isDecimal2(dBruto - dDescuento)

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = objUtilBL.isDecimal(item.cantidad.ToString())

                    Detalle.PRECIO = dprecioUnidad

                    'If (UnidadMedidas = "UN") Then
                    'Detalle.PRECIO = objUtilBL.isDecimal(item.precio * 1.12)
                    'Else
                    'Detalle.PRECIO = objUtilBL.isDecimal(item.precio * 1.12) / objUtilBL.isDecimal(comProducto.unidadesCaja)
                    'End If


                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = 0
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                Next
            Else
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0

                    item = objDocumentoBL.getNotaCreditoByItem(dtFactura.Rows(i))

                    'Traer el precio por caja del material
                    'comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())

                    'dtClienteListaPrecio = objDocumentoBL.ListaPrecioCliente(factura.idCliente)

                    'dtListaPrecio = objDocumentoBL.ListaPrecioProducto(item.idProducto.ToString(), dtClienteListaPrecio.Rows(0).Item("LISTAPRECIO"))

                    'UnidadMedidas = dtListaPrecio.Rows(0).Item("UM").ToString()

                    dprecioUnidad = objUtilBL.isDecimal6((item.importe) / (item.cantidad))
                    dBruto = objUtilBL.isDecimal(item.cantidad * dprecioUnidad)

                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************
                    'If (UnidadMedidas = "UN") Then
                    'dBruto = (objUtilBL.isDecimal(item.cantidad) * (objUtilBL.isDecimal(item.precio * 1.12)))
                    'Else
                    'dBruto = (objUtilBL.isDecimal(item.cantidad) * (objUtilBL.isDecimal(item.precio * 1.12) / objUtilBL.isDecimal(comProducto.unidadesCaja)))
                    'End If


                    'Actualizar el registro del detalle de nota de credito detalle con los campos 

                    'objDocumentoBL.actualizarDetalleNC(dtFactura.Rows(i).Item("id_detNC").ToString, dtFactura.Rows(i).Item("idEncNC").ToString, dtFactura.Rows(i).Item("idProducto").ToString, objUtilBL.isDecimal(dBruto), objUtilBL.isDecimal(dBruto / 1.12))

                    'Importe = cantidad * precio  update detalle
                    'importe sin iva = importe / 1.12

                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************
                    If (item.importeDesto = 0) Then
                        dDescuento = 0
                        dPorcentaje = 0
                    Else
                        dPorcentaje = item.porcentajeDesto
                        dDescuento = (objUtilBL.isDecimal(dBruto * (dPorcentaje / 100)))
                    End If
                    dTotal = dBruto - dDescuento
                    dNeto = objUtilBL.isDecimal(dTotal / 1.12)
                    dIva = objUtilBL.isDecimal(dNeto * 0.12)
                    'Un Ajuston 
                    dNeto = dBruto - dDescuento - dIva
                    dBruto = dDescuento + dNeto + dIva - dOtros
                    '*************************** FIN CALCULO GUATEFACTURAS  ***************************

                    vBruto = objUtilBL.isDecimal2(vBruto) + dBruto
                    vDescuento = objUtilBL.isDecimal2(vDescuento) + dDescuento
                    vExento = objUtilBL.isDecimal2(vExento) + 0
                    vOtros = objUtilBL.isDecimal2(vOtros) + dOtros
                    vNeto = objUtilBL.isDecimal2(vNeto) + dNeto
                    vIsr = vIsr + 0
                    vIva = objUtilBL.isDecimal2(vIva) + dIva
                    vTotal = objUtilBL.isDecimal2(vTotal) + objUtilBL.isDecimal2(dBruto - dDescuento)

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = objUtilBL.isDecimal(item.cantidad.ToString())

                    Detalle.PRECIO = dprecioUnidad
                    'If (UnidadMedidas = "UN") Then
                    ' Detalle.PRECIO = objUtilBL.isDecimal(item.precio * 1.12)
                    ' Else
                    ' Detalle.PRECIO = objUtilBL.isDecimal(item.precio * 1.12) / objUtilBL.isDecimal(comProducto.unidadesCaja)
                    ' End If

                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = 0
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    If (i = 0) Then
                        objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                    Else
                        If (i = dtFactura.Rows.Count - 1) Then
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                        Else
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                        End If
                    End If
                Next
            End If



            ' ******* Asignación de Datos de Facturación Documento Encabezado *******
            Documento.NITRECEPTOR = Cliente.nit

            

            

            Documento.NOMBRE = Cliente.negocio
            Documento.DIRECCION = Cliente.direccion
            Documento.TIPOVENTA = "B"
            Documento.DESTINOVENTA = 1
            Documento.FECHA = Format(CDate(factura.fechaEmision), "dd/MM/yyyy")
            Documento.MONEDA = 1
            Documento.TASA = 1.0
            Documento.REFERENCIA = factura.numero
            Documento.NUMEROACCESO = factura.numeroacceso
            Documento.SERIEADMIN = factura.serie
            Documento.NUMEROADMIN = factura.numero
            Documento.REVERSION = "N"
            Documento.BRUTO = vBruto
            Documento.DESCUENTO = vDescuento
            Documento.EXENTO = vExento
            Documento.OTROS = vOtros
            Documento.NETO = vNeto
            Documento.ISR = vIsr
            Documento.IVA = vIva
            Documento.TOTAL = vTotal
            Documento.NUMEROABONO = 0
            Documento.FECHAVENCIMIENTO = ""
            Documento.MONTOABONO = 0
            Dim dtElectronico As DataTable
            If (factura.idEncFacturaRelacionada = 0) Then
                dtElectronico = objDocumentoBL.datosElectronicosCXC(idEncabezado)
            Else
                dtElectronico = objDocumentoBL.datosElectronicos(factura.idEncFacturaRelacionada)
            End If


            comNotaCreditoEnv.serieFEL = dtElectronico.Rows(0).Item("NUMEROAUTORIZACION")
            comNotaCreditoEnv.preimpreso = dtElectronico.Rows(0).Item("PREIMPRESO")

            If (factura.idEncFacturaRelacionada = 0) Then
                Documento.SERIEFEL = comNotaCreditoEnv.preimpreso
                Documento.PREIMPRESO = comNotaCreditoEnv.serieFEL
            Else
                Documento.SERIEFEL = comNotaCreditoEnv.serieFEL.Substring(0, 8)
                Documento.PREIMPRESO = comNotaCreditoEnv.preimpreso
            End If



            'objDocumentoBL.actualizarNCImporte(idEncabezado, vBruto)

            ' ******************************************************************************

            'objDocumentoBL.actualizarNCIMPORTEDESTOENV(factura.idEncFacturaRelacionada, vBruto)

            If (vTotal > 0) Then
                id_glo_api = "7e3e0ab330fffd33f244089444ac5db7"

                ' ********************************************************* Convertir Objeto a JSON *********************************************************
                objEncabezado = "{""ENCABEZADO"":" & PW.JSON.JSONHelper.ObjectToString(Documento) & ",""DETALLE"":["
                objDetalle = objDetalle & "]}"
                objEncabezado = objEncabezado.Replace("&", " ")
                objDetalle = objDetalle.Replace("&", " ")
                '****** Conectividad al servicio Nautilus ******
                PostData = [String].Format("encabezado={0}&detalle={1}&sociedad={2}&tipo={3}&ruta={4}&serie={5}&correlativo={6}&latitud={7}&longitud={8}", objEncabezado, objDetalle, id_glo_sociedad, 2, id_glo_codRuta, factura.serie, factura.numero, gps.Latitude, gps.Longitude)
                request.Headers.Add("Authorization", id_glo_api)
                request.Credentials = CredentialCache.DefaultCredentials
                CType(request, HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
                request.Method = "POST"
                Dim byteArray As Byte() = Encoding.ASCII.GetBytes(PostData)
                request.ContentType = "application/x-www-form-urlencoded"
                request.ContentLength = byteArray.Length
                Dim dataStream As Stream = request.GetRequestStream()
                dataStream.Write(byteArray, 0, byteArray.Length)
                dataStream.Close()
                Dim response As WebResponse = request.GetResponse()
                'dataStream = response.GetResponseStream()
                Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
                dataStream = response.GetResponseStream()
                Dim reader As New StreamReader(dataStream)
                Dim responseFromServer As String = reader.ReadToEnd()
                reader.Close()
                dataStream.Close()
                response.Close()
                'MessageBox.Show(responseFromServer)


                If displayNC(responseFromServer, idEncabezado) Then
                    objDocumentoBL.UpdateTipoReceptorNC(Documento.TIPORECEPTOR, Documento.IDRECEPTOR, idEncabezado)
                    Return True
                Else
                    Return False
                End If
                Return True
            Else
                Return False
            End If


            ' *************************************************** Fin de la rutina de comunicacion ****************************************************
        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
            MsgBox("No se pudo generar NOTA DE CREDITO CON VALOR 0")
            Return False
        End Try
        'TextBox1.Text = PW.JSON.JSONHelper.ObjectToString(objprova)
    End Function

    Public Function generadorNABONOREF(ByVal NotaCredito As Integer, ByRef Cliente As ClienteCO) As Boolean
        '*** Declaración de Variables de la función ***
        Dim vBruto As Double = 0
        Dim vDescuento As Double = 0
        Dim vExento As Double = 0
        Dim vOtros As Double = 0
        Dim vNeto As Double = 0
        Dim vIsr As Double = 0
        Dim vIva As Double = 0
        Dim vTotal As Double = 0
        Dim dBruto, dDescuento, dTotal, dNeto, dIva, dPorcentaje, dOtros, dExento As Double
        Dim item As ItemCO
        Dim factura As New documentoCO      'Variable del tipo data Table
        Dim dtFactura As DataTable
        Dim objEncabezado As String = ""
        Dim objDetalle As String = ""
        Dim PostData As String = ""
        Dim dtCredenciales As DataTable
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL

        Dim servidorFEL As String = ""
        Dim servicioFEL As String = ""
        Dim protocoloFEL As String = ""
        dtCredenciales = objDocumentoBL.credencialesServidor()
        servidorFEL = dtCredenciales.Rows(0).Item("SERVIDOR")
        servicioFEL = dtCredenciales.Rows(0).Item("SERVICIO")
        protocoloFEL = dtCredenciales.Rows(0).Item("PROTOCOLO")

        If id_glo_protocolo = "https" Then
            servidorFEL = ""
            protocoloFEL = id_glo_protocolo
        End If
        'Envio de la Ubicación de donde se esta Generando la Factura Electronica
        Try
            gps.start()
            '--- Default values if no GPS is captured.
            If gps.Latitude = "" Then gps.Latitude = "0"
            If gps.Longitude = "" Then gps.Longitude = "0"
            If Len(gps.Latitude) > 15 Then gps.Latitude = gps.Latitude.Substring(0, 15)
            If Len(gps.Longitude) > 15 Then gps.Longitude = gps.Longitude.Substring(0, 15)
        Catch ex As Exception
            gps.stop()
        End Try


        'Dim request As WebRequest = WebRequest.Create("http://192.9.2.56/NAUTILUSQA/v1/generadocumentona")
        'Dim request As WebRequest = WebRequest.Create("HTTP" & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentona")


        Dim request As WebRequest = WebRequest.Create("HTTP" & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentona")
        

        Try
            Cursor.Current = Cursors.Default
            'Lectura de datos Detalle Facturación

            dtFactura = objDocumentoBL.getNotaCreditoDetalle(NotaCredito)
            factura = objDocumentoBL.getNotaCredito(NotaCredito)
            '*************************************************************************
            Dim registros = 0
            registros = dtFactura.Rows.Count

            If (registros = 1) Then
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dExento = 0
                    dPorcentaje = 0

                    item = objDocumentoBL.getNotaCreditoByItem(dtFactura.Rows(i))

                    'Traer el precio por caja del material
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())

                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************
                    dBruto = item.importe
                    dPorcentaje = item.porcentajeDesto
                    dDescuento = (objUtilBL.isDecimal(dBruto * (dPorcentaje / 100)))
                    dTotal = dBruto - dDescuento
                    dNeto = 0
                    dIva = 0
                    'Un Ajuston 
                    dExento = dBruto - dDescuento - dIva
                    dNeto = 0
                    dBruto = dBruto - dDescuento
                    '*************************** FIN CALCULO GUATEFACTURAS  ***************************

                    vBruto = objUtilBL.isDecimal2(vBruto) + dBruto
                    vDescuento = objUtilBL.isDecimal2(vDescuento) + dDescuento
                    vExento = objUtilBL.isDecimal2(vExento) + dExento
                    vOtros = objUtilBL.isDecimal2(vOtros) + dOtros
                    vNeto = objUtilBL.isDecimal2(vNeto) + dNeto
                    vIsr = vIsr + 0
                    vIva = objUtilBL.isDecimal2(vIva) + dIva
                    vTotal = objUtilBL.isDecimal2(vTotal) + objUtilBL.isDecimal2(dBruto - dDescuento)

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = objUtilBL.isDecimal(item.cantidad.ToString())
                    Detalle.PRECIO = ((item.importe / item.cantidad))
                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = dExento
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                Next
            Else
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0
                    vExento = 0
                    item = objDocumentoBL.getNotaCreditoByItem(dtFactura.Rows(i))

                    'Traer el precio por caja del material
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())


                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************
                    dBruto = item.importe
                    dPorcentaje = item.porcentajeDesto
                    dDescuento = (objUtilBL.isDecimal(dBruto * (dPorcentaje / 100)))
                    dTotal = dBruto - dDescuento
                    dNeto = 0
                    dIva = 0
                    'Un Ajuston 
                    dExento = dBruto - dDescuento - dIva
                    dNeto = 0
                    dBruto = dBruto - dDescuento
                    '*************************** FIN CALCULO GUATEFACTURAS  ***************************
                    vBruto = objUtilBL.isDecimal2(vBruto) + dBruto
                    vDescuento = objUtilBL.isDecimal2(vDescuento) + dDescuento
                    vExento = objUtilBL.isDecimal2(vExento) + dExento
                    vOtros = objUtilBL.isDecimal2(vOtros) + dOtros
                    vNeto = objUtilBL.isDecimal2(vNeto) + dNeto
                    vIsr = vIsr + 0
                    vIva = objUtilBL.isDecimal2(vIva) + dIva
                    vTotal = objUtilBL.isDecimal2(vTotal) + objUtilBL.isDecimal2(dBruto - dDescuento)

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = objUtilBL.isDecimal(item.cantidad.ToString())

                    Detalle.PRECIO = ((item.importe / item.cantidad))
                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = objUtilBL.isDecimal(dExento)
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    If (i = 0) Then
                        objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                    Else
                        If (i = dtFactura.Rows.Count - 1) Then
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                        Else
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                        End If
                    End If
                Next
            End If



            ' ******* Asignación de Datos de Facturación Documento Encabezado *******
            Documento.NITRECEPTOR = Cliente.nit
            Documento.NOMBRE = Cliente.negocio
            Documento.DIRECCION = Cliente.direccion
            Documento.TIPOVENTA = "B"
            Documento.DESTINOVENTA = 1
            Documento.FECHA = Format(CDate(factura.fechaEmision), "dd/MM/yyyy")
            Documento.MONEDA = 1
            Documento.TASA = 1.0
            Documento.REFERENCIA = factura.numero
            Documento.NUMEROACCESO = ""
            Documento.SERIEADMIN = factura.serie
            Documento.NUMEROADMIN = factura.numero
            Documento.REVERSION = "N"
            Documento.BRUTO = vBruto
            Documento.DESCUENTO = vDescuento
            Documento.EXENTO = vTotal
            Documento.OTROS = vOtros
            Documento.NETO = vNeto
            Documento.ISR = vIsr
            Documento.IVA = vIva
            Documento.TOTAL = vTotal
            Documento.NUMEROABONO = 0
            Documento.FECHAVENCIMIENTO = ""
            Documento.MONTOABONO = 0
            Dim dtElectronico As DataTable
            dtElectronico = objDocumentoBL.datosElectronicosCXC(NotaCredito)

            'Documento.SERIEFEL = NotaCredito.serieFEL.Substring(0, 8)
            Documento.SERIEFEL = dtElectronico.Rows(0).Item("PREIMPRESO")
            Documento.PREIMPRESO = dtElectronico.Rows(0).Item("NUMEROAUTORIZACION")



            ' ******************************************************************************

            id_glo_api = "7e3e0ab330fffd33f244089444ac5db7"

            ' ********************************************************* Convertir Objeto a JSON *********************************************************
            objEncabezado = "{""ENCABEZADO"":" & PW.JSON.JSONHelper.ObjectToString(Documento) & ",""DETALLE"":["
            objDetalle = objDetalle & "]}"
            '****** Conectividad al servicio Nautilus ******
            PostData = [String].Format("encabezado={0}&detalle={1}&sociedad={2}&tipo={3}&ruta={4}&serie={5}&correlativo={6}&latitud={7}&longitud={8}", objEncabezado, objDetalle, id_glo_sociedad, 2, id_glo_codRuta, factura.serie, factura.numero, gps.Latitude, gps.Longitude)
            request.Headers.Add("Authorization", id_glo_api)
            request.Credentials = CredentialCache.DefaultCredentials
            CType(request, HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
            request.Method = "POST"
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(PostData)
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()
            Dim response As WebResponse = request.GetResponse()
            'dataStream = response.GetResponseStream()
            Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            dataStream.Close()
            response.Close()
            'MessageBox.Show(responseFromServer)

            displayNC(responseFromServer, NotaCredito)
            Return True
            ' *************************************************** Fin de la rutina de comunicacion ****************************************************
        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
            MsgBox("No se pudo generar NOTA DE CREDITO")
            Return False
        End Try
        'TextBox1.Text = PW.JSON.JSONHelper.ObjectToString(objprova)
    End Function

    Public Function generadorNABONO(ByVal NotaCredito As documentoCO, ByRef Cliente As ClienteCO) As Boolean
        '*** Declaración de Variables de la función ***
        Dim vBruto As Double = 0
        Dim vDescuento As Double = 0
        Dim vExento As Double = 0
        Dim vOtros As Double = 0
        Dim vNeto As Double = 0
        Dim vIsr As Double = 0
        Dim vIva As Double = 0
        Dim vTotal As Double = 0
        Dim dBruto, dDescuento, dTotal, dNeto, dIva, dPorcentaje, dOtros, dExento As Double
        Dim item As ItemCO
        Dim factura As New documentoCO      'Variable del tipo data Table
        Dim dtFactura As DataTable
        Dim objEncabezado As String = ""
        Dim objDetalle As String = ""
        Dim PostData As String = ""
        Dim dtCredenciales As DataTable
        Dim comProducto As New ProductoCO
        Dim objProductoBL As New ProductoBL

        Dim servidorFEL As String = ""
        Dim servicioFEL As String = ""
        Dim protocoloFEL As String = ""
        dtCredenciales = objDocumentoBL.credencialesServidor()
        servidorFEL = dtCredenciales.Rows(0).Item("SERVIDOR")
        servicioFEL = dtCredenciales.Rows(0).Item("SERVICIO")
        protocoloFEL = dtCredenciales.Rows(0).Item("PROTOCOLO")

        If id_glo_protocolo = "https" Then
            servidorFEL = ""
            protocoloFEL = id_glo_protocolo
        End If
        'Envio de la Ubicación de donde se esta Generando la Factura Electronica
        Try
            gps.start()
            '--- Default values if no GPS is captured.
            If gps.Latitude = "" Then gps.Latitude = "0"
            If gps.Longitude = "" Then gps.Longitude = "0"
            If Len(gps.Latitude) > 15 Then gps.Latitude = gps.Latitude.Substring(0, 15)
            If Len(gps.Longitude) > 15 Then gps.Longitude = gps.Longitude.Substring(0, 15)
        Catch ex As Exception
            gps.stop()
        End Try


        'Dim request As WebRequest = WebRequest.Create("http://192.9.2.56/NAUTILUSQA/v1/generadocumentona")
        'Dim request As WebRequest = WebRequest.Create("HTTP" & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentona")


        Dim request As WebRequest = WebRequest.Create("HTTP" & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentona")
        

        Try
            Cursor.Current = Cursors.Default
            'Lectura de datos Detalle Facturación

            dtFactura = objDocumentoBL.getNotaCreditoDetalle(NotaCredito.idEncabezado)
            factura = objDocumentoBL.getNotaCredito(NotaCredito.idEncabezado)
            '*************************************************************************
            Dim registros = 0
            registros = dtFactura.Rows.Count

            If (registros = 1) Then
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dExento = 0
                    dPorcentaje = 0

                    item = objDocumentoBL.getNotaCreditoByItem(dtFactura.Rows(i))

                    'Traer el precio por caja del material
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())

                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************
                    dBruto = item.importe
                    dPorcentaje = item.porcentajeDesto
                    dDescuento = (objUtilBL.isDecimal(dBruto * (dPorcentaje / 100)))
                    dTotal = dBruto - dDescuento
                    dNeto = 0
                    dIva = 0
                    'Un Ajuston 
                    dExento = dBruto - dDescuento - dIva
                    dNeto = 0
                    dBruto = dBruto - dDescuento
                    '*************************** FIN CALCULO GUATEFACTURAS  ***************************

                    vBruto = objUtilBL.isDecimal2(vBruto) + dBruto
                    vDescuento = objUtilBL.isDecimal2(vDescuento) + dDescuento
                    vExento = objUtilBL.isDecimal2(vExento) + dExento
                    vOtros = objUtilBL.isDecimal2(vOtros) + dOtros
                    vNeto = objUtilBL.isDecimal2(vNeto) + dNeto
                    vIsr = vIsr + 0
                    vIva = objUtilBL.isDecimal2(vIva) + dIva
                    vTotal = objUtilBL.isDecimal2(vTotal) + objUtilBL.isDecimal2(dBruto - dDescuento)

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = objUtilBL.isDecimal(item.cantidad.ToString())
                    Detalle.PRECIO = ((item.importe / item.cantidad))
                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = dExento
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                Next
            Else
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0
                    vExento = 0
                    dExento = 0
                    item = objDocumentoBL.getNotaCreditoByItem(dtFactura.Rows(i))

                    'Traer el precio por caja del material
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())


                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************
                    dBruto = item.importe
                    dPorcentaje = item.porcentajeDesto
                    dDescuento = (objUtilBL.isDecimal(dBruto * (dPorcentaje / 100)))
                    dTotal = dBruto - dDescuento
                    dNeto = 0
                    dIva = 0
                    'Un Ajuston 
                    dExento = dBruto - dDescuento - dIva
                    dNeto = 0
                    dBruto = dBruto - dDescuento
                    '*************************** FIN CALCULO GUATEFACTURAS  ***************************
                    vBruto = objUtilBL.isDecimal2(vBruto) + dBruto
                    vDescuento = objUtilBL.isDecimal2(vDescuento) + dDescuento
                    vExento = objUtilBL.isDecimal2(vExento) + dExento
                    vOtros = objUtilBL.isDecimal2(vOtros) + dOtros
                    vNeto = objUtilBL.isDecimal2(vNeto) + dNeto
                    vIsr = vIsr + 0
                    vIva = objUtilBL.isDecimal2(vIva) + dIva
                    vTotal = objUtilBL.isDecimal2(vTotal) + objUtilBL.isDecimal2(dBruto - dDescuento)

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = objUtilBL.isDecimal(item.cantidad.ToString())
                    Detalle.PRECIO = ((item.importe / item.cantidad))
                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = dExento
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    If (i = 0) Then
                        objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                    Else
                        If (i = dtFactura.Rows.Count - 1) Then
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                        Else
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                        End If
                    End If
                Next
            End If



            ' ******* Asignación de Datos de Facturación Documento Encabezado *******
            Documento.NITRECEPTOR = Cliente.nit
            Documento.NOMBRE = Cliente.negocio
            Documento.DIRECCION = Cliente.direccion
            Documento.TIPOVENTA = "B"
            Documento.DESTINOVENTA = 1
            Documento.FECHA = Format(CDate(factura.fechaEmision), "dd/MM/yyyy")
            Documento.MONEDA = 1
            Documento.TASA = 1.0
            Documento.REFERENCIA = factura.numero
            Documento.NUMEROACCESO = ""
            Documento.SERIEADMIN = factura.serie
            Documento.NUMEROADMIN = factura.numero
            Documento.REVERSION = "N"
            Documento.BRUTO = vBruto
            Documento.DESCUENTO = vDescuento
            Documento.EXENTO = vTotal
            Documento.OTROS = vOtros
            Documento.NETO = vNeto
            Documento.ISR = vIsr
            Documento.IVA = vIva
            Documento.TOTAL = vTotal
            Documento.NUMEROABONO = 0
            Documento.FECHAVENCIMIENTO = ""
            Documento.MONTOABONO = 0
            'Documento.SERIEFEL = NotaCredito.serieFEL.Substring(0, 8)
            Dim dtElectronico As DataTable
            dtElectronico = objDocumentoBL.datosElectronicosCXC(NotaCredito.idEncabezado)

            'Documento.SERIEFEL = NotaCredito.serieFEL.Substring(0, 8)
            Documento.SERIEFEL = dtElectronico.Rows(0).Item("PREIMPRESO")
            Documento.PREIMPRESO = dtElectronico.Rows(0).Item("NUMEROAUTORIZACION")



            ' ******************************************************************************

            id_glo_api = "7e3e0ab330fffd33f244089444ac5db7"

            ' ********************************************************* Convertir Objeto a JSON *********************************************************
            objEncabezado = "{""ENCABEZADO"":" & PW.JSON.JSONHelper.ObjectToString(Documento) & ",""DETALLE"":["
            objDetalle = objDetalle & "]}"
            '****** Conectividad al servicio Nautilus ******
            PostData = [String].Format("encabezado={0}&detalle={1}&sociedad={2}&tipo={3}&ruta={4}&serie={5}&correlativo={6}&latitud={7}&longitud={8}", objEncabezado, objDetalle, 4000, 2, id_glo_codRuta, factura.serie, factura.numero, gps.Latitude, gps.Longitude)
            request.Headers.Add("Authorization", id_glo_api)
            request.Credentials = CredentialCache.DefaultCredentials
            CType(request, HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
            request.Method = "POST"
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(PostData)
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()
            Dim response As WebResponse = request.GetResponse()
            'dataStream = response.GetResponseStream()
            Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            dataStream.Close()
            response.Close()
            'MessageBox.Show(responseFromServer)

            If (displayNC(responseFromServer, NotaCredito.idEncabezado)) Then
                Return True
            Else
                Return False
            End If
            'Return True
            ' *************************************************** Fin de la rutina de comunicacion ****************************************************
        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
            MsgBox("No se pudo generar NOTA DE CREDITO")
            Return False
        End Try
        'TextBox1.Text = PW.JSON.JSONHelper.ObjectToString(objprova)
    End Function

    Public Function display(ByVal cadena As String, ByVal factura As String) As Boolean
        Cursor.Current = Cursors.WaitCursor
        ' The file system path we need to split.|
        Dim s As String = cadena
        Dim estado As String = ""
        Dim serie As String = ""
        Dim autorizacion As String = ""
        Dim preimpreso As String = ""
        Dim direccionfel As String = ""
        Dim nombre_fel As String = ""
        ' Split the string on the backslash character.
        Dim parts As String() = s.Split(New Char() {";"c})
        Dim contador As Integer = 0
        Dim impresion As String = ""
        ' Loop through result strings with For Each.
        Dim part As String
        Try
            For Each part In parts
                If (contador = 1) Then
                    'impresion = impresion & "Error:" & part & Chr(13)
                    estado = part
                End If
                If (contador = 3) Then
                    impresion = impresion & "Serie:" & part & Chr(13)
                    serie = part
                End If
                If (contador = 5) Then
                    impresion = impresion & "Numero Autorización:" & part & Chr(13)
                    autorizacion = part
                End If
                If (contador = 7) Then
                    impresion = impresion & "Nombre:" & part & Chr(13)
                    nombre_fel = part
                End If
                If (contador = 9) Then
                    impresion = impresion & "Preimpreso:" & part
                    preimpreso = part
                End If
                If (contador = 11) Then
                    impresion = impresion & "Direccion:" & part
                    direccionfel = part
                End If
                contador = contador + 1
            Next
            'Dim conta = (Len(preimpreso)) - 1
            Dim conta = (Len(preimpreso))
            Dim cadena2 As String = ""
            cadena2 = Left(preimpreso, conta)
            If (Len(cadena2) > 0) Then
                objDocumentoBL.postFactura(factura, serie, autorizacion, cadena2, direccionfel, nombre_fel)
                MsgBox(impresion)
                Return True
            Else
                MessageBox.Show(cadena)
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        
        
    End Function

    Public Function displayNC(ByVal cadena As String, ByVal factura As String) As Boolean
        Cursor.Current = Cursors.WaitCursor
        ' The file system path we need to split.
        Dim s As String = cadena
        Dim estado As String = ""
        Dim serie As String = ""
        Dim autorizacion As String = ""
        Dim direccionfel As String = ""
        Dim preimpreso As String = ""
        Dim nombre_fel As String = ""
        ' Split the string on the backslash character.
        Dim parts As String() = s.Split(New Char() {";"c})
        Dim contador As Integer = 0
        Dim impresion As String = ""
        ' Loop through result strings with For Each.
        Dim part As String
        For Each part In parts
            If (contador = 1) Then
                'impresion = impresion & "Error:" & part & Chr(13)
                estado = part
            End If
            If (contador = 3) Then
                impresion = impresion & "Serie:" & part & Chr(13)
                serie = part
            End If
            If (contador = 5) Then
                impresion = impresion & "Numero Autorización:" & part & Chr(13)
                autorizacion = part
            End If
            If (contador = 7) Then
                impresion = impresion & "Nombre:" & part & Chr(13)
                nombre_fel = part
            End If
            If (contador = 9) Then
                impresion = impresion & "Preimpreso:" & part
                preimpreso = part
            End If

            If (contador = 11) Then
                impresion = impresion & "Direccion:" & part
                direccionfel = part
            End If

            contador = contador + 1
        Next
        'Dim conta = (Len(preimpreso)) - 1
        Dim conta = (Len(preimpreso))
        Dim cadena2 As String = ""
        cadena2 = Left(preimpreso, conta)
        If (Len(cadena2) > 0) Then
            objDocumentoBL.postFacturaNOTACREDITO(factura, serie, autorizacion, cadena2, direccionfel, nombre_fel)
            MsgBox(impresion)
            Return True
        Else
            Return False
        End If
    End Function


    Public Function displayAnulacion(ByVal cadena As String) As Boolean
        Cursor.Current = Cursors.WaitCursor
        ' The file system path we need to split.
        Dim s As String = cadena
        Dim estado As String = ""
        Dim serie As String = ""
        Dim autorizacion As String = ""
        Dim preimpreso As String = ""
        ' Split the string on the backslash character.
        Dim parts As String() = s.Split(New Char() {";"c})
        Dim contador As Integer = 0
        Dim impresion As String = ""
        Dim nombre_fel As String = ""
        Dim bandera As Boolean
        ' Loop through result strings with For Each.
        Try
            Dim part As String
            For Each part In parts
                If (contador = 1) Then
                    'impresion = impresion & "Error:" & part & Chr(13)
                    estado = part
                End If
                If (estado = "0") Then
                    If (contador = 3) Then
                        impresion = impresion & " " & part & Chr(13)
                        serie = part
                    End If
                    If (contador = 5) Then
                        impresion = impresion & "Emisor:" & part & Chr(13)
                        autorizacion = part
                    End If
                    If (contador = 7) Then
                        impresion = impresion & "Preimpreso:" & part & Chr(13)
                    End If
                    bandera = True
                Else
                    If (contador = 3) Then
                        impresion = impresion & "ERROR: " & part & Chr(13)
                        serie = part
                    End If
                    bandera = False
                End If

                contador = contador + 1
            Next
            'Dim conta = (Len(preimpreso)) - 1
            Dim conta = (Len(preimpreso))
            Dim cadena2 As String = ""
            cadena2 = Left(preimpreso, conta)
            MsgBox(impresion)
            Return bandera
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function generador2(ByVal idEncabezado As Integer, ByRef Cliente As ClienteCO) As Boolean
        '*** Declaración de Variables de la función ***
        Dim vBruto As Double = 0
        Dim vDescuento As Double = 0
        Dim vExento As Double = 0
        Dim vOtros As Double = 0
        Dim vNeto As Double = 0
        Dim vIsr As Double = 0
        Dim vIva As Double = 0
        Dim vTotal As Double = 0
        Dim dBruto, dDescuento, dTotal, dNeto, dIva, dPorcentaje, dOtros As Double
        Dim item As ItemCO
        Dim factura As New documentoCO      'Variable del tipo data Table
        Dim dtFactura As DataTable
        Dim objEncabezado As String = ""
        Dim objDetalle As String = ""
        Dim PostData As String = ""
        Dim dtCredenciales As DataTable
        Dim servidorFEL As String = ""
        Dim servicioFEL As String = ""
        Dim objProductoBL As New ProductoBL
        Dim comCliente As New ClienteCO
        Dim protocoloFEL As String = ""
        Dim comProducto As New ProductoCO
        dtCredenciales = objDocumentoBL.credencialesServidor()
        servidorFEL = dtCredenciales.Rows(0).Item("SERVIDOR")
        servicioFEL = dtCredenciales.Rows(0).Item("SERVICIO")
        protocoloFEL = dtCredenciales.Rows(0).Item("PROTOCOLO")
        If id_glo_protocolo = "https" Then
            servidorFEL = ""
            protocoloFEL = id_glo_protocolo
        End If
        'Envio de la Ubicación de donde se esta Generando la Factura Electronica
        Try
            gps.start()
            '--- Default values if no GPS is captured.
            If gps.Latitude = "" Then gps.Latitude = "0"
            If gps.Longitude = "" Then gps.Longitude = "0"
            If Len(gps.Latitude) > 15 Then gps.Latitude = gps.Latitude.Substring(0, 15)
            If Len(gps.Longitude) > 15 Then gps.Longitude = gps.Longitude.Substring(0, 15)
        Catch ex As Exception
            gps.stop()
        End Try
        'Dim request As WebRequest = WebRequest.Create("http://192.9.0.36:8080/nautilusp/v1/generadocumento")
        'Dim request As WebRequest = WebRequest.Create(protocoloFEL & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentodlsa")

        Dim request As WebRequest = WebRequest.Create("HTTP" & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentodlsa")
        
        Try
            Cursor.Current = Cursors.Default
            'Lectura de datos Detalle Facturación
            dtFactura = objDocumentoBL.getFacturaDetalle(idEncabezado)
            factura = objDocumentoBL.getFactura(idEncabezado)
            comCliente = objCliente.getDetalleDelCliente(factura.idCliente)
            Cliente.nit = comCliente.nit
            Cliente.numeroDi = comCliente.numeroDi
            '*************************************************************************
            Dim registros = 0
            registros = dtFactura.Rows.Count

            If (registros = 1) Then
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0

                    item = objDocumentoBL.getFacturaByItem(dtFactura.Rows(i).Item("item").ToString, dtFactura.Rows(i))
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())
                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************
                    dBruto = ((objUtilBL.isDecimal(item.cantidad) / objUtilBL.isDecimal(comProducto.unidadesCaja)) * (objUtilBL.isDecimal(item.precio)))

                    If (item.porcentajeDesto < 0) Then
                        dPorcentaje = item.porcentajeDesto * (-1)
                    Else
                        dPorcentaje = item.porcentajeDesto
                    End If

                    dDescuento = (objUtilBL.isDecimal(dBruto * (dPorcentaje / 100)))
                    dTotal = dBruto - dDescuento
                    dNeto = objUtilBL.isDecimal(dTotal / 1.12)
                    dIva = objUtilBL.isDecimal(dNeto * 0.12)
                    'Un Ajuston 
                    dNeto = dBruto - dDescuento - dIva
                    dBruto = dDescuento + dNeto + dIva - dOtros
                    '*************************** FIN CALCULO GUATEFACTURAS  ***************************

                    vBruto = objUtilBL.isDecimal2(vBruto) + dBruto
                    vDescuento = objUtilBL.isDecimal2(vDescuento) + dDescuento
                    vExento = objUtilBL.isDecimal2(vExento) + 0
                    vOtros = objUtilBL.isDecimal2(vOtros) + dOtros
                    vNeto = objUtilBL.isDecimal2(vNeto) + dNeto
                    vIsr = vIsr + 0
                    vIva = objUtilBL.isDecimal2(vIva) + dIva
                    vTotal = objUtilBL.isDecimal2(vTotal) + objUtilBL.isDecimal2(dBruto - dDescuento)

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = objUtilBL.isDecimal(item.cantidad.ToString())
                    If (Detalle.CANTIDAD < comProducto.unidadesCaja) Then
                        Detalle.PRECIO = objUtilBL.isDecimal(item.precio)
                    Else
                        Detalle.PRECIO = objUtilBL.isDecimal(item.precio) / objUtilBL.isDecimal(comProducto.unidadesCaja)
                    End If
                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = 0
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                Next
            Else
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0

                    item = objDocumentoBL.getFacturaByItem(dtFactura.Rows(i).Item("item").ToString, dtFactura.Rows(i))
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())
                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************
                    dBruto = ((objUtilBL.isDecimal(item.cantidad) / objUtilBL.isDecimal(comProducto.unidadesCaja)) * (objUtilBL.isDecimal(item.precio)))

                    If (item.porcentajeDesto < 0) Then

                        dPorcentaje = item.porcentajeDesto * (-1)
                    Else
                        dPorcentaje = item.porcentajeDesto
                    End If

                    dDescuento = (objUtilBL.isDecimal(dBruto * (dPorcentaje / 100)))
                    dTotal = dBruto - dDescuento
                    dNeto = objUtilBL.isDecimal(dTotal / 1.12)
                    dIva = objUtilBL.isDecimal(dNeto * 0.12)
                    'Un Ajuston 
                    dNeto = dBruto - dDescuento - dIva
                    dBruto = dDescuento + dNeto + dIva - dOtros
                    '*************************** FIN CALCULO GUATEFACTURAS  ***************************

                    vBruto = objUtilBL.isDecimal2(vBruto) + dBruto
                    vDescuento = objUtilBL.isDecimal2(vDescuento) + dDescuento
                    vExento = objUtilBL.isDecimal2(vExento) + 0
                    vOtros = objUtilBL.isDecimal2(vOtros) + dOtros
                    vNeto = objUtilBL.isDecimal2(vNeto) + dNeto
                    vIsr = vIsr + 0
                    vIva = objUtilBL.isDecimal2(vIva) + dIva
                    vTotal = objUtilBL.isDecimal2(vTotal) + objUtilBL.isDecimal2(dBruto - dDescuento)

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = objUtilBL.isDecimal(item.cantidad.ToString())
                    Detalle.PRECIO = objUtilBL.isDecimal(item.precio) / objUtilBL.isDecimal(comProducto.unidadesCaja)
                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = 0
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    If (i = 0) Then
                        objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                    Else
                        If (i = dtFactura.Rows.Count - 1) Then
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                        Else
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                        End If
                    End If
                Next
            End If



            ' ******* Asignación de Datos de Facturación Documento Encabezado *******
            Documento.NITRECEPTOR = Cliente.nit
            Documento.CUIRECEPTOR = Cliente.numeroDi

            Try
                Dim texto As String = ""
                texto = Replace(Cliente.nit, "-", "")
                Cliente.nit = Replace(texto, "/", "")
            Catch ex As Exception
                Cliente.nit = "CF"
            End Try



            'El cliente tiene NIT
            If Cliente.nit <> "CF" Then
                Documento.TIPORECEPTOR = 4
                Documento.IDRECEPTOR = Cliente.nit
            Else
                'el cliente tiene CF en su dato maestro
                If vTotal <= id_glo_total_cf Then
                    Documento.TIPORECEPTOR = 4
                    Documento.IDRECEPTOR = Cliente.nit
                Else
                    If Len(Cliente.numeroDi) > 0 Then
                        If Cliente.numeroDi.Substring(0, 1) <> "P" Then
                            Documento.TIPORECEPTOR = 2
                            Documento.IDRECEPTOR = Cliente.numeroDi
                        Else
                            Try
                                Dim quitarP As String = ""
                                quitarP = Replace(Cliente.numeroDi, "P", "")
                                Documento.TIPORECEPTOR = 3
                                Documento.IDRECEPTOR = quitarP
                            Catch ex As Exception
                                Documento.TIPORECEPTOR = 4
                                Documento.IDRECEPTOR = Cliente.nit
                            End Try
                        End If
                    End If
                End If
            End If


            Documento.NOMBRE = Cliente.negocio
            Documento.DIRECCION = Cliente.direccion
            Documento.TIPOVENTA = "B"
            Documento.DESTINOVENTA = 1
            Documento.FECHA = Format(CDate(factura.fechaEmision), "dd/MM/yyyy")
            Documento.MONEDA = 1
            Documento.TASA = 1.0
            Documento.REFERENCIA = factura.numero
            Documento.NUMEROACCESO = ""
            Documento.SERIEADMIN = factura.serie
            Documento.NUMEROADMIN = factura.numero
            Documento.REVERSION = "N"
            Documento.BRUTO = vBruto
            Documento.DESCUENTO = vDescuento
            Documento.EXENTO = vExento
            Documento.OTROS = vOtros
            Documento.NETO = vNeto
            Documento.ISR = vIsr
            Documento.IVA = vIva
            Documento.TOTAL = vTotal
            Documento.NUMEROABONO = 0
            If (factura.fechaVence Is DBNull.Value) Then
                Documento.FECHAVENCIMIENTO = Format(CDate(factura.fechaVence), "yyyyMMdd")
            Else
                Documento.FECHAVENCIMIENTO = Format(CDate(factura.fechaEmision), "yyyyMMdd")
            End If

            Documento.MONTOABONO = 0
            ' ******************************************************************************

            id_glo_api = "7e3e0ab330fffd33f244089444ac5db7"

            ' ********************************************************* Convertir Objeto a JSON *********************************************************
            objEncabezado = "{""ENCABEZADO"":" & PW.JSON.JSONHelper.ObjectToString(Documento) & ",""DETALLE"":["
            objDetalle = objDetalle & "]}"
            '****** Conectividad al servicio Nautilus ******
            objEncabezado = objEncabezado.Replace("&", " ")
            objDetalle = objDetalle.Replace("&", " ")
            PostData = [String].Format("encabezado={0}&detalle={1}&sociedad={2}&tipo={3}&ruta={4}&serie={5}&correlativo={6}&latitud={7}&longitud={8}", objEncabezado, objDetalle, id_glo_sociedad, 2, id_glo_codRuta, factura.serie, factura.numero, gps.Latitude, gps.Longitude)
            'PostData = [String].Format("encabezado={0}&detalle={1}" + objEncabezado
            request.Headers.Add("Authorization", id_glo_api)
            request.Credentials = CredentialCache.DefaultCredentials
            CType(request, HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
            request.Method = "POST"
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(PostData)
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()
            Dim response As WebResponse = request.GetResponse()
            'dataStream = response.GetResponseStream()
            Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            dataStream.Close()
            response.Close()
            'Respuesta = PW.JSON.JSONHelper.StringToObject(responseFromServer, GetType(felRespuesta))
            If display(responseFromServer, idEncabezado) Then
                '********* INICIO LOGICA DE AJUSTE ****** 
                objDocumentoBL.UpdateTipoReceptor(Documento.TIPORECEPTOR, Documento.IDRECEPTOR, idEncabezado)
                Return True
            End If


            'MessageBox.Show(responseFromServer)

            'Dim respuestas As felRespuesta
            'respuestas = PW.JSON.JSONHelper.StringToObject(responseFromServer, GetType(felRespuesta))
            'Console.WriteLine(respuestas.ESTADO)
            Return True
            ' *************************************************** Fin de la rutina de comunicacion ****************************************************
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            Return False
        End Try
        'TextBox1.Text = PW.JSON.JSONHelper.ObjectToString(objprova)
    End Function

    Public Function generador3(ByVal idEncabezado As Integer, ByRef Cliente As ClienteCO) As Boolean
        '*** Declaración de Variables de la función ***
        Dim vBruto As Double = 0
        Dim vDescuento As Double = 0
        Dim vExento As Double = 0
        Dim vOtros As Double = 0
        Dim vNeto As Double = 0
        Dim vIsr As Double = 0
        Dim vIva As Double = 0
        Dim vTotal As Double = 0
        Dim dBruto, dDescuento, dDescuentoDPP, dTotal, dNeto, dIva, dPorcentaje, dOtros, dprecioUnidad As Double
        Dim item As ItemCO
        Dim factura As New documentoCO      'Variable del tipo data Table
        Dim dtFactura As DataTable
        Dim objEncabezado As String = ""
        Dim objDetalle As String = ""
        Dim PostData As String = ""
        Dim dtCredenciales As DataTable
        Dim servidorFEL As String = ""
        Dim servicioFEL As String = ""
        Dim objProductoBL As New ProductoBL
        Dim comCliente As New ClienteCO
        Dim protocoloFEL As String = ""
        Dim comProducto As New ProductoCO
        dtCredenciales = objDocumentoBL.credencialesServidor()
        servidorFEL = dtCredenciales.Rows(0).Item("SERVIDOR")
        servicioFEL = dtCredenciales.Rows(0).Item("SERVICIO")
        protocoloFEL = dtCredenciales.Rows(0).Item("PROTOCOLO")
        If id_glo_protocolo = "https" Then
            servidorFEL = ""
            protocoloFEL = id_glo_protocolo
        End If
        'Envio de la Ubicación de donde se esta Generando la Factura Electronica
        Try
            gps.start()
            '--- Default values if no GPS is captured.
            If gps.Latitude = "" Then gps.Latitude = "0"
            If gps.Longitude = "" Then gps.Longitude = "0"
            If Len(gps.Latitude) > 15 Then gps.Latitude = gps.Latitude.Substring(0, 15)
            If Len(gps.Longitude) > 15 Then gps.Longitude = gps.Longitude.Substring(0, 15)
        Catch ex As Exception
            gps.stop()
        End Try
        'Dim request As WebRequest = WebRequest.Create("http://192.9.0.36:8080/nautilusp/v1/generadocumento")
        'Dim request As WebRequest = WebRequest.Create(protocoloFEL & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentodlsa2")

        'Dim request As WebRequest = WebRequest.Create("http" & "://" + "192.9.2.56" + "/" + "NAUTILUSPRD" + "/v1/generadocumentodlsa2")
        Dim request As WebRequest = WebRequest.Create(protocoloFEL & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentodlsa2")
        
        Try
            Cursor.Current = Cursors.Default
            'Lectura de datos Detalle Facturación
            dtFactura = objDocumentoBL.getFacturaDetalle(idEncabezado)
            factura = objDocumentoBL.getFactura(idEncabezado)
            comCliente = objCliente.getDetalleDelCliente(factura.idCliente)
            Cliente.nit = comCliente.nit
            Cliente.numeroDi = comCliente.numeroDi
            '*************************************************************************
            Dim registros = 0
            Dim Pagocontado As Integer = 0
            Dim PagoCredito As Integer = 0
            Dim PagoCheque As Integer = 0
            Dim descuentodpp As Integer = 0

            registros = dtFactura.Rows.Count

            If (registros = 1) Then
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0

                    item = objDocumentoBL.getFacturaByItem(dtFactura.Rows(i).Item("item").ToString, dtFactura.Rows(i))
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())
                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************

                    'Generación de calculos nuevos según la definición realizada en reunión IT
                    'dprecioUnidad = objUtilBL.isDecimal4((item.precio) / (comProducto.unidadesCaja))
                    dprecioUnidad = objUtilBL.isDecimal6((item.importe) / (item.cantidad))
                    dBruto = objUtilBL.isDecimal(item.cantidad * dprecioUnidad)

                    'If (item.porcentajeDesto < 0) Then
                    ' dPorcentaje = objUtilBL.isDecimal4(((item.importeDesto + item.valorIvaDesto) * 100) / dBruto) * (-1)
                    ' Else
                    ' dPorcentaje = objUtilBL.isDecimal4(((item.importeDesto + item.valorIvaDesto) * 100) / dBruto)
                    ' End If

                    'objDocumentoBL.actualizarDetalleFact(dtFactura.Rows(i).Item("id_detFactura").ToString, idEncabezado, dtFactura.Rows(i).Item("idProducto").ToString, objUtilBL.isDecimal(dBruto), objUtilBL.isDecimal(dBruto / 1.12))



                    If (item.importeDesto = 0) Then
                        dDescuento = 0
                        dPorcentaje = 0
                    Else
                        'dPorcentaje = objUtilBL.isDecimal4(((item.importeDesto + item.valorIvaDesto) * 100) / dBruto) * (-1)
                        dDescuento = objUtilBL.isDecimal(item.importeDesto * (-1) + item.valorIvaDesto * (-1))
                        descuentodpp = item.importeDestoPP
                        dPorcentaje = objUtilBL.isDecimal4(dDescuento * 100 / dBruto)
                    End If


                    'dDescuento = objUtilBL.isDecimal(item.importeDesto + item.valorIvaDesto)
                    'dPorcentaje = objUtilBL.isDecimal4(dDescuento * 100 / dBruto)
                    dNeto = objUtilBL.isDecimal((dBruto - dDescuento) / 1.12)
                    dIva = objUtilBL.isDecimal(dBruto - dNeto - dDescuento)
                    dTotal = objUtilBL.isDecimal(dNeto + dIva)


                    vBruto = vBruto + dBruto
                    vDescuento = vDescuento + dDescuento
                    vExento = vExento + 0
                    vOtros = vOtros + dOtros
                    vNeto = vNeto + dNeto
                    vIsr = vIsr + 0
                    vIva = vIva + dIva
                    vTotal = vTotal + dTotal

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.CANTIDADGRAVABLE = 0
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = objUtilBL.isDecimal5(item.cantidad.ToString())

                    'If (Detalle.CANTIDAD < comProducto.unidadesCaja) Then
                    ' Detalle.PRECIO = objUtilBL.isDecimal5(item.precio)
                    'Else
                    'Detalle.PRECIO = objUtilBL.isDecimal(item.precio) / objUtilBL.isDecimal(comProducto.unidadesCaja)
                    'End If
                    Detalle.PRECIO = dprecioUnidad

                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = 0
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal5(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                Next
            Else
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0
                    dprecioUnidad = 0

                    item = objDocumentoBL.getFacturaByItem(dtFactura.Rows(i).Item("item").ToString, dtFactura.Rows(i))
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())
                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************

                    'Generación de calculos nuevos según la definición realizada en reunión IT
                    dprecioUnidad = objUtilBL.isDecimal6((item.importe) / (item.cantidad))
                    dBruto = objUtilBL.isDecimal(item.cantidad * dprecioUnidad)

                    'If (item.porcentajeDesto < 0) Then
                    ' dPorcentaje = objUtilBL.isDecimal4(((item.importeDesto + item.valorIvaDesto) * 100) / dBruto) * (-1)
                    ' Else
                    ' dPorcentaje = objUtilBL.isDecimal4(((item.importeDesto + item.valorIvaDesto) * 100) / dBruto)
                    ' End If

                    'objDocumentoBL.actualizarDetalleFact(dtFactura.Rows(i).Item("id_detFactura").ToString, idEncabezado, dtFactura.Rows(i).Item("idProducto").ToString, objUtilBL.isDecimal(dBruto), objUtilBL.isDecimal(dBruto / 1.12))



                    If (item.importeDesto = 0) Then
                        dDescuento = 0
                        dPorcentaje = 0
                    Else
                        'dPorcentaje = objUtilBL.isDecimal4(((item.importeDesto + item.valorIvaDesto) * 100) / dBruto) * (-1)
                        dDescuento = objUtilBL.isDecimal(item.importeDesto * (-1) + item.valorIvaDesto * (-1))
                        dPorcentaje = objUtilBL.isDecimal4(dDescuento * 100 / dBruto)
                    End If


                    'dDescuento = objUtilBL.isDecimal(item.importeDesto + item.valorIvaDesto)
                    'dPorcentaje = objUtilBL.isDecimal4(dDescuento * 100 / dBruto)
                    dNeto = objUtilBL.isDecimal((dBruto - dDescuento) / 1.12)
                    dIva = objUtilBL.isDecimal(dBruto - dNeto - dDescuento)
                    dTotal = objUtilBL.isDecimal(dNeto + dIva)


                    vBruto = vBruto + dBruto
                    vDescuento = vDescuento + dDescuento
                    vExento = vExento + 0
                    vOtros = vOtros + dOtros
                    vNeto = vNeto + dNeto
                    vIsr = vIsr + 0
                    vIva = vIva + dIva
                    vTotal = vTotal + dTotal

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.CANTIDADGRAVABLE = 0
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = objUtilBL.isDecimal(item.cantidad.ToString())
                    Detalle.PRECIO = dprecioUnidad
                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = 0
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = dTotal
                    Detalle.TIPOVENTADET = "B"
                    If (i = 0) Then
                        objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                    Else
                        If (i = dtFactura.Rows.Count - 1) Then
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                        Else
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                        End If
                    End If
                Next
            End If



            ' ******* Asignación de Datos de Facturación Documento Encabezado *******
            Documento.NITRECEPTOR = Cliente.nit
            Documento.CUIRECEPTOR = Cliente.numeroDi

            Try
                Dim texto As String = ""
                texto = Replace(Cliente.nit, "-", "")
                Cliente.nit = Replace(texto, "/", "")
            Catch ex As Exception
                Cliente.nit = "CF"
            End Try



            'El cliente tiene NIT
            If Cliente.nit <> "CF" Then
                Documento.TIPORECEPTOR = 4
                Documento.IDRECEPTOR = Cliente.nit
            Else
                'el cliente tiene CF en su dato maestro
                If vTotal <= id_glo_total_cf Then
                    Documento.TIPORECEPTOR = 4
                    Documento.IDRECEPTOR = Cliente.nit
                Else
                    If Len(Cliente.numeroDi) > 0 Then
                        If Cliente.numeroDi.Substring(0, 1) <> "P" Then
                            Documento.TIPORECEPTOR = 2
                            Documento.IDRECEPTOR = Cliente.numeroDi
                        Else
                            Try
                                Dim quitarP As String = ""
                                quitarP = Replace(Cliente.numeroDi, "P", "")
                                Documento.TIPORECEPTOR = 3
                                Documento.IDRECEPTOR = quitarP
                            Catch ex As Exception
                                Documento.TIPORECEPTOR = 4
                                Documento.IDRECEPTOR = Cliente.nit
                            End Try
                        End If
                    End If
                End If
            End If

            Documento.NOMBRE = Cliente.negocio
            Documento.DIRECCION = Cliente.direccion
            Documento.TIPOVENTA = "B"
            Documento.DESTINOVENTA = 1
            Documento.FECHA = Format(CDate(factura.fechaEmision), "dd/MM/yyyy")
            Documento.MONEDA = 1
            Documento.TASA = 1.0
            Documento.REFERENCIA = factura.numero
            If (Len(factura.numeroacceso) > 0) Then
                Documento.NUMEROACCESO = factura.numeroacceso
            Else
                Documento.NUMEROACCESO = "" 'factura.numeroacceso
            End If
            Documento.SERIEADMIN = factura.serie
            Documento.NUMEROADMIN = factura.numero
            Documento.REVERSION = "N"
            Documento.BRUTO = vBruto
            Documento.DESCUENTO = vDescuento
            Documento.EXENTO = vExento
            Documento.OTROS = vOtros
            Documento.NETO = vNeto
            Documento.ISR = vIsr
            Documento.IVA = vIva
            Documento.TOTAL = vTotal
            Documento.NUMEROABONO = 0
            If (factura.fechaVence Is DBNull.Value) Then
                Documento.FECHAVENCIMIENTO = Format(CDate(factura.fechaVence), "yyyyMMdd")
            Else
                Documento.FECHAVENCIMIENTO = Format(CDate(factura.fechaEmision), "yyyyMMdd")
            End If

            Documento.MONTOABONO = 0
            ' ******************************************************************************
            'Actualizar Recibo
            'objDocumentoBL.actualizarReciboImporte(OrdenVenta.idEncabezado, vBruto)

            'Actualizar Recibo_pagos
            'objDocumentoBL.actualizarReciboImporte(OrdenVenta.idEncabezado, vBruto)

            'Actualizar tabla recibos pagos

            '********* FIN LOGICA DE AJUSTE ****** 

            'objDocumentoBL.actualizarFACTImporte(idEncabezado, vBruto)

            'Actualizar Recibo_pagos
            'objDocumentoBL.actualizarReciboImporte(idEncabezado, vBruto)

            id_glo_api = "7e3e0ab330fffd33f244089444ac5db7"

            ' ********************************************************* Convertir Objeto a JSON *********************************************************
            objEncabezado = "{""ENCABEZADO"":" & PW.JSON.JSONHelper.ObjectToString(Documento) & ",""DETALLE"":["
            objDetalle = objDetalle & "]}"
            '****** Conectividad al servicio Nautilus ******
            'objEncabezado = QuitarCaracteres(objEncabezado)
            'objDetalle = QuitarCaracteres(objDetalle)
            objEncabezado = objEncabezado.Replace("&", " ")
            objDetalle = objDetalle.Replace("&", " ")
            PostData = [String].Format("encabezado={0}&detalle={1}&sociedad={2}&tipo={3}&ruta={4}&serie={5}&correlativo={6}&latitud={7}&longitud={8}", objEncabezado, objDetalle, id_glo_sociedad, 2, id_glo_codRuta, factura.serie, factura.numero, gps.Latitude, gps.Longitude)
            'PostData = [String].Format("encabezado={0}&detalle={1}" + objEncabezado
            request.Headers.Add("Authorization", id_glo_api)
            request.Credentials = CredentialCache.DefaultCredentials
            CType(request, HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
            request.Method = "POST"
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(PostData)
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()
            Dim response As WebResponse = request.GetResponse()
            'dataStream = response.GetResponseStream()
            Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            dataStream.Close()
            response.Close()
            'Respuesta = PW.JSON.JSONHelper.StringToObject(responseFromServer, GetType(felRespuesta))

            If display(responseFromServer, idEncabezado) Then

                objDocumentoBL.UpdateTipoReceptor(Documento.TIPORECEPTOR, Documento.IDRECEPTOR, idEncabezado)
                Return True
                '********* INICIO LOGICA DE AJUSTE ****** 
                'importeActual = factura.importe
                'importeDestoActual = factura.importeDesto

                'diferenciasImporte = factura.importe - objUtilBL.isDecimal(vBruto)

                'Actualizar el importe de la factura de 
                'diferenciasImporteDesto = factura.importeDesto + objUtilBL.isDecimal(vDescuento)

                'objDocumentoBL.actualizarFACTImporte(idEncabezado, objUtilBL.isDecimal(diferenciasImporte), factura.idReciboRelacionado)

                'If diferenciasImporte <> 0 Then
                'If (objDocumentoBL.getPagoReciboFactura(idEncabezado, "E", factura.idReciboRelacionado) = True) Then
                ' objDocumentoBL.actualizarReciboPagos(factura.idReciboRelacionado, diferenciasImporte, "E")
                'Else
                '   If (objDocumentoBL.getPagoReciboFactura(idEncabezado, "C", factura.idReciboRelacionado) > 0) Then
                'objDocumentoBL.actualizarReciboPagos(factura.idReciboRelacionado, diferenciasImporte, "C")
                'Else
                '   If (objDocumentoBL.getPagoReciboFactura(idEncabezado, "CR", factura.idReciboRelacionado) > 0) Then
                'objDocumentoBL.actualizarReciboPagos(factura.idReciboRelacionado, diferenciasImporte, "CR")
                'End If
                'End If
                'End If
                'End If
            End If
            'MessageBox.Show(responseFromServer)

            'Dim respuestas As felRespuesta
            'respuestas = PW.JSON.JSONHelper.StringToObject(responseFromServer, GetType(felRespuesta))
            'Console.WriteLine(respuestas.ESTADO)
            Return True
            ' *************************************************** Fin de la rutina de comunicacion ****************************************************
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            Return False
        End Try
        'TextBox1.Text = PW.JSON.JSONHelper.ObjectToString(objprova)
    End Function

    Public Function generadorFaltante(ByVal idEncabezado As Integer, ByRef Cliente As ClienteCO) As Boolean
        '*** Declaración de Variables de la función ***
        Dim vBruto As Double = 0
        Dim vDescuento As Double = 0
        Dim vExento As Double = 0
        Dim vOtros As Double = 0
        Dim vNeto As Double = 0
        Dim vIsr As Double = 0
        Dim vIva As Double = 0
        Dim vTotal As Double = 0
        Dim dBruto, dDescuento, dTotal, dNeto, dIva, dPorcentaje, dOtros, dprecioUnidad As Double
        Dim item As ItemCO
        Dim factura As New documentoCO      'Variable del tipo data Table
        Dim dtFactura As DataTable
        Dim objEncabezado As String = ""
        Dim objDetalle As String = ""
        Dim PostData As String = ""
        Dim dtCredenciales As DataTable
        Dim servidorFEL As String = ""
        Dim servicioFEL As String = ""
        Dim objProductoBL As New ProductoBL
        Dim protocoloFEL As String = ""
        Dim comProducto As New ProductoCO
        dtCredenciales = objDocumentoBL.credencialesServidor()
        servidorFEL = dtCredenciales.Rows(0).Item("SERVIDOR")
        servicioFEL = dtCredenciales.Rows(0).Item("SERVICIO")
        protocoloFEL = dtCredenciales.Rows(0).Item("PROTOCOLO")
        If id_glo_protocolo = "https" Then
            servidorFEL = ""
            protocoloFEL = id_glo_protocolo
        End If
        'Envio de la Ubicación de donde se esta Generando la Factura Electronica
        Try
            gps.start()
            '--- Default values if no GPS is captured.
            If gps.Latitude = "" Then gps.Latitude = "0"
            If gps.Longitude = "" Then gps.Longitude = "0"
            If Len(gps.Latitude) > 15 Then gps.Latitude = gps.Latitude.Substring(0, 15)
            If Len(gps.Longitude) > 15 Then gps.Longitude = gps.Longitude.Substring(0, 15)
        Catch ex As Exception
            gps.stop()
        End Try
        'Dim request As WebRequest = WebRequest.Create("http://192.9.0.36:8080/nautilusp/v1/generadocumento")
        'Dim request As WebRequest = WebRequest.Create(protocoloFEL & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentodlsa2")

        'Dim request As WebRequest = WebRequest.Create("http" & "://" + "192.9.2.56" + "/" + "NAUTILUSPRD" + "/v1/generadocumentodlsa2")
        Dim request As WebRequest = WebRequest.Create(protocoloFEL & "://" + servidorFEL + "/" + servicioFEL + "/v1/generadocumentodlsa2")

        Try
            Cursor.Current = Cursors.Default
            'Lectura de datos Detalle Facturación
            dtFactura = objDocumentoBL.getFacturaDetalle(idEncabezado)
            factura = objDocumentoBL.getFactura(idEncabezado)
            '*************************************************************************
            Dim registros = 0
            Dim Pagocontado As Integer = 0
            Dim PagoCredito As Integer = 0
            Dim PagoCheque As Integer = 0

            registros = dtFactura.Rows.Count

            If (registros = 1) Then
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0

                    item = objDocumentoBL.getFacturaByItem(dtFactura.Rows(i).Item("item").ToString, dtFactura.Rows(i))
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())
                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************

                    'Generación de calculos nuevos según la definición realizada en reunión IT
                    'dprecioUnidad = objUtilBL.isDecimal4((item.precio) / (comProducto.unidadesCaja))
                    dprecioUnidad = objUtilBL.isDecimal6((item.importe) / (item.cantidad))
                    dBruto = objUtilBL.isDecimal(item.cantidad * dprecioUnidad)

                    If (item.importeDesto = 0) Then
                        dDescuento = 0
                        dPorcentaje = 0
                    Else
                        'dPorcentaje = objUtilBL.isDecimal4(((item.importeDesto + item.valorIvaDesto) * 100) / dBruto) * (-1)
                        dDescuento = objUtilBL.isDecimal(item.importeDesto * (-1) + item.valorIvaDesto * (-1))
                        dPorcentaje = objUtilBL.isDecimal4(dDescuento * 100 / dBruto)
                    End If


                    'dDescuento = objUtilBL.isDecimal(item.importeDesto + item.valorIvaDesto)
                    'dPorcentaje = objUtilBL.isDecimal4(dDescuento * 100 / dBruto)
                    dNeto = objUtilBL.isDecimal((dBruto - dDescuento) / 1.12)
                    dIva = objUtilBL.isDecimal(dBruto - dNeto - dDescuento)
                    dTotal = objUtilBL.isDecimal(dNeto + dIva)


                    vBruto = vBruto + dBruto
                    vDescuento = vDescuento + dDescuento
                    vExento = vExento + 0
                    vOtros = vOtros + dOtros
                    vNeto = vNeto + dNeto
                    vIsr = vIsr + 0
                    vIva = vIva + dIva
                    vTotal = vTotal + dTotal

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.CANTIDADGRAVABLE = 0
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = objUtilBL.isDecimal5(item.cantidad.ToString())

                    'If (Detalle.CANTIDAD < comProducto.unidadesCaja) Then
                    ' Detalle.PRECIO = objUtilBL.isDecimal5(item.precio)
                    'Else
                    'Detalle.PRECIO = objUtilBL.isDecimal(item.precio) / objUtilBL.isDecimal(comProducto.unidadesCaja)
                    'End If
                    Detalle.PRECIO = dprecioUnidad

                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = 0
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = objUtilBL.isDecimal5(dBruto - dDescuento)
                    Detalle.TIPOVENTADET = "B"
                    objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                Next
            Else
                ' ******************--- Detalle de la factura ******************---
                For i As Integer = 0 To dtFactura.Rows.Count - 1
                    dBruto = 0
                    dDescuento = 0
                    dTotal = 0
                    dNeto = 0
                    dIva = 0
                    dOtros = 0
                    dPorcentaje = 0
                    dprecioUnidad = 0

                    item = objDocumentoBL.getFacturaByItem(dtFactura.Rows(i).Item("item").ToString, dtFactura.Rows(i))
                    comProducto = objProductoBL.getDetalleDelProducto(item.idProducto.ToString())
                    '*************************** CALCULO REGLAS GUATEFACTURAS ***************************

                    'Generación de calculos nuevos según la definición realizada en reunión IT
                    dprecioUnidad = objUtilBL.isDecimal6((item.importe) / (item.cantidad))
                    dBruto = objUtilBL.isDecimal(item.cantidad * dprecioUnidad)

                    'If (item.porcentajeDesto < 0) Then
                    ' dPorcentaje = objUtilBL.isDecimal4(((item.importeDesto + item.valorIvaDesto) * 100) / dBruto) * (-1)
                    ' Else
                    ' dPorcentaje = objUtilBL.isDecimal4(((item.importeDesto + item.valorIvaDesto) * 100) / dBruto)
                    ' End If

                    'objDocumentoBL.actualizarDetalleFact(dtFactura.Rows(i).Item("id_detFactura").ToString, idEncabezado, dtFactura.Rows(i).Item("idProducto").ToString, objUtilBL.isDecimal(dBruto), objUtilBL.isDecimal(dBruto / 1.12))



                    If (item.importeDesto = 0) Then
                        dDescuento = 0
                        dPorcentaje = 0
                    Else
                        'dPorcentaje = objUtilBL.isDecimal4(((item.importeDesto + item.valorIvaDesto) * 100) / dBruto) * (-1)
                        dDescuento = objUtilBL.isDecimal(item.importeDesto * (-1) + item.valorIvaDesto * (-1))
                        dPorcentaje = objUtilBL.isDecimal4(dDescuento * 100 / dBruto)
                    End If


                    'dDescuento = objUtilBL.isDecimal(item.importeDesto + item.valorIvaDesto)
                    'dPorcentaje = objUtilBL.isDecimal4(dDescuento * 100 / dBruto)
                    dNeto = objUtilBL.isDecimal((dBruto - dDescuento) / 1.12)
                    dIva = objUtilBL.isDecimal(dBruto - dNeto - dDescuento)
                    dTotal = objUtilBL.isDecimal(dNeto + dIva)


                    vBruto = vBruto + dBruto
                    vDescuento = vDescuento + dDescuento
                    vExento = vExento + 0
                    vOtros = vOtros + dOtros
                    vNeto = vNeto + dNeto
                    vIsr = vIsr + 0
                    vIva = vIva + dIva
                    vTotal = vTotal + dTotal

                    Detalle.ITEM = i
                    Detalle.PRODUCTO = item.idProducto.ToString()
                    Detalle.CANTIDADGRAVABLE = 0
                    Detalle.DESCRIPCION = item.descripcion.ToString()
                    Detalle.MEDIDA = 1
                    Detalle.CANTIDAD = objUtilBL.isDecimal(item.cantidad.ToString())
                    Detalle.PRECIO = dprecioUnidad
                    Detalle.PORCDESC = dPorcentaje
                    Detalle.IMPBRUTO = dBruto
                    Detalle.IMPDESCUENTO = dDescuento
                    Detalle.IMPEXENTO = 0
                    Detalle.IMPOTROS = dOtros
                    Detalle.IMPNETO = dNeto
                    Detalle.IMPISR = 0
                    Detalle.IMPIVA = dIva
                    Detalle.IMPTOTAL = dTotal
                    Detalle.TIPOVENTADET = "B"
                    If (i = 0) Then
                        objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                    Else
                        If (i = dtFactura.Rows.Count - 1) Then
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle)
                        Else
                            objDetalle = objDetalle + PW.JSON.JSONHelper.ObjectToString(Detalle) & ","
                        End If
                    End If
                Next
            End If



            ' ******* Asignación de Datos de Facturación Documento Encabezado *******
            Documento.NITRECEPTOR = Cliente.nit
            Documento.TIPORECEPTOR = "4"
            Documento.NOMBRE = Cliente.negocio
            Documento.DIRECCION = Cliente.direccion
            Documento.TIPOVENTA = "B"
            Documento.DESTINOVENTA = 1
            Documento.FECHA = Format(CDate(factura.fechaEmision), "dd/MM/yyyy")
            Documento.MONEDA = 1
            Documento.TASA = 1.0
            Documento.REFERENCIA = factura.numero
            If (factura.numeroacceso.Length > 0) Then
                Documento.NUMEROACCESO = factura.numeroacceso
            Else
                Documento.NUMEROACCESO = "" 'factura.numeroacceso
            End If
            Documento.SERIEADMIN = factura.serie
            Documento.NUMEROADMIN = factura.numero
            Documento.REVERSION = "N"
            Documento.BRUTO = vBruto
            Documento.DESCUENTO = vDescuento
            Documento.EXENTO = vExento
            Documento.OTROS = vOtros
            Documento.NETO = vNeto
            Documento.ISR = vIsr
            Documento.IVA = vIva
            Documento.TOTAL = vTotal
            Documento.NUMEROABONO = 0
            If (factura.fechaVence Is DBNull.Value) Then
                Documento.FECHAVENCIMIENTO = Format(CDate(factura.fechaVence), "yyyyMMdd")
            Else
                Documento.FECHAVENCIMIENTO = Format(CDate(factura.fechaEmision), "yyyyMMdd")
            End If

            Documento.MONTOABONO = 0
            ' ******************************************************************************





            'Actualizar Recibo
            'objDocumentoBL.actualizarReciboImporte(OrdenVenta.idEncabezado, vBruto)

            'Actualizar Recibo_pagos
            'objDocumentoBL.actualizarReciboImporte(OrdenVenta.idEncabezado, vBruto)

            'Actualizar tabla recibos pagos

            '********* FIN LOGICA DE AJUSTE ****** 






            'objDocumentoBL.actualizarFACTImporte(idEncabezado, vBruto)

            'Actualizar Recibo_pagos
            'objDocumentoBL.actualizarReciboImporte(idEncabezado, vBruto)

            id_glo_api = "7e3e0ab330fffd33f244089444ac5db7"

            ' ********************************************************* Convertir Objeto a JSON *********************************************************
            objEncabezado = "{""ENCABEZADO"":" & PW.JSON.JSONHelper.ObjectToString(Documento) & ",""DETALLE"":["
            objDetalle = objDetalle & "]}"
            '****** Conectividad al servicio Nautilus ******
            'objEncabezado = QuitarCaracteres(objEncabezado)
            'objDetalle = QuitarCaracteres(objDetalle)
            objEncabezado = objEncabezado.Replace("&", " ")
            objDetalle = objDetalle.Replace("&", " ")
            PostData = [String].Format("encabezado={0}&detalle={1}&sociedad={2}&tipo={3}&ruta={4}&serie={5}&correlativo={6}&latitud={7}&longitud={8}", objEncabezado, objDetalle, id_glo_sociedad, 2, id_glo_codRuta, factura.serie, factura.numero, gps.Latitude, gps.Longitude)
            'PostData = [String].Format("encabezado={0}&detalle={1}" + objEncabezado
            request.Headers.Add("Authorization", id_glo_api)
            request.Credentials = CredentialCache.DefaultCredentials
            CType(request, HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ru; rv:1.9.2.3) Gecko/20100401 Firefox/4.0 (.NET CLR 3.5.30729)"
            request.Method = "POST"
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(PostData)
            request.ContentType = "application/x-www-form-urlencoded"
            request.ContentLength = byteArray.Length
            Dim dataStream As Stream = request.GetRequestStream()
            dataStream.Write(byteArray, 0, byteArray.Length)
            dataStream.Close()
            Dim response As WebResponse = request.GetResponse()
            'dataStream = response.GetResponseStream()
            Console.WriteLine(CType(response, HttpWebResponse).StatusDescription)
            dataStream = response.GetResponseStream()
            Dim reader As New StreamReader(dataStream)
            Dim responseFromServer As String = reader.ReadToEnd()
            reader.Close()
            dataStream.Close()
            response.Close()
            'Respuesta = PW.JSON.JSONHelper.StringToObject(responseFromServer, GetType(felRespuesta))
            If display(responseFromServer, idEncabezado) Then
                '********* INICIO LOGICA DE AJUSTE ****** 
                'importeActual = factura.importe
                'importeDestoActual = factura.importeDesto

                'diferenciasImporte = factura.importe - objUtilBL.isDecimal(vBruto)

                'Actualizar el importe de la factura de 
                'diferenciasImporteDesto = factura.importeDesto + objUtilBL.isDecimal(vDescuento)

                'objDocumentoBL.actualizarFACTImporte(idEncabezado, objUtilBL.isDecimal(diferenciasImporte), factura.idReciboRelacionado)

                'If diferenciasImporte <> 0 Then
                'If (objDocumentoBL.getPagoReciboFactura(idEncabezado, "E", factura.idReciboRelacionado) = True) Then
                ' objDocumentoBL.actualizarReciboPagos(factura.idReciboRelacionado, diferenciasImporte, "E")
                'Else
                '   If (objDocumentoBL.getPagoReciboFactura(idEncabezado, "C", factura.idReciboRelacionado) > 0) Then
                'objDocumentoBL.actualizarReciboPagos(factura.idReciboRelacionado, diferenciasImporte, "C")
                'Else
                '   If (objDocumentoBL.getPagoReciboFactura(idEncabezado, "CR", factura.idReciboRelacionado) > 0) Then
                'objDocumentoBL.actualizarReciboPagos(factura.idReciboRelacionado, diferenciasImporte, "CR")
                'End If
                'End If
                'End If
                'End If
            End If
            'MessageBox.Show(responseFromServer)

            'Dim respuestas As felRespuesta
            'respuestas = PW.JSON.JSONHelper.StringToObject(responseFromServer, GetType(felRespuesta))
            'Console.WriteLine(respuestas.ESTADO)
            Return True
            ' *************************************************** Fin de la rutina de comunicacion ****************************************************
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            Return False
        End Try
        'TextBox1.Text = PW.JSON.JSONHelper.ObjectToString(objprova)
    End Function

    Public Function QuitarCaracteres(ByVal cadena As String, Optional ByVal chars As String = "&" + Chr(34)) As String
        Dim i As Integer
        Dim nCadena As String
        On Error Resume Next
        'Asignamos valor a la cadena de trabajo para
        'no modificar la que envía el cliente.
        nCadena = cadena
        For i = 1 To Len(chars)
            nCadena = Replace(nCadena, Mid(chars, i, 1), " ")
        Next i
        'Devolvemos la cadena tratada
        QuitarCaracteres = nCadena
    End Function

End Class
