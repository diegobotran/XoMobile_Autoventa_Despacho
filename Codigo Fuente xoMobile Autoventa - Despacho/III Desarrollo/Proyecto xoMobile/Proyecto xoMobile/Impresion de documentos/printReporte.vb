Imports System
Imports System.IO.Ports
Imports System.IO
Imports System.Data
Imports System.Data.SqlServerCe
Imports System.Text
Imports Proyecto_xoMobile_Packs

Public Class printReporte
    Public mAncho As Integer = 62
    Dim objUtilBL As New UtilitarioBL
    Dim Cadena_impresion As String
    Dim Puerto As String
    Dim Velocidad As Long
    Dim Conexion As SqlCeConnection
    Dim times As Integer = 3
    Dim objCe As New ceClient
    Dim objimpresion As New Impresion
    Dim objUtil As New UtilitarioBL

    Public Function liquidacion_ProductoTerminado() As Boolean
        Dim rsb As New StringBuilder
        Dim objRuta As New RutaBL
        Dim objBodega As New BodegaBL
        Dim oBodega As New BodegaDT
        Dim dtRuta As New DataTable
        Dim dtBodega As New DataTable
        Dim inventario As New inventarioCO
        Dim tarrCantidad() As String
        Dim tcajasTotal As Integer
        Dim tunidadesTotal As Integer
        Dim rarrCantidad() As String
        Dim rcajasTotal As Integer
        Dim runidadesTotal As Integer
        inventario.tTipo = 1
        dtRuta = objRuta.obtenerRutaActiva()
        objBodega.getArticuloDevuelto(dtBodega, inventario)

        '--- Encabezado
        With objimpresion
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("------ O R D E N   D E   T R A N S F E R E N C I A ------")
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine()
            rsb.AppendLine("Empresa     : " + Trim(dtRuta.Rows(0).Item("nomEmpresa").ToString()))
            rsb.AppendLine("Fecha Emi.  :   " + Date.Now.ToString)
            rsb.AppendLine("Usuario     : " + id_glo_usuario.ToString)
            rsb.AppendLine("Bod. origen : " + dtRuta.Rows(0).Item("codRuta").ToString() + "- RUTA " + dtRuta.Rows(0).Item("codRuta").ToString())
            rsb.AppendLine("Bod. destino: " + id_glo_BodegaProducto.ToString + "-" + co_glo_bodegaPT)
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("Cod.        Descripcion                  Solic.   Entreg.")
            rsb.AppendLine("---------------------------------------------------------#")
            For i As Integer = 0 To dtBodega.Rows.Count - 1
                With dtBodega.Rows(i)
                    Dim a As Integer = Len(Trim(.Item("descripcion").ToString.Substring(0, 33))) 'Ancho Variable                    
                    rsb.Append(objimpresion.AlinIzq(.Item("idproducto").ToString() + " | ", 11))
                    rsb.Append(Trim(.Item("descripcion").ToString.Substring(0, 33)))
                    rsb.Append(objimpresion.AlinDer(.Item("TeoricoResumen").ToString() + " | ", mAncho - (a + 21)))
                    rsb.AppendLine(.Item("FisicoResumen").ToString() & "#")
                    tarrCantidad = .Item("TeoricoResumen").ToString.Split("/")
                    rarrCantidad = .Item("FisicoResumen").ToString.Split("/")
                    tcajasTotal = tcajasTotal + (objUtil.isDecimal(tarrCantidad(0).ToString))
                    tunidadesTotal = tunidadesTotal + (objUtil.isDecimal(tarrCantidad(1).ToString))
                    rcajasTotal = rcajasTotal + (objUtil.isDecimal(rarrCantidad(0).ToString))
                    runidadesTotal = runidadesTotal + (objUtil.isDecimal(rarrCantidad(1).ToString))
                End With
            Next
            rsb.AppendLine()
            rsb.AppendLine("#---------------------------------------------------------")
            rsb.AppendLine(.AlinIzq("T O T A L  S O L I C I T A D O :               " + tcajasTotal.ToString + "/" + tunidadesTotal.ToString, mAncho))
            rsb.AppendLine(.AlinIzq("T O T A L  E N T R E G A D O :                 " + rcajasTotal.ToString + "/" + runidadesTotal.ToString, mAncho))
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine()
            rsb.AppendLine(.AlinCent("---------------------------------------------", mAncho))
            rsb.AppendLine(.AlinCent("ORIGEN: " + co_glo_vendedor, mAncho) + vbCrLf)
            rsb.AppendLine()
            rsb.AppendLine(.AlinCent("---------------------------------------------", mAncho))
            rsb.AppendLine(.AlinCent("DESTINO: " + co_glo_bodeguero, mAncho) + vbCrLf)
            rsb.AppendLine()
            rsb.AppendLine()
            '----  Roturas
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("---------- R O T U R A S   E N T R E G A D A S ----------")
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine()
            rsb.AppendLine("Empresa     : " + Trim(dtRuta.Rows(0).Item("nomEmpresa").ToString()))
            rsb.AppendLine("Fecha Emi.  :   " + Date.Now.ToString)
            rsb.AppendLine("Usuario     : " + id_glo_usuario.ToString)
            rsb.AppendLine("Bod. origen : " + dtRuta.Rows(0).Item("codRuta").ToString() + "- RUTA " + dtRuta.Rows(0).Item("codRuta").ToString())
            rsb.AppendLine("Bod. destino: " + id_glo_BodegaProducto.ToString + "-" + co_glo_bodegaPT)
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("Cod.        Descripcion                  Rotura Unidades ")
            rsb.AppendLine("---------------------------------------------------------")
            For i As Integer = 0 To dtBodega.Rows.Count - 1
                With dtBodega.Rows(i)
                    If .Item("Rotura").ToString() > 0 Then
                        Dim a As Integer = Len(Trim(.Item("descripcion").ToString.Substring(0, 33))) 'Ancho Variable                    
                        rsb.Append(objimpresion.AlinIzq(.Item("idproducto").ToString() & " | ", 11))
                        rsb.Append(Trim(.Item("descripcion").ToString.Substring(0, 33)))
                        rsb.AppendLine(objimpresion.AlinDer(.Item("Rotura").ToString(), mAncho - (a + 21)))
                    End If
                End With
            Next
            rsb.AppendLine(.AlinCent("---------------------------------------------", mAncho))
            rsb.AppendLine(.AlinCent("NOMBRE DEL SUPERVISOR QUE VALIDA EL CORTE: ", mAncho) + vbCrLf + vbCrLf)
            rsb.AppendLine(.AlinCent("---------------------------------------------", mAncho))
            rsb.AppendLine(.AlinCent("FIRMA DEL SUPERVISOR QUE VALIDA EL CORTE: ", mAncho) + vbCrLf + vbCrLf)
            rsb.AppendLine(.AlinCent("---------------------------------------------", mAncho))
            rsb.AppendLine(.AlinCent("ORIGEN: " + co_glo_vendedor, mAncho) + vbCrLf)
            rsb.AppendLine()
            rsb.AppendLine(.AlinCent("---------------------------------------------", mAncho))
            rsb.AppendLine(.AlinCent("DESTINO: " + co_glo_bodeguero, mAncho) + vbCrLf)
            rsb.AppendLine()
            rsb.AppendLine()
        End With
        Dim arreglo As String()
        arreglo = rsb.ToString.Split("#")
        If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
            Return True
        Else
            MsgBox("La impresora no esta disponible.")
            Return False
        End If

    End Function

    Public Function liquidacion_Envase() As Boolean
        Dim rsb As New StringBuilder
        Dim objRuta As New RutaBL
        Dim objBodega As New BodegaBL
        Dim oBodega As New BodegaDT
        Dim dtRuta As New DataTable
        Dim dtBodega As New DataTable
        Dim inventario As New inventarioCO
        Dim sum_fisCaja, sum_fisUnidad, sum_teoCaja, sum_teoUnidad, sum_difCaja, sum_difUnidad As Integer
        Dim sum_valor As Decimal
        inventario.tTipo = 0
        dtRuta = objRuta.obtenerRutaActiva()
        objBodega.getArticuloDevuelto(dtBodega, inventario)

        '--- Encabezado
        With objimpresion
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("-----  CONFIRMACION  DE  ENVASE  RECIBIDO  EN  RUTA  ----")
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine()
            rsb.AppendLine("Empresa  : " + Trim(dtRuta.Rows(0).Item("nomEmpresa").ToString()))
            rsb.AppendLine("Fecha Emi:   " + Date.Now.ToString)
            rsb.AppendLine("Usuario  : " + id_glo_usuario.ToString)
            rsb.AppendLine("Bodega origen: " + dtRuta.Rows(0).Item("codRuta").ToString() + "- RUTA " + dtRuta.Rows(0).Item("codRuta").ToString())
            rsb.AppendLine("Bodega destino: " + id_glo_BodegaEnvase.ToString + "-" + co_glo_bodegaEnvase)
            rsb.AppendLine()
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("Cod.        Descripcion                                  ")
            rsb.AppendLine("            Teorico    Fisico    Diferencia    Valor (Q) ")
            rsb.AppendLine("---------------------------------------------------------")
            For i As Integer = 0 To dtBodega.Rows.Count - 1
                With dtBodega.Rows(i)
                    rsb.Append(objimpresion.AlinIzq(.Item("idproducto").ToString(), 12))
                    rsb.AppendLine(Trim(.Item("descripcion").ToString))
                    rsb.Append(objimpresion.AlinDer(.Item("TeoricoResumen").ToString(), 16))
                    rsb.Append(objimpresion.AlinDer(.Item("FisicoResumen").ToString(), 12))
                    rsb.Append(objimpresion.AlinDer(.Item("DiferenciaResumen").ToString(), 9))
                    rsb.AppendLine(objimpresion.AlinDer(FormatCurrency(.Item("importe").ToString(), 2), 15))
                    rsb.AppendLine("#---------------------------------------------------------")
                    sum_fisCaja = sum_fisCaja + .Item("FCJ")
                    sum_fisUnidad = sum_fisUnidad + .Item("FUN")
                    sum_teoCaja = sum_teoCaja + .Item("TCJ")
                    sum_teoUnidad = sum_teoUnidad + .Item("TUN")
                    sum_difCaja = sum_difCaja + .Item("DCJ")
                    sum_difUnidad = sum_difUnidad + .Item("DUN")
                    sum_valor = sum_valor + .Item("importe")
                End With
            Next
            rsb.AppendLine()
            rsb.Append("#TOTAL : ")
            rsb.Append(objimpresion.AlinDer(sum_teoCaja.ToString + "/" + sum_teoUnidad.ToString, 12))
            rsb.Append(objimpresion.AlinDer(sum_fisCaja.ToString + "/" + sum_fisUnidad.ToString, 8))
            rsb.Append(objimpresion.AlinDer(sum_difCaja.ToString + "/" + sum_difUnidad.ToString, 9))
            rsb.AppendLine(objimpresion.AlinDer(FormatCurrency(sum_valor.ToString, 2), 15))
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine()
            rsb.AppendLine()
            rsb.AppendLine("   ------------------------------------------------------")
            rsb.AppendLine(.AlinCent("VENDEDOR: " + co_glo_vendedor, mAncho) + vbCrLf)
            rsb.AppendLine()
            rsb.AppendLine("   ------------------------------------------------------")
            rsb.AppendLine(.AlinCent("BODEGA: " + co_glo_bodeguero, mAncho) + vbCrLf)
            rsb.AppendLine()
        End With
        Dim arreglo As String()
        arreglo = rsb.ToString.Split("#")
        If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
            Return True
        Else
            MsgBox("La impresora no esta disponible.")
            Return False
        End If

    End Function

    Public Function inventario_actualProducto(ByVal lstCarga As Windows.Forms.ListView) As Boolean
        Dim rsb As New StringBuilder
        Dim objRuta As New RutaBL
        Dim dtRuta As New DataTable
        dtRuta = objRuta.obtenerRutaActiva()
        
        Try
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine(" I N V E N T A R I O  A C T U A L  D E  P R O D U C T O  ")
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine()
            rsb.AppendLine("Empresa     : " + Trim(dtRuta.Rows(0).Item("nomEmpresa").ToString()))
            rsb.AppendLine("Fecha Emi.  : " + Date.Now.ToString + "  Usuario  : " + id_glo_usuario.ToString)
            rsb.AppendLine("Ruta        : " + dtRuta.Rows(0).Item("codRuta").ToString())
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("Cod.        Descripcion           C. Inicial    C. Actual")
            rsb.AppendLine("---------------------------------------------------------")
            Dim descripcion As String = ""
            For i As Integer = 0 To lstCarga.Items.Count - 1
                With lstCarga.Items(i)
                    descripcion = .SubItems(2).Text()
                    If descripcion.Length >= 33 Then
                        descripcion = Trim(descripcion.Substring(0, 33))
                    ElseIf descripcion.Length = 0 Then
                        descripcion = "DESCRIPCION NO DISPONIBLE"
                    End If

                    Dim a As Integer = Len(descripcion) 'Ancho Variable                                        
                    rsb.Append(objimpresion.AlinIzq(.SubItems(1).Text() + " | ", 11))
                    rsb.Append(descripcion)

                    rsb.Append(objimpresion.AlinDer(.SubItems(3).Text() + " |# ", mAncho - (a + 21)))
                    rsb.AppendLine(.SubItems(4).Text())
                End With
            Next
            rsb.AppendLine("----------- F I N  D E  L A  I M P R E S I O N ------------")
            rsb.AppendLine()

            Dim arreglo As String()
            arreglo = rsb.ToString.Split("#")

            If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
                Return True
            Else
                MsgBox("La impresora no esta disponible.")
                Return False
            End If
        Catch ex As Exception
            MsgBox("Se ha producido un error que impide que el documento se imprima. " + ex.Message)
            Return False
        End Try
    End Function

    Public Function inventario_actualEnvase(ByVal lstCarga As Windows.Forms.ListView) As Boolean
        Dim rsb As New StringBuilder
        Dim objRuta As New RutaBL
        Dim dtRuta As New DataTable
        Dim descripcion As String = ""

        Try

            dtRuta = objRuta.obtenerRutaActiva()
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine(" I N V E N T A R I O  D E  E N V A S E  R E C I B I D O  ")
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine()
            rsb.AppendLine("Empresa     : " + Trim(dtRuta.Rows(0).Item("nomEmpresa").ToString()))
            rsb.AppendLine("Fecha Emi.  : " + Date.Now.ToString + "  Usuario  : " + id_glo_usuario.ToString)
            rsb.AppendLine("Ruta        : " + dtRuta.Rows(0).Item("codRuta").ToString())
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("Cod.        Descripcion                     C. Actual    ")
            rsb.AppendLine("---------------------------------------------------------#")
            For i As Integer = 0 To lstCarga.Items.Count - 1
                With lstCarga.Items(i)
                    descripcion = .SubItems(2).Text()
                    If descripcion.Length >= 33 Then
                        descripcion = Trim(descripcion.Substring(0, 33))
                    ElseIf descripcion.Length = 0 Then
                        descripcion = "DESCRIPCION NO DISPONIBLE"
                    End If
                    Dim a As Integer = Len(descripcion) 'Ancho Variable                                        
                    rsb.Append(objimpresion.AlinIzq(.SubItems(1).Text() + " | ", 11))
                    rsb.Append(descripcion)
                    rsb.AppendLine(objimpresion.AlinDer(.SubItems(4).Text + "  ", mAncho - (a + 21)))
                End With
            Next
            rsb.AppendLine("----------- F I N  D E  L A  I M P R E S I O N ----------")
            rsb.AppendLine()

            '--- Impresion del documento  ---'
            Dim arreglo As String()
            arreglo = rsb.ToString.Split("#")
            If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
                Return True
            Else
                MsgBox("La impresora no esta disponible.")
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function


    Public Function inventario_venta(ByVal lstCarga As Windows.Forms.ListView) As Boolean
        Dim rsb As New StringBuilder
        Dim objRuta As New RutaBL
        Dim dtRuta As New DataTable
        Dim cliente As String = ""
        Dim mensaje As String = ""
        Dim Litros As Decimal = 0
        Dim descripcion As String = ""
        Dim Texto As String = ""
        Dim texto_1 As String = ""
        Try
            dtRuta = objRuta.obtenerRutaActiva()

            Texto = Texto + "------------------------------------------" + vbCrLf
            Texto = Texto + objimpresion.AlinCent("----P R O D U C T O   V E N D I D O ----", mAncho) + vbCrLf
            Texto = Texto + "------------------------------------------" + vbCrLf

            Texto = Texto + ("Empresa  : " + Trim(dtRuta.Rows(0).Item("nomEmpresa").ToString())) + vbCrLf
            Texto = Texto + ("Fecha Emi.  : " + Date.Now.ToString + "  Usuario  : " + id_glo_usuario.ToString) + vbCrLf
            Texto = Texto + ("Ruta        : " + dtRuta.Rows(0).Item("codRuta").ToString()) + vbCrLf
            Texto = Texto + ("---------------------------------------------------------") + vbCrLf
            Texto = Texto + ("Cod.        Descripcion                     C. Actual    ") + vbCrLf
            Texto = Texto + ("---------------------------------------------------------") + vbCrLf
            For i As Integer = 0 To lstCarga.Items.Count - 1
                With lstCarga.Items(i)
                    descripcion = .SubItems(2).Text()
                    If descripcion.Length >= 33 Then
                        descripcion = Trim(descripcion.Substring(0, 33))
                    ElseIf descripcion.Length = 0 Then
                        descripcion = "DESCRIPCION NO DISPONIBLE"
                    End If
                    Dim a As Integer = Len(descripcion) 'Ancho Variable
                    Texto = Texto + (objimpresion.AlinIzq(.SubItems(1).Text() + " | ", 11))
                    Texto = Texto + descripcion
                    Texto = Texto + objimpresion.AlinDer(.SubItems(4).Text + "  ", mAncho - (a + 21)) + vbCrLf

                    If .SubItems(5).Text = "1" Then
                        'Litros = Convert.ToDecimal(.SubItems(6).Text) + Litros
                        Litros = Decimal.Round(Convert.ToDecimal(.SubItems(6).Text()), 2) + Litros
                    End If
                End With
                If i = times Then
                    times = times + 3
                    Texto = Texto + "@"
                End If
            Next
            Texto = Texto + "---------------------------------" + vbCrLf
            Texto = Texto + ("L I T R O S  V E N D I D O S : " + FormatNumber(Litros, 2)) + vbCrLf
            Texto = Texto + ("----------- F I N  D E  L A  I M P R E S I O N ----------") + vbCrLf
            Texto = Texto + "---------------------------------"
            Texto = Texto + "---------------------------------"
            Texto = Texto + "---------------------------------"

            

            '--- Impresion del documento  ---'
            If Not objimpresion.ConfirmaImpresion("Inventario de producto vendido.") Then
                Return False
            End If

            Dim arreglo As String()
            arreglo = Texto.ToString.Split("@")

            If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
                Return True
            Else
                MsgBox("La impresora no esta disponible.")
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function


    Public Function documentosEmitidos(ByVal estado As String, ByVal idcliente As Integer, ByVal impresion As Boolean) As Boolean
        Dim rsb As New StringBuilder
        Dim tipoDocumento As String = ""
        Dim cliente As String = ""
        Dim objRuta As New RutaBL
        Dim dtCarga, dtRuta As New DataTable
        Dim dvCarga As New DataView
        Dim strFiltro As String = ""
        Dim Total As Decimal
        Dim TLitros As Decimal
        Try
            dtCarga = objRuta.obtenerDocumentosEmitidos
            dtRuta = objRuta.obtenerRutaActiva()
            dvCarga = dtCarga.DefaultView
            rsb.AppendLine("---------------------------------------------------------")

            'Filtro por estado
            Select Case estado
                Case 0

                    strFiltro = " estado = 2 " ''and idRuta = " + id_glo_ruta.ToString()
                    rsb.AppendLine("-------- D O C U M E N T O S   A N U L A D O S  ---------")
                Case 1
                    strFiltro = " estado <> 2 " '' and idRuta = " + id_glo_ruta.ToString()
                    rsb.AppendLine("-------- D O C U M E N T O S   E M I T I D O S  ---------")

                Case 2
            End Select

            'Filtro por codigo de cliente
            If idcliente <> 0 Then
                dvCarga.RowFilter = strFiltro + "  and idcliente = " + idcliente.ToString
            Else
                dvCarga.RowFilter = strFiltro
            End If
            dtCarga = dvCarga.ToTable
            If dtCarga.Rows.Count <= 0 Then
                MsgBox("No existen documentos para imprimir.")
                Return True
            End If
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine()
            rsb.AppendLine("Empresa     : " + Trim(dtRuta.Rows(0).Item("nomEmpresa").ToString()))
            rsb.AppendLine("Fecha Emi.  : " + Date.Now.ToString + "  Usuario  : " + id_glo_usuario.ToString)
            rsb.AppendLine("Ruta        : " + dtRuta.Rows(0).Item("codRuta").ToString())
            For i As Integer = 0 To dtCarga.Rows.Count - 1
                With dtCarga.Rows(i)
                    If tipoDocumento = .Item("tipoD").ToString() Then
                    Else
                        tipoDocumento = .Item("tipoD").ToString()
                        rsb.AppendLine()
                        rsb.AppendLine("---------------------------------------------------------")
                        rsb.AppendLine(objimpresion.AlinCent(tipoDocumento, mAncho) & "|")
                        rsb.AppendLine("---------------------------------------------------------")
                        rsb.AppendLine("Documento.  Cliente    Importe   Caso    Doc. Relacionado")
                        rsb.AppendLine("---------------------------------------------------------|")
                    End If
                    rsb.Append(objimpresion.AlinIzq(Trim(.Item("fserie").ToString()) + "-" + .Item("fnumero").ToString(), 12))
                    rsb.Append(objimpresion.AlinIzq(.Item("idcliente").ToString(), 10) & "|")
                    rsb.Append(objimpresion.AlinIzq(FormatCurrency(.Item("importe").ToString(), 2), 10))
                    rsb.Append(objimpresion.AlinIzq(.Item("CPAGO").ToString(), 10))
                    rsb.AppendLine(objimpresion.AlinIzq(Trim(.Item("FRELSE").ToString()) + "-" + .Item("FRELNO").ToString() + " " + FormatNumber(.Item("litros").ToString(), 2), 16))

                    'rsb.AppendLine(objimpresion.AlinIzq(Trim(.Item("litros")), 8))
                    If tipoDocumento = "RECIBO" Then
                        Total = .Item("importe") + Total
                    End If
                    TLitros = .Item("litros") + TLitros

                End With
            Next

            Select Case estado
                Case 1
                    rsb.AppendLine("---------------------------------------------------------")
                    rsb.AppendLine("TOTAL RECAUDADO       " + FormatCurrency(Total.ToString, 2))
                    rsb.AppendLine("- TOTAL LITROS -       " + FormatNumber(TLitros.ToString, 2))
                    rsb.AppendLine("---------------------------------------------------------")
                Case 2
            End Select
            rsb.AppendLine()
            rsb.AppendLine("----------- F I N  D E  L A  I M P R E S I O N ----------")
            rsb.AppendLine()
            rsb.AppendLine()

            '--- Impresion del documento  ---'
            If impresion Then


                Dim arreglo As String()
                arreglo = rsb.ToString.Split("|")
                If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
                    Return True
                Else
                    MsgBox("La impresora no esta disponible.")
                    Return False
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function


    Public Function resumenMarcas(ByVal estado As String, ByVal idcliente As Integer, ByVal impresion As Boolean) As Boolean
        Dim rsb As New StringBuilder
        Dim tipoDocumento As String = ""
        Dim cliente As String = ""
        Dim objRuta As New RutaBL
        Dim dtCarga, dtRuta As New DataTable
        Dim dvCarga As New DataView
        Dim strFiltro As String = ""

        Dim TLitros As Decimal
        Dim TImporte As Decimal
        Try
            TLitros = 0
            TImporte = 0
            dtCarga = objRuta.obtenerResumenMarca
            dtRuta = objRuta.obtenerRutaActiva()
            dvCarga = dtCarga.DefaultView
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("-------- V E N T A S   P O R   M A R C A  ---------")


            If dtCarga.Rows.Count <= 0 Then
                MsgBox("No existen documentos para imprimir.")
                Return True
            End If
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine()
            rsb.AppendLine("Empresa     : " + Trim(dtRuta.Rows(0).Item("nomEmpresa").ToString()))
            rsb.AppendLine("Fecha Emi.  : " + Date.Now.ToString + "  Usuario  : " + id_glo_usuario.ToString)
            rsb.AppendLine("Ruta        : " + dtRuta.Rows(0).Item("codRuta").ToString())
            For i As Integer = 0 To dtCarga.Rows.Count - 1
                With dtCarga.Rows(i)


                    rsb.AppendLine()
                    If (i = 0) Then
                        rsb.AppendLine("---------------------------------------------------------")
                        rsb.AppendLine("Marca.  Descripcion           Litros        Importe ")
                        rsb.AppendLine("---------------------------------------------------------|")
                    End If

                    rsb.Append(objimpresion.AlinIzq(Trim(.Item("idMarca").ToString()), 10))
                    rsb.Append(objimpresion.AlinIzq(.Item("desc_marca").ToString(), 18) & "|")
                    rsb.Append(objimpresion.AlinIzq(.Item("total_litros").ToString(), 10))
                    rsb.Append(objimpresion.AlinIzq(FormatCurrency(.Item("importeSinIva").ToString(), 2), 14))

                    TLitros = .Item("total_litros") + TLitros
                    TImporte = .Item("importeSinIva") + TImporte

                End With
            Next

            Select Case estado
                Case 1
                    rsb.AppendLine("---------------------------------------------------------")
                    rsb.AppendLine("TOTAL IMPORTE       " + FormatCurrency(TImporte.ToString, 2))
                    rsb.AppendLine("- TOTAL LITROS -       " + FormatNumber(TLitros.ToString, 2))
                    rsb.AppendLine("---------------------------------------------------------")
                Case 2
            End Select
            rsb.AppendLine()
            rsb.AppendLine("----------- F I N  D E  L A  I M P R E S I O N ----------")
            rsb.AppendLine()
            rsb.AppendLine()

            '--- Impresion del documento  ---'
            If impresion Then
                Dim arreglo As String()
                arreglo = rsb.ToString.Split("|")
                If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
                    Return True
                Else
                    MsgBox("La impresora no esta disponible.")
                    Return False
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function


    Public Function resumenSINFEL(ByVal estado As String, ByVal idcliente As Integer, ByVal impresion As Boolean) As Boolean
        Dim rsb As New StringBuilder
        Dim tipoDocumento As String = ""
        Dim cliente As String = ""
        Dim objRuta As New RutaBL
        Dim dtCarga, dtRuta As New DataTable
        Dim dvCarga As New DataView
        Dim strFiltro As String = ""

        Dim TLitros As Decimal
        Dim TImporte As Decimal
        Try
            TLitros = 0
            TImporte = 0
            dtCarga = objRuta.obtenerSINFEL
            dtRuta = objRuta.obtenerRutaActiva()
            dvCarga = dtCarga.DefaultView
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("-------- DOCUMENTOS SIN FEL  ---------")


            If dtCarga.Rows.Count <= 0 Then
                MsgBox("No existen documentos para imprimir.")
                Return True
            End If
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine()
            rsb.AppendLine("Empresa     : " + Trim(dtRuta.Rows(0).Item("nomEmpresa").ToString()))
            rsb.AppendLine("Fecha Emi.  : " + Date.Now.ToString + "  Usuario  : " + id_glo_usuario.ToString)
            rsb.AppendLine("Ruta        : " + dtRuta.Rows(0).Item("codRuta").ToString())
            For i As Integer = 0 To dtCarga.Rows.Count - 1
                With dtCarga.Rows(i)


                    rsb.AppendLine()
                    If (i = 0) Then
                        rsb.AppendLine("---------------------------------------------------------")
                        rsb.AppendLine("Numero.  Cliente             Tipo        Importe ")
                        rsb.AppendLine("---------------------------------------------------------|")
                    End If

                    rsb.Append(objimpresion.AlinIzq(Trim(.Item("numero").ToString()), 10))
                    rsb.Append(objimpresion.AlinIzq(.Item("idCliente").ToString(), 18) & "|")
                    rsb.Append(objimpresion.AlinIzq(.Item("Tipo").ToString(), 10))
                    rsb.Append(objimpresion.AlinIzq(FormatCurrency(.Item("importe").ToString(), 2), 14))

                    TLitros = TLitros + 1


                End With
            Next

            Select Case estado
                Case 1
                    rsb.AppendLine("---------------------------------------------------------")
                    rsb.AppendLine("TOTAL DOCUMENTOS SIN FEL       " & TLitros)
                    rsb.AppendLine("---------------------------------------------------------")
                Case 2
            End Select
            rsb.AppendLine()
            rsb.AppendLine("----------- F I N  D E  L A  I M P R E S I O N ----------")
            rsb.AppendLine()
            rsb.AppendLine()

            '--- Impresion del documento  ---'
            If impresion Then
                Dim arreglo As String()
                arreglo = rsb.ToString.Split("|")
                If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
                    Return True
                Else
                    MsgBox("La impresora no esta disponible.")
                    Return False
                End If
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function


    Public Function resumenPresupuestoV() As Boolean
        Dim rsb As New StringBuilder
        Dim tipoDocumento As String = ""
        Dim cliente As String = ""
        Dim objRuta As New RutaBL
        Dim dtCarga, dtRuta As New DataTable
        Dim dvCarga As New DataView
        Dim strFiltro As String = ""

        Dim VPresupuesto As Decimal
        Dim VReal As Decimal
        Dim NVD As Decimal
        Try
            VPresupuesto = 0
            VReal = 0
            NVD = 0
            dtCarga = objRuta.obtenerResumenPresupuesto
            dtRuta = objRuta.obtenerRutaActiva()
            dvCarga = dtCarga.DefaultView
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("-------- P R E S U P U E S T O   R  U  T  A   ---------")

            If dtCarga.Rows.Count <= 0 Then
                MsgBox("No existen documentos para imprimir.")
                Return True
            End If
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine()
            rsb.AppendLine("Empresa     : " + Trim(dtRuta.Rows(0).Item("nomEmpresa").ToString()))
            rsb.AppendLine("Fecha Emi.  : " + Date.Now.ToString + "  Usuario  : " + id_glo_usuario.ToString)
            rsb.AppendLine("Ruta        : " + dtRuta.Rows(0).Item("codRuta").ToString())
            For i As Integer = 0 To dtCarga.Rows.Count - 1
                With dtCarga.Rows(i)

                    rsb.AppendLine()
                    If (i = 0) Then
                        rsb.AppendLine("---------------------------------------------------------")
                        rsb.AppendLine("Marca           Presupuesto     Vta_Real     Diferencia ")
                        rsb.AppendLine("---------------------------------------------------------|")
                    End If

                    rsb.Append(objimpresion.AlinIzq(Trim(.Item("marca").ToString()), 15) & "|")
                    rsb.Append(objimpresion.AlinIzq(FormatCurrency(.Item("mt_presupuesto").ToString(), 2), 15))
                    rsb.Append(objimpresion.AlinIzq(FormatCurrency(.Item("venta_real").ToString(), 2), 15))
                    rsb.Append(objimpresion.AlinIzq(FormatCurrency(.Item("mt_presupuesto") - .Item("venta_real"), 2), 15))

                    VPresupuesto = .Item("mt_presupuesto") + VPresupuesto
                    VReal = .Item("venta_real") + VReal
                    NVD = .Item("nec_vta_dia") + NVD

                End With
            Next


            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("PRESUPUESTO : " + FormatCurrency(VPresupuesto.ToString, 2))
            rsb.AppendLine("VENTA REAL : " + FormatCurrency(VReal.ToString, 2))
            If (VPresupuesto > 0) Then
                rsb.AppendLine("AVANCE  : " + (objUtil.isDecimal((VReal / VPresupuesto) * 100)).ToString + "%")
            Else
                rsb.AppendLine("AVANCES : " + ("00" + "%".ToString()))
            End If
            rsb.AppendLine("DIFERENCIA : " + FormatCurrency((VPresupuesto - VReal).ToString, 2))
            'rsb.AppendLine("NECESIDAD : " + (objUtil.isDecimal(NVD.ToString).ToString))
            rsb.AppendLine("---------------------------------------------------------")

            rsb.AppendLine()
            rsb.AppendLine("----------- F I N  D E  L A  I M P R E S I O N ----------")
            rsb.AppendLine()
            rsb.AppendLine()

            Dim arreglo As String()
            arreglo = rsb.ToString.Split("|")
            If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
                Return True
            Else
                MsgBox("La impresora no esta disponible.")
                Return False
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Public Function resumenPresupuesto() As Boolean
        Dim rsb As New StringBuilder
        Dim tipoDocumento As String = ""
        Dim cliente As String = ""
        Dim objRuta As New RutaBL
        Dim dtCarga, dtRuta As New DataTable
        Dim dvCarga As New DataView
        Dim strFiltro As String = ""

        Dim LPresupuesto As Decimal
        Dim LReal As Decimal
        Dim NCL As Decimal
        Dim Diferencias As Decimal
        Try
            LPresupuesto = 0
            LReal = 0
            NCL = 0
            Diferencias = 0
            dtCarga = objRuta.obtenerResumenPresupuesto
            dtRuta = objRuta.obtenerRutaActiva()
            dvCarga = dtCarga.DefaultView
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("-------- P R E S U P U E S T O   R  U  T  A  ---------")


            If dtCarga.Rows.Count <= 0 Then
                MsgBox("No existen documentos para imprimir.")
                Return True
            End If
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine()
            rsb.AppendLine("Empresa     : " + Trim(dtRuta.Rows(0).Item("nomEmpresa").ToString()))
            rsb.AppendLine("Fecha Emi.  : " + Date.Now.ToString + "  Usuario  : " + id_glo_usuario.ToString)
            rsb.AppendLine("Ruta        : " + dtRuta.Rows(0).Item("codRuta").ToString())
            For i As Integer = 0 To dtCarga.Rows.Count - 1
                With dtCarga.Rows(i)


                    rsb.AppendLine()
                    If (i = 0) Then
                        rsb.AppendLine("---------------------------------------------------------")
                        rsb.AppendLine("Marca           Presupuesto     Lit_Real       Diferencia ")
                        rsb.AppendLine("---------------------------------------------------------|")
                    End If

                    rsb.Append(objimpresion.AlinIzq(Trim(.Item("marca").ToString()), 15) & "|")
                    rsb.Append(objimpresion.AlinIzq(FormatNumber(.Item("lit_presupuesto").ToString(), 3), 15))
                    rsb.Append(objimpresion.AlinIzq(FormatNumber(.Item("lit_real").ToString(), 3), 15))
                    rsb.Append(objimpresion.AlinIzq(FormatNumber(.Item("lit_presupuesto") - .Item("lit_real"), 3), 15))

                    LPresupuesto = .Item("lit_presupuesto") + LPresupuesto
                    LReal = .Item("lit_real") + LReal
                    NCL = .Item("nec_lit_dia") + NCL
                End With
            Next


            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("PRESUPUESTO LITROS : " + (FormatNumber(LPresupuesto.ToString, 2)).ToString)
            rsb.AppendLine("LITROS REALES : " + FormatNumber(LReal.ToString, 2))
            If (LPresupuesto > 0) Then
                rsb.AppendLine("AVANCES : " + (objUtil.isDecimal((LReal / LPresupuesto) * 100)).ToString + "%")
            Else
                rsb.AppendLine("AVANCES : " + ("00" + "%".ToString()))
            End If

            rsb.AppendLine("DIFERENCIA : " + (FormatNumber(LPresupuesto.ToString - objUtil.isDecimal(LReal).ToString, 2).ToString))
            'rsb.AppendLine("NECESIDAD : " + (objUtil.isDecimal(NCL.ToString).ToString))
            rsb.AppendLine("---------------------------------------------------------")


            rsb.AppendLine()
            rsb.AppendLine("----------- F I N  D E  L A  I M P R E S I O N ----------")
            rsb.AppendLine()
            rsb.AppendLine()

            Dim arreglo As String()
            arreglo = rsb.ToString.Split("|")
            If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
                Return True
            Else
                MsgBox("La impresora no esta disponible.")
                Return False
            End If



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Public Function resumenPresupuestoDia(ByVal diad As String) As Boolean
        Dim rsb As New StringBuilder
        Dim tipoDocumento As String = ""
        Dim cliente As String = ""
        Dim objRuta As New RutaBL
        Dim dtCarga, dtRuta As New DataTable
        Dim dvCarga As New DataView
        Dim strFiltro As String = ""

        Dim LPresupuesto As Decimal
        Dim LReal As Decimal
        Dim NCL As Decimal
        Try
            LPresupuesto = 0
            LReal = 0
            NCL = 0
            dtCarga = objRuta.obtenerResumenPresupuestoD(diad)
            dtRuta = objRuta.obtenerRutaActiva()
            dvCarga = dtCarga.DefaultView
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("---------- PRESUPUESTO  DEL  DIA  EN  LITROS ------------")


            If dtCarga.Rows.Count <= 0 Then
                MsgBox("No existen documentos para imprimir.")
                Return True
            End If
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine()
            rsb.AppendLine("Empresa     : " + Trim(dtRuta.Rows(0).Item("nomEmpresa").ToString()))
            rsb.AppendLine("Fecha Emi.  : " + Date.Now.ToString + "  Usuario  : " + id_glo_usuario.ToString)
            rsb.AppendLine("Ruta        : " + dtRuta.Rows(0).Item("codRuta").ToString())
            rsb.AppendLine("Dia Visita  : " + diad)

            For i As Integer = 0 To dtCarga.Rows.Count - 1
                With dtCarga.Rows(i)


                    rsb.AppendLine()
                    If (i = 0) Then
                        rsb.AppendLine("---------------------------------------------------------")
                        rsb.AppendLine("Marca           Presupuesto     Lit_Real       NEC_Lit ")
                        rsb.AppendLine("---------------------------------------------------------|")
                    End If

                    rsb.Append(objimpresion.AlinIzq(Trim(.Item("marca").ToString()), 15) & "|")
                    rsb.Append(objimpresion.AlinIzq((.Item("lit_presupuesto").ToString()), 15))
                    rsb.Append(objimpresion.AlinIzq(.Item("lit_real").ToString(), 15))
                    rsb.Append(objimpresion.AlinIzq(.Item("nec_lit_dia").ToString(), 15))


                    LPresupuesto = .Item("lit_presupuesto") + LPresupuesto
                    LReal = .Item("lit_real") + LReal
                    NCL = .Item("nec_lit_dia") + NCL

                End With
            Next


            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("PRESUPUESTO LITROS : " + (FormatNumber(LPresupuesto.ToString, 2)).ToString)
            rsb.AppendLine("LITROS REALES : " + FormatNumber(LReal.ToString, 2))
            If (LPresupuesto > 0) Then
                rsb.AppendLine("AVANCES : " + (objUtil.isDecimal((LReal / LPresupuesto) * 100)).ToString + "%")
            Else
                rsb.AppendLine("AVANCES : " + ("00" + "%".ToString()))
            End If

            rsb.AppendLine("DIFERENCIA : " + (FormatNumber((LPresupuesto.ToString - objUtil.isDecimal(LReal).ToString), 2).ToString))
            rsb.AppendLine("NECESIDAD : " + (FormatNumber(NCL.ToString, 2).ToString))
            rsb.AppendLine("---------------------------------------------------------")


            rsb.AppendLine()
            rsb.AppendLine("----------- F I N  D E  L A  I M P R E S I O N ----------")
            rsb.AppendLine()
            rsb.AppendLine()

            Dim arreglo As String()
            arreglo = rsb.ToString.Split("|")
            If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
                Return True
            Else
                MsgBox("La impresora no esta disponible.")
                Return False
            End If



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function


    Public Function resumenPresupuestoDiaV(ByVal diad As String) As Boolean
        Dim rsb As New StringBuilder
        Dim tipoDocumento As String = ""
        Dim cliente As String = ""
        Dim objRuta As New RutaBL
        Dim dtCarga, dtRuta As New DataTable
        Dim dvCarga As New DataView
        Dim strFiltro As String = ""

        Dim LPresupuesto As Decimal
        Dim LReal As Decimal
        Dim NCL As Decimal
        Try
            LPresupuesto = 0
            LReal = 0
            NCL = 0
            dtCarga = objRuta.obtenerResumenPresupuestoD(diad)
            dtRuta = objRuta.obtenerRutaActiva()
            dvCarga = dtCarga.DefaultView
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("---------- PRESUPUESTO DEL DIA VALORIZADO  --------------")


            If dtCarga.Rows.Count <= 0 Then
                MsgBox("No existen documentos para imprimir.")
                Return True
            End If
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine()
            rsb.AppendLine("Empresa     : " + Trim(dtRuta.Rows(0).Item("nomEmpresa").ToString()))
            rsb.AppendLine("Fecha Emi.  : " + Date.Now.ToString + "  Usuario  : " + id_glo_usuario.ToString)
            rsb.AppendLine("Ruta        : " + dtRuta.Rows(0).Item("codRuta").ToString())
            rsb.AppendLine("Dia Visita  : " + diad)

            For i As Integer = 0 To dtCarga.Rows.Count - 1
                With dtCarga.Rows(i)


                    rsb.AppendLine()
                    If (i = 0) Then
                        rsb.AppendLine("---------------------------------------------------------")
                        rsb.AppendLine("Marca           Presupuesto     Lit_Real       NEC_Lit ")
                        rsb.AppendLine("---------------------------------------------------------|")
                    End If

                    rsb.Append(objimpresion.AlinIzq(Trim(.Item("marca").ToString()), 15) & "|")
                    rsb.Append(objimpresion.AlinIzq(FormatCurrency(.Item("mt_presupuesto").ToString(), 2), 15))
                    rsb.Append(objimpresion.AlinIzq(FormatCurrency(.Item("venta_real").ToString(), 2), 15))
                    rsb.Append(objimpresion.AlinIzq(FormatCurrency(.Item("nec_vta_dia").ToString(), 2), 15))


                    LPresupuesto = .Item("mt_presupuesto") + LPresupuesto
                    LReal = .Item("venta_real") + LReal
                    NCL = .Item("nec_vta_dia") + NCL

                End With
            Next


            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("PRESUPUESTO  : " + (FormatCurrency(LPresupuesto.ToString, 2)))
            rsb.AppendLine("VENTA REAL : " + (FormatCurrency(LReal.ToString, 2)))
            If (LPresupuesto > 0) Then
                rsb.AppendLine("AVANCES : " + (objUtil.isDecimal((LReal / LPresupuesto) * 100)).ToString + "%")
            Else
                rsb.AppendLine("AVANCES : " + ("00" + "%".ToString()))
            End If

            rsb.AppendLine("DIFERENCIA : " + (FormatCurrency((LPresupuesto.ToString - objUtil.isDecimal(LReal).ToString), 2).ToString))
            rsb.AppendLine("NECESIDAD : " + (FormatCurrency(NCL.ToString, 2).ToString))
            rsb.AppendLine("---------------------------------------------------------")


            rsb.AppendLine()
            rsb.AppendLine("----------- F I N  D E  L A  I M P R E S I O N ----------")
            rsb.AppendLine()
            rsb.AppendLine()

            Dim arreglo As String()
            arreglo = rsb.ToString.Split("|")
            If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
                Return True
            Else
                MsgBox("La impresora no esta disponible.")
                Return False
            End If



        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function


    


    Public Function chequesRecibidos() As Boolean
        Dim rsb As New StringBuilder
        Dim objRuta As New RutaBL
        Dim dtRuta As New DataTable
        Dim dtCarga As New DataTable
        Dim dvCarga As New DataView
        Dim cliente As String = ""
        Try

            dtCarga = objRuta.obtenerChequesRecibidos
            dtRuta = objRuta.obtenerRutaActiva()
            dvCarga = dtCarga.DefaultView
            dvCarga.RowFilter = " idRuta = " + id_glo_ruta.ToString()
            dtCarga = dvCarga.ToTable
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("------------ C H E Q U E S   R E C I B I D O S ----------")
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine()
            rsb.AppendLine("Empresa     : " + Trim(dtRuta.Rows(0).Item("nomEmpresa").ToString()))
            rsb.AppendLine("Fecha Emi.  : " + Date.Now.ToString + "  Usuario  : " + id_glo_usuario.ToString)
            rsb.AppendLine("Ruta        : " + dtRuta.Rows(0).Item("codRuta").ToString())
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("Documento   Banco                         Importe        ")
            rsb.AppendLine("---------------------------------------------------------")
            For i As Integer = 0 To dtCarga.Rows.Count - 1
                With dtCarga.Rows(i)
                    rsb.Append(objimpresion.AlinIzq(.Item("documento").ToString(), 12))
                    rsb.Append(objimpresion.AlinIzq(.Item("showvalue").ToString(), 30))
                    rsb.AppendLine(FormatCurrency(.Item("importe").ToString, 2))
                End With
            Next
            rsb.AppendLine("----------- F I N  D E  L A  I M P R E S I O N ----------")
            rsb.AppendLine()

            '--- Impresion del documento  ---'
            If Not objimpresion.ConfirmaImpresion("Cheques recibidos.") Then
                Return False
            End If

            If objimpresion.Imprime_Documento(rsb.ToString) = 0 Then
                Return True
            Else
                MsgBox("La impresora no esta disponible.")
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Public Function finDia(ByVal lstMovimientos As Windows.Forms.ListView) As Boolean
        Dim rsb As New StringBuilder
        Dim objRuta As New RutaBL
        Dim dtRuta As New DataTable
        Dim dtMovimiento As New DataTable
        Dim dtCobros As New DataTable
        Dim dtEficiencia As New DataTable
        Dim dtDiferencia As New DataTable
        Dim dtIntegracion As New DataTable
        Dim dtIntegracionv As New DataTable
        Dim dtDeposito As New DataTable
        Dim dtCupones As New DataTable
        Dim dvCarga As New DataView
        Dim cliente As String = ""
        Dim oDespacho As New DespachoDT
        Dim dvIntegracion As New DataView
        Dim idCliente As String = "idCliente"
        Dim otras_vias As Integer = 0


        'Dim sumICJ, sumIUN, sumFCJ, sumFUN, sumVCJ, sumVUN As Integer
        Try
            dtDiferencia = objRuta.consultarDiferencias()
            dtMovimiento = objRuta.obtenerMovimientoLiquidacion()
            dtCobros = objRuta.ObtenerMovCobros()
            dtEficiencia = objRuta.ObtenerEficiencia()
            dtIntegracion = objRuta.obtenerIntegracionRep()
            dtRuta = objRuta.obtenerRutaActiva()
            dtIntegracionv = objRuta.obtenerIntegracion()

            dtCupones = objRuta.obtenerCupones()
        

            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("------------        L I Q U I D A C I O N      ----------")
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("")
            rsb.AppendLine("Empresa     : " + Trim(dtRuta.Rows(0).Item("nomEmpresa").ToString()))
            rsb.AppendLine("Fecha Emi.  : " + Date.Now.ToString + "  Usuario  : " + id_glo_usuario.ToString())
            rsb.AppendLine("Ruta        : " + dtRuta.Rows(0).Item("codRuta").ToString())
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("Producto   (CJ/UN)                                       ")
            rsb.AppendLine("           Inicial     Devuelto     Venta                ")
            rsb.AppendLine("---------------------------------------------------------")
            For i As Integer = 0 To lstMovimientos.Items.Count - 1
                'Dim drow As DataRow = lstMovimientos.Items(i)
                With lstMovimientos.Items(i)
                    rsb.Append(objimpresion.AlinIzq(.SubItems("0").Text() + " ", 10))
                    rsb.AppendLine(Trim(.SubItems("1").Text))
                    rsb.Append(objimpresion.AlinDer(.SubItems("3").Text(), 15))
                    rsb.Append(objimpresion.AlinDer(.SubItems("5").Text(), 12))
                    rsb.AppendLine(objimpresion.AlinDer(.SubItems("4").Text(), 13) & "|")
                    rsb.AppendLine("---------------------------------------------------------|")
                    'sumICJ = sumICJ + objUtil.isDecimal(.SubItems("ICJ").ToString())
                    'sumIUN = sumIUN + objUtil.isDecimal(.SubItems("IUN").ToString())
                    'sumFCJ = sumFCJ + objUtil.isDecimal(.SubItems("FCJ").ToString())
                    'sumFUN = sumFUN + objUtil.isDecimal(.SubItems("FUN").ToString())
                    'sumVCJ = sumVCJ + objUtil.isDecimal(.SubItems("VCJ").ToString())
                    'sumVUN = sumVUN + objUtil.isDecimal(.SubItems("VUN").ToString())
                End With
            Next
            'rsb.AppendLine("TOTALES    " + sumICJ.ToString + "/" + sumIUN.ToString + "      " + sumFCJ.ToString + "/" + sumFUN.ToString + "          " + sumVCJ.ToString + "/" + sumVUN.ToString & "|")
            rsb.AppendLine("")
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("------------        I N T E G R A C I O N      ----------")
            rsb.AppendLine("---------------------------------------------------------")

            dvIntegracion = dtIntegracion.DefaultView
            dvIntegracion.RowFilter = " integracion = 'Integracion'"
            dtIntegracion = dvIntegracion.ToTable


            For i As Integer = 0 To dtIntegracion.Rows.Count - 1
                Dim drow As DataRow = dtIntegracion.Rows(i)
                Select Case Trim(drow("partida").ToString())
                    Case "DEVOLUCION PT"
                    Case Else
                        Dim a As Integer = Len(Trim(drow.Item("partida").ToString)) 'Ancho Variable      
                        If (Trim(drow("partida").ToString()) = "(-) OTRAS VIAS") Then
                            otras_vias = objUtil.isDecimal(drow("importe").ToString())
                        End If

                        If (Trim(drow("partida").ToString()) = "TOTAL_DEPOSITO") Then
                            rsb.Append(Trim(drow("partida").ToString()))
                            rsb.AppendLine(objimpresion.AlinDer(FormatCurrency(Math.Abs(objUtil.isDecimal(drow("importe").ToString()) + otras_vias)), mAncho - (a + 5)))
                            rsb.Append("|")
                        Else
                            rsb.Append(Trim(drow("partida").ToString()))
                            rsb.AppendLine(objimpresion.AlinDer(FormatCurrency(Math.Abs(objUtil.isDecimal(drow("importe").ToString()))), mAncho - (a + 5)))
                            rsb.Append("|")
                        End If
                End Select
            Next
            Dim dt As New DataTable
            Dim oceExport As New ceExportData
            dt = oceExport.agregarIntegraciON()
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    Dim drow As DataRow = dt.Rows(i)
                    rsb.AppendLine("|")
                    rsb.Append("NO. BOLETA ")
                    rsb.AppendLine(objimpresion.AlinDer(drow("noBoleta").ToString(), mAncho - (11 + 5)))
                    rsb.Append("DEPOSITADO")
                    rsb.AppendLine(objimpresion.AlinDer(FormatCurrency(Math.Abs(objUtil.isDecimal(drow("VALORBOLETA").ToString()))), mAncho - (10 + 5)))
                    rsb.Append("DIFERENCIA")
                    rsb.AppendLine(objimpresion.AlinDer(FormatCurrency(Math.Abs(objUtil.isDecimal(drow("importeDiferencia").ToString()))), mAncho - (10 + 5)))
                    For j As Integer = 0 To dtDiferencia.Rows.Count - 1
                        rsb.AppendLine("|")
                        rsb.Append(objimpresion.AlinIzq(dtDiferencia.Rows(j).Item("tipoMotivo").ToString(), 10))
                        rsb.Append(objimpresion.AlinIzq(dtDiferencia.Rows(j).Item("Motivo").ToString(), 25))
                        rsb.AppendLine(objimpresion.AlinDer(FormatCurrency(Math.Abs(objUtil.isDecimal(dtDiferencia.Rows(j).Item("valor").ToString()))), 12))
                    Next
                Next
            Else
            End If
            rsb.AppendLine()
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("")
            rsb.AppendLine("|")
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("------------  O T R A S    V I A S   ----------")
            rsb.AppendLine("---------------------------------------------------------")

            Try
                If dtIntegracionv.Rows.Count > 0 Then
                    For ab As Integer = 0 To dtIntegracionv.Rows.Count - 1
                        Dim drows As DataRow = dtIntegracionv.Rows(ab)
                        Dim lviv As New ListViewItem(drows("partida").ToString())
                        lviv.SubItems.Add(FormatCurrency(objUtil.isDecimal(drows("importe").ToString()), 2))
                        Select Case Trim(drows("idRubro").ToString())
                            Case "X"
                                Dim aa As Integer = Len(Trim(drows.Item("partida").ToString)) 'Ancho Variable
                                rsb.Append(Trim(drows("partida").ToString()))
                                rsb.AppendLine(objimpresion.AlinDer(FormatCurrency(Math.Abs(objUtil.isDecimal(drows("importe").ToString()))), mAncho - (aa + 5)))
                                rsb.Append("|")
                                'rsb.AppendLine((drows("partida").ToString()) + " " + FormatCurrency(drows("importe").ToString()))
                        End Select
                    Next

                    'For i As Integer = 0 To dtCupones.Rows.Count - 1
                    'Dim drow As DataRow = dtCupones.Rows(i)
                    'Select Case Trim(drow("idViaPago").ToString())
                    '    Case "W"
                    'rsb.AppendLine(" Cupones Sorpresa : " + FormatCurrency(drow("importe").ToString()))
                    '    Case "Y"
                    'rsb.AppendLine(" Cupones #2 : " + FormatCurrency(drow("importe").ToString()))
                    '    Case "Z"
                    'rsb.AppendLine(" Cupones #5 : " + FormatCurrency(drow("importe").ToString()))
                    'End Select
                    'Next
                End If
        Catch ex As Exception

            End Try

            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("")
            rsb.AppendLine("|")
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("------------  R E S U M E N    R U T A   ----------")
            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("")
            rsb.AppendLine("Empresa     : " + Trim(dtRuta.Rows(0).Item("nomEmpresa").ToString()))
            rsb.AppendLine("Fecha Emi.  : " + Date.Now.ToString + "  Usuario  : " + id_glo_usuario.ToString())
            rsb.AppendLine("Ruta        : " + dtRuta.Rows(0).Item("codRuta").ToString())
            rsb.AppendLine("---------------------------------------------------------")
            If dtMovimiento.Rows.Count > 0 Then
                For i As Integer = 0 To dtMovimiento.Rows.Count - 1
                    Dim drow As DataRow = dtMovimiento.Rows(i)
                    Dim kmr As Integer = 0
                    kmr = objUtil.isDecimal(drow("km_fin").ToString()) - objUtil.isDecimal(drow("km_inicio").ToString())
                    rsb.AppendLine("Kilometros Recorridos : " + kmr.ToString())
                    rsb.AppendLine("Salida: " + drow("hora_entrada").ToString())
                    rsb.AppendLine("Entrada: " + drow("hora_salida").ToString())
                Next
            Else

            End If
            If (dtRuta.Rows(0).Item("tipoRuta").ToString() <> "16") Then
                If dtCobros.Rows.Count > 0 Then
                    For i As Integer = 0 To dtCobros.Rows.Count - 1
                        Dim drow As DataRow = dtCobros.Rows(i)
                        rsb.AppendLine("Efectividad Cobro : " + drow("COBRADOS").ToString())
                        rsb.AppendLine("Cantidad Cobros :  " + drow("COBRADOS").ToString())
                        rsb.AppendLine("Cobros Pendientes :  " + drow("NCOBRADOS").ToString())
                    Next
                End If
            Else
                If dtCobros.Rows.Count > 0 Then
                    For i As Integer = 0 To dtCobros.Rows.Count - 1
                        Dim drow As DataRow = dtCobros.Rows(i)
                        rsb.AppendLine("Efectividad Cobro : " + drow("COBRADOS").ToString())
                        rsb.AppendLine("Cantidad Cobros :  " + drow("COBRADOS").ToString())
                        rsb.AppendLine("Cobros Pendientes :  " + drow("NCOBRADOS").ToString())
                    Next
                End If

                If dtEficiencia.Rows.Count > 0 Then
                    Dim eficiencia As Decimal
                    Dim nentregas As Integer
                    For i As Integer = 0 To dtEficiencia.Rows.Count - 1
                        Dim drow As DataRow = dtEficiencia.Rows(i)
                        eficiencia = objUtil.isDecimal(drow("despachos").ToString()) / objUtil.isDecimal(drow("total").ToString())
                        nentregas = objUtil.isDecimal(drow("total").ToString()) - objUtil.isDecimal(drow("despachos").ToString())
                        rsb.AppendLine("Efectividad Entrega : " + eficiencia.ToString() + "%")
                        rsb.AppendLine("Cantidad Entregas :  " + drow("despachos").ToString())
                        rsb.AppendLine("Cantidad Rechazos :  " + nentregas.ToString)
                    Next
                End If

            End If

            rsb.AppendLine("---------------------------------------------------------")
            rsb.AppendLine("")
            rsb.AppendLine("")
            rsb.AppendLine("")
            rsb.AppendLine(objimpresion.AlinCent("VENDEDOR: " + co_glo_vendedor, mAncho) + vbCrLf & "")
            rsb.AppendLine("")
            rsb.AppendLine("   ------------------------------------------------------")
            rsb.AppendLine(objimpresion.AlinCent("LIQUIDADOR: " + co_glo_liquidador, mAncho) + vbCrLf & "")
            rsb.AppendLine("")
            rsb.AppendLine("----------- F I N  D E  L A  I M P R E S I O N ----------")
            rsb.AppendLine("")
            Dim arreglo As String()
            arreglo = rsb.ToString.Split("|")
            If objimpresion.Imprime_Documento_grande(arreglo) = 0 Then
                Return True
            Else
                MsgBox("La impresora no esta disponible.")
                Return False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return True
    End Function
End Class